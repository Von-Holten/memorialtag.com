<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProduktDetaljer.aspx.cs" Inherits="teest.ProduktDetaljer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
    <style>
        .tableView2 {
            border-collapse: collapse;
            width: 100%;
        }

            .tableView2 td, .tableView2 th {
                border-bottom: 1px solid #ddd;
                border-top: 1px solid #ddd;
                padding: 8px;
                vertical-align: middle;
            }
    </style>

    <article>
        <div>
            <h1>
                <asp:Label ID="labProduktNavn" runat="server" Text="Label"></asp:Label></h1>
        </div>
        <br />
        <table style="width: 100%;" class="tableView2">
            <tr>
                <td rowspan="2" style="width=30%;">
                    <img id="imgProdukt" runat="server" src="" height="300" width="300" />
                </td>
                <td style="width=25%;">Beskrivelse:
                    <br />
                    <asp:Label ID="labBeskrivelse" runat="server" Text=""></asp:Label><br />
                </td>
                <td style="width=25%;">Produktets Pris:
                    <br />
                    <asp:Label ID="labProduktetsPris" runat="server" Text=""></asp:Label><br />
                </td>
                <td style="width=20%;">Pris i alt:<br />
                    <asp:Label ID="labPrisIAlt" runat="server" Text=""></asp:Label><br />

                </td>
            </tr>
            <tr>
                <td>Antal:
                    <br />
                    <asp:DropDownList ID="DropDown" runat="server" Class="small" Style="border-radius: 20px; width: 11vw; height: 4vh; margin: -0.8vh 0vh; font-size: 1.8vh;" OnSelectedIndexChanged="DropDownChanged" AutoPostBack="True">
                        <asp:ListItem Text="1 grav" Value="1" />
                        <asp:ListItem Text="2 grave" Value="2" />
                        <asp:ListItem Text="3 grave" Value="3" />
                        <asp:ListItem Text="4 grave" Value="4" />
                        <asp:ListItem Text="5 grave" Value="5" />
                        <asp:ListItem Text="6 grave" Value="6" />
                        <asp:ListItem Text="7 grave" Value="7" />
                        <asp:ListItem Text="8 grave" Value="8" />
                        <asp:ListItem Text="9 grave" Value="9" />
                        <asp:ListItem Text="10 grave" Value="10" />
                        <asp:ListItem Text="11 grave" Value="11" />
                        <asp:ListItem Text="12 grave" Value="12" />
                        <asp:ListItem Text="13 grave" Value="13" />
                        <asp:ListItem Text="14 grave" Value="14" />
                        <asp:ListItem Text="15 grave" Value="15" />
                        <asp:ListItem Text="16 grave" Value="16" />
                        <asp:ListItem Text="17 grave" Value="17" />
                        <asp:ListItem Text="18 grave" Value="18" />
                        <asp:ListItem Text="19 grave" Value="19" />
                        <asp:ListItem Text="20 grave" Value="20" />
                    </asp:DropDownList>
                </td>
                <td>Pris for antal grave:<br />
                    <asp:Label ID="labGravPris" runat="server" Text=""></asp:Label>
                </td>
                <td>
                    <asp:Button Class="button" ID="btnTilføj" runat="server" Text="Føj til kurv" OnClick="btnTilføj_Click" /></td>
            </tr>
        </table>
    </article>

    <article style="height: 34vh;">
        <br />
        <h1>Din Indkøbskurv:</h1>
        <br />
        <table class="tableView2" id="tableIndkøbskurv" runat="server" visible="false">
            <tr>
                <td rowspan="2" style="width: 30%;">
                    <img id="imgIndkøbskurv" src="" style="height: 19vh;" runat="server" /></td>
                <td style="width: 30%;">Produkt:<br />
                    <asp:Label ID="labDetails" runat="server" Text="Label"></asp:Label>
                </td>
                <td style="width: 20%;">Antal Grave:<br />
                    <asp:DropDownList ID="DropDownList1" runat="server" Class="small" Style="border-radius: 20px; width: 11vw; height: 4vh; margin: -0.8vh 0vh; font-size: 1.8vh;" OnSelectedIndexChanged="DropDownChanged" AutoPostBack="True">
                        <asp:ListItem Text="1 grav" Value="1" />
                        <asp:ListItem Text="2 grave" Value="2" />
                        <asp:ListItem Text="3 grave" Value="3" />
                        <asp:ListItem Text="4 grave" Value="4" />
                        <asp:ListItem Text="5 grave" Value="5" />
                        <asp:ListItem Text="6 grave" Value="6" />
                        <asp:ListItem Text="7 grave" Value="7" />
                        <asp:ListItem Text="8 grave" Value="8" />
                        <asp:ListItem Text="9 grave" Value="9" />
                        <asp:ListItem Text="10 grave" Value="10" />
                        <asp:ListItem Text="11 grave" Value="11" />
                        <asp:ListItem Text="12 grave" Value="12" />
                        <asp:ListItem Text="13 grave" Value="13" />
                        <asp:ListItem Text="14 grave" Value="14" />
                        <asp:ListItem Text="15 grave" Value="15" />
                        <asp:ListItem Text="16 grave" Value="16" />
                        <asp:ListItem Text="17 grave" Value="17" />
                        <asp:ListItem Text="18 grave" Value="18" />
                        <asp:ListItem Text="19 grave" Value="19" />
                        <asp:ListItem Text="20 grave" Value="20" />
                    </asp:DropDownList>
                </td>
                <td style="width: 20%;">Pris:<br />
                    <asp:Label ID="labTotal" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
        </table>
        <center><asp:Button Class="button" ID="btnBuy" runat="server" Text="Gå til betaling" Visible="false" OnClick="btnBuy_Click"/></center>
    </article>
</asp:Content>
