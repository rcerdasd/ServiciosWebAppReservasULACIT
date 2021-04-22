<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmTiqueteAvion.aspx.cs" Inherits="AppReservasULACIT.frmTiqueteAvion" Async="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/bootstrap.min.css" rel="stylesheet">
    <script src="js/jquery-3.4.1.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <style type="text/css">
        .auto-style1 {
            width: 156px;
        }

        .auto-style2 {
            width: 156px;
            height: 31px;
        }

        .auto-style3 {
            height: 31px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="Container">
        <br />
        <br />
        <h1>
            <asp:Label ID="Label1" runat="server" Text="Mantenimiento de Tiquete de Avion"></asp:Label></h1>
        <asp:GridView Width="100%" ID="gvTiqueteAvion" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="gvTiqueteAvion_RowDataBound">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>

          <table style="width: 100%;">
            <tr>
                <td class="auto-style1">
                    <asp:Label ID="TiqAvionCodigo" runat="server" Text="Codigo Tiquete Avion"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTiqAvionCodigo" runat="server"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
             <tr>
                <td class="auto-style1">
                    <asp:Label ID="ResevaAvion" runat="server" Text="Codigo Reserva Avion"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlReservaAvionCodigo" runat="server"></asp:DropDownList>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:Label ID="TiqueteAvionOrigen" runat="server" Text="Origen Tiquete Avion"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTiqueteAvionOrigen" runat="server"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:Label ID="TiqueteAvionDestino" runat="server" Text="Destino Tiquete Avion "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTiqueteAvionDestino" runat="server"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">Precio Tiquete Avion</td>
                <td>
                    <asp:TextBox ID="txtTiqueteAvionPrecio" runat="server"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
                     
        </table>

           <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" OnClick="btnIngresar_Click" CssClass="btn btn-primary" />
        <asp:Button ID="btnModificar" runat="server" Text="Modificar" CssClass="btn bg-success" OnClick="btnModificar_Click" />
        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" OnClick="btnEliminar_Click" />
        <br />
        <asp:Label ID="lblResultado" runat="server" Text="Resultado" Visible="false"></asp:Label>

    </div>
</asp:Content>
