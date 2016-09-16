using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.OData;
using Office.Data;
using Office.Data.Models;

namespace Web.OdataModel
{
    public class T_ImgMessageController : ODataController
    {
        private WechatEntities db = new WechatEntities();

        [EnableQuery]
        public IQueryable<T_ImgMessage> Get()
        {
            return db.T_ImgMessage;
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