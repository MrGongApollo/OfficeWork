using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Office.Data;

namespace Web.Controllers
{
    public class BaseController : Controller
    {
        #region 操作类型枚举
        /// <summary>
        /// 操作类型枚举
        /// </summary>
        public enum OperateTypeEnum
        {
            /// <summary>
            /// 查看
            /// </summary>
            view,
            /// <summary>
            /// 登录
            /// </summary>
            login,
            /// <summary>
            /// 新增
            /// </summary>
            add,
            /// <summary>
            /// 修改
            /// </summary>
            modify,
            /// <summary>
            /// 删除
            /// </summary>
            delete
        }
        #endregion

        //protected OfficeEntities CurrentContext = new OfficeEntities();

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && CurrentContext == null)
        //    {
        //        this.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
	}
}