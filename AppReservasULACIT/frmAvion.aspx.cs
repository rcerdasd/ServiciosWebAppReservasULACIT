using AppReservasULACIT.Controllers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppReservasULACIT
{
    public partial class frmAvion : System.Web.UI.Page
    {
        AerolineaManager aerolineaManager = new AerolineaManager();
        IEnumerable<Models.Aerolinea> aerolineas = new ObservableCollection<Models.Aerolinea>();
        IEnumerable<Models.Avion> aviones = new ObservableCollection<Models.Avion>();
        AvionManager avionManager = new AvionManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InicializarControles();
            }

        }
        async void InicializarControles()
        {

            try
            {
                aviones = await avionManager.ObtenerAviones(Session["TokenUsuario"].ToString());
                gvAviones.DataSource = aviones.ToList();
                gvAviones.DataBind();
            }
            catch (Exception e)
            {
                lblResultado.Text = "Hubo un error al inicializar controles. Detalle: " + e.Message;
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
            }

            aerolineas = await aerolineaManager.ObtenerAerolineas(Session["TokenUsuario"].ToString());

            ddlAerolinea.DataSource = aerolineas.ToList();
            ddlAerolinea.DataTextField = "AER_NOMBRE";
            ddlAerolinea.DataValueField = "AER_CODIGO";
            ddlAerolinea.DataBind();

            txtAero.Text = ddlAerolinea.SelectedValue;

        }

        protected void gvAviones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "Código de avión";
                e.Row.Cells[1].Text = "Código de aerolínea";
                e.Row.Cells[2].Text = "Cantidad de asientos";
                e.Row.Cells[3].Text = "Modelo";
                e.Row.Cells[4].Text = "Estado";
                e.Row.Cells[5].Text = "Descripción";
            }
        }

        async protected void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarInsertar())
                {
                    Models.Avion avionIngresado = new Models.Avion();
                    Models.Avion avion = new Models.Avion()
                    {
                        AER_CODIGO = Convert.ToInt32(ddlAerolinea.SelectedValue),
                        AVI_CANT_ASIENTOS = Convert.ToInt32(txtCantAsi.Text),
                        AVI_DESCRIPCION = txtDesc.Text,
                        AVI_ESTADO = ddlEstado.SelectedValue,
                        AVI_MODELO = txtModelo.Text
                    };

                    avionIngresado = await avionManager.Ingresar(avion, Session["TokenUsuario"].ToString());

                    if (avionIngresado != null)
                    {
                        lblResultado.Text = "Avión ingresado correctamente";
                        lblResultado.ForeColor = Color.Green;
                        lblResultado.Visible = true;
                        InicializarControles();
                    }
                    else
                    {
                        lblResultado.Text = "Error al crear avión";
                        lblResultado.ForeColor = Color.Maroon;
                        lblResultado.Visible = true;
                    }
                }
            }
            catch (Exception exc)
            {
                lblResultado.Text = "Hubo un error al ingresar el avión. Detalle: " + exc.Message;
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
            }
        }

        private bool ValidarInsertar()
        {
            if (string.IsNullOrEmpty(txtCantAsi.Text))
            {
                lblResultado.Text = "Debe ingresar la cantidad de asientos";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtModelo.Text))
            {
                lblResultado.Text = "Debe ingresar el modelo";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtDesc.Text))
            {
                lblResultado.Text = "Debe ingresar la descripción";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }


            return true;
        }

        async protected void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarInsertar() && (!string.IsNullOrEmpty(txtCodigo.Text)))
                {
                    Models.Avion avionModificado = new Models.Avion();
                    Models.Avion avion = new Models.Avion()
                    {
                        AVI_CODIGO = Convert.ToInt32(txtCodigo.Text),
                        AER_CODIGO = Convert.ToInt32(ddlAerolinea.SelectedValue),
                        AVI_CANT_ASIENTOS = Convert.ToInt32(txtCantAsi.Text),
                        AVI_DESCRIPCION = txtDesc.Text,
                        AVI_ESTADO = ddlEstado.SelectedValue,
                        AVI_MODELO = txtModelo.Text

                    };

                    avionModificado = await avionManager.Actualizar(avion, Session["TokenUsuario"].ToString());

                    if (avionModificado != null)
                    {
                        lblResultado.Text = "Avión actualizado correctamente";
                        lblResultado.ForeColor = Color.Green;
                        lblResultado.Visible = true;
                        InicializarControles();
                    }
                    else
                    {
                        lblResultado.Text = "Error al actualizar avión";
                        lblResultado.ForeColor = Color.Maroon;
                        lblResultado.Visible = true;
                    }
                }
                else
                {
                    lblResultado.Text = "Debe ingresar todos los datos";
                    lblResultado.ForeColor = Color.Maroon;
                    lblResultado.Visible = true;
                }
            }
            catch (Exception exce)
            {

                lblResultado.Text = "Hubo un error al actualizar el avión. Detalle: " + exce.Message;
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
            }

        }

        async protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCodigo.Text))
                {
                    string codigoEliminado = string.Empty;
                    codigoEliminado = await avionManager.Eliminar(txtCodigo.Text, Session["TokenUsuario"].ToString());

                    if (!string.IsNullOrEmpty(codigoEliminado))
                    {
                        InicializarControles();
                        lblResultado.Text = "Avión eliminado con éxito.";
                        lblResultado.ForeColor = Color.Green;
                        lblResultado.Visible = true;
                    }
                    else
                    {
                        lblResultado.Text = "Hubo un error al eliminar el avión.";
                        lblResultado.ForeColor = Color.Maroon;
                        lblResultado.Visible = true;
                    }

                }
                else
                {
                    lblResultado.Text = "Debe ingresar el código";
                    lblResultado.ForeColor = Color.Maroon;
                    lblResultado.Visible = true;
                }
            }
            catch (Exception excep)
            {

                lblResultado.Text = "Hubo un error al eliminar el avión. Detalle: " + excep.Message;
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
            }
        }

        protected void ddlAerolinea_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtAero.Text = ddlAerolinea.SelectedValue;
        }
    }
}