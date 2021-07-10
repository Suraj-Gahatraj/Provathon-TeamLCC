using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProctoringWebApp.Controllers
{
    public class SessionController : Controller
    {
        public IActionResult Index(int id)
        {
            return View();
        }

        
    }
}
