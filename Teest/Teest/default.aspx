<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Teest._default" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">

    <%-- Her starter vores artikel --%>
    <article class="main">
        <br />
        <%-- Vores artikel er delt op i div med klassen div for at bredde indholdet ud i flere kolonner --%>
        <div class="div">
            <h3>NFC</h3>
            <h3>QR</h3>
            <h3>Browser</h3>
        </div>
        <%-- Div tagget bruges både til vores overskrifter, billeder og tekster --%>
        <div class="div">
            <img src="./images/nfc.jpg" alt="Billede" style="width: 20vw; height: 30vh;"/>
            <img src="./images/qr.jpg" alt="Billede" style="width: 20vw; height: 30vh;"/>
            <img src="./images/browser.jpg" alt="Billede" style="width: 20vw; height: 30vh;"/>
        </div>

        <div class="div">
            <p></p>
            <p></p>
            <p></p>
        </div>
    </article>

    <%-- Vores artikel der indeholder vores søgeformular --%>
    <article id="soeg" class="search">
        <div class="div">
        <center>
        <asp:TextBox runat="server" ID="Search" type="text" name="Search" placeholder="Søg f.eks. på ID, Navn, Årstal, By eller Kirkegård..." style="width: 40vw; height: 1vh;" class="wrapper"/>
        <asp:Button ID="btnSearch" runat="server" Text="Søg" style="margin-left: -28vh;" OnClick="btnSearch_Click" Class="button"/></center>
        </div>
    </article>

    <%-- Vores artikel der indeholder vores loginformular --%>
    <section class="login" runat="server">
        <center><h2>Log ind</h2></center>
        <p></p>
        <label for="email"><b>E-Mail:</b></label>
        <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
        <label for="password"><b>Kodeord:</b></label>
        <asp:TextBox ID="txtPassword" TextMode="Password" runat="server"></asp:TextBox>
        <div class="div">
            <p id="rememberTekst" runat="server">Husk mig:<input type="checkbox" name="remember" id="remember" checked="checked" runat="server"/></p>
            <asp:Button ID="btnLogin" runat="server" Text="Godkend" OnClick="btnLogin_Click" Class="button"/>
            <p id="linkTekst" runat="server">Glemt <a href="#" id="linkGlemtKode" runat="server">kodeord?</a></p>
            <asp:Button ID="logUd" runat="server" Text="Log Af" visible="false" OnClick="logUd_Click" class="button" BackColor="red"/>
        </div>
        <center><asp:Label ID="labelLoginError" runat="server" Text="Angiv veligst korrekt brugernavn og kode!" Visible="false" ForeColor="#CC0000"></asp:Label></center>
    </section>

    <%-- Vores aside der indeholder link til vores registreringsside --%>
    <aside class="buy" runat="server">
        <center><h2>Bestil et Memorial Tag</h2></center>
        <p></p>
        <label><b>Hvad er det?</b></label>
        <ul>
            <li>Et jordspyd med NFC og QR tag</li>
            <li>En eller flere personlige mindesider</li>
            <li>Et efterliv i skyen til dine kære</li>
        </ul>
        <center><h3>Fra 299,95 kr.-</h3></center>
        <center><asp:Button runat="server" Class="button" text="Bestil Memorial Tag" ID="btnBestil" OnClick="btnBestil_Click"/></center>
    </aside> 

    <%-- Vores Modal Popup vindue der vises når der trykkes på Skift Kodeord linket --%>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <ajaxToolkit:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="linkGlemtKode" CancelControlID="btnClose" BackgroundCssClass="modalBackground" DropShadow="true">
    </ajaxToolkit:ModalPopupExtender>
    <%-- Indholdet i popup kassen ligger indenfor Panelets tags --%>
    <asp:Panel ID="Panel1" runat="server" Class="modalPopup" align="center" style="Display: none; height: 24vh; width: 25vw;">
        <div style="height: 50vh;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <br />
                    <h1>Bekræft:</h1>
                    <br />
                    <h2>Angiv din Email:</h2>
                    <asp:TextBox ID="txtForgotPass" runat="server" Style="width: 20vw;"></asp:TextBox>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:Label ID="labPassError" runat="server" Text="Den angivne Email findes ikke!" ForeColor="red" Visible="false"></asp:Label><br />
                
                <asp:Button ID="btnClose" runat="server" Text="Tilbage" style="background-color: dodgerblue; width: 10vw;" class="button"/>
                <asp:Button ID="btnPasswordReset" runat="server" Text="Send Kode"  style=" width: 10vw;" class="button" OnClick="btnPasswordReset_Click"/>
        </div>


    </asp:Panel>

</asp:Content>