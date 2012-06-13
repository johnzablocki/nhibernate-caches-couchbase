using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHibernateCouchbaseCacheSample.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			ViewBag.Message = "2nd Level Cache for NHibernate using Couchbase";

			return View();
		}
		
	}
}
