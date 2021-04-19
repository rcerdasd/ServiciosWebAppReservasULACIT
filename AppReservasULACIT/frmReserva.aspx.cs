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
    public partial class frmReserva : System.Web.UI.Page
    {
        IEnumerable<Models.Reserva> reservas = new ObservableCollection<Models.Reserva>();
        ReservaManager reservaManager = new ReservaManager();

        async private void InicializarControles()
        {
            try
            {
                reservas = await reservaManager.ObtenerReservas(Session["TokenUsuario"].ToString());
                gvReservas.DataSource = reservas.ToList();
                gvReservas.DataBind();
            }
            catch (Exception e)
            {

                lblResultado.Text = "Hubo un error al enlistar las reservas. Detalle: " + e.Message;
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
                if (calFecIng.SelectedDate!=null && calFechaSalida.SelectedDate != null)
                {
                    if(calFechaSalida.SelectedDate > calFecIng.SelectedDate)
                    {
                        return true;
                    }
                    else
                    {
                        lblResultado.Text = "La fecha de salida es anterior a la fecha de ingreso";
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
                lblResultado.Text = "Ocurrio un error para validar las fechas. "+ex.Message;
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }
        }

        protected void gvReservas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "Codigo reserva";
                e.Row.Cells[1].Text = "Codigo usuario";
                e.Row.Cells[2].Text = "Codigo habitacion";
                e.Row.Cells[3].Text = "Fecha ingreso";
                e.Row.Cells[4].Text = "Fecha salida";
            }
        }

        private bool ValidarInsertar()
        {

            if (string.IsNullOrEmpty(txtCodHab.Text))
            {
                lblResultado.Text = "Debe ingresar el codigo de la habitacion";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtCodigoUsuario.Text))
            {
                lblResultado.Text = "Debe ingresar el codigo del usuario";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(calFechaSalida.SelectedDate.ToString()))
            {
                lblResultado.Text = "Debe seleccionar la fecha de ingreso";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(calFechaSalida.SelectedDate.ToString()))
            {
                lblResultado.Text = "Debe seleccionar la fecha de salida";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            return true;
        }

        protected void btnMostrarCalendarioIngreso_Click(object sender, EventArgs e)
        {
            calFecIng.Visible = true;
        }

        protected void calFecIng_SelectionChanged(object sender, EventArgs e)
        {
            calFecIng.Visible = false;
            txtFechaIngreso.Text = calFecIng.SelectedDate.ToShortDateString();
        }

        protected void btnCalFechaSalida_Click(object sender, EventArgs e)
        {
            calFechaSalida.Visible = true;
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            calFechaSalida.Visible = false;
            txtFechaSalida.Text = calFechaSalida.SelectedDate.ToShortDateString();
        }

        async protected void btnIngresar_Click(object sender, EventArgs e)
        {

            try
            {
                if (ValidarInsertar())
                {
                    if (validadFechas())
                    {
                        Models.Reserva reservaIngresada = new Models.Reserva();
                        Models.Reserva reserva = new Models.Reserva()
                        {
                            USU_CODIGO = Convert.ToInt32(txtCodigoUsuario.Text),
                            HAB_CODIGO = Convert.ToInt32(txtCodHab.Text),
                            RES_FECHA_INGRESO = calFecIng.SelectedDate,
                            RES_FECHA_SALIDA = calFechaSalida.SelectedDate
                        };

                        reservaIngresada = await reservaManager.Ingresar(reserva, Session["TokenUsuario"].ToString());

                        if (reservaIngresada != null)
                        {
                            lblResultado.Text = "Reserva ingresada correctamente";
                            lblResultado.ForeColor = Color.Green;
                            lblResultado.Visible = true;
                            InicializarControles();
                        }
                        else
                        {
                            lblResultado.Text = "Error al crear la reserva";
                            lblResultado.ForeColor = Color.Maroon;
                            lblResultado.Visible = true;
                        }
                    }
                }
            }
            catch (Exception err)
            {

                lblResultado.Text = "Hubo un error al ingresar la reserva. Detalle: " + err.Message;
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
            }
        }

        async protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (ValidarInsertar() && (!string.IsNullOrEmpty(txtCodHab.Text)))
            {
                try
                {
                    if (ValidarInsertar())
                    {
                        Models.Reserva reservaIngresada = new Models.Reserva();
                        Models.Reserva reserva = new Models.Reserva()
                        {
                            RES_CODIGO = Convert.ToInt32(txtCodigo.Text),
                            USU_CODIGO = Convert.ToInt32(txtCodigoUsuario.Text),
                            HAB_CODIGO = Convert.ToInt32(txtCodHab.Text),
                            RES_FECHA_INGRESO = calFecIng.SelectedDate,
                            RES_FECHA_SALIDA = calFechaSalida.SelectedDate
                        };

                        reservaIngresada = await reservaManager.Actualizar(reserva, Session["TokenUsuario"].ToString());

                        if (reservaIngresada != null)
                        {
                            lblResultado.Text = "Reserva modificada correctamente";
                            lblResultado.ForeColor = Color.Green;
                            lblResultado.Visible = true;
                            InicializarControles();
                        }
                        else
                        {
                            lblResultado.Text = "Error al modificar reserva";
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
                if (!string.IsNullOrEmpty(txtCodigo.Text))
                {
                    string codigoEliminado = string.Empty;
                    codigoEliminado = await reservaManager.Eliminar(txtCodigo.Text, Session["TokenUsuario"].ToString());
                    if (!string.IsNullOrEmpty(codigoEliminado))
                    {
                        lblResultado.Text = "Reserva eliminada con exito";
                        lblResultado.ForeColor = Color.Green;
                        lblResultado.Visible = true;
                        InicializarControles();
                    }
                    else
                    {
                        lblResultado.Text = "Error al elminar la reserva";
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

                lblResultado.Text = "Hubo un error al eliminar la reserva. Detalle: " + error.Message;
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
            }

        }
    }
}