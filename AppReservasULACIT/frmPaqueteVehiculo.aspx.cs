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
    public partial class frmPaqueteVehiculo : System.Web.UI.Page
    {
        IEnumerable<Models.PaqueteVehiculo> paquetesVehiculos = new ObservableCollection<Models.PaqueteVehiculo>();
        PaqueteVehiculoManager paqueteVehiculoManager = new PaqueteVehiculoManager();

        async private void InicializarControles()
        {
            try
            {
                paquetesVehiculos = await paqueteVehiculoManager.ObtenerPaquetesVehiculos(Session["TokenUsuario"].ToString());
                gvPaquetesVehiculos.DataSource = paquetesVehiculos.ToList();
                gvPaquetesVehiculos.DataBind();
            }
            catch (Exception e)
            {

                lblResultado.Text = "Hubo un error al enlistar los paquetes de los vehiculos. Detalle: " + e.Message;
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            InicializarControles();
        }

        protected void gvPaquetesVehiculos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "Codigo paquete vehiculo";
                e.Row.Cells[1].Text = "Codigo vehiculo";
                e.Row.Cells[2].Text = "Paquete seguro";
                e.Row.Cells[3].Text = "Paquete bicicleta";
                e.Row.Cells[4].Text = "Paquete descripcion";
            }
        }

        private bool ValidarInsertar()
        {

            if (string.IsNullOrEmpty(txtCodVehiculo.Text))
            {
                lblResultado.Text = "Debe ingresar el codigo del vehiculo";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtPaqSeguro.Text))
            {
                lblResultado.Text = "Debe ingresar si el paquete cuenta con seguro o no";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtPaqBicicleta.Text))
            {
                lblResultado.Text = "Debe ingresar si paquete incluye bicicleta o no";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtPaqDescripcion.Text))
            {
                lblResultado.Text = "Debe ingresar una descripcion";
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
                    Models.PaqueteVehiculo paqueteVehiculoIngresado = new Models.PaqueteVehiculo();
                    Models.PaqueteVehiculo paqueteVehiculo = new Models.PaqueteVehiculo()
                    {
                        VEH_CODIGO = Convert.ToInt32(txtCodVehiculo.Text),
                        PAQ_SEGURO = txtPaqSeguro.Text,
                        PAQ_BICICLETA = txtPaqBicicleta.Text,
                        PAQ_DESCRIPCION = txtPaqDescripcion.Text
                    };

                    paqueteVehiculoIngresado = await paqueteVehiculoManager.Ingresar(paqueteVehiculo, Session["TokenUsuario"].ToString());

                    if (paqueteVehiculoIngresado != null)
                    {
                        lblResultado.Text = "Paquete de vehiculo ingresado correctamente";
                        lblResultado.ForeColor = Color.Green;
                        lblResultado.Visible = true;
                        InicializarControles();
                    }
                    else
                    {
                        lblResultado.Text = "Error al crear el paquete de vehiculo";
                        lblResultado.ForeColor = Color.Maroon;
                        lblResultado.Visible = true;
                    }
                }
            }
            catch (Exception err)
            {

                lblResultado.Text = "Hubo un error al ingresar el paquete de vehiculo. Detalle: " + err.Message;
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
            }
        }

        async protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (ValidarInsertar() && (!string.IsNullOrEmpty(txtPaqVehCodigo.Text)))
            {
                try
                {
                    if (ValidarInsertar())
                    {
                        Models.PaqueteVehiculo paqueteVehiculoIngresado = new Models.PaqueteVehiculo();
                        Models.PaqueteVehiculo paqueteVehiculo = new Models.PaqueteVehiculo()
                        {
                            PAQ_VEH_CODIGO = Convert.ToInt32(txtPaqVehCodigo.Text),
                            VEH_CODIGO = Convert.ToInt32(txtCodVehiculo.Text),
                            PAQ_SEGURO = txtPaqSeguro.Text,
                            PAQ_BICICLETA = txtPaqBicicleta.Text,
                            PAQ_DESCRIPCION = txtPaqDescripcion.Text
                        };

                        paqueteVehiculoIngresado = await paqueteVehiculoManager.Actualizar(paqueteVehiculo, Session["TokenUsuario"].ToString());

                        if (paqueteVehiculoIngresado != null)
                        {
                            lblResultado.Text = "Paquete de vehiculo modificado correctamente";
                            lblResultado.ForeColor = Color.Green;
                            lblResultado.Visible = true;
                            InicializarControles();
                        }
                        else
                        {
                            lblResultado.Text = "Error al modificar paquete de vehiculo";
                            lblResultado.ForeColor = Color.Maroon;
                            lblResultado.Visible = true;
                        }
                    }
                }
                catch (Exception er)
                {

                    lblResultado.Text = "Hubo un error. Detalle: " + er.Message;
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

        async protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtPaqVehCodigo.Text))
                {
                    string codigoEliminado = string.Empty;
                    codigoEliminado = await paqueteVehiculoManager.Eliminar(txtPaqVehCodigo.Text, Session["TokenUsuario"].ToString());
                    if (!string.IsNullOrEmpty(codigoEliminado))
                    {
                        lblResultado.Text = "Paquete de vehiculo eliminado con exito";
                        lblResultado.ForeColor = Color.Green;
                        lblResultado.Visible = true;
                        InicializarControles();
                    }
                    else
                    {
                        lblResultado.Text = "Error al elminar el paquete de vehiculo";
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
            catch (Exception error)
            {

                lblResultado.Text = "Hubo un error al eliminar el paquete de vehiculo. Detalle: " + error.Message;
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
            }
        }
    }
}