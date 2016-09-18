﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.OData;
using Office.Data.Models;

namespace Web.OdataModel
{
    public class T_TextMessageController : OdataBaseController
    {
        [EnableQuery]
        public IQueryable<T_TextMessage> Get()
        {
            return base._db.T_TextMessage;
        }
    }
}