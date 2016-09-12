using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Office.Web.Models;
using Office.Data;
using System.Security.Cryptography;
using System.Text;
using Office.Web.Help;

namespace Office.Web.Controllers
{
    public class LoginController : BaseController
    {
        //
        // GET: /Login/
        public ActionResult Index()
        {
            return View();
        }

        #region 用户登录
        [HttpPost]
        public ActionResult LoginChecked(string username, string password)
        {
            base.fin_r = base.error_r;
            try
            {
                using (WechatEntities db = new WechatEntities())
                {
                    T_SysUser user = db.T_SysUser.Where(u => u.UserId == username&&u.IsDeleted==false).FirstOrDefault();
                    #region 判断用户是否存在
                    if (user != null)
                    {
                        #region 密码正确
                        if (user.LoginPsw ==new CommonHelper().MD5(password))
                        {
                            Session["User"] = user;
                            base.fin_r = new KeyValueModel
                            {
                                Key = "success",
                                Value = "1秒钟后自动跳转"
                            };
                        }
                        #endregion
                        #region 不正确
                        else
                        {
                            base.fin_r.Value = "用户名或者密码不正确";
                        }
                        #endregion
                    }
                    #endregion
                    #region 用户不存在
                    else
                    {
                        base.fin_r.Value = "不存在该用户";
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                base.fin_r.Value = ex.Message;
            }
            return JsonR();
        }
        #endregion

        #region 用户注销
        [HttpGet]
        public ActionResult SignOut()
        {
            
            Session["User"] = null;
            base.fin_r = new KeyValueModel {
            Key="success",
            Value="注销成功"
            };
            return JsonR(JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}