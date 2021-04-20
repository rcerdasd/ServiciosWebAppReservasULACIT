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
    public partial class frmAerolinea : System.Web.UI.Page
    {
        IEnumerable<Models.Aerolinea> aerolineas = new ObservableCollection<Models.Aerolinea>();
        AerolineaManager aerolineaManager = new AerolineaManager();
        async protected void Page_Load(object sender, EventArgs e)
        {
            InicializarControles();
        }

        async private void InicializarControles()
        {
            try
            {
                aerolineas = await aerolineaManager.ObtenerAerolineas(Session["TokenUsuario"].ToString());
                gvAerolineas.DataSource = aerolineas.ToList();
                gvAerolineas.DataBind();
            }
            catch (Exception e)
            {
                lblResultado.Text = "Hubo un error al inicializar controles. Detalle: " + e.Message;
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
            }

        }

        protected void gvAerolineas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "Codigo";
                e.Row.Cells[1].Text = "Nombre";
                e.Row.Cells[2].Text = "País";
                e.Row.Cells[3].Text = "Telefono";
                e.Row.Cells[4].Text = "Email";
            }
        }

        async protected void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarInsertar())
                {
                    Models.Aerolinea aerolineaIngresada = new Models.Aerolinea();
                    Models.Aerolinea aerolinea = new Models.Aerolinea()
                    {
                        AER_NOMBRE = txtNombre.Text,
                        AER_EMAIL = txtEmail.Text,
                        AER_PAIS = txtPais.Text,
                        AER_TELEFONO = txtTelefono.Text
                    };

                    aerolineaIngresada = await aerolineaManager.Ingresar(aerolinea, Session["TokenUsuario"].ToString());

                    if (aerolineaIngresada != null)
                    {
                        lblResultado.Text = "Aerolínea ingresada orrectamente";
                        lblResultado.ForeColor = Color.Green;
                        lblResultado.Visible = true;
                        InicializarControles();
                    }
                    else
                    {
                        lblResultado.Text = "Error al crear aerolínea";
                        lblResultado.ForeColor = Color.Maroon;
                        lblResultado.Visible = true;
                    }
                }
            }
            catch (Exception exc)
            {
                lblResultado.Text = "Hubo un error al ingresar la aerolínea. Detalle: " + exc.Message;
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
            }
        }

        private bool ValidarInsertar()
        {
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                lblResultado.Text = "Debe ingresar el nombre";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                lblResultado.Text = "Debe ingresar el email";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtTelefono.Text))
            {
                lblResultado.Text = "Debe ingresar el teléfono";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtPais.Text))
            {
                lblResultado.Text = "Debe ingresar el país";
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
                    Models.Aerolinea aerolineaModificada = new Models.Aerolinea();
                    Models.Aerolinea aerolinea = new Models.Aerolinea()
                    {
                        AER_CODIGO = Convert.ToInt32(txtCodigo.Text),
                        AER_NOMBRE = txtNombre.Text,
                        AER_EMAIL = txtEmail.Text,
                        AER_PAIS = txtPais.Text,
                        AER_TELEFONO = txtTelefono.Text
                    };

                    aerolineaModificada = await aerolineaManager.Actualizar(aerolinea, Session["TokenUsuario"].ToString());

                    if (aerolineaModificada != null)
                    {
                        lblResultado.Text = "Aerolínea actualizada correctamente";
                        lblResultado.ForeColor = Color.Green;
                        lblResultado.Visible = true;
                        InicializarControles();
                    }
                    else
                    {
                        lblResultado.Text = "Error al actualizar aerolínea";
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
            catch (Exception ex)
            {
                lblResultado.Text = "Hubo un error al actualizar la aerolínea. Detalle: " + ex.Message;
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
                    codigoEliminado = await aerolineaManager.Eliminar(txtCodigo.Text, Session["TokenUsuario"].ToString());

                    if (!string.IsNullOrEmpty(codigoEliminado))
                    {
                        InicializarControles();
                        lblResultado.Text = "Aerolínea eliminado con exito.";
                        lblResultado.ForeColor = Color.Green;
                        lblResultado.Visible = true;
                    }
                    else
                    {
                        lblResultado.Text = "Hubo un error al eliminar la aerolínea.";
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
            catch (Exception ex)
            {

                lblResultado.Text = "Hubo un error al eliminar la aerolínea. Detalle: " + ex.Message;
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
            }
        }
    }
}