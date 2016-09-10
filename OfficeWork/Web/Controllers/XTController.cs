using Office.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using Web.Models;
using wxCOM;
using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace Web.Controllers
{
    public class XTController : BaseController
    {
        #region 视图
        public ActionResult XT_SysLogs()
        {
            return View();
        }

        public ActionResult XT_Menus()
        {
            return View();
        }

        public ActionResult XT_UserManage()
        {
            return View();
        }

        public ActionResult XT_FontIcons()
        {
            return View();
        }

        public ActionResult XT_SysMenus()
        {
            return View();
        }
        
        #endregion

        #region 获取系统日志
        [HttpGet]
        public ActionResult GetSysLogs(int offSet = 0, int pageSize = 10, string sortType = "asc", string orderBy = "CreateTime")
        {
            try
            {
                using (WechatEntities context = new WechatEntities())
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

        #region 菜单实体
        private class WxMenu
        {
            public List<WxButtonMenu> button { get; set; }
        }

        private class WxBaseMenu
        {
            public string name { get; set; }
            public string url { get; set; }
            public string type { get; set; }
            public string key { get; set; }
        }

        private sealed class WxButtonMenu : WxBaseMenu
        {
            public List<WxBaseMenu> sub_button { get; set; }
        }
        #endregion

        #region 发布菜单
        [HttpGet]
        public ActionResult PublishWXMenus()
        {
            KeyValueModel ret = new KeyValueModel { Key = "error", Value = "操作失败！" },
              successret = new KeyValueModel { Key = "success", Value = "操作成功！" };
            try
            {
                using (WechatEntities context = new WechatEntities())
                {
                    var menus = context.T_Wx_Menus.Where(m => m.IsDeleted == false);
                    #region 一级菜单
                    var menubuttons = menus.Where(k => k.MenuLevel == "button").OrderBy(g => g.SortNum)
                        .Select(p => new WxButtonMenu
                        {
                           key= p.MenuId,
                           name = p.MenuName,
                           type = p.MenuType,
                           url = p.MenuUrl
                        })
                        .ToList();
                    #endregion

                    #region 二级菜单
                    foreach (var mn in menubuttons)
                    {
                        mn.sub_button = menus.Where(k => k.ParentMenu == mn.key).OrderBy(o=>o.SortNum).Select(p => new WxBaseMenu
                        {
                            key = p.MenuId,
                            name = p.MenuName,
                            type = p.MenuType,
                            url = p.MenuUrl,
                        })
                        .ToList();
                    }
                    #endregion

                    WxMenu btn=new WxMenu{
                        button=menubuttons
                    };

                   WXApiUrl WXApiUrl= new WXApiUrl();
                   string CreateWxMenuUrl = string.Format(WXApiUrl.Dic_WXUrls[WXApiUrl.Enum_WXUrls.CreateWxMenu], new Utils().GetAccessToken());
                   string backmsg = wxCOM.Utils.SendPostHttpRequest(CreateWxMenuUrl, JsonConvert.SerializeObject(btn));
                   RetMsg retmsg = JsonConvert.DeserializeObject<RetMsg>(backmsg);
                   if (retmsg.errcode != 0)
                   {
                       ret.Value = RetMsg.DicWxRetMsg[retmsg.errcode];
                   }
                   else
                   {
                       ret = successret;
                   }
                }
            }
            catch (Exception ex)
            {
                ret.Value = ex.Message;
            }
            return Json(ret, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 获取一级菜单
        [HttpGet]
        public JsonResult GetWxbuttons()
        {
            try
            {
                using (WechatEntities context = new WechatEntities())
                {
                    var list = context.T_Wx_Menus.Where(m => m.MenuLevel == "button" && m.IsDeleted == false).OrderBy(s => s.SortNum)
                          .Select(p => new
                          {
                              name = p.MenuName,
                              key = p.MenuId
                          }).ToList();
                    return Json(list, JsonRequestBehavior.AllowGet);
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
                using (WechatEntities context = new WechatEntities())
                {
                    var list = context.T_Wx_Menus.Where(m => m.IsDeleted == false);
                    //if (!string.IsNullOrEmpty(filter.MenuName))
                    //{
                    //    list = list.Where(m => m.MenuName.Contains(filter.MenuName));
                    //}
                    //if (filter.MenuLevel != "all")
                    //{
                    //    list = list.Where(m => m.MenuLevel == filter.MenuLevel);
                    //}
                    //if (filter.MenuType != "all")
                    //{
                    //    list = list.Where(m => m.MenuType == filter.MenuType);
                    //}
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
                using (WechatEntities context = new WechatEntities())
                {
                    switch (operatype)
                    {
                        #region 新增
                        case "add":
                            if (context.T_Wx_Menus.Any(m => m.MenuId == menu.MenuId))
                            {
                                ret.Value = "已经存在菜单代码为" + menu.MenuId + "的菜单";
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
                        #endregion
                        #region 修改
                        case "modify":
                            T_Wx_Menus _menu = context.T_Wx_Menus.Where(m => m.MenuId == menu.MenuId && m.IsDeleted == false).FirstOrDefault();
                            if (_menu != null)
                            {
                                _menu.ModifyTime = DateTime.Now;
                                _menu.SortNum = menu.SortNum;
                                _menu.MenuName = menu.MenuName;
                                _menu.MenuUrl = menu.MenuUrl;
                                _menu.MenuType = menu.MenuType;
                                context.SaveChanges();
                                ret = successret;
                            }
                            else
                            {
                                ret.Value = "未能找到此菜单，可能已被删除！";
                            }

                            break;
                        #endregion
                        #region 删除
                        case "delete":
                            T_Wx_Menus MN = context.T_Wx_Menus.Where(m => m.MenuId == menu.MenuId).FirstOrDefault();
                            if (MN != null)
                            {
                                MN.IsDeleted = true;
                                MN.ModifyTime = DateTime.Now;
                                context.SaveChanges();
                                ret = successret;
                            }
                            break;
                        #endregion

                    }
                }
            }
            catch (Exception ex)
            {
                ret.Value = ex.Message;
            }

            return Json(ret, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #endregion

        #region 系统菜单
        #region 获取一级菜单
        [HttpGet]
        public JsonResult GetSysTopMenus()
        {
            try
            {
                using (WechatEntities context = new WechatEntities())
                {
                    var list = context.T_SysMenus.Where(m => m.MenuLevel == 1 && m.IsDeleted == false).OrderBy(s=>s.SortNum)
                          .Select(p => new
                          {
                              name = p.MenuName,
                              key = p.MenuId
                          }).ToList();
                    return Json(list, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 保存菜单
        /// <summary>
        /// 保存菜单
        /// </summary>
        /// <param name="menu">菜单数据</param>
        /// <param name="operatype">操作类型</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SysMenusSave(T_SysMenus menu, string operatype)
        {
            KeyValueModel ret = base.error_r;
            try
            {
                using (WechatEntities context = new WechatEntities())
                {
                    switch (operatype)
                    {
                        #region 新增
                        case "add":
                            if (context.T_SysMenus.Any(m => m.MenuId == menu.MenuId))
                            {
                                ret.Value = "已经存在菜单代码为" + menu.MenuId + "的菜单";
                            }
                            else
                            {
                                switch (menu.MenuLevel)
                                {
                                    case 1:
                                    case 2:
                                        var cnt = context.T_SysMenus.Where(m => m.MenuLevel == menu.MenuLevel && m.IsDeleted == false);
                                         menu.CreateTime = DateTime.Now;
                                         context.T_SysMenus.Add(menu);
                                         context.SaveChanges();
                                         ret = base.success_r;
                                        break;
                                }

                            }
                            break;
                        #endregion
                        #region 修改
                        case "modify":
                            T_SysMenus _menu = context.T_SysMenus.Where(m => m.MenuId == menu.MenuId && m.IsDeleted == false).FirstOrDefault();
                            if (_menu != null)
                            {
                                _menu.ModifyTime = DateTime.Now;
                                _menu.SortNum = menu.SortNum;
                                _menu.MenuName = menu.MenuName;
                                _menu.ParentId = menu.ParentId;
                                _menu.ParentMenuName = menu.ParentMenuName;
                                _menu.Islink = menu.Islink;
                                _menu.MenuIcon = menu.MenuIcon;
                                _menu.MenuUrl = menu.MenuUrl;
                                context.SaveChanges();
                                ret = base.success_r;
                            }
                            else
                            {
                                ret.Value = "未能找到此菜单，可能已被删除！";
                            }

                            break;
                        #endregion
                        #region 删除
                        case "delete":
                            T_SysMenus MN = context.T_SysMenus.Where(m => m.MenuId == menu.MenuId&&m.IsDeleted==false).FirstOrDefault();
                            if (MN != null)
                            {
                                MN.IsDeleted = true;
                                MN.ModifyTime = DateTime.Now;
                                context.SaveChanges();
                                ret = base.success_r;
                            }
                            break;
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                ret.Value = ex.Message;
            }

            return Json(ret, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 获取菜单信息
        [HttpGet]
        public ActionResult GetSysMenus(int offSet = 0, int pageSize = 10, string sortType = "asc", string orderBy = "CreateTime")
        {
            try
            {
                using (WechatEntities context = new WechatEntities())
                {
                    var list = context.T_SysMenus.Where(m => m.IsDeleted == false);
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
        #endregion

        #region 字体图标
        [HttpGet]
        public ActionResult GetIcons()
        {
            KeyValueModel ret = base.error_r;
            try
            {
                using (WechatEntities et=new WechatEntities())
                {
                   var Icons=et.T_FontIcons.Where(i => i.IsDeleted == false).OrderBy(s => s.SortNum).Select(p => new
                    {
                        FontClass=p.FontClass
                    }).ToList();
                   ret = base.success_r;
                   ret.Value = JsonConvert.SerializeObject(Icons);
                }
                
            }
            catch (Exception ex)
            {
                ret.Value = ex.Message;
            }
            return Json(ret, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}