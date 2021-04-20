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
    public partial class frmReservaVehiculo : System.Web.UI.Page
    {
        IEnumerable<Models.ReservaVehiculo> reservasVehiculos = new ObservableCollection<Models.ReservaVehiculo>();
        ReservaVehiculoManager reservaVehiculoManager = new ReservaVehiculoManager();

        async private void InicializarControles()
        {
            try
            {
                reservasVehiculos = await reservaVehiculoManager.ObtenerReservasVehiculos(Session["TokenUsuario"].ToString());
                gvReservaVehiculos.DataSource = reservasVehiculos.ToList();
                gvReservaVehiculos.DataBind();
            }
            catch (Exception e)
            {

                lblResultado.Text = "Hubo un error al enlistar las reservas de los vehiculos. Detalle: " + e.Message;
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            InicializarControles();
        }

        private bool validadFechas()
        {
            try
            {
                if (calFecSalida.SelectedDate != null && calFechaRegreso.SelectedDate != null)
                {
                    if (calFechaRegreso.SelectedDate > calFecSalida.SelectedDate)
                    {
                        return true;
                    }
                    else
                    {
                        lblResultado.Text = "La fecha de Regreso es anterior a la fecha de Salida";
                        lblResultado.ForeColor = Color.Maroon;
                        lblResultado.Visible = true;
                        return false;
                    }
                }
                else
                {
                    lblResultado.Text = "No selecciono alguna fecha";
                    lblResultado.ForeColor = Color.Maroon;
                    lblResultado.Visible = true;
                    return false;
                }
            }
            catch (Exception ex)
            {
                lblResultado.Text = "Ocurrio un error para validar las fechas. " + ex.Message;
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }
        }

        protected void gvReservaVehiculos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "Codigo reserva vehiculo";
                e.Row.Cells[1].Text = "Codigo usuario";
                e.Row.Cells[2].Text = "Codigo paquete vehiculo";
                e.Row.Cells[3].Text = "Fecha salida";
                e.Row.Cells[4].Text = "Fecha regreso";
            }
        }

        private bool ValidarInsertar()
        {

            if (string.IsNullOrEmpty(txtCodUsuario.Text))
            {
                lblResultado.Text = "Debe ingresar el codigo del usuario";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtCodPaquete.Text))
            {
                lblResultado.Text = "Debe ingresar el codigo del paquete";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(calFecSalida.SelectedDate.ToString()))
            {
                lblResultado.Text = "Debe seleccionar la fecha de salida";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(calFechaRegreso.SelectedDate.ToString()))
            {
                lblResultado.Text = "Debe seleccionar la fecha de regreso";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            return true;
        }

        protected void btnCalFecSalida_Click(object sender, EventArgs e)
        {
            calFecSalida.Visible = true;
        }

        protected void calFecSalida_SelectionChanged(object sender, EventArgs e)
        {
            calFecSalida.Visible = false;
            txtFechaSalida.Text = calFecSalida.SelectedDate.ToShortDateString();
        }

        protected void btnCalFecRegreso_Click(object sender, EventArgs e)
        {
            calFechaRegreso.Visible = true;
        }

        protected void CalFecRegreso_SelectionChanged(object sender, EventArgs e)
        {
            calFechaRegreso.Visible = false;
            txtFechaRegreso.Text = calFechaRegreso.SelectedDate.ToShortDateString();
        }

        async protected void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarInsertar())
                {
                    if (validadFechas())
                    {
                        Models.ReservaVehiculo reservaVehiculoIngresada = new Models.ReservaVehiculo();
                        Models.ReservaVehiculo reservaVehiculo = new Models.ReservaVehiculo()
                        {
                            USU_CODIGO = Convert.ToInt32(txtCodUsuario.Text),
                            PAQ_VEH_CODIGO = Convert.ToInt32(txtCodPaquete.Text),
                            RES_VEH_FEC_SALIDA = calFecSalida.SelectedDate,
                            RES_VEH_FEC_REGRESO = calFechaRegreso.SelectedDate
                        };

                        reservaVehiculoIngresada = await reservaVehiculoManager.Ingresar(reservaVehiculo, Session["TokenUsuario"].ToString());

                        if (reservaVehiculoIngresada != null)
                        {
                            lblResultado.Text = "Reserva de vehiculo ingresada correctamente";
                            lblResultado.ForeColor = Color.Green;
                            lblResultado.Visible = true;
                            InicializarControles();
                        }
                        else
                        {
                            lblResultado.Text = "Error al crear la reserva de vehiculo";
                            lblResultado.ForeColor = Color.Maroon;
                            lblResultado.Visible = true;
                        }
                    }
                }
            }
            catch (Exception err)
            {

                lblResultado.Text = "Hubo un error al ingresar la reserva de vehiculo. Detalle: " + err.Message;
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
            }
        }

        async protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (ValidarInsertar() && (!string.IsNullOrEmpty(txtResVehCodigo.Text)))
            {
                try
                {
                    if (ValidarInsertar())
                    {
                        Models.ReservaVehiculo reservaVehiculoIngresada = new Models.ReservaVehiculo();
                        Models.ReservaVehiculo reservaVehiculo = new Models.ReservaVehiculo()
                        {
                            RES_VEH_CODIGO = Convert.ToInt32(txtResVehCodigo.Text),
                            USU_CODIGO = Convert.ToInt32(txtCodUsuario.Text),
                            PAQ_VEH_CODIGO = Convert.ToInt32(txtCodPaquete.Text),
                            RES_VEH_FEC_SALIDA = calFecSalida.SelectedDate,
                            RES_VEH_FEC_REGRESO = calFechaRegreso.SelectedDate
                        };

                        reservaVehiculoIngresada = await reservaVehiculoManager.Actualizar(reservaVehiculo, Session["TokenUsuario"].ToString());

                        if (reservaVehiculoIngresada != null)
                        {
                            lblResultado.Text = "Reserva de vehiculo modificada correctamente";
                            lblResultado.ForeColor = Color.Green;
                            lblResultado.Visible = true;
                            InicializarControles();
                        }
                        else
                        {
                            lblResultado.Text = "Error al modificar reserva de vehiculo";
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
                if (!string.IsNullOrEmpty(txtResVehCodigo.Text))
                {
                    string codigoEliminado = string.Empty;
                    codigoEliminado = await reservaVehiculoManager.Eliminar(txtResVehCodigo.Text, Session["TokenUsuario"].ToString());
                    if (!string.IsNullOrEmpty(codigoEliminado))
                    {
                        lblResultado.Text = "Reserva de vehiculo eliminada con exito";
                        lblResultado.ForeColor = Color.Green;
                        lblResultado.Visible = true;
                        InicializarControles();
                    }
                    else
                    {
                        lblResultado.Text = "Error al elminar la reserva de vehiculo";
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

                lblResultado.Text = "Hubo un error al eliminar la reserva de vehiculo. Detalle: " + error.Message;
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
            }
        }
    }
}