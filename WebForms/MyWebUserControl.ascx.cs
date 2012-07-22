using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace WebForms
{
	public partial class MyWebUserControl : System.Web.UI.UserControl
	{
		protected override void OnInit(EventArgs e)
		{
			var cookie = Request.Cookies["Expression"];
			if(cookie != null)
				Expression.Text = cookie.Value;
			base.OnInit(e);
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
		}

		protected override void CreateChildControls()
		{
			base.CreateChildControls();
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			Expression.Focus();
			Response.AppendCookie(new HttpCookie("Expression", Expression.Text));
		}

		protected override void Render(HtmlTextWriter writer)
		{
			base.Render(writer);
		}

		protected void OnCompute(object sender, EventArgs e)
		{
			string expression;
			try
			{
				expression = new Calculator().Compute(Expression.Text);
			}
			catch(Exception exception)
			{
				expression = exception.Message;
			}
			Expression.Text = expression;
		}
	}
}