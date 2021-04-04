<%@ Page Async="true" Language="C#" AutoEventWireup="true" CodeBehind="Registrar.aspx.cs" Inherits="AppReservasULACIT.Registrar" UnobtrusiveValidationMode="None" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Registro de usuario</title>
    <style>
body {font-family: Arial, Helvetica, sans-serif;}
form {border: 3px solid #f1f1f1;}

input[type=text], input[type=password] {
    width: 100%;
    padding: 12px 20px;
    margin: 8px 0;
    display: inline-block;
    border: 1px solid #ccc;
    box-sizing: border-box;
}

.button {
    background-color: #243054;
    color: white;
    padding: 14px 20px;
    margin: 8px 0;
    border: none;
    cursor: pointer;
    width: 100%;
}

button:hover {
    opacity: 0.8;
}

.cancelbtn {
    width: 100%;
    padding: 10px 18px;
    color: white;
    background-color: #898989;
}

.imgcontainer {
    text-align: center;
    margin: 24px 0 12px 0;
}

img.avatar {
    width: 10%;
    border-radius: 10%;
}

/* Clear floats */
.clearfix::after {
    content: "";
    clear: both;
    display: table;
}

.container {
    padding: 16px;
}

span.psw {
    float: right;
    padding-top: 16px;
}

/* Change styles for span and cancel button on extra small screens */
@media screen and (max-width: 300px) {
    span.psw {
       display: block;
       float: none;
        text-align: left;
    }
    .cancelbtn {
       width: 100%;
    }
}
</style>

</head>
<body>
     
    <form id="form1" runat="server">
       
        <div class="container">
            <h1>Registro</h1>
            <p>Complete el siguiente formulario para registrarse</p>
            <!-- Nombre -->
            <asp:TextBox runat="server" Placeholder="Ingrese su nombre" ID="txtNombre" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorNombre" runat="server"
                ControlToValidate="txtNombre" 
                CssClass="text-danger" ErrorMessage="El nombre es obligatorio" />
            <!-- Identificacion -->
            <asp:TextBox ID="txtIdentificacion" runat="server" Placeholder="Ingrese su identificacion" 
                CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorId" runat="server" 
                ErrorMessage="La identificacion es obligatoria" ControlToValidate="txtIdentificacion"/>
            <!-- Email -->
            <asp:TextBox ID="txtEmail" runat="server" Placeholder="Ingrese su correo electronico" 
                CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorEmail" runat="server" 
                ErrorMessage="El correo electronico es obligatorio" ControlToValidate="txtEmail"/>
            <!-- Telefono -->
            <asp:TextBox ID="txtTelefono" runat="server" Placeholder="Ingrese su telefono" 
                CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorTel" runat="server" 
                ErrorMessage="El telefono es obligatorio" ControlToValidate="txtTelefono"/>
            <br />
            <br />
            <asp:Label ID="Label1" runat="server" Text="Seleccione su fecha de nacimiento"></asp:Label>
            <br />
            <asp:Button ID="btnMostrarCalendario" runat="server" Text="Mostrar calendario" OnClick="btnMostrarCalendario_Click" CausesValidation="False" />
            <asp:Calendar ID="calFecNac" runat="server" BackColor="White" BorderColor="Black" DayNameFormat="Shortest" Font-Names="Times New Roman" Font-Size="10pt" ForeColor="Black" Height="220px" NextPrevFormat="FullMonth" TitleFormat="Month" Width="400px" Visible="False" OnSelectionChanged="calFecNac_SelectionChanged">
                <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" ForeColor="#333333" Height="10pt" />
                <DayStyle Width="14%" />
                <NextPrevStyle Font-Size="8pt" ForeColor="White" />
                <OtherMonthDayStyle ForeColor="#999999" />
                <SelectedDayStyle BackColor="#CC3333" ForeColor="White" />
                <SelectorStyle BackColor="#CCCCCC" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" ForeColor="#333333" Width="1%" />
                <TitleStyle BackColor="Black" Font-Bold="True" Font-Size="13pt" ForeColor="White" Height="14pt" />
                <TodayDayStyle BackColor="#CCCC99" />
            </asp:Calendar>
            <br />
            <!-- Password -->
            <asp:TextBox ID="txtPassword" runat="server" Placeholder="Ingrese su password" 
                CssClass="form-control" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorPass" runat="server" 
                ErrorMessage="El password es obligatorio" ControlToValidate="txtPassword"/>
            <!-- Confirmar password -->
              <asp:TextBox ID="txtConfirmarPassword" runat="server" Placeholder="Confirme su password" 
                CssClass="form-control" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorConf" runat="server" 
                ErrorMessage="Es obligatorio confirmar el password" ControlToValidate="txtConfirmarPassword"/>
            <asp:CompareValidator ID="CompareValidatorPassword" runat="server" 
                ErrorMessage="Los password no coinciden" ControlToValidate="txtPassword" 
                ControlToCompare="txtConfirmarPassword"></asp:CompareValidator>
            <br />
            <br />
            <asp:Button ID="btnRegistrar" runat="server" Text="Registrarme" CssClass="button" OnClick="btnRegistrar_Click" />
            <asp:Label ID="lblResultado" runat="server" Text="Label" Visible="false"></asp:Label>
        </div>
    </form>
    
</body>
</html>
