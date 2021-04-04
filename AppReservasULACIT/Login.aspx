<%@ Page Async="true" Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AppReservasULACIT.Login" UnobtrusiveValidationMode="None" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Inicie sesion</title>
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
            <h1><asp:Label ID="Label1" runat="server" Text="Ingrese sus credenciales"></asp:Label></h1>
            <asp:TextBox ID="txtIdentificacion" Placeholder="Ingrese su identificacion" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorId" 
                runat="server" ErrorMessage="La identificacion es obligatoria" 
                ControlToValidate="txtIdentificacion" ForeColor="Maroon"></asp:RequiredFieldValidator>
            <asp:TextBox ID="txtPassword" Placeholder="Ingrese su password" TextMode="Password" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorPassword" runat="server" 
                ErrorMessage="El password es obligatorio"
               ControlToValidate="txtPassword" ForeColor="Maroon"></asp:RequiredFieldValidator>
            <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" CssClass="button" OnClick="btnIngresar_Click" />
            <asp:Label ID="lblError" runat="server" Text="Credenciales invalidas" 
                Visible="false" 
                ForeColor="Maroon"></asp:Label><br />
            <a href="Registrar.aspx">Registrarme</a>
        </div>
    </form>
</body>
</html>
