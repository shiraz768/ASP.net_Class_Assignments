using Microsoft.AspNetCore.Mvc;
using Shiraz1.Data;
using Shiraz1.Models;
using System.Diagnostics;

namespace Shiraz1.Controllers
{
    public class websiteController : Controller
    {
        //private readonly ILogger<websiteController> _logger;
        //private readonly IConfiguration _configuration;

        //public websiteController(ILogger<websiteController> logger)
        //{
        //    this._logger = logger;
        //}

        public IActionResult Index()
        {
            return View();
        }
        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}

    }
}
