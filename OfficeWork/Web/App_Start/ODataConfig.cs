using Microsoft.OData.Edm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.OData.Builder;
using Office.Data;
using Office.Data.Models;

namespace Web
{
    public class ODataConfig
    {
        public static IEdmModel GetEdmModel()
        {

            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.Namespace = "Odata";
            builder.ContainerName = "DefaultContainer";

            builder.EntitySet<T_SysMenus>("T_SysMenus");
            builder.EntitySet<T_Wx_Menus>("T_Wx_Menus");
            builder.EntitySet<T_FontIcons>("T_FontIcons");
            builder.EntitySet<T_SysLogs>("T_SysLogs");

            return builder.GetEdmModel();

        }
    }
}