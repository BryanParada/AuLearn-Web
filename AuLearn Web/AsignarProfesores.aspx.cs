using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AuLearn_Web
{
    public partial class AsignarProfesores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           //if (!IsPostBack)
           // {
           // if (DropDownProfes.Items.Count == 0)
           //     {

           //         DivBodyP.Visible = false;
           //         DivAdvertencia.Visible = true;

           //     }
           // }

            //DropDownProfes.Items.Count == 0)

            
        }


        protected void btnAsignar_Click(object sender, EventArgs e)
        {
            if (DropDownProfes.SelectedValue == "" || DropDownCurso.SelectedValue == "")
            {
                Response.Write("<script>window.alert('No hay profesores y/o Cursos a los cuáles asignar un curso');</script>");
            }
            else
            {
                int id_curso = Convert.ToInt32(DropDownCurso.SelectedValue);
                int id_usuario = Convert.ToInt32(DropDownProfes.SelectedValue);

                Conexion con = new Conexion();

                con.asignar_curso_ProfesorSP(id_curso, id_usuario);

                Response.Write("<script>window.alert('Profesor Asignado con éxito.');</script>");
                Response.Redirect(Request.RawUrl);
            }


        }

        protected void GridViewListado_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Desvincular")
            {

                int index = Convert.ToInt32(e.CommandArgument);

                GridViewRow selectedRow = GridViewListado.Rows[index];
                TableCell id_asignar_curso = selectedRow.Cells[0];


                string id_asignar_curso_c = id_asignar_curso.Text;


                Conexion con = new Conexion();


                con.desvincular_curso(id_asignar_curso_c);
                Response.Redirect(Request.RawUrl);
            }
        }
    }
}