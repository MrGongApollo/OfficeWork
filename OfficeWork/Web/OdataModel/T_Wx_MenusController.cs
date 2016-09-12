using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.OData;
using Office.Data;
using Office.Data.Models;

namespace Web.OdataModel
{
    public class T_Wx_MenusController : ODataController
    {
        private WechatEntities db = new WechatEntities();

        [EnableQuery]
        public IQueryable<T_Wx_Menus> Get()
        {
            return db.T_Wx_Menus;
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