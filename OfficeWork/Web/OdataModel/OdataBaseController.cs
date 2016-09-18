using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.OData;
using Web.Filter;
using Office.Data;
using Office.Data.Models;

namespace Web.OdataModel
{
    public class OdataBaseController : ODataController
    {
        protected WechatEntities _db = new WechatEntities();
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
	}
}