<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="editor.aspx.cs" Inherits="Teest.editor" MaintainScrollPositionOnPostback="true" %>

<%-- Vores indhold starter ved dette tag, head, header og footer defineres i site.master filen --%>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">

    <%-- Her starter vores artikel med felter til generel information - Denne er bygget op omkring en tabel  --%>
    <article class="info">
		<br/>
		<h1>Generel Information:</h1>
		<table style="width: 100%; text-align: left;">
		<tr>
			<th>Fornavn:</th>
			<th>Efternavn:</th>
		</tr>
		<tr>
			<td><asp:TextBox class="edit" ID="txtFornavn" MaxLength="50" Name="given name" runat="server" required></asp:TextBox></td>
			<td><asp:TextBox class="edit" ID="txtEfternavn" MaxLength="50" Name="family name" runat="server" required></asp:TextBox></td>
		</tr>
		<tr>
			<th>Fødselsdato:</th>
			<th>Dødsdato:</th>
		</tr>
		<tr>
			<td><asp:TextBox class="edit" ID="txtFødselsdato" runat="server" required></asp:TextBox></td>
			<td><asp:TextBox class="edit" ID="txtDødsdato" runat="server" required></asp:TextBox></td>
		</tr>
		<tr>
			<th>Fødeby:</th>
			<th>Sidste Bopæl:</th>
		</tr>
		<tr>
			<td><asp:TextBox class="edit" ID="txtFødeby" MaxLength="50" runat="server" required></asp:TextBox></td>
			<td><asp:TextBox class="edit" ID="txtSidsteBopæl" MaxLength="50" runat="server" required></asp:TextBox></td>
		</tr>
		<tr>
			<th>Stilling:</th>
			<th>Nærmeste pårørende:</th>
		</tr>
		<tr>
			<td><asp:TextBox class="edit" ID="txtStilling" MaxLength="50" runat="server" required></asp:TextBox></td>
			<td><asp:TextBox class="edit" ID="txtNærmestePårørende" MaxLength="100" runat="server" required></asp:TextBox></td>
		</tr>
        <tr>
			<th>Facebook Link:</th>
            <th>MyHeritage Link:</th>
		</tr>
        <tr>
			<td><asp:TextBox class="edit" ID="txtFacebookLink" MaxLength="100" runat="server" required/></td>
            <td><asp:TextBox class="edit" ID="txtMyHeritageLink" MaxLength="100" runat="server" required/></td>
		</tr>
		</table>
		<br/>
    </article>
            
            <%-- Her er vores aside som gør det muligt at uploade et profilbillede --%>
            <aside class="img" style="float: right; padding: 1vh 1vh 1vh 1vh; height: 43vh; width:25vw;">

                <div>
                <center>
                <br/>
                <br/>
                <h1>Profilbillede:</h1>
                <br />
                <br />
                <p>Upload et billede til siden</p>
                <br />
                    <%-- Vi bruger asp:fileupload til at tage imod et profilbillede --%>
                <br /><asp:FileUpload ID="fileUploadProfile" runat="server"></asp:FileUpload>
                <br />
                <br />
                <br/>
                </center>
                <p style="margin-left: 4vw; margin-right: 4vw;">OBS: Billedet vil automatisk blive skaleret op eller ned så det passer bedst muligt i kassen.</p>
                <br />
                <p style="margin-left: 4vw; margin-right: 4vw;">Du kan bruge: jpg, jpeg, og png.</p>
                <br />
                <br />
                    <%-- Dette label vises kun ved fejl --%>
                <center><asp:Label ID="StatusLabelProfile" runat="server" Text="Upload Status: " Visible="false" ForeColor="Red"></asp:Label></center>
                </div>
            </aside>

            <%-- Her har vi vores sektion med textbokse for biografi, uddannelse osv. --%>
            <section class="bio">
				    <br />
					<h1>Biografi:</h1>
                    <asp:TextBox id="txtaBiografi" runat="server" TextMode="MultiLine" MaxLength="5000" width="100%" rows="20"></asp:TextBox>
					<br/>
					<h3>Uddannelse:</h3>
                    <asp:TextBox id="txtaUddannelse" runat="server" TextMode="MultiLine" MaxLength="2000" width="100%" rows="20"></asp:TextBox>
					<br>
					<h3>Karriere:</h3>
                    <asp:TextBox id="txtaKarriere" runat="server" TextMode="MultiLine" MaxLength="2000" width="100%" rows="20"></asp:TextBox>
					<br/>
					<h3>Bedrifter:</h3>
                    <asp:TextBox id="txtaBedrifter" runat="server" TextMode="MultiLine" MaxLength="2000" width="100%" rows="20"></asp:TextBox>
            </section>

            <%-- Vores sektion til relationer --%>
			<section class="relationer">
			    <br />
				<h1>Relationer:</h1>
				<br>
                <h2>Tilføj familiemedlemer:</h2>
                <br />
                <table>
                    <tr>
                    <td>Titel:</td>
                    <td>
                    <%-- DropDown liste med alle de muligheder der skal kunne vælges --%>
                    <asp:DropDownList ID="dropdownTitel" Class="small" runat="server" style="border-radius: 20px; width: 11vw; height: 4vh; margin: -0.8vh 0vh; font-size: 1.8vh;">
                        <asp:ListItem Text="Oldemor (Mors side)" Value="Oldemor (Mors side)" />
                        <asp:ListItem Text="Oldefar (Mors side)" Value="Oldefar (Mors side)" />
                        <asp:ListItem Text="Oldemor (Fars side)" Value="Oldemor (Fars side)" />
                        <asp:ListItem Text="Oldefar Fars side" Value="Oldefar (Fars side)" />
                        <asp:ListItem Text="Mormor" Value="Mormor" />
                        <asp:ListItem Text="Morfar" Value="Morfar" />
                        <asp:ListItem Text="Farmor" Value="Farmor" />
                        <asp:ListItem Text="Farfar" Value="Farfar" />
                        <asp:ListItem Text="Mor" Value="Mor" />
                        <asp:ListItem Text="Far" Value="Far" />
                        <asp:ListItem Text="Søster" Value="Søster" />
                        <asp:ListItem Text="Bror" Value="Bror" />
                        <asp:ListItem Text="Datter" Value="Datter" />
                        <asp:ListItem Text="Søn" Value="Søn" />
                        <asp:ListItem Text="Barnebarn" Value="Barnebarn" />
                        <asp:ListItem Text="Oldebarn" Value="Oldebarn" />
                        <asp:ListItem Text="Moster" Value="Moster" />
                        <asp:ListItem Text="Onkel (Mors side)" Value="Onkel (Mors side)" />
                        <asp:ListItem Text="Kusine (Mors side)" Value="Kusine (Mors side)" />
                        <asp:ListItem Text="Fætter (Mors side)" Value="Fætter (Mors side)" />
                        <asp:ListItem Text="Nevø (Mors side)" Value="Nevø (Mors side)" />
                        <asp:ListItem Text="Niece (Mors side)" Value="Niece (Mors side)" />
                        <asp:ListItem Text="Faster" Value="Faster" />
                        <asp:ListItem Text="Onkel (Fars side)" Value="Onkel (Fars side)" />
                        <asp:ListItem Text="Kusine (Fars side)" Value="Kusine (Fars side)" />
                        <asp:ListItem Text="Fætter (Fars side)" Value="Fætter (Fars side)" />
                        <asp:ListItem Text="Nevø (Fars side)" Value="Nevø (Fars side)" />
                        <asp:ListItem Text="Niece (Fars side)" Value="Niece (Fars side)" />
                    </asp:DropDownList></td>
                    <td>Navn:</td>
                    <td><asp:TextBox ID="txtNavn" runat="server" Class="small"></asp:TextBox></td>
                    </tr>
                    <tr>
                    <td>Årstal:</td>
                    <td><asp:TextBox ID="txtÅr" runat="server" Class="small"></asp:TextBox></td>
                    <td>Link:</td>
                    <td><asp:TextBox ID="txtLink" runat="server" Class="small"></asp:TextBox></td>
                    </tr>
                </table>
                <center><asp:Button ID="btnRelationer" runat="server" Class="button" Text="Tilføj" /></center>
                <asp:ListView ID="listRelationer" runat="server"></asp:ListView>
				<br/>
				<br/>
			</section>

            <%-- Vores tidslinje hvor brugeren kan indtaste punkter om den afdøde --%>
			<aside class="timeline">
			    <br />
				<h1>Tidslinje:</h1>
				<br/>
                <h2>Tilføj punkter:</h2>
                <br />
                <table>
                    <tr>
                    <td>Navn:</td>
                    <td><asp:TextBox ID="txtT" runat="server" Class="small"></asp:TextBox></td>
                    <td>Årstal:</td>
                    <td><asp:TextBox ID="txtY" runat="server" Class="small"></asp:TextBox></td>
                    </tr>
                    <tr>
                    <td>Besk.:</td>
                    <td colspan="4"><asp:TextBox ID="txtBeskrivelse" runat="server" Class="edit" Style="width: 25.5vw;"></asp:TextBox></td>
                    </tr>
                </table>
                <center><asp:Button ID="btnTidslinje" runat="server" Class="button" Text="Tilføj" /></center>
                <asp:ListView ID="listTidslinje" runat="server"></asp:ListView>
				<br/>
				<br/>
			</aside>

    <%-- Vores galleri hvor brugeren med drag and drop kan uploade billeder --%>
	<section class="galleri">
        <br />
        <h1>Galleri:</h1>
        <br />
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <center><ajaxToolkit:AjaxFileUpload ID="AjaxFileUpload1" runat="server" BorderStyle="Dashed" OnUploadComplete="AjaxFileUpload1_UploadComplete" MaxFileSize="0" MaximumNumberOfFiles="11" ClearFileListAfterUpload="True" AllowedFileTypes="jpg,jpeg,png,mp4,mp3" Width="100%" UploaderStyle="Modern" CompleteBackColor="White" UploadingBackColor="#CCFFFF" AutoStartUpload="true"/></center>
		<br>
	</section>

  <%-- Vores sektion til thumbnails bruges på denne side til en knap der kører en update til vores SQL server --%>
  <section class="thumbnails">
      <br />
      <br />
    <center><asp:Button ID="btnAfslutRedigering" runat="server" Class="button" Text="Gem Ændringer" OnClick="btnAfslutRedigering_Click" />
      <br /><asp:Label ID="labError" runat="server" Text="Label" Visible="false" ForeColor="Red"></asp:Label></center>
    <br />
  </section>
</asp:Content>