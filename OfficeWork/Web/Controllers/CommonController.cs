using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Office.Web.Controllers
{
    public class CommonController : Controller
    {
        //
        // GET: /Common/
        public ActionResult Share_Fonts()
        {
            return View();
        }

        public ActionResult Share_Search()
        {
            return View();
        }

        public ActionResult Share_MsgCenter()
        {
            return View();
        }
	}
}