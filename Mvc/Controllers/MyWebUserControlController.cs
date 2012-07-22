using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Mvc.Controllers
{
    public class MyWebUserControlController : LegacyItemController
    {
	    protected override Control CreateItem(Page page)
	    {
			// http://www.cmswire.com/cms/tips-tricks/aspnet-reusing-web-user-controls-and-forms-000915.php
			// http://support.microsoft.com/kb/910441/en-us?spid=8940&sid=global
			return page.LoadControl("~/bin/WebForms.dll/WebForms.MyWebUserControl.ascx");
	    }

	    protected virtual string PageUrl
	    {
			get { return "/MyWebUserControl"; }
	    }
    }
}
