<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="controlpanel.aspx.cs" Inherits="Teest.controlpanel" MaintainScrollPositionOnPostback="true" %>

<%-- Vores indhold starter ved dette tag, head, header og footer defineres i site.master filen --%>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
    
<%-- Styling til tabellen over Memorial Tags - Virker ikke når det skrives i CSS filen*/ --%>
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

    .modalBackground{
        background-color: gray;
        filter: alpha(opacity=50);
        opacity: 0.5;
    }

.modalPopup {
    background-color: #FFFFFF;
    border-width: 3px;
    border-style: solid;
    border-color: black;
    padding-top: 10px;
    padding-left: 10px;
    width: 300px;
    height: 140px;
}
</style>


    <%-- Vores artikel der indeholder informationer om brugeren der er logget ind --%>
    <article>
        			<br/>
                    <div class="div">
                        <h1><asp:Label ID="labUserOverskrift" runat="server" Text=""></asp:Label></h1>
                        <p></p>
                        <h2><asp:Label ID="kundeID" runat="server" Text="Label" Style="width: 25%;"/></h2>
                    </div>
					<br/>
					<table style="width: 100%; text-align: left;">
					<tr >
						<th style="width: 30%;">Fornavn:</th>
						<th style="width: 30%;">Efternavn:</th>
                        <th style="width: 30%;">Password:</th>
                        <th rowspan="4" style="width: 10%;">
                            <asp:Button ID="Button2" runat="server" Text="Gå til produkter" class="button" style="width: 10vw;"/>
                        </th>
					</tr>
					<tr>
						<td><asp:Label ID="labFornavn" runat="server" Text="Label"></asp:Label></td>
						<td><asp:Label ID="labEfternavn" runat="server" Text="Label"></asp:Label></td>
                        <td><asp:TextBox Enabled="false" ID="Password" type="password" runat="server" Text="" Style="width: 12vw; margin-bottom: 0;" required pattern=".{4,50}" ></asp:TextBox></td>
					</tr>
					<tr>
						<th>Telefon:</th>
						<th>Email:</th>
                        <th>Nyt Password:</th>
					</tr>
					<tr>
						<td><asp:Label ID="labTelefon" runat="server" Text="Label"></asp:Label></td>
						<td><asp:Label ID="labEmail" runat="server" Text="Label"></asp:Label></td>
                        <td><asp:TextBox Enabled="false" ID="NytPassword" type="password" runat="server" Text="" Style="width: 12vw; margin-bottom: 0;"  required pattern=".{4,50}"></asp:TextBox></td>
					</tr>
					<tr>
						<th>Land:</th>
						<th>By:</th>
                        <th>Gentag Password:</th>
                        <th rowspan="4">
                            <asp:Button ID="btnDeleteUser" runat="server" Text="Slet Bruger" class="button" style="width: 10vw; background-color:red;"/>
                        </th>
					</tr>
					<tr>
						<td><asp:Label ID="labLand" runat="server" Text="Label"></asp:Label></td>
						<td><asp:Label ID="labBy" runat="server" Text="Label"></asp:Label></td>
                        <td><asp:TextBox Enabled="false" ID="GentagPassword" type="password" runat="server" Text="" Style="width: 12vw; margin-bottom: 0;"  required pattern=".{4,50}"></asp:TextBox></td>
                    </tr>
                    <tr>
						<th>Postnr:</th>
						<th>Vej:</th>
                        <th><asp:Label ID="labError" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label></th>
					</tr>
                    <tr>
						<td><asp:Label ID="labPostnr" runat="server" Text="Label"></asp:Label></td>
						<td><asp:Label ID="labVej" runat="server" Text="Label"></asp:Label></td>
                        <td><asp:Button class="button" ID="Button1" runat="server" Text="Skift Password" OnClick="Button1_Click" /></td>
					</tr>
					</table>
                    
					<br/>
    </article>

        <%-- Vores sektion der viser de memorial tags der er tilknyttet brugeren med et listview --%>
        <section style="min-height:34vh;">
            <br />
            <h1>Memorial Tags:</h1>
            <br />
            <asp:DataList ID="dl" runat="server" DataSourceID="SqlDataSource1" RepeatLayout="Table" style="width: 100%;">  
        <ItemTemplate>
            <table class="tableView">  
                <tr onclick="document.location='editor.aspx?id=<%# Eval("GravID") %>'" style="cursor:grab">
                    <td style="border-bottom: 1px solid #ddd;">ID:  <br /><%# Eval("GravID") %></td>
                     <td style="border-bottom: 1px solid #ddd;">Navn:  <br /><%# Eval("Fornavn") %> <%# Eval("Efternavn") %></td> 
                     <td style="border-bottom: 1px solid #ddd;">Født:  <br /><%# Eval("Fødselsdato") %></td> 
                     <td style="border-bottom: 1px solid #ddd;">Død:  <br /><%# Eval("Dødsdato") %></td>
                     <td colspan="2" rowspan="2" align="right"><img src="./Uploads/<%# Eval("GravID") %>/profile.jpg" onerror="this.onerror=null;this.src='./Images/profileavatar.png';" alt="Profil Billede" style="max-width: 200px; height: 150px;"/></td>

                </tr>
                
            </table>  
        </ItemTemplate>  
    </asp:DataList>  
            <br />
        </section>

<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <ajaxToolkit:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="btnDeleteUser" CancelControlID="btnClose" BackgroundCssClass="modalBackground" DropShadow="true">

    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="Panel1" runat="server" Class="modalPopup" align="center" style="Display: none; height: 32vh; width: 25vw;">
        <div style="height: 50vh;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <br />
                    <h1>Bekræft:</h1>
                    <br />
                    <h2>Vil du virkelig slette din bruger?</h2>
                    <br />
                    <p style="font-weight:bold;">Bemærk:<br /></p><p>Dette vil slette alle dine brugerdata!</p>
                    <p>Angiv kodeord:</p>
                    <asp:TextBox ID="txtDeletePass" runat="server" Style="width: 20vw;"></asp:TextBox>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:Label ID="labPassError" runat="server" Text="Det angive password er ikke korrekt!" ForeColor="red" Visible="false"></asp:Label><br />
                
                <asp:Button ID="btnClose" runat="server" Text="Tilbage" style="background-color: dodgerblue; width: 10vw;" class="button"/>
                <asp:Button ID="btnDelete" runat="server" Text="Slet bruger"  style="background-color: red; width: 10vw;" class="button" OnClick="btnDelete_Click"/>
        </div>


    </asp:Panel>


</asp:Content>