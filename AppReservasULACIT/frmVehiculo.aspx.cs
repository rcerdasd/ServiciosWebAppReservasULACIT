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
    public partial class frmVehiculo : System.Web.UI.Page
    {
        VehiculoManager vehiculoManager = new VehiculoManager();
        IEnumerable<Vehiculo> vehiculos = new ObservableCollection<Vehiculo>();

        IEnumerable<Rentacar> rentacars = new ObservableCollection<Rentacar>();
        RentacarManager rentacarManager = new RentacarManager();


async private void InicializarControles()
        {
            try
            {
                vehiculos = await vehiculoManager.ObtenerVehiculos(Session["TokenUsuario"].ToString());
                gvVehiculos.DataSource = vehiculos.ToList();
                gvVehiculos.DataBind();

                rentacars = await rentacarManager.ObtenerRentacars(Session["TokenUsuario"].ToString());
                ddlRentacars.DataSource = rentacars.ToList();
                ddlRentacars.DataTextField = "REN_NOMBRE";
                ddlRentacars.DataValueField = "REN_CODIGO";
                ddlRentacars.DataBind();
            }
            catch (Exception e)
            {

                lblResultado.Text = "Hubo un error al enlistar los vehiculos. Detalle: " + e.Message;
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
            }

        }

        private bool ValidarInsertar()
        {
            if (ddlRentacars.SelectedValue.Equals(null))
            {
                lblResultado.Text = "Debe seleccionar el rentacar";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtPasajeros.Text))
            {
                lblResultado.Text = "Debe ingresar la cantidad de pasajeros del vehiculo";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtModelo.Text))
            {
                lblResultado.Text = "Debe ingresar el modelo del vehiculo";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtDescripcion.Text))
            {
                lblResultado.Text = "Debe ingresar la descripcion del vehiculo";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            return true;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InicializarControles();
            }
        }

        protected void gvVehiculos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "Codigo";
                e.Row.Cells[1].Text = "Codigo de rentacar";
                e.Row.Cells[2].Text = "Pasajeros";
                e.Row.Cells[3].Text = "Modelo";
                e.Row.Cells[4].Text = "Estado";
                e.Row.Cells[4].Text = "Descripcion";
            }
        }

        async protected void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarInsertar())
                {
                    Vehiculo vehiculoIngresado = new Vehiculo();
                    Vehiculo vehiculo = new Vehiculo()
                    {
                        REN_CODIGO = Convert.ToInt32(ddlRentacars.SelectedValue),
                        VEH_CANT_PASAJEROS = Convert.ToInt32(txtPasajeros.Text.Trim()),
                        VEH_MODELO = txtModelo.Text,
                        VEH_ESTADO = rblEstado.SelectedValue,
                        VEH_DESCRIPCION = txtDescripcion.Text

                    };
                    vehiculoIngresado = await vehiculoManager.Ingresar(vehiculo, Session["TokenUsuario"].ToString());

                    if (vehiculoIngresado != null)
                    {
                        lblResultado.Text = "Vehiculo " + vehiculoIngresado.VEH_MODELO+ " creado con exito.";
                        lblResultado.ForeColor = Color.Green;
                        lblResultado.Visible = true;
                        InicializarControles();
                    }
                    else
                    {
                        lblResultado.Text = "El vehiculo no se pudo crear.";
                        lblResultado.BackColor = Color.Maroon;
                        lblResultado.Visible = true;
                    }
                }

            }
            catch (Exception ex)
            {
                lblResultado.Text = "El vehiculo no se pudo crear. Detalle " + ex.Message;
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
                    Vehiculo vehiculoModificado = new Vehiculo();
                    Vehiculo vehiculo = new Vehiculo()
                    {
                        VEH_CODIGO = Convert.ToInt32(txtCodigo.Text.Trim()),
                        REN_CODIGO = Convert.ToInt32(ddlRentacars.SelectedValue),
                        VEH_CANT_PASAJEROS = Convert.ToInt32(txtPasajeros.Text.Trim()),
                        VEH_MODELO = txtModelo.Text,
                        VEH_ESTADO = rblEstado.SelectedValue,
                        VEH_DESCRIPCION = txtDescripcion.Text

                    };

                    vehiculoModificado = await vehiculoManager.Actualizar(vehiculo, Session["TokenUsuario"].ToString());

                    if (vehiculoModificado != null)
                    {
                        lblResultado.Text = "El vehiculo se ha modificado con exito";
                        lblResultado.ForeColor = Color.Green;
                        lblResultado.Visible = true;
                        InicializarControles();
                    }
                    else
                    {
                        lblResultado.Text = "El vehiculo no se pudo modificar";
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
                    lblResultado.Text = "Hubo un error al modificar el vehiculo. Detalle: " + exc.Message;
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
                    codigoEliminado = await vehiculoManager.Eliminar(txtCodigo.Text.Trim(), Session["TokenUsuario"].ToString());
                    if (codigoEliminado != null)
                    {
                        lblResultado.Text = "El vehiculo codigo " + codigoEliminado + " fue eliminado con exito";
                        lblResultado.ForeColor = Color.Green;
                        lblResultado.Visible = true;
                        InicializarControles();
                    }
                    else
                    {
                        lblResultado.Text = "El vehiculo no pudo ser eliminado.";
                        lblResultado.ForeColor = Color.Maroon;
                        lblResultado.Visible = true;
                    }
                }
                else
                {
                    lblResultado.Text = "Debe ingresar el codigo del vehiculo.";
                    lblResultado.ForeColor = Color.Maroon;
                    lblResultado.Visible = true;
                }
            }
            catch (Exception er)
            {
                lblResultado.Text = "Hubo un error al eliminar el vehiculo. Detalle: " + er.Message;
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
            }
        }
    }
}