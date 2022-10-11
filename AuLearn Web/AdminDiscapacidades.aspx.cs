using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AuLearn_Web
{
    public partial class AdminDiscapacidades : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnCancelar.Visible = false;
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Conexion con = new Conexion();

            if (btnAgregar.Text == "Editar")
            {
                string mensaje = con.modificar_DiscaSP(Convert.ToInt32(labelID.Text), txtNombre.Text, txtDesc.Text);
                Response.Write("<script>window.alert('" + mensaje + "');</script>");
                Response.Redirect(Request.RawUrl);
            }
            else 
            {
                string mensaje = con.ingresar_DiscaSP(txtNombre.Text, txtDesc.Text);
                Response.Write("<script>window.alert('" + mensaje + "');</script>");
                Response.Redirect(Request.RawUrl);
            
            }
            
        }

        protected void GridDisca_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                labelTitulo.Text = "Editando Alumno";
                btnAgregar.Text = "Editar";
                btnCancelar.Visible = true;

                int index = Convert.ToInt32(e.CommandArgument);

                // Get the last name of the selected author from the appropriate
                // cell in the GridView control.
                GridViewRow selectedRow = GridDisca.Rows[index];
                TableCell id = selectedRow.Cells[0];
                TableCell nombreDisca = selectedRow.Cells[1];
                TableCell descripcion = selectedRow.Cells[2];

                labelID.Text = id.Text;
                txtNombre.Text = HttpUtility.HtmlDecode(nombreDisca.Text); 
                txtDesc.Text = HttpUtility.HtmlDecode(descripcion.Text);

                 

            }

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }


    }
}