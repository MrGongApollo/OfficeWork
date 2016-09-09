using Office.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using Web.Models;

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

        public ActionResult XT_Menus()
        {
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

        #region 微信自定义菜单
        #region 发布菜单
        
        #endregion

        #region 获取一级菜单
        [HttpGet]
        public JsonResult GetWxbuttons()
        {
            try
            {
                using (wxEntities context=new wxEntities())
                {
                  var list= context.T_Wx_Menus.Where(m => m.MenuLevel == "button" && m.IsDeleted == false)
                        .Select(p=>new
                        {
                           name=p.MenuName,
                           key=p.MenuId
                        }).ToList();
                  return Json(list,JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 获取菜单信息
        [HttpGet]
        public ActionResult GetMenus(int offSet = 0, int pageSize = 10, string sortType = "asc", string orderBy = "CreateTime")
        {
            try
            {
                using (wxEntities context = new wxEntities())
                {
                    var list = context.T_Wx_Menus;
                    int cnt = list.Count();
                    string orderExpression = string.Format("{0} {1}", orderBy, sortType);
                    var _list = list.OrderBy(orderExpression).Skip(offSet).Take(pageSize).ToList();

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

        #region 保存菜单
        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="menu">菜单数据</param>
        /// <param name="operatype">操作类型</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult WxMenusSave(T_Wx_Menus menu, string operatype)
        {
            KeyValueModel ret = new KeyValueModel { Key = "error", Value = "操作失败！" },
                          successret = new KeyValueModel { Key = "success", Value = "操作成功！" };
            try
            {
                using (wxEntities context = new wxEntities())
                {
                    switch (operatype)
                    {
                        case "add":
                            if (context.T_Wx_Menus.Any(m => m.MenuId == menu.MenuId))
                            {
                                ret.Value = "已经存在菜单代码为" + menu.MenuId+"的菜单";
                            }
                            else
                            {
                                switch (menu.MenuLevel)
                                {
                                    case "button":
                                    case "sub_button":
                                        var cnt = context.T_Wx_Menus.Where(m => m.MenuLevel == menu.MenuLevel && m.IsDeleted == false);
                                        #region 一级菜单
                                        if (menu.MenuLevel == "button")
                                        {
                                            if (cnt.Count() >= 3)
                                            {
                                                ret.Value = "已经存在3个1级菜单";
                                            }
                                            else
                                            {
                                                menu.CreateTime = DateTime.Now;
                                                context.T_Wx_Menus.Add(menu);
                                                context.SaveChanges();
                                                ret = successret;
                                            }

                                        }
                                        #endregion
                                        #region 二级菜单
                                        else
                                        {
                                            if (cnt.Where(k => k.ParentMenu == menu.ParentMenu).Count() >= 5)
                                            {
                                                ret.Value = "已经存在5个二级菜单";
                                            }
                                            else
                                            {
                                                menu.CreateTime = DateTime.Now;
                                                context.T_Wx_Menus.Add(menu);
                                                context.SaveChanges();
                                                ret = successret;
                                            }
                                        }
                                        #endregion
                                        break;
                                }

                            }
                            break;
                        case "modify":

                            break;
                        case "delete":
                            T_Wx_Menus _menu = context.T_Wx_Menus.Where(m => m.MenuId == menu.MenuId).FirstOrDefault();
                            if (_menu != null) {
                                _menu.IsDeleted = false;
                                _menu.ModifyTime = DateTime.Now;
                                context.SaveChanges();
                                ret = successret;
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return Json(ret, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #endregion
    }
}