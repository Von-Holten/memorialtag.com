<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="Teest.register" MasterPageFile="~/Site.Master" MaintainScrollPositionOnPostback="true" %>

<%-- Vores indhold starter ved dette tag, head, header og footer defineres i site.master filen --%>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">
    
    <%-- Artikel til beskrivelse af processen --%>
    <article id="main" class="main">
        <br />

        <%-- De 3 overskrifter i en div med klassen div for at sprede dem ud --%>
        <div class="div">
            <h3>1. Bestil</h3>
            <h3>2. Log ind</h3>
            <h3>3. Udfyld</h3>
        </div>

        <%-- Vores 3 billeder, stadig med klassen div for at sprede dem ud --%>
        <div class="div">
            <img src="./images/order.jpg" alt="Billede"  style="width: 20vw; height: 30vh;"/>
            <img src="./images/login.jpg" alt="Billede"  style="width: 20vw; height: 30vh;"/>
            <img src="./images/edit.png" alt="Billede"  style="width: 20vw; height: 30vh;"/>
        </div>

        <%-- Vores beskrivelser under billederne --%>
        <div class="div">
            <p>Tekst her</p>
            <p>Tekst her</p>
            <p>Tekst her</p>
        </div>
    </article>

        <%-- Vores registreringsformular, den ligger i en section, fordi hele siden er pakket ind i en formular (site.master) --%>
		<div class="subMain">	
                            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
			<section action="#" method="post" class="register">
                <center><br><h2 >Bestil et Memorial Tag:</h2>
				<asp:TextBox ID="txtFornavn" runat="server" placeholder="Fornavn" MaxLength="50" name="name" type="text" required=""></asp:TextBox>
				<asp:TextBox ID="txtEfternavn" runat="server" placeholder="Efternavn" MaxLength="50" name="name" type="text" required=""></asp:TextBox>
				<asp:TextBox ID="txtTelefon" runat="server" placeholder="Telefon" MaxLength="20" name="phone" type="text" required=""></asp:TextBox>
				<asp:TextBox ID="txtEmail" runat="server" placeholder="Email" MaxLength="70" name="email" type="email" required=""></asp:TextBox>
				<asp:TextBox ID="txtPassword" runat="server"  placeholder="Kodeord" MaxLength="50" name="Password" type="password" required=""></asp:TextBox>
                <asp:TextBox ID="txtConfirm" runat="server"  placeholder="Gentag kodeord" MaxLength="50" name="Password" type="password" required=""></asp:TextBox><br>
                <asp:TextBox ID="txtLand" runat="server"  placeholder="Land" name="Country" Value="DK" MaxLength="50" type="text" required="" Enabled="false" style="width: 11.5vw;"></asp:TextBox>
                <asp:TextBox ID="txtBy" runat="server"  placeholder="By" name="City" type="text" MaxLength="50" required="" style="width: 15vw;"></asp:TextBox>
                <asp:TextBox ID="txtPostnr" runat="server"  placeholder="Postnr" name="zip" type="text" required="" style="width: 3vw;"></asp:TextBox>
                <asp:TextBox ID="txtVej" runat="server"  placeholder="Vej" name="address" MaxLength="50" type="text" required="" style="width: 23.5vw;"></asp:TextBox><br>
			    <asp:Button Class="button" ID="btnPayment" runat="server" Text="Udfør" OnClick="btnPayment_Click"/></center>
                <center><asp:Label ID="labError" runat="server" Text="Der skete en fejl, prøv igen!" ForeColor="Red" Visible="false"></asp:Label></center>
			</section>
		</div>
</asp:Content>