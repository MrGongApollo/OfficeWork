using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.OData;
using Office.Data.Models;

namespace Web.OdataModel
{
    public class T_Wx_MenusController : OdataBaseController
    {
        [EnableQuery]
        public IQueryable<T_Wx_Menus> Get()
        {
            return base._db.T_Wx_Menus;
        }
    }
}