<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NotApproved.aspx.cs" Inherits="teest.NotApproved" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">

    <section style="height: 80vh; margin-top: 1vh;">
    <p>
     <center>
        <asp:TextBox ID="TextBox1" runat="server" Text="Siden er ikke godkendt" font-size= "32px" Height="49px" Width="339px"></asp:TextBox>
     </center>
     <br/>
        <center>
        <img alt="" src="Images/PError.jpg" style="width: 507px; height: 585px" /><br />
        </center>
    </p>
         <center>
          <asp:Button Class="button" ID="Button1" runat="server" Text="Tilbage Til Forsiden" Height="39px" OnClick="Button1_Click" Width="172px" />
         </center>
    <br/>
    <section>

</asp:Content>
