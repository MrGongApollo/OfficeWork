using Office.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;

namespace Web.Controllers
{
    public class XTController : BaseController
    {
        //
        // GET: /XT/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult XT_SysLogs()
        {
            return View();
        }

        public ActionResult XT_Menus() {
            return View();
        }

        #region 获取系统日志
        [HttpGet]
        public ActionResult GetSysLogs(int offSet = 0, int pageSize = 10, string sortType = "asc", string orderBy = "CreateTime")
        {
            try
            {
                using (wxEntities context = new wxEntities())
                {
                    var list = context.T_SysLogs;
                    int cnt = list.Count();
                    string orderExpression = string.Format("{0} {1}", orderBy, sortType);
                    var _list = list.OrderBy(orderExpression).Skip(offSet).Take(pageSize).ToList();
                    //string _strsql = string.Format("SELECT TOP {0} * FROM T_YH_HiddenDanger WHERE HiddenDangerId NOT IN (SELECT TOP {1} HiddenDangerId FROM T_YH_HiddenDanger) ORDER BY {2} {3}", pageSize, offSet, orderBy, sortType);

                    //var _list = context.T_YH_HiddenDanger.SqlQuery(_strsql).AsNoTracking().ToList();


                    return Json(new
                    {
                        total = cnt,
                        rows = _list
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
	}
}