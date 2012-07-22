using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Mvc
{
	public static class WebFormsHelper
	{
		private class ContainerPage : Page
		{
			#region Content
			private readonly HtmlForm _form = new HtmlForm();
			private readonly HtmlHead _head = new HtmlHead();

			protected override void OnInit(EventArgs e)
			{
				base.OnInit(e);

				var html = new HtmlGenericControl("html");
				Controls.Add(html);

				_head.ID = "WebFormsHead";
				html.Controls.Add(_head);

				var body = new HtmlGenericControl("body");
				html.Controls.Add(body);

				_form.ID = "WebFormsForm";
				_form.Name = "WebFormsForm";
				_form.Enctype = "multipart/form-data";
				body.Controls.Add(_form);
			}

			public void AddHead(Control control)
			{
				_head.Controls.Add(control);
			}

			public void AddControl(Control control)
			{
				_form.Controls.Add(control);
			}
			#endregion

			#region SessionState
			private static HttpSessionState GetMockHttpSessionState()
			{
				var staticObjects = new HttpStaticObjectsCollection();
				var itemCollection = new SessionStateItemCollection();
				IHttpSessionState sessionStateContainer =
					new HttpSessionStateContainer(Guid.NewGuid().ToString("N"),
												  itemCollection,
												  staticObjects,
												  1,
												  true,
												  HttpCookieMode.UseUri,
												  SessionStateMode.InProc,
												  false);
				var state = (HttpSessionState) Activator.CreateInstance(
					typeof(HttpSessionState),
					BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.CreateInstance,
					null,
					new object[] { sessionStateContainer },
					CultureInfo.CurrentCulture);
				return state;
			}

			private HttpSessionState _session;

			public override HttpSessionState Session
			{
				get { return _session ?? (_session = GetMockHttpSessionState()); }
			}
			#endregion
		}

		private static void TransferCookies(HttpResponse source, HttpResponse target)
		{
			foreach (var cookie in source.Cookies.Cast<string>()
				.Select(key => source.Cookies[key])
				.Where(cookie => cookie != null))
			{
				target.Cookies.Add(cookie);
			}
		}

		private static string Clean(string source)
		{
			return source.Replace("&nbsp;", "&#160;");
		}

		public class ItemContent
		{
			public string ControlHtml { get; set; }
		}

		public static ItemContent RenderLegacyItem(Func<Page, Control> contentCreator)
		{
			var page = CreatePage();
			var content = contentCreator(page);
			page.AddControl(content);

			var stringWriter = new StringWriter();
			var response = ProcessRequest(page, stringWriter);

			TransferCookies(response, HttpContext.Current.Response);
			var form = GetForm(Clean(stringWriter.ToString()));

			return new ItemContent
				{
					ControlHtml = form,
				};
		}

		private static HttpResponse ProcessRequest(IHttpHandler page, TextWriter writer)
		{
			var response = new HttpResponse(writer);
			var context = new HttpContext(HttpContext.Current.Request, response);
			context.SetSessionStateBehavior(SessionStateBehavior.Required);
			page.ProcessRequest(context);
			return response;
		}

		private static ContainerPage CreatePage()
		{
			var page = new ContainerPage
				{
					RenderingCompatibility = new Version(3, 5),
					ClientIDMode = ClientIDMode.AutoID,
				};

			var scriptManager = new ScriptManager
				{
					ID = "MyScriptManager",
					EnableHistory = true,
					EnableSecureHistoryState = false
				};
			AddDefaultScripts(scriptManager);
			page.AddControl(scriptManager);

			page.AddHead(new ContentPlaceHolder {ID = "HeadContent"});
			return page;
		}

		private static string GetForm(string rendered)
		{
			const string formStartTag = "<form";
			const string formEndTag = "</form>";
			var formStart = rendered.IndexOf(formStartTag, StringComparison.Ordinal);
			var formEnd = rendered.LastIndexOf(formEndTag, StringComparison.Ordinal);
			var form = rendered.Substring(formStart, formEnd - formStart + formEndTag.Length);
			return form;
		}

		private static void AddDefaultScripts(ScriptManager scriptManager)
		{
			scriptManager.Scripts.Add(new ScriptReference {Name = "MsAjaxBundle"});
			scriptManager.Scripts.Add(new ScriptReference
				{Name = "WebForms.js", Assembly = "System.Web", Path = "~/Scripts/WebForms/WebForms.js"});
			scriptManager.Scripts.Add(new ScriptReference
				{Name = "WebUIValidation.js", Assembly = "System.Web", Path = "~/Scripts/WebForms/WebUIValidation.js"});
			scriptManager.Scripts.Add(new ScriptReference
				{Name = "MenuStandards.js", Assembly = "System.Web", Path = "~/Scripts/WebForms/MenuStandards.js"});
			scriptManager.Scripts.Add(new ScriptReference
				{Name = "GridView.js", Assembly = "System.Web", Path = "~/Scripts/WebForms/GridView.js"});
			scriptManager.Scripts.Add(new ScriptReference
				{Name = "DetailsView.js", Assembly = "System.Web", Path = "~/Scripts/WebForms/DetailsView.js"});
			scriptManager.Scripts.Add(new ScriptReference
				{Name = "TreeView.js", Assembly = "System.Web", Path = "~/Scripts/WebForms/TreeView.js"});
			scriptManager.Scripts.Add(new ScriptReference
				{Name = "WebParts.js", Assembly = "System.Web", Path = "~/Scripts/WebForms/WebParts.js"});
			scriptManager.Scripts.Add(new ScriptReference
				{Name = "Focus.js", Assembly = "System.Web", Path = "~/Scripts/WebForms/Focus.js"});
			scriptManager.Scripts.Add(new ScriptReference {Name = "WebFormsBundle"});
		}

		private static readonly Regex Form = new Regex("<form.*</form>");
	}
}