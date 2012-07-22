using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Mvc.Controllers
{
	public abstract partial class LegacyItemController : Controller
	{
		protected abstract Control CreateItem(Page page);

		public virtual ActionResult Index()
		{
			var item = WebFormsHelper.RenderLegacyItem(CreateItem);
			ViewBag.ControlHtml = item.ControlHtml;
			return View("LegacyItem");
		}
	}
}