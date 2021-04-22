using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppReservasULACIT.Controllers;

namespace AppReservasULACIT
{
    public partial class frmAsientoAvion : System.Web.UI.Page
    {
        AvionManager avionManager = new AvionManager();
        IEnumerable<Models.Avion> aviones = new ObservableCollection<Models.Avion>();

        IEnumerable<Models.AsientoAvion> asientos = new ObservableCollection<Models.AsientoAvion>();
        AsientoAvionManager asientoManager = new AsientoAvionManager();
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
                asientos = await asientoManager.ObtenerAsientos(Session["TokenUsuario"].ToString());
                gvAsientos.DataSource = asientos.ToList();
                gvAsientos.DataBind();
            }
            catch (Exception e)
            {
                lblResultado.Text = "Hubo un error al inicializar controles. Detalle: " + e.Message;
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
            }

            aviones = await avionManager.ObtenerAviones(Session["TokenUsuario"].ToString());
            
            ddlAvion.DataSource = aviones.ToList();
            ddlAvion.DataTextField = "AVI_CODIGO";
            ddlAvion.DataValueField = "AVI_MODELO";
            ddlAvion.DataBind();

            txtModelo.Text = ddlAvion.SelectedValue;

        }

        protected void gvAsientos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "Código de asiento";
                e.Row.Cells[1].Text = "Código de avión";
                e.Row.Cells[2].Text = "Número de asiento";
                e.Row.Cells[3].Text = "Posición";
                e.Row.Cells[4].Text = "Clase";
                e.Row.Cells[5].Text = "Precio";
            }
        }

        async protected void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarInsertar())
                {
                    Models.AsientoAvion asientoIngresado = new Models.AsientoAvion();
                    Models.AsientoAvion asiento = new Models.AsientoAvion()
                    {
                        ASI_AVI_CLASE = txtClase.Text,
                        ASI_AVI_NUMERO = Convert.ToInt32(txtNum.Text),
                        ASI_AVI_POSICION = txtPos.Text,
                        ASI_AVI_PRECIO = Convert.ToDecimal(txtPrecio.Text),
                        AVI_CODIGO = Convert.ToInt32(ddlAvion.SelectedItem.ToString())
                    };

                    asientoIngresado = await asientoManager.Ingresar(asiento, Session["TokenUsuario"].ToString());

                    if (asientoIngresado != null)
                    {
                        lblResultado.Text = "Asiento ingresado correctamente";
                        lblResultado.ForeColor = Color.Green;
                        lblResultado.Visible = true;
                        InicializarControles();
                    }
                    else
                    {
                        lblResultado.Text = "Error al crear asiento";
                        lblResultado.ForeColor = Color.Maroon;
                        lblResultado.Visible = true;
                    }
                }
            }
            catch (Exception exc)
            {
                lblResultado.Text = "Hubo un error al ingresar el asiento. Detalle: " + exc.Message;
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
            }
        }


        private bool ValidarInsertar()
        {
            if (string.IsNullOrEmpty(txtClase.Text))
            {
                lblResultado.Text = "Debe ingresar la clase";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtNum.Text))
            {
                lblResultado.Text = "Debe ingresar el número de asiento";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtPos.Text))
            {
                lblResultado.Text = "Debe ingresar la posición";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtPrecio.Text))
            {
                lblResultado.Text = "Debe ingresar el precio";
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
                    Models.AsientoAvion asientoModificado = new Models.AsientoAvion();
                    Models.AsientoAvion asiento = new Models.AsientoAvion()
                    {
                        ASI_AVI_CODIGO = Convert.ToInt32(txtCodigo.Text),
                        ASI_AVI_CLASE = txtClase.Text,
                        ASI_AVI_NUMERO = Convert.ToInt32(txtNum.Text),
                        ASI_AVI_POSICION = txtPos.Text,
                        ASI_AVI_PRECIO = Convert.ToDecimal(txtPrecio.Text),
                        AVI_CODIGO = Convert.ToInt32(ddlAvion.SelectedItem.ToString())
                    };

                    asientoModificado = await asientoManager.Actualizar(asiento, Session["TokenUsuario"].ToString());

                    if (asientoModificado != null)
                    {
                        lblResultado.Text = "Asiento actualizado correctamente";
                        lblResultado.ForeColor = Color.Green;
                        lblResultado.Visible = true;
                        InicializarControles();
                    }
                    else
                    {
                        lblResultado.Text = "Error al actualizar asiento";
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
                lblResultado.Text = "Hubo un error al actualizar el asiento. Detalle: " + exce.Message;
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
                    codigoEliminado = await asientoManager.Eliminar(txtCodigo.Text, Session["TokenUsuario"].ToString());

                    if (!string.IsNullOrEmpty(codigoEliminado))
                    {
                        InicializarControles();
                        lblResultado.Text = "Asiento eliminado con éxito.";
                        lblResultado.ForeColor = Color.Green;
                        lblResultado.Visible = true;
                    }
                    else
                    {
                        lblResultado.Text = "Hubo un error al eliminar el asiento.";
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

                lblResultado.Text = "Hubo un error al eliminar el asiento. Detalle: " + excep.Message;
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
            }
        }

        protected void ddlAvion_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtModelo.Text = ddlAvion.SelectedValue;
        }
    }
}