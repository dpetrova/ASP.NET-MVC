﻿using System.Web;
using System.Web.Mvc;

namespace ASP_NET_MVC_Caching_Homework
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
