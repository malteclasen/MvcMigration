using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace WebForms
{
	public class MyControl : Control
	{
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
		}

		protected override void CreateChildControls()
		{
			Controls.Add(new HtmlGenericControl("div"){InnerText="MyControl"});
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
		}

		protected override void Render(HtmlTextWriter writer)
		{
			base.Render(writer);
		}
	}
}