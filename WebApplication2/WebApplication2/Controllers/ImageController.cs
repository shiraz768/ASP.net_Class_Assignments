using WebApplication2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApplication2.Data;

namespace Lab204.Controllers
{
    public class ImageController : Controller
    {

        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment enviroment;

        public ImageController(ApplicationDbContext context, IWebHostEnvironment enviroment)

        {

            this.context = context;
            this.enviroment = enviroment;
        }
        public IActionResult Index()
        {
            var data = context.items.ToList();
            return View(data);
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(Item model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string uniquefilename = UploadImage(model);
                    var data = new Item()
                    {
                        Name = model.Name,
                        Category = model.Category,
                        Description = model.Description,
                        Path = uniquefilename
                    };
                    TempData["success"] = "The Shopping center added successfully!";
                    context.items.Add(data);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["error"] = "The Shoppinge center is not created please check!";
                }
                ModelState.AddModelError("", "Model property is not valid please check");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex.Message);
            }

            return View(model);
        }

        private string UploadImage(Item model)
        {
            string uniquefilename = "";
            if (model.image != null)
            {
                string UploadFolder = Path.Combine(enviroment.WebRootPath, "images");
                uniquefilename = Guid.NewGuid().ToString() + "_" + model.image.FileName;
                string FilePath = Path.Combine(UploadFolder, uniquefilename);
                using (var fileStream = new FileStream(FilePath, FileMode.Create))
                {
                    model.image.CopyTo(fileStream);
                }
            }
            return uniquefilename;
        }
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            else
            {
                var data = context.items.Where(e => e.Id == id).SingleOrDefault();
                if (data != null)
                {
                    string deleteFromFolder = Path.Combine(enviroment.WebRootPath, "images");
                    string currentImage = Path.Combine(Directory.GetCurrentDirectory(), deleteFromFolder, data.Path);
                    if (currentImage != null)
                    {
                        if (System.IO.File.Exists(currentImage))
                        {
                            System.IO.File.Delete(currentImage);
                        }
                    }
                    TempData["success"] = "The shopping center is deleted successfully!";
                    context.items.Remove(data);
                    context.SaveChanges();
                    //TempData["Success"] = "Record Deleted!";
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var data = context.items.Where(e => e.Id == id).SingleOrDefault();
            return View(data);

        }
        public IActionResult Edit(int id)
        {
            var data = context.items.Where(e => e.Id == id).SingleOrDefault();
            if (data != null)
            {
                return View(data);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult Edit(Item model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = context.items.Where(e => e.Id == model.Id).SingleOrDefault();
                    string uniqueFileName = string.Empty;
                    if (model.image != null)
                    {
                        if (data.Path != null)
                        {
                            string filepath = Path.Combine(enviroment.WebRootPath, "images", data.Path);
                            if (System.IO.File.Exists(filepath))
                            {
                                System.IO.File.Delete(filepath);
                            }
                        }
                        uniqueFileName = UploadImage(model);
                    }
                    data.Name = model.Name;
                    data.Category = model.Category;
                    data.Description = model.Description;

                    if (model.image != null)
                    {
                        data.Path = uniqueFileName;
                    }
                    TempData["success"] = "The Shopping center is updated successfully!";
                    context.items.Update(data);
                    context.SaveChanges();

                }
                else
                {
                    TempData["error"] = "The Shoppinge center is not created please check!";
                    return View(model);
                }


            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return RedirectToAction("index");
        }
    }
}


