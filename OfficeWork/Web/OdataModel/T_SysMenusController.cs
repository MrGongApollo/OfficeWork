using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.OData;
using Office.Data.Models;

namespace Web.OdataModel
{
    public class T_SysMenusController : OdataBaseController
    {
        [EnableQuery]
        public IQueryable<T_SysMenus> Get()
        {
            return base._db.T_SysMenus;
        }
    }
}