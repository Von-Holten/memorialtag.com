<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="payment.aspx.cs" Inherits="Teest.payment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
    

    <%-- Alt dette er tyvstjålet fra YourPay.io --%>
    <article action="https://payments.yourpay.se/betalingsvindue.php" method="post" class="form contact-form alt clearfix">
        <h1>Betaling:</h1>

        Dit Forretningsnummer (Vi har indtastet et du kan teste med)<br>
        <input name="MerchantNumber" type="text" placeholder="Yourpay forretningsnummer" value="60004771"><br>
        <input name="ShopPlatform" type="TEXT" placeholder="Din shopplatform" value="YourpayTestBetaling-ShopPlatform"><br>
        URL adressen vi skal returnere til efter betalingen er gennemført
        <input name="accepturl" type="text" placeholder="https://www.yourpay.io/da-dk/demo/" value="https://www.yourpay.io/da-dk/demo/"><br>
        Tidsstempel fra din server:
        <input name="time" type="text" placeholder="Unix Tidsstempel" value="1493029215"><br><br>
        Autocapture:
        <input name="autocapture" type="text" value="yes"  placeholder="Ved yes, hæves pengene automatisk når ordren godkendes"><br><br>
        Beløb: 100 = 1 kr.
        <input name="amount" type="text" placeholder="Beløb 100 = 1 EUR/DKK/SEK || 1000 = 10 EUR/DKK/SEK" value="5,00"><br><br>
        Valuta for betalingen:
        <select name="currency">
        <option value="208">DKK</option>
        <option value="978">EUR</option>
        <option value="758">SEK</option>
        <option value="840">USD</option>
        </select><br><br>
        Sprog på betalingsvindue<br>
        <select name="lang">
        <option value="da-dk">Dansk</option>
        <option value="en-gb">Engelsk</option>
        <option value="de-de">Tysk</option>
        <option value="se-se">Svensk</option>
        <option value="fr-fr">Fransk</option>
        </select><br><br>
        <input type="submit" value="Klik her for at åbne betalingsvinduet" class="usion-button button-flat button-round button-large button-custom button-1 btn-orange-hover-light">
    </article>

</asp:Content>