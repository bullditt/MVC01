using MVC01.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC01.App_Start
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			//filters.Add(new HandleErrorAttribute());//ExceptionFilter
			filters.Add(new EmployeeExceptionFilter());
			filters.Add(new AuthorizeAttribute());
		}
	}
}