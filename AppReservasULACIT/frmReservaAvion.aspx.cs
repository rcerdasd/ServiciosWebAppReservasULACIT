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
    public partial class frmReservaAvion : System.Web.UI.Page
    {
        IEnumerable<Models.ReservaAvion> reservaAvions = new ObservableCollection<Models.ReservaAvion>();
        ReservaAvionManager reservaAvionManager = new ReservaAvionManager();

        IEnumerable<Models.Usuario> usuarios = new ObservableCollection<Models.Usuario>();
        UsuarioManager usuarioManager = new UsuarioManager();

        IEnumerable<Models.Avion> aviones = new ObservableCollection<Models.Avion>();
        AvionManager avionManager = new AvionManager();

        async private void InicializarControles()
        {
            try
            {
                usuarios = await usuarioManager.ObtenerUsuarios(Session["TokenUsuario"].ToString());
                ddlUsuCodigo.DataSource = usuarios.ToList();
                ddlUsuCodigo.DataTextField = "USU_NOMBRE";
                ddlUsuCodigo.DataValueField = "USU_CODIGO";
                ddlUsuCodigo.DataBind();

                aviones = await avionManager.ObtenerAviones(Session["TokenUsuario"].ToString());
                ddlAviCodigo.DataSource = aviones.ToList();
                ddlAviCodigo.DataTextField = "AVI_DESCRIPCION";
                ddlAviCodigo.DataValueField = "AVI_CODIGO";
                ddlAviCodigo.DataBind();

                reservaAvions = await reservaAvionManager.ObtenerReservasAvion(Session["TokenUsuario"].ToString());
                gvRAvion.DataSource = reservaAvions.ToList();
                gvRAvion.DataBind();
            }
            catch (Exception e)
            {

                lblResultado.Text = "No se pudo enlistar las reservas de avión. Detalle: " + e.Message;
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InicializarControles();
            }
        }

        private bool CheckFecha()
        {
            try
            {
                if (calFecVuelo == null)
                {



                    lblResultado.Text = "No selecciono alguna fecha";
                    lblResultado.ForeColor = Color.Maroon;
                    lblResultado.Visible = true;
                    return false;

                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                lblResultado.Text = "No se pudo validar la fecha . " + ex.Message;
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }
        }

        protected void gvRAvion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "Codigo reserva avion";
                e.Row.Cells[1].Text = "Codigo usuario";
                e.Row.Cells[2].Text = "Codigo avion";
                e.Row.Cells[3].Text = "Fecha del vuelo";
                e.Row.Cells[4].Text = "Duracion del vuelo";
                e.Row.Cells[5].Text = "Escala del vuelo";
            }
        }

        private bool ValidarInsertar()
        {

            if (string.IsNullOrEmpty(ddlUsuCodigo.SelectedValue))
            {
                lblResultado.Text = "Debe seleccionar el codigo del usuario";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(ddlAviCodigo.SelectedValue))
            {
                lblResultado.Text = "Debe seleccionar el codigo del avion";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(calFecVuelo.SelectedDate.ToString()))
            {
                lblResultado.Text = "Debe seleccionar la fecha del vuelo";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtDuracion.Text))
            {
                lblResultado.Text = "Debe ingresar la duracion del vuelo";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }
            if (string.IsNullOrEmpty(txtEscala.Text))
            {
                lblResultado.Text = "Debe ingresar la escala del vuelo";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            return true;
        }

        protected void btnCalFecSalida_Click(object sender, EventArgs e)
        {
            calFecVuelo.Visible = true;
        }

        protected void calFecSalida_SelectionChanged(object sender, EventArgs e)
        {
            calFecVuelo.Visible = false;
            txtFecVuelo.Text = calFecVuelo.SelectedDate.ToShortDateString();
        }


        async protected void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarInsertar())
                {
                    if (CheckFecha())
                    {
                        Models.ReservaAvion reservaAvionIngresada = new Models.ReservaAvion();
                        Models.ReservaAvion reservaAvion = new Models.ReservaAvion()
                        {
                            USU_CODIGO = Convert.ToInt32(ddlUsuCodigo.SelectedValue),
                            AVI_CODIGO = Convert.ToInt32(ddlAviCodigo.SelectedValue),
                            RES_AVI_FEC_VUELO= calFecVuelo.SelectedDate,
                           RES_AVI_DURACION=Convert.ToDecimal(txtDuracion.Text),
                           RES_AVI_ESCALA=txtEscala.Text
                        };

                        reservaAvionIngresada = await reservaAvionManager.Ingresar(reservaAvion, Session["TokenUsuario"].ToString());

                        if (reservaAvionIngresada != null)
                        {
                            lblResultado.Text = "Se ingreso la reserva de avion correctamente";
                            lblResultado.ForeColor = Color.Green;
                            lblResultado.Visible = true;
                            InicializarControles();
                        }
                        else
                        {
                            lblResultado.Text = "Error al crear la reserva de avion";
                            lblResultado.ForeColor = Color.Maroon;
                            lblResultado.Visible = true;
                        }
                    }
                }
            }
            catch (Exception err)
            {

                lblResultado.Text = "Hubo un error al ingresar la reserva de avion. Detalle: " + err.Message;
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
            }
        }

        async protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (ValidarInsertar() && (!string.IsNullOrEmpty(txtCodigoReserva.Text)))
            {
                try
                {
                    if (ValidarInsertar())
                    {
                        Models.ReservaAvion reservaAvionIngresada = new Models.ReservaAvion();
                        Models.ReservaAvion reservaAvion = new Models.ReservaAvion()
                        {
                            RES_AVI_CODIGO = Convert.ToInt32(txtCodigoReserva.Text),
                            USU_CODIGO = Convert.ToInt32(ddlUsuCodigo.SelectedValue),
                            AVI_CODIGO = Convert.ToInt32(ddlAviCodigo.SelectedValue),
                            RES_AVI_FEC_VUELO = calFecVuelo.SelectedDate,
                            RES_AVI_DURACION = Convert.ToDecimal(txtDuracion.Text),
                            RES_AVI_ESCALA=txtEscala.Text
                        };

                        reservaAvionIngresada = await reservaAvionManager.Actualizar(reservaAvion, Session["TokenUsuario"].ToString());

                        if (reservaAvionIngresada != null)
                        {
                            lblResultado.Text = "Se modifico correctamente la reserva de avion";
                            lblResultado.ForeColor = Color.Green;
                            lblResultado.Visible = true;
                            InicializarControles();
                        }
                        else
                        {
                            lblResultado.Text = "Error al modificar reserva de avion";
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
                if (!string.IsNullOrEmpty(txtCodigoReserva.Text))
                {
                    string codigoEliminado = string.Empty;
                    codigoEliminado = await reservaAvionManager.Eliminar(txtCodigoReserva.Text, Session["TokenUsuario"].ToString());
                    if (!string.IsNullOrEmpty(codigoEliminado))
                    {
                        lblResultado.Text = "Se elimino correctamente la reserva de avion";
                        lblResultado.ForeColor = Color.Green;
                        lblResultado.Visible = true;
                        InicializarControles();
                    }
                    else
                    {
                        lblResultado.Text = "Error al elminar la reserva de avion";
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

                lblResultado.Text = "Hubo un error al eliminar la reserva de avion. Detalle: " + error.Message;
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
            }
        }
    }
}