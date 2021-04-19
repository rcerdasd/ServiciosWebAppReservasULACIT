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
    public partial class frmHabitacion : System.Web.UI.Page
    {
        IEnumerable<Models.Habitacion> habitaciones = new ObservableCollection<Models.Habitacion>();
        HabitacionManager habitacionManager = new HabitacionManager();

        async private void InicializarControles()
        {
            try
            {
                habitaciones = await habitacionManager.ObtenerHabitaciones(Session["TokenUsuario"].ToString());
                gvHabitaciones.DataSource = habitaciones.ToList();
                gvHabitaciones.DataBind();
            }
            catch (Exception e)
            {

                lblResultado.Text = "Hubo un error. Detalle: " + e.Message;
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            InicializarControles();
        }

        private bool ValidarInsertar()
        {
            if (string.IsNullOrEmpty(txtCodigoHotel.Text))
            {
                lblResultado.Text = "Debe ingresar el codigo del hotel";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtNumHab.Text))
            {
                lblResultado.Text = "Debe ingresar el numero de la habitacion";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtCapacidad.Text))
            {
                lblResultado.Text = "Debe ingresar la capacidad de la habitacion";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtTipo.Text))
            {
                lblResultado.Text = "Debe ingresar el tipo de la habitacion";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtDescrip.Text))
            {
                lblResultado.Text = "Debe ingresar la descripcion de la habitacion";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtPrecio.Text))
            {
                lblResultado.Text = "Debe ingresar el precio de la habitacion";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            return true;
        }


        protected void gvHabitaciones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "Codigo";
                e.Row.Cells[1].Text = "Codigo hotel";
                e.Row.Cells[2].Text = "Numero";
                e.Row.Cells[3].Text = "Capacidad";
                e.Row.Cells[4].Text = "Tipo";
                e.Row.Cells[5].Text = "Descripcion";
                e.Row.Cells[6].Text = "Estado";
                e.Row.Cells[7].Text = "Precio";
            }
        }


        async protected void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarInsertar())
                {
                    Models.Habitacion habitacionIngresada = new Models.Habitacion();
                    Models.Habitacion habitacion = new Models.Habitacion()
                    {
                        HOT_CODIGO = Convert.ToInt32(txtCodigoHotel.Text),
                        HAB_NUMERO = Convert.ToInt32(txtNumHab.Text),
                        HAB_CAPACIDAD = Convert.ToInt32(txtCapacidad.Text),
                        HAB_TIPO = txtTipo.Text,
                        HAB_DESCRIPCION = txtDescrip.Text,
                        HAB_PRECIO = Convert.ToDecimal(txtPrecio.Text),
                        HAB_ESTADO = "a"
                    };

                    habitacionIngresada = await habitacionManager.Ingresar(habitacion, Session["TokenUsuario"].ToString());

                    if (habitacionIngresada != null)
                    {
                        lblResultado.Text = "Habitacion ingresada correctamente";
                        lblResultado.ForeColor = Color.Green;
                        lblResultado.Visible = true;
                        InicializarControles();
                    }
                    else
                    {
                        lblResultado.Text = "Error al crear la habitacion";
                        lblResultado.ForeColor = Color.Maroon;
                        lblResultado.Visible = true;
                    }
                }
            }
            catch (Exception err)
            {

                lblResultado.Text = "Hubo un error al ingresar. Detalle: " + err.Message;
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
            }
        }

        async protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (ValidarInsertar() && (!string.IsNullOrEmpty(txtCodigoHabitacion.Text)))
            {
                try
                {
                    if (ValidarInsertar())
                    {
                        Models.Habitacion habitacionModificada = new Models.Habitacion();
                        Models.Habitacion habitacion = new Models.Habitacion()
                        {
                            HAB_CODIGO = Convert.ToInt32(txtCodigoHabitacion.Text),
                            HOT_CODIGO = Convert.ToInt32(txtCodigoHotel.Text),
                            HAB_NUMERO = Convert.ToInt32(txtNumHab.Text),
                            HAB_CAPACIDAD = Convert.ToInt32(txtCapacidad.Text),
                            HAB_TIPO = txtTipo.Text,
                            HAB_DESCRIPCION = txtDescrip.Text,
                            HAB_ESTADO = "a",
                            HAB_PRECIO = Convert.ToDecimal(txtPrecio.Text)
                        };

                        
                        habitacionModificada = await habitacionManager.Actualizar(habitacion, Session["TokenUsuario"].ToString());

                        if (habitacionModificada != null)
                        {
                            lblResultado.Text = "Habitacion modificada correctamente";
                            lblResultado.ForeColor = Color.Green;
                            lblResultado.Visible = true;
                            InicializarControles();
                        }
                        else
                        {
                            lblResultado.Text = "Error al modificar habitacion";
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
                if (!string.IsNullOrEmpty(txtCodigoHabitacion.Text))
                {
                    string codigoEliminado = string.Empty;
                    codigoEliminado = await habitacionManager.Eliminar(txtCodigoHabitacion.Text, Session["TokenUsuario"].ToString());
                    if (!string.IsNullOrEmpty(codigoEliminado))
                    {
                        lblResultado.Text = "Habitacion eliminada con exito";
                        lblResultado.ForeColor = Color.Green;
                        lblResultado.Visible = true;
                        InicializarControles();
                    }
                    else
                    {
                        lblResultado.Text = "Error al elminar la habitacion";
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

                lblResultado.Text = "Hubo un error al eliminar la habitacion. Detalle: " + error.Message;
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
            }

        }
    }
}
