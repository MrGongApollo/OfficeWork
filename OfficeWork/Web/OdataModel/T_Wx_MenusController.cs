﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.OData;
using Office.Data;

namespace Web.OdataModel
{
    public class T_SysMenusController : ODataController
    {
        private WechatEntities db = new WechatEntities();

        public IQueryable<T_SysMenus> Get()
        {
            return db.T_SysMenus;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}