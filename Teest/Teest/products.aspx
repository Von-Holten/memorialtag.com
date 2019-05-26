<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="products.aspx.cs" Inherits="Teest.products" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">

    <article class="minimum">
        <h1>Produkter:</h1>
        <p>
            <br /> <%-- Vores produkter hives fra vores SqlDataSource1 og vises i vores tabel --%>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:memorialtagConnectionString %>" DeleteCommand="DELETE FROM [ProduktTabel] WHERE [ProduktID] = @ProduktID" InsertCommand="INSERT INTO [ProduktTabel] ([ProduktNavn], [ProduktPris], [ProduktBeskrivelse ], [Image]) VALUES (@ProduktNavn, @ProduktPris, @ProduktBeskrivelse_, @Image)" ProviderName="<%$ ConnectionStrings:memorialtagConnectionString.ProviderName %>" SelectCommand="SELECT [ProduktID], [ProduktNavn], [ProduktPris], [ProduktBeskrivelse ] AS ProduktBeskrivelse_, [Image] FROM [ProduktTabel]" UpdateCommand="UPDATE [ProduktTabel] SET [ProduktNavn] = @ProduktNavn, [ProduktPris] = @ProduktPris, [ProduktBeskrivelse ] = @ProduktBeskrivelse_, [Image] = @Image WHERE [ProduktID] = @ProduktID">
                <DeleteParameters>
                    <asp:Parameter Name="ProduktID" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="ProduktNavn" Type="String" />
                    <asp:Parameter Name="ProduktPris" Type="Double" />
                    <asp:Parameter Name="ProduktBeskrivelse_" Type="String" />
                    <asp:Parameter Name="Image" Type="String" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="ProduktNavn" Type="String" />
                    <asp:Parameter Name="ProduktPris" Type="Double" />
                    <asp:Parameter Name="ProduktBeskrivelse_" Type="String" />
                    <asp:Parameter Name="Image" Type="String" />
                    <asp:Parameter Name="ProduktID" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>
            <asp:ListView ID="ListView1" runat="server" DataSourceID="SqlDataSource1">
                <LayoutTemplate>
                    <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                </LayoutTemplate>
                <ItemTemplate>
                    <table border="1" class="tableView" style="width: 100%;">
                        <tr onclick="document.location='Produktdetaljer.aspx?produktid=<%#Eval("ProduktID")%>'">
                            <td style="width: 40%;" style="cursor:grab">
                                Navn: <%#Eval("ProduktNavn") %> <br />
                            </td>
                            <td style="width: 40%;" style="cursor:grab">
                                Pris: Dkk<%#Eval("ProduktPris") %><br /></td>
                            <td style="text-align: right;">
                                <img src="Images/<%#Eval("Image") %>" height="100" width="180" style="cursor:grab"/>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <div>Desværre, produktet er ikke tilgængeligt!</div>
                </EmptyDataTemplate>
            </asp:ListView>
            <br />
            </p>
    </article>



</asp:Content>