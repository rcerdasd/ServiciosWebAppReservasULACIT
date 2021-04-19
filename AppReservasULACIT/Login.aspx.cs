using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppReservasULACIT.Models;
using AppReservasULACIT.Controllers;
using System.IdentityModel.Tokens.Jwt;
using System.Web.Security;

namespace AppReservasULACIT
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        async protected void btnIngresar_Click(object sender, EventArgs e)
        {
            UsuarioManager usuarioManager = new UsuarioManager();
            Usuario usuario = await usuarioManager.Validar(txtIdentificacion.Text, txtPassword.Text);
            JwtSecurityToken jwtSecurityToken;

            if (usuario != null)
            {
                var jwtHandler = new JwtSecurityTokenHandler();
                jwtSecurityToken = jwtHandler.ReadJwtToken(usuario.CadenaToken);
                Session["IdUsuario"] = usuario.USU_IDENTIFICACION;
                Session["NombreUsuario"] = usuario.USU_NOMBRE;
                Session["TokenUsuario"] = usuario.CadenaToken;
                Session["InicioSesion"] = jwtSecurityToken.ValidFrom.ToString("dd/MM/yyyy HH:mm:ss");
                Session["FinSesion"] = jwtSecurityToken.ValidTo.ToString("dd/MM/yyyy HH:mm:ss");

                FormsAuthentication.RedirectFromLoginPage(txtIdentificacion.Text, true);

            }
            else
                lblError.Visible = true;
        }
    }
}