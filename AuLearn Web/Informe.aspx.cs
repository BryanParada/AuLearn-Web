using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AuLearn_Web
{
    public partial class Informe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string nombreAlumno = DropDownListNombre.SelectedValue;
            ButtonPDF.Visible = false;

           
        }

        protected void Buscar_Click(object sender, EventArgs e)
        {
            
            ButtonPDF.Visible = true;

            /*GridViewRow selectedRow = GridViewDatos.Rows[0];

            TableCell nombreAlumnoDG = selectedRow.Cells[0];
            labelNombre.Text = "Nombre Alumno: " + nombreAlumnoDG.Text;

            TableCell edad = selectedRow.Cells[1];
            labelEdad.Text = "Edad: " + edad.Text;

            TableCell td = selectedRow.Cells[2];
            labelTD.Text = "Tipo Discapacidad: " + td.Text;

            TableCell gd = selectedRow.Cells[3];
            labelGD.Text = "Grado Discapacidad: " + gd.Text;*/
            string rut =DropDownListNombre.SelectedValue;
            SqlConnection cn = new SqlConnection("Data Source=172.16.115.244; Initial Catalog=aulearn; User ID=aulearn; password=S0p0rt3.,");
            SqlCommand cmd=new SqlCommand("select(cast(datediff(dd, P.fecha_nacimiento ,GETDATE()) / 365.25 as int)) as 'Edad', P.nombre + ' ' + P.apellido AS 'Nombre Alumno',TD.tipo_discapacidad as 'Tipo Discapacidad',GD.grado_discapacidad as 'Grado de Discapacidad' from Persona as P inner join Estudiante as E on  E.rut_persona=P.rut_persona inner join Tipo_discapacidad as TD on E.id_tipo_discapacidad=TD.id_tipo_discapacidad inner join Integrantes_curso as IC on IC.id_estudiante=E.id_estudiante inner join Curso as C on C.id_curso=IC.id_curso inner join Grado_discapacidad as GD on GD.id_grado_discapacidad=C.id_grado_discapacidad where E.rut_persona='"+rut+"'", cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if(dr.Read())
            {        
                //Console.WriteLine(Convert.ToString(dr[0));
                labelNombre.Text = "Nombre Alumno: " + Convert.ToString(dr[1]);
                labelEdad.Text = "Edad: " + Convert.ToString(dr[0]);
                labelTD.Text = "Tipo Discapacidad: " + Convert.ToString(dr[2]);
                labelGD.Text = "Grado Discapacidad: " + Convert.ToString(dr[3]);
            }
            
 
        }

        
       
       

     

       
       

         
    }
}