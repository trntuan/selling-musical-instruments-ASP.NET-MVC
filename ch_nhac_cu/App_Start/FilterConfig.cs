﻿using System.Web;
using System.Web.Mvc;

namespace ch_nhac_cu
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
