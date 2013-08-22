<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BreakingNewsBackend._Default" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
       <asp:Button ID="Button1" runat="server" Text="World News" OnClick="Button1_Click" />
       <asp:Button ID="Button2" runat="server" Text="Tech News" OnClick="Button2_Click" />
       <asp:Button ID="Button3" runat="server" Text="Sports News" OnClick="Button3_Click" />
</asp:Content>
