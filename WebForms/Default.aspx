<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebForms._Default" %>
<%@ Register Assembly="WebForms" Namespace="WebForms" TagPrefix="wf" %>
<%@ Register TagPrefix="wf" TagName="MyWebUserControl" Src="~\MyWebUserControl.ascx" %>
<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1>
                    <%: Page.Title %>.</h1>
                <h2>Modify this template to jump-start your ASP.NET application.</h2>
            </hgroup>
            <p>
                To learn more about ASP.NET, visit <a href="http://asp.net" title="ASP.NET Website">http://asp.net</a>. The page features <mark>videos, tutorials, and samples</mark>to help you get the most from ASP.NET. If you have any questions about ASP.NET visit <a href="http://forums.asp.net/18.aspx" title="ASP.NET Forum">our forums</a>.
            </p>
        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>Control Types</h3>
    <ol class="round">
        <li class="one">
            <h5>class derived from System.Web.UI.Control</h5>
            <wf:MyControl ID="MyControl" runat="server"></wf:MyControl>
        </li>   
        <li class="two">
            <h5>ascx file</h5>
            <wf:MyWebUserControl ID="MyWebUserControl" runat="server"></wf:MyWebUserControl>
        </li>           
    </ol>
</asp:Content>
