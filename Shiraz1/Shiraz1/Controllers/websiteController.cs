using Microsoft.AspNetCore.Mvc;

namespace Shiraz1.Controllers
{
    public class websiteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
      
    }
}
