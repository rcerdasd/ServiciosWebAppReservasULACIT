using System;
using System.Collections.Generic;
using System.Linq;
using AppReservasULACIT.Controllers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppReservasULACIT.Models;

namespace AppReservasULACIT
{
    public partial class frmTiqueteAvion : System.Web.UI.Page
    {
        TiqueteAvionManager tiqueteAvionManager = new TiqueteAvionManager();
        IEnumerable<Tiquete_Avion> tiquete_Avions = new ObservableCollection<Tiquete_Avion>();

        IEnumerable<ReservaAvion> reservaAvions = new ObservableCollection<ReservaAvion>();
        ReservaAvionManager reservaAvionManager = new ReservaAvionManager();


        async private void InicializarControles()
        {
            try
            {
                tiquete_Avions = await tiqueteAvionManager.ObtenerTiquetesAvion(Session["TokenUsuario"].ToString());
                gvTiqueteAvion.DataSource = tiquete_Avions.ToList();
                gvTiqueteAvion.DataBind();

                reservaAvions = await reservaAvionManager.ObtenerReservasAvion(Session["TokenUsuario"].ToString());
                ddlReservaAvionCodigo.DataSource = reservaAvions.ToList();
                ddlReservaAvionCodigo.DataTextField = "RES_AVI_ESCALA";
                ddlReservaAvionCodigo.DataValueField = "RES_AVI_CODIGO";
                ddlReservaAvionCodigo.DataBind();
            }
            catch (Exception e)
            {

                lblResultado.Text = "Hubo un error al enlistar los tiquetes de avion. Detalle: " + e.Message;
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
            }

        }

        private bool ValidarInsertar()
        {
            if (ddlReservaAvionCodigo.SelectedValue.Equals(null))
            {
                lblResultado.Text = "Debe seleccionar la reserva de avión";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtTiqueteAvionOrigen.Text))
            {
                lblResultado.Text = "Debe ingresar el origen del tiquete de avión";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtTiqueteAvionDestino.Text))
            {
                lblResultado.Text = "Debe ingresar el destino del tiquete de avión ";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtTiqueteAvionPrecio.Text))
            {
                lblResultado.Text = "Debe ingresar el precio del tiquete de avión ";
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

        protected void gvTiqueteAvion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "Codigo";
                e.Row.Cells[1].Text = "Codigo de Reserva de Avion";
                e.Row.Cells[2].Text = "Origen del tiquete";
                e.Row.Cells[3].Text = "Destino del tiquete";
                e.Row.Cells[4].Text = "Precio del tiquete";
            }
        }

        async protected void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarInsertar())
                {
                    Tiquete_Avion tiqueteIngresado = new Tiquete_Avion();
                    Tiquete_Avion tiquete_Avion = new Tiquete_Avion()
                    {
                        RES_AVI_CODIGO = Convert.ToInt32(ddlReservaAvionCodigo.SelectedValue),
                        TIQ_AVI_ORIGEN = txtTiqueteAvionOrigen.Text,
                        TIQ_AVI_DESTINO=txtTiqueteAvionDestino.Text,
                        TIQ_AVI_PRECIO=Convert.ToDecimal(txtTiqueteAvionPrecio.Text.Trim())

                    };
                    tiqueteIngresado = await tiqueteAvionManager.Ingresar(tiquete_Avion, Session["TokenUsuario"].ToString());

                    if (tiqueteIngresado != null)
                    {
                        lblResultado.Text = "El Tiquete de avión fue creado con exito.";
                        lblResultado.ForeColor = Color.Green;
                        lblResultado.Visible = true;
                        InicializarControles();
                    }
                    else
                    {
                        lblResultado.Text = "El tiquete de avion no se pudo crear.";
                        lblResultado.BackColor = Color.Maroon;
                        lblResultado.Visible = true;
                    }
                }

            }
            catch (Exception ex)
            {
                lblResultado.Text = "El tiquete de avion no se pudo crear. Detalle " + ex.Message;
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
            }
        }

        async protected void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarInsertar() && !string.IsNullOrEmpty(txtTiqAvionCodigo.Text))
                {
                    Tiquete_Avion tiqueteModificado = new Tiquete_Avion();
                    Tiquete_Avion tiquete_Avion = new Tiquete_Avion()
                    {
                        TIQ_AVI_CODIGO = Convert.ToInt32(txtTiqAvionCodigo.Text.Trim()),
                        RES_AVI_CODIGO = Convert.ToInt32(ddlReservaAvionCodigo.SelectedValue),
                        TIQ_AVI_ORIGEN = txtTiqueteAvionOrigen.Text,
                        TIQ_AVI_DESTINO = txtTiqueteAvionDestino.Text,
                        TIQ_AVI_PRECIO=Convert.ToDecimal(txtTiqueteAvionPrecio.Text)

                    };

                    tiqueteModificado = await tiqueteAvionManager.Actualizar(tiquete_Avion, Session["TokenUsuario"].ToString());

                    if (tiqueteModificado != null)
                    {
                        lblResultado.Text = "El tiquete de avion se ha modificado con exito";
                        lblResultado.ForeColor = Color.Green;
                        lblResultado.Visible = true;
                        InicializarControles();
                    }
                    else
                    {
                        lblResultado.Text = "El tiquete de avion no se pudo modificar";
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
                    lblResultado.Text = "Hubo un error al modificar el tiquete de avion. Detalle: " + exc.Message;
                    lblResultado.ForeColor = Color.Maroon;
                    lblResultado.Visible = true;
                }
            }
        }

        async protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtTiqAvionCodigo.Text))
                {
                    string codigoEliminado = null;
                    codigoEliminado = await tiqueteAvionManager.Eliminar(txtTiqAvionCodigo.Text.Trim(), Session["TokenUsuario"].ToString());
                    if (codigoEliminado != null)
                    {
                        lblResultado.Text = "El tiquete de avion con el código " + codigoEliminado + " fue eliminado con exito";
                        lblResultado.ForeColor = Color.Green;
                        lblResultado.Visible = true;
                        InicializarControles();
                    }
                    else
                    {
                        lblResultado.Text = "El tiquete de avión no pudo ser eliminado.";
                        lblResultado.ForeColor = Color.Maroon;
                        lblResultado.Visible = true;
                    }
                }
                else
                {
                    lblResultado.Text = "Debe ingresar el codigo del tiquete de avión.";
                    lblResultado.ForeColor = Color.Maroon;
                    lblResultado.Visible = true;
                }
            }
            catch (Exception er)
            {
                lblResultado.Text = "Hubo un error al eliminar el tiquete de avión . Detalle: " + er.Message;
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
            }
        }

    }
}