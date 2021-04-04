using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppReservasULACIT.Models;
using AppReservasULACIT.Controllers;
using System.Drawing;

namespace AppReservasULACIT
{
    public partial class Registrar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnMostrarCalendario_Click(object sender, EventArgs e)
        {
            calFecNac.Visible = true;
        }

        protected void calFecNac_SelectionChanged(object sender, EventArgs e)
        {
            calFecNac.Visible = false;
        }

        async protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario()
            { 
                USU_NOMBRE = txtNombre.Text,
                USU_IDENTIFICACION = txtIdentificacion.Text,
                USU_EMAIL = txtEmail.Text,
                USU_TELEFONO = txtTelefono.Text,
                USU_ESTADO = "A",
                USU_FEC_NAC = calFecNac.SelectedDate,
                USU_PASSWORD = txtPassword.Text
            };

            UsuarioManager usuarioManager = new UsuarioManager();

            Usuario usuarioRegistrado = await usuarioManager.Registrar(usuario);

            if (!string.IsNullOrEmpty(usuarioRegistrado.USU_NOMBRE))
                Response.Redirect("Login.aspx");
            else
            {
                lblResultado.Text = "Error al crear usuario";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
            }
        }
    }
}