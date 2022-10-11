using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AuLearn_Web
{
    public partial class AsignarCursos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
 

        }

        protected void btnAsignar_Click(object sender, EventArgs e)
        {
            if (DropDownAlumnos.SelectedValue == "" || DropDownCurso.SelectedValue == "")
            {
                Response.Write("<script>window.alert('No hay alumnos a los cuales asignar un curso');</script>");
            }
            else
            {
                int id_curso = Convert.ToInt32(DropDownCurso.SelectedValue);
                int id_estudiante = Convert.ToInt32(DropDownAlumnos.SelectedValue);

                Conexion con = new Conexion();

                con.asignar_cursoSP(id_curso, id_estudiante);

                Response.Write("<script>window.alert('Alumno Asignado con éxito.');</script>");
                Response.Redirect(Request.RawUrl);
            }
            
               
           

          
        }

        protected void GridViewListado_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Editar")
            {
                //HACEEEER
                //labelTitulo.Text = "Editando Alumno";
                //btnAgregar.Text = "Editar";
                //btnCancelar.Visible = true;

                //txtRut.Visible = false;
                //txtRutOculto.Visible = true;

                ////txtRut.Attributes.Add("class", "form-control col-xs-3");
                //// Convert the row index stored in the CommandArgument
                //// property to an Integer.
                //int index = Convert.ToInt32(e.CommandArgument);

                //// Get the last name of the selected author from the appropriate
                //// cell in the GridView control.
                //GridViewRow selectedRow = GridViewAlumnos.Rows[index];
                //TableCell rutAlumno = selectedRow.Cells[0];
                //TableCell Nombres = selectedRow.Cells[1];
                //TableCell Apellidos = selectedRow.Cells[2];
                //TableCell FechaNAC = selectedRow.Cells[3];

                //txtRut.Text = rutAlumno.Text;
                //txtRutOculto.Text = rutAlumno.Text;
                //txtNombre.Text = Nombres.Text;
                //txtApellido.Text = Apellidos.Text;


                ////DateTime fn = DateTime.Parse(FechaNAC.Text);
                //string fn = FechaNAC.Text;
                //string fn2 = fn.Replace(@"/", "-");
                ////no funciona!
                //this.txtcalendario.Text = DateTime.Now.ToString("yyyy-MM-dd");
                ////string fn2 = "2016-10-10";
                //txtcalendario.Text = fn2;
            }


        }
    }
}