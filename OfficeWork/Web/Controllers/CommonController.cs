﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Filter;

namespace Web.Controllers
{
    [LoginChecked]
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

        public ActionResult User_Setting()
        {
            return View();
        }

        public ActionResult User_Info()
        {
            return View();
        }
	}
}