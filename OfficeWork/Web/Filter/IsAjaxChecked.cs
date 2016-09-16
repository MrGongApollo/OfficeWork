using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Web.Filter
{
    #region 登录验证
    /// <summary>
    /// 判断是否是ajax请求
    /// </summary>
    public class IsAjaxCheckedAttribute : AuthorizeAttribute
    {

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!httpContext.Request.IsAjaxRequest())
            {
                return false;
            }
            return true;
        }


    }
    #endregion
}