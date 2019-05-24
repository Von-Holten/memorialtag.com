<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="template.aspx.cs" Inherits="Teest.template" MasterPageFile="~/Site.Master" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">

                <%-- Her starter vores artikel med generel info om den afdøde --%>
                <article class="info">
					<br>
                        <div class="div">
					    <h1><asp:Label ID="labFuldeNavn" runat="server" Text="Label" Style="width: 50%;"/></h1>
						<h2><asp:Label ID="labKirkegård" runat="server" Text="Label" Style="width: 25%;"/></h2>
                        <h2><asp:Label ID="labID" runat="server" Text="Label" Style="width: 25%;"/></h2>
                        </div>
					<br>
					<table style="width: 100%; text-align: left;">
					<tr>
						<th>Fornavn:</th>
						<th>Efternavn:</th>
					</tr>
					<tr>
						<td><asp:Label ID="labFornavn" runat="server" Text="Label"></asp:Label></td>
						<td><asp:Label ID="labEfternavn" runat="server" Text="Label"></asp:Label></td>
					</tr>
					<tr>
						<th>Fødselsdato:</th>
						<th>Dødsdato:</th>
					</tr>
					<tr>
						<td><asp:Label ID="labFødselsdato" runat="server" Text="Label"></asp:Label></td>
						<td><asp:Label ID="labDødsdato" runat="server" Text="Label"></asp:Label></td>
					</tr>
					<tr>
						<th>Fødeby:</th>
						<th>Sidste Bopæl:</th>
					</tr>
					<tr>
						<td><asp:Label ID="labFødeby" runat="server" Text="Label"></asp:Label></td>
						<td><asp:Label ID="labSidsteBopæl" runat="server" Text="Label"></asp:Label></td>
					</tr>
					<tr>
						<th>Stilling:</th>
						<th>Nærmeste pårørende:</th>
					</tr>
					<tr>
						<td><asp:Label ID="labStilling" runat="server" Text="Label"></asp:Label></td>
						<td><asp:Label ID="labNærmestePårørende" runat="server" Text="Label"></asp:Label></td>
					</tr>
					</table>
                    <div class="div">
                        <p></p>
                        <p></p>
                        <asp:ImageButton ID="btnLinkFB" runat="server"  ImageUrl="~/Images/facebook.png" style="Height:5vh; Width:5vh;" />
                        <asp:ImageButton ID="btnLinkMH" runat="server"  ImageUrl="~/Images/mh.jpg" style="Height:5vh; Width:5vh;" />
                        <p></p>
                        <p></p>
                    </div>       
            </article>
	
            <%-- Vores aside der hiver profilbilledet fra den afdødes mappe - Findes der ikke et profile.jpg i mapen vises vores avatar istedet --%>
            <aside id="billedeSkift" runat="server" class="imgUpload" style="background: url(./images/avatar.png); background-repeat: no-repeat; background-position: center;">
                <asp:Image class="billede" ID="imgProfil" runat="server" style="max-width: 100%; min-height: 60%; max-height: 100%;"/>
            </aside>

            <%-- Vores sektion til Biografi, Uddannelse, Karriere og Bedrifter --%>
            <section class="bio" id="Biografi" runat="server">
				<br />
                <div class="border">
                    <h1><asp:Label ID="labBiografiOverskrift" runat="server" Text="Biografi:"/></h1><br />
                    <p><asp:Label ID="labBiografi" runat="server" Text="Label"/></p>
                    <br/>
                </div>
                <div class="border">
                    <br/>
                    <h4><asp:Label class="h4" ID="labUdannelseOverskrift" runat="server" Text="Uddannelse:"/></h4><br />
                    <p><asp:Label ID="labUddannelse" runat="server" Text="Label"/></p>
					<br/>
                </div>
                <div class="border">
					<br/>
                    <h4><asp:Label ID="labKarriereOverskrift" runat="server" Text="Karriere:"/></h4><br />
                    <p><asp:Label ID="labKarriere" runat="server" Text="Label"/></p>
					<br/>
                </div>
                <div class="border">
					<br/>
                    <h4><asp:Label ID="labBedrifterOverskrift" runat="server" Text="Bedrifter:"/></h4><br />
                    <p><asp:Label ID="labBedrifter" runat="server" Text="Label"/></p>
                    <br/>
                </div>
                <br/>
            </section>
            
            <%-- Vores sektion med relationer --%>
			<section class="relationer" id="Relations" runat="server">
							<br />
					<h1>Relationer:</h1>
					<br>
					<br>
			</section>
            
            <%-- Vores artikel med vores tidslinje --%>
			<aside class="timeline" id="TimeLine" runat="server">
				<br />
				<h1>Tidslinje:</h1>
				<br>
				<br>
                <asp:Label ID="tidslinjeFødselsår" runat="server" Text="Født"></asp:Label>
                <asp:Label ID="tidslinjeDødsår" runat="server" Text="Død"></asp:Label>
			</aside>

    <%-- Vores galleri som egentlig bare består af et stort billede og en container til video/lyd --%>
	<section class="galleri" runat="server">
		<center><h1>Galleri:</h1></center>
        <br />
        <center><asp:Image ID="bigImage" runat="server" style="max-height:60vh; max-width:70vw;"/></center>
        <center><video runat="server" id="video" style="width: 70vw; height: 60vh;" Visible="false" controls autoplay>
            <source src=""/>
            Din browser understøtter ikke video med html
            </video></center>
		<br />
	</section>

  <%-- Vores sektion som viser thumbnails over de billeder der er uploadet til den afdødes mappe - gælder også for video og lyd --%>
  <section class="thumbnails">
    <div id="ImageGallery" style="overflow:auto; height:100%; width:100%; display:inline-block; position: relative; overflow-x: scroll; overflow-y: hidden; -webkit-overflow-scrolling: touch; white-space: nowrap;">
    <center><asp:ImageButton ID="Image1" runat="server" style="height: 17vh; max-width: 14vw; display: inline-block; margin: 0 0.25%;" Visible="False" OnClick="Image1_Click"/>
    <asp:ImageButton ID="Image2" runat="server" style="height: 17vh; max-width: 14vw; display: inline-block; margin: 0 0.25%;" Visible="False" OnClick="Image2_Click"/>
    <asp:ImageButton ID="Image3" runat="server" style="height: 17vh; max-width: 14vw; display: inline-block; margin: 0 0.25%;" Visible="False" OnClick="Image3_Click"/>
    <asp:ImageButton ID="Image4" runat="server" style="height: 17vh; max-width: 14vw; display: inline-block; margin: 0 0.25%;" Visible="False" OnClick="Image4_Click"/>
    <asp:ImageButton ID="Image5" runat="server" style="height: 17vh; max-width: 14vw; display: inline-block; margin: 0 0.25%;" Visible="False" OnClick="Image5_Click"/>
    <asp:ImageButton ID="Image6" runat="server" style="height: 17vh; max-width: 14vw; display: inline-block; margin: 0 0.25%;" Visible="False" OnClick="Image6_Click"/>
    <asp:ImageButton ID="Image7" runat="server" style="height: 17vh; max-width: 14vw; display: inline-block; margin: 0 0.25%;" Visible="False" OnClick="Image7_Click"/>
    <asp:ImageButton ID="Image8" runat="server" style="height: 17vh; max-width: 14vw; display: inline-block; margin: 0 0.25%;" Visible="False" OnClick="Image8_Click"/>
    <asp:ImageButton ID="Image9" runat="server" style="height: 17vh; max-width: 14vw; display: inline-block; margin: 0 0.25%;" Visible="False" OnClick="Image9_Click"/>
    <asp:ImageButton ID="Image10" runat="server" style="height: 17vh; max-width: 14vw; display: inline-block; margin: 0 0.25%;" Visible="False" OnClick="Image10_Click"/>
    <asp:ImageButton ID="Image11" runat="server" style="height: 17vh; max-width: 14vw; display: inline-block; margin: 0 0.25%;" Visible="False" OnClick="Image11_Click"/>
    </center></div>
  </section>

    <article>
        <asp:RadioButton ID="RadioButton1" GroupName="r1" Text="Godkend" runat="server" visible="false"/>
        <asp:RadioButton ID="RadioButton2" GroupName="r1" Text="Tag siden ned" gr runat="server"  visible="false"/>
        <asp:Button ID="Button1" runat="server" Text="Gem" OnClick="Button1_Click"  visible="false"/>
        <asp:Label ID="Label1" runat="server" Text="Label" visible="false"></asp:Label>
    </article>
</asp:Content>