using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace arcadeX.Controllers
{
    public class StoreController : Controller
    {
        [HttpGet]
        public ActionResult HomePage()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Game()
        {
            return View();
        }
    }
}