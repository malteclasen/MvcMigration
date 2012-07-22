<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyWebUserControl.ascx.cs" Inherits="WebForms.MyWebUserControl" %>
<div>
    MyWebUserControl
</div>
<div>
    <asp:TextBox runat="server" ID="Expression"></asp:TextBox>
    <asp:Button runat="server" ID="Compute" OnClick="OnCompute" Text="="/>
</div>