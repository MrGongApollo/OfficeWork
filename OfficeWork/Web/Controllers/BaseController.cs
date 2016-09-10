using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Office.Data;
using Web.Models;

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

        protected KeyValueModel success_r = new KeyValueModel { Key="success",Value="操作成功！"},
                error_r = new KeyValueModel { Key = "error", Value = "操作失败！" };        
	}
}