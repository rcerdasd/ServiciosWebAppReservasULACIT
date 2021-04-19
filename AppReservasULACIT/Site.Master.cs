using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppReservasULACIT
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Text = "Bienvenido " + Session["NombreUsuario"].ToString();
            Label2.Text = "Inicio se sesion: " + Session["InicioSesion"].ToString() + " Fin de sesion " + Session["FinSesion"].ToString();
        }
    }
}