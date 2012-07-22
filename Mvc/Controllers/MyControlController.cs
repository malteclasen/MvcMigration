using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Mvc.Controllers
{
    public class MyControlController : LegacyItemController
    {
	    protected override Control CreateItem(Page page)
	    {
		    return new WebForms.MyControl();
	    }

	    protected virtual string PageUrl
	    {
			get { return "/MyControl"; }
	    }
    }
}
