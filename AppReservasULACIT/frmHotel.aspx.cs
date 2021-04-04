using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppReservasULACIT.Models;
using AppReservasULACIT.Controllers;
using System.Collections.ObjectModel;
using System.Drawing;

namespace AppReservasULACIT
{
    public partial class frmHotel : System.Web.UI.Page
    {
        IEnumerable<Models.Hotel> hoteles = new ObservableCollection<Models.Hotel>();
        HotelManager hotelManager = new HotelManager();

        async protected void Page_Load(object sender, EventArgs e)
        {
            InicializarControles();
        }

        async protected void btnIngresar_Click(object sender, EventArgs e)
        {
            if(ValidarInsertar())
            {
                Models.Hotel hotelIngresado = new Models.Hotel();
                Models.Hotel hotel = new Models.Hotel()
                {
                    HOT_NOMBRE = txtNombre.Text,
                    HOT_EMAIL = txtEmail.Text,
                    HOT_DIRECCION = txtDireccion.Text,
                    HOT_TELEFONO = txtTelefono.Text,
                    HOT_CATEGORIA = ddlCategoria.SelectedValue
                };

                hotelIngresado = await hotelManager.Ingresar(hotel, Session["TokenUsuario"].ToString());

                if(hotelIngresado != null)
                {
                    lblResultado.Text = "Hotel ingresado correctamente";
                    lblResultado.ForeColor = Color.Green;
                    lblResultado.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblResultado.Text = "Error al crear hotel";
                    lblResultado.ForeColor = Color.Maroon;
                    lblResultado.Visible = true;
                }
            }
        }

        async private void InicializarControles()
        {
            hoteles = await hotelManager.ObtenerHoteles(Session["TokenUsuario"].ToString());
            gvHoteles.DataSource = hoteles.ToList();
            gvHoteles.DataBind();

        }

        private bool ValidarInsertar()
        {
            if(string.IsNullOrEmpty(txtNombre.Text))
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
                lblResultado.Text = "Debe ingresar el telefono";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtDireccion.Text))
            {
                lblResultado.Text = "Debe ingresar la direccion";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            return true;
        }

        protected void gvHoteles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "Codigo";
                e.Row.Cells[1].Text = "Nombre";
                e.Row.Cells[2].Text = "Email";
                e.Row.Cells[3].Text = "Direccion";
                e.Row.Cells[4].Text = "Telefono";
                e.Row.Cells[5].Text = "Categoria";
            }
        }

        async protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (ValidarInsertar() && (!string.IsNullOrEmpty(txtCodigo.Text)))
            {
                if (ValidarInsertar())
                {
                    Models.Hotel hotelModificado = new Models.Hotel();
                    Models.Hotel hotel = new Models.Hotel()
                    {
                        HOT_CODIGO = Convert.ToInt32(txtCodigo.Text),
                        HOT_NOMBRE = txtNombre.Text,
                        HOT_EMAIL = txtEmail.Text,
                        HOT_DIRECCION = txtDireccion.Text,
                        HOT_TELEFONO = txtTelefono.Text,
                        HOT_CATEGORIA = ddlCategoria.SelectedValue
                    };

                    hotelModificado = await hotelManager.Actualizar(hotel, Session["TokenUsuario"].ToString());

                    if (hotelModificado != null)
                    {
                        lblResultado.Text = "Hotel modificado correctamente";
                        lblResultado.ForeColor = Color.Green;
                        lblResultado.Visible = true;
                        InicializarControles();
                    }
                    else
                    {
                        lblResultado.Text = "Error al modificar hotel";
                        lblResultado.ForeColor = Color.Maroon;
                        lblResultado.Visible = true;
                    }
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
            if (!string.IsNullOrEmpty(txtCodigo.Text))
            {
                string codigoEliminado = string.Empty;
                codigoEliminado = await hotelManager.Eliminar(txtCodigo.Text, Session["TokenUsuario"].ToString());
                if (!string.IsNullOrEmpty(codigoEliminado))
                {
                    lblResultado.Text = "Hotel eliminado con exito";
                    lblResultado.ForeColor = Color.Green;
                    lblResultado.Visible = true;
                }
            }
            else
            {
                lblResultado.Text = "Eror al elminar el hotel";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
            }

        }
    }
}