using AppReservasULACIT.Controllers;
using AppReservasULACIT.Models;
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
    public partial class frmRentacar : System.Web.UI.Page
    {
        IEnumerable<Rentacar> rentacars = new ObservableCollection<Rentacar>();
        RentacarManager rentacarManager = new RentacarManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            InicializarControles();
        }

        async private void InicializarControles()
        {
            try
            {
                rentacars = await rentacarManager.ObtenerRentacars(Session["TokenUsuario"].ToString());
                gvRentacars.DataSource = rentacars.ToList();
                gvRentacars.DataBind();
            }
            catch (Exception e)
            {

                lblResultado.Text = "Hubo un error al enlistar los rentacars. Detalle: " + e.Message;
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
            }

        }

        protected void gvRentacars_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "Codigo";
                e.Row.Cells[1].Text = "Nombre";
                e.Row.Cells[2].Text = "Pais";
                e.Row.Cells[3].Text = "Telefono";
                e.Row.Cells[4].Text = "Email";
            }
        }

        private bool ValidarInsertar()
        {
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                lblResultado.Text = "Debe ingresar el nombre del rentacar";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtPais.Text))
            {
                lblResultado.Text = "Debe ingresar el pais del rentacar";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtTelefono.Text))
            {
                lblResultado.Text = "Debe ingresar el telefono del retacar";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                lblResultado.Text = "Debe ingresar el email del rentacar";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            return true;
        }

        async protected void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarInsertar())
                {
                    Rentacar rentacarIngresado = new Rentacar();
                    Rentacar rentacar = new Rentacar()
                    {
                        REN_NOMBRE = txtNombre.Text,
                        REN_PAIS = txtPais.Text,
                        REN_TELEFONO = txtTelefono.Text,
                        REN_EMAIL = txtEmail.Text
                    };
                    rentacarIngresado = await rentacarManager.Ingresar(rentacar, Session["TokenUsuario"].ToString());

                    if (rentacarIngresado != null)
                    {
                        lblResultado.Text = "Rentacar "+rentacarIngresado.REN_NOMBRE+" creado con exito.";
                        lblResultado.ForeColor = Color.Green;
                        lblResultado.Visible = true;
                        InicializarControles();
                    }
                    else
                    {
                        lblResultado.Text = "El rentacar no se pudo crear.";
                        lblResultado.BackColor = Color.Maroon;
                        lblResultado.Visible = true;
                    }
                }

            }
            catch (Exception ex)
            {
                lblResultado.Text = "El rentacar no se pudo crear. Detalle "+ex.Message;
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
            }
        }

        async protected void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarInsertar() && !string.IsNullOrEmpty(txtCodigo.Text))
                {
                    Rentacar rentacarModificado = new Rentacar();
                    Rentacar rentacar = new Rentacar()
                    {
                        REN_CODIGO = Convert.ToInt32(txtCodigo.Text.Trim()),
                        REN_NOMBRE = txtNombre.Text,
                        REN_PAIS = txtPais.Text,
                        REN_EMAIL = txtEmail.Text,
                        REN_TELEFONO = txtTelefono.Text

                    };

                    rentacarModificado = await rentacarManager.Actualizar(rentacar, Session["TokenUsuario"].ToString());

                    if (rentacarModificado != null)
                    {
                        lblResultado.Text = "El rentacar se ha modificado con exito";
                        lblResultado.ForeColor = Color.Green;
                        lblResultado.Visible = true;
                        InicializarControles();
                    }
                    else
                    {
                        lblResultado.Text = "El rentacar no se pudo modificar";
                        lblResultado.ForeColor = Color.Maroon;
                        lblResultado.Visible = true;
                    }
                }
                else
                {
                    lblResultado.Text = "Debe ingresar el codigo";
                    lblResultado.ForeColor = Color.Maroon;
                    lblResultado.Visible = true;
                }


            }
            catch (Exception exc)
            {
                {
                    lblResultado.Text = "Hubo un error al modificar el rentacar. Detalle: " + exc.Message;
                    lblResultado.ForeColor = Color.Maroon;
                    lblResultado.Visible = true;
                }
            }
        }

        async protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCodigo.Text))
                {
                    string codigoEliminado = null;
                    codigoEliminado = await rentacarManager.Eliminar(txtCodigo.Text.Trim(),Session["TokenUsuario"].ToString());
                    if (codigoEliminado != null)
                    {
                        lblResultado.Text = "El rentacar codigo " + codigoEliminado + " fue eliminado con exito";
                        lblResultado.ForeColor = Color.Green;
                        lblResultado.Visible = true;
                        InicializarControles();
                    }
                    else
                    {
                        lblResultado.Text = "El rentacar no pudo ser eliminado.";
                        lblResultado.ForeColor = Color.Maroon;
                        lblResultado.Visible = true;
                    }
                }
                else
                {
                    lblResultado.Text = "Debe ingresar el codigo del rentacar.";
                    lblResultado.ForeColor = Color.Maroon;
                    lblResultado.Visible = true;
                }
            }
            catch (Exception er)
            {
                lblResultado.Text = "Hubo un error al eliminar el rentacar. Detalle: " +er.Message;
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
            }
        }
    }
}