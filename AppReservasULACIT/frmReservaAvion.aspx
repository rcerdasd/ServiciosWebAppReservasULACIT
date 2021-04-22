<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmReservaAvion.aspx.cs" Inherits="AppReservasULACIT.frmReservaAvion" Async="true" %>
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
            <asp:Label ID="Label1" runat="server" Text="Mantenimiento de Reserva de Avion"></asp:Label></h1>
        <asp:GridView Width="100%" ID="gvRAvion" runat="server" CellPadding="5" ForeColor="#333333" GridLines="None" OnRowDataBound="gvRAvion_RowDataBound">
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
                    <asp:Label ID="CodigoReserva" runat="server" Text="Codigo Reserva de Avión"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCodigoReserva" runat="server"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
             <tr>
                <td class="auto-style1">
                    <asp:Label ID="usuCodigo" runat="server" Text="Codigo de Usuario"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlUsuCodigo" runat="server"></asp:DropDownList>
                </td>
                <td>&nbsp;</td>
            </tr>
             <tr>
                <td class="auto-style1">
                    <asp:Label ID="AviCodigo" runat="server" Text="Codigo de Avion"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlAviCodigo" runat="server"></asp:DropDownList>
                </td>
                <td>&nbsp;</td>
            </tr>
             <tr>
                <td class="auto-style1">
                    <asp:Label ID="Label4" runat="server" Text="Fecha del Vuelo"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFecVuelo" runat="server" Enabled="false"></asp:TextBox>
                    <asp:Button ID="btnFecVuelo" runat="server" Text="Mostrar calendario" CausesValidation="False" OnClick="btnCalFecSalida_Click" />
                    <asp:Calendar ID="calFecVuelo" runat="server" BackColor="White" BorderColor="#999999" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" Width="200px" Visible="False" OnSelectionChanged="calFecSalida_SelectionChanged" CellPadding="4">
                        <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                        <NextPrevStyle VerticalAlign="Bottom" />
                        <OtherMonthDayStyle ForeColor="#808080" />
                        <SelectedDayStyle BackColor="#666666" ForeColor="White" Font-Bold="True" />
                        <SelectorStyle BackColor="#CCCCCC" />
                        <TitleStyle BackColor="#999999" Font-Bold="True" BorderColor="Black" />
                        <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <WeekendDayStyle BackColor="#FFFFCC" />
                    </asp:Calendar>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">Duración del vuelo</td>
                <td>
                    <asp:TextBox ID="txtDuracion" runat="server"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
                <tr>
                <td class="auto-style1">Escala del vuelo</td>
                <td>
                    <asp:TextBox ID="txtEscala" runat="server"></asp:TextBox>
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
