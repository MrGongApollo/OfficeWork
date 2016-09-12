using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using Office.Data;
using System.Text;
using Web.Filter;
using Office.Data.Models;

namespace Web.Controllers
{
    [LoginChecked]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Home()
        {
            return View();
        }

        #region 获取当前用户
        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetCurentUser()
        {
            base.fin_r = base.error_r;
            T_SysUser user = Session["User"] as T_SysUser;
            user.LoginPsw = "********";
            base.fin_r = base.success_r;
            return JsonR(user, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}