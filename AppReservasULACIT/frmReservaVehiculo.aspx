<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmReservaVehiculo.aspx.cs" Async="true" Inherits="AppReservasULACIT.frmReservaVehiculo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/bootstrap.min.css" rel="stylesheet">
    <script src="js/jquery-3.4.1.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <style type="text/css">
        .auto-style1 {
            width: 156px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-md">
        <br />
        <br />
        <h1>
            <asp:Label ID="Label1" runat="server" Text="Reserva Vehiculo"></asp:Label></h1>
        <asp:GridView Width="100%" ID="gvReservaVehiculos" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="gvReservaVehiculos_RowDataBound">
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
        <table class="table-success" style="width: 100%;">
            <tr>
                <td class="auto-style1">
                    <asp:Label ID="Label2" runat="server" Text="Codigo reserva vehiculo"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtResVehCodigo" runat="server"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:Label ID="Label3" runat="server" Text="Codigo usuario"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlUsuarios" runat="server"></asp:DropDownList>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:Label ID="Label6" runat="server" Text="Codigo paquete"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlPaquete" runat="server"></asp:DropDownList>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:Label ID="Label4" runat="server" Text="Fecha de partida"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFechaSalida" runat="server" Enabled="false"></asp:TextBox>
                    <asp:Button ID="btnCalFecSalida" runat="server" Text="Mostrar calendario" CausesValidation="False" OnClick="btnCalFecSalida_Click" />
                    <asp:Calendar ID="calFecSalida" runat="server" BackColor="White" BorderColor="Black" DayNameFormat="Shortest" Font-Names="Times New Roman" Font-Size="10pt" ForeColor="Black" Height="220px" NextPrevFormat="FullMonth" TitleFormat="Month" Width="400px" Visible="False" OnSelectionChanged="calFecSalida_SelectionChanged">
                        <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" ForeColor="#333333" Height="10pt" />
                        <DayStyle Width="14%" />
                        <NextPrevStyle Font-Size="8pt" ForeColor="White" />
                        <OtherMonthDayStyle ForeColor="#999999" />
                        <SelectedDayStyle BackColor="#CC3333" ForeColor="White" />
                        <SelectorStyle BackColor="#CCCCCC" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" ForeColor="#333333" Width="1%" />
                        <TitleStyle BackColor="Black" Font-Bold="True" Font-Size="13pt" ForeColor="White" Height="14pt" />
                        <TodayDayStyle BackColor="#CCCC99" />
                    </asp:Calendar>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:Label ID="Label5" runat="server" Text="Fecha de regreso"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFechaRegreso" runat="server" Enabled="false"></asp:TextBox>
                    <asp:Button ID="btnCalFecRegreso" runat="server" Text="Mostrar calendario" CausesValidation="False" OnClick="btnCalFecRegreso_Click" />
                    <asp:Calendar ID="calFechaRegreso" runat="server" BackColor="White" BorderColor="Black" DayNameFormat="Shortest" Font-Names="Times New Roman" Font-Size="10pt" ForeColor="Black" Height="220px" NextPrevFormat="FullMonth" TitleFormat="Month" Width="400px" Visible="False" OnSelectionChanged="CalFecRegreso_SelectionChanged">
                        <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" ForeColor="#333333" Height="10pt" />
                        <DayStyle Width="14%" />
                        <NextPrevStyle Font-Size="8pt" ForeColor="White" />
                        <OtherMonthDayStyle ForeColor="#999999" />
                        <SelectedDayStyle BackColor="#CC3333" ForeColor="White" />
                        <SelectorStyle BackColor="#CCCCCC" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" ForeColor="#333333" Width="1%" />
                        <TitleStyle BackColor="Black" Font-Bold="True" Font-Size="13pt" ForeColor="White" Height="14pt" />
                        <TodayDayStyle BackColor="#CCCC99" />
                    </asp:Calendar>
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
        <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" OnClick="btnIngresar_Click" CssClass="btn btn-primary" />
        <asp:Button ID="btnModificar" runat="server" Text="Modificar" CssClass="btn bg-success" OnClick="btnModificar_Click" />
        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" OnClick="btnEliminar_Click" />
        <br />
        <br />
        <asp:Label ID="lblResultado" runat="server" Text="Resultado" Visible="false"></asp:Label>

    </div>
</asp:Content>
