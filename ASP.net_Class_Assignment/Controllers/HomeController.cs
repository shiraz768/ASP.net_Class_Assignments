using Microsoft.AspNetCore.Mvc;
using Shiraz.models;
using Shiraz.Models;
using System.Diagnostics;

namespace Shiraz.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ClientContext clientContext;

        public HomeController(ILogger<HomeController> logger, ClientContext 
            clientContext)
        {
            _logger = logger;
            this.clientContext = clientContext;
        }

        public IActionResult Index()
        {
            var data = clientContext.PermanentCustomers.ToList();
            return View(data);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PermanentCustomer permanentCustomers)
        {
            if (ModelState.IsValid)
            {
                clientContext.Add(permanentCustomers);
                clientContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int id)
        {
            if (id == null || clientContext.PermanentCustomers == null)
            {
                return NotFound();
            }
            var data = clientContext.PermanentCustomers.Find(id);
            if (data == null)
            {
                return NotFound();
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, PermanentCustomer PC)
        {
            if (id != PC.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                clientContext.Update(PC);
                clientContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Details(int id)
        {
            if (id == null || clientContext.PermanentCustomers == null)
            {
                return NotFound();
            }
            var data = clientContext.PermanentCustomers.Find(id);
            if (data == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                return View(data);
            }
            return View();
        }
        public IActionResult Delete(int id, PermanentCustomer PC)
        {
            if (id == null || clientContext.PermanentCustomers == null)
            {
                return NotFound();
            }
            var data = clientContext.PermanentCustomers.Find(id);
            if (data == null)
            {
                return NotFound();
            }
            if (data != null)
            {
                string script = $@"
 
          <script>
         alert('Deleted Successfully');
         window.location.href = '/Home/Index';
         </script>";
                clientContext.Remove(data);
                clientContext.SaveChanges();

                // Display a confirmation message using JavaScript
                return Content(script, "text/html");
            }

            return View(data);
            
        }
     
     

        public IActionResult Privacy()
        {
            return View();
        }

      

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
