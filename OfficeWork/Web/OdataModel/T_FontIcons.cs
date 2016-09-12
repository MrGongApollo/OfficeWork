using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.OData;
using Office.Data;
using Office.Data.Models;

namespace Web.OdataModel
{
    public class T_FontIconsController : ODataController
    {
        private WechatEntities db = new WechatEntities();

        [EnableQuery]
        public IQueryable<T_FontIcons> Get()
        {
            return db.T_FontIcons;
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