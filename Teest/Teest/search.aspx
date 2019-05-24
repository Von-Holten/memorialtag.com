<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="search.aspx.cs" Inherits="Teest.search" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server"><  
  
<%-- Styling af tabellen til søgeresultater - Virker ikke når det sættes i CSS filen --%>
<style>
.tableView {
  border-collapse: collapse;
  width: 100%;
}
.tableView td, .tableView th {
  border-bottom: 1px solid #ddd;
  border-top: 1px solid #ddd;
  padding: 8px;
  vertical-align: middle;
}
.tableView tr:nth-child(even){background-color: #f2f2f2;}
.tableView tr:hover {background-color: #ddd;}
</style>

    <%-- Vores artikel der viser søgeresultaterne --%>
    <article class="minimum" style="height: auto;">
        <h1>Søgeresultater:</h1>
       
            <br />
            <%-- En dataliste der indeholder en tabel der har referencer til databasen --%>
            <asp:DataList ID="dl" runat="server" DataSourceID="SqlDataSource1" RepeatLayout="Table" style="width: 100%;">  
        <ItemTemplate>
            <table class="tableView">  
                <tr onclick="document.location='template.aspx?id=<%# Eval("GravID") %>'" style="cursor:grab">
                     <td style="border-bottom: 1px solid #ddd;">Navn:  <br /><%# Eval("Fornavn") %> <%# Eval("Efternavn") %></td> 
                     <td style="border-bottom: 1px solid #ddd;">Født:  <br /><%# Eval("Fødselsdato") %></td> 
                     <td style="border-bottom: 1px solid #ddd;">Død:  <br /><%# Eval("Dødsdato") %></td>
                     <td style="border-bottom: 1px solid #ddd;">Kirkegård:  <br /><%# Eval("KirkeNavn") %></td>
                     <td style="border-bottom: 1px solid #ddd;">Fødeby::  <br /><%# Eval("Fødeby") %></td>
                     <td style="border-bottom: 1px solid #ddd;">SidsteBopæl:  <br /><%# Eval("SidsteBopæl") %></td>
                     <td colspan="2" rowspan="2" align="right"><img src="./Uploads/<%# Eval("GravID") %>/profile.jpg"  onerror="this.onerror=null;this.src='./Images/profileavatar.png';" alt="Profil Billede"  style="max-width: 200px; height: 150px;"/></td>
                </tr>
            </table>
        </ItemTemplate>  
    </asp:DataList>
            <br />
        <asp:Label ID="labError" runat="server" Text="Din søgning gav ingen resultater!" Visible="false"></asp:Label>
    </article>
</asp:Content>