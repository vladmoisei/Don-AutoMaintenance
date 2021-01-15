using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWithBlazor.Controllers
{
    public class ChecksController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
