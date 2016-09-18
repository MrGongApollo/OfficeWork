using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.OData;
using Office.Data.Models;

namespace Web.OdataModel
{
    public class T_SysLogsController : OdataBaseController
    {
        [EnableQuery]
        public IQueryable<T_SysLogs> Get()
        {
            return base._db.T_SysLogs;
        }
    }
}