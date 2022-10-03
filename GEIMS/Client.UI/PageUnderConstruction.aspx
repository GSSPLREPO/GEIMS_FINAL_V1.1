<%@ Page Title="" Language="C#" MasterPageFile="~/Master/TrustMain.Master" AutoEventWireup="true" CodeBehind="PageUnderConstruction.aspx.cs" Inherits="GEIMS.Client.UI.PageUnderConstruction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        img.watermark
        {
            filter: alpha(opacity=35);
            opacity: .35;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
    <div id="divMain" style="width: 100%; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
        </div>
        <div runat="server" id="CollegeDashboard" class="water-img" width="100%" style="text-align:center;">
            <asp:Image class="watermark" ID="logowaterimg" runat="server" Width="600px" Height="400px"
                ImageUrl="../Images/pageunderconstruction.jpg" />
        </div>
    </div>
</asp:Content>
