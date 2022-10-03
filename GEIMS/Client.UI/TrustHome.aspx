<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrustHome.aspx.cs" MasterPageFile="~/Master/TrustMain.Master" Inherits="GEIMS.Client.UI.TrustHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        img.watermark
        {
            filter: alpha(opacity=25);
            opacity: 0.08;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
    <div id="divMain" style="width: 100%; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
            <%--Trust Home--%>
            <asp:Label runat="server" ID="lblTrustName"></asp:Label>
        </div>
        <div runat="server" id="CollegeDashboard" class="water-img" width="100%" style="text-align:center;">
           <%-- <asp:Image class="watermark" ID="logowaterimg" runat="server" Width="500px" Height="400px"
                ImageUrl="../Images/SKMLogo.jpg" />--%>
        
            <asp:Image ImageUrl="~/Images/FERTILIZER NAGAR SCHOOL LOGO COLOUR copy.jpg" CssClass="watermark" runat="server" ID="imgphoto"
                                  Width="435px" Height="400px"/>
        </div>
    </div>
</asp:Content>