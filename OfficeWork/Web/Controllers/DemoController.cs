using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class DemoController : Controller
    {
        //
        // GET: /Demo/
        public ActionResult CheckBox()
        {
            return View();
        }

        public ActionResult Bootstrapvalidator()
        {
            return View();
        }
        public ActionResult Bootstrapcolorpicker()
        {
            return View();
        }
        
	}
}