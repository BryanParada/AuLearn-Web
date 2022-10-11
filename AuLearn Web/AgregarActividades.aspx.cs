using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AuLearn_Web
{
    public partial class AgregarActividades : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.txtFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
            btnCancelar.Visible = false;


            string rutActual = (string)(Session["rutAct"]);
            labelRut.Text = rutActual;

            if ((Session["role"].ToString() == "SYSAdmin") || (Session["role"].ToString() == "Admin"))
            {

                string cons2 = @"select N.id_nota as id_nota, 
 C.nombre_curso as 'Curso',
 GD.grado_discapacidad as 'Grado Discapacidad',
 P.nombre + ' ' + P.apellido AS 'Nombre Alumno',
 N.nota as 'Nota', 
 N.observacion as 'Observación',
 A.descripcion as 'Descripción Actividad',
 TA.tipo_actividad as 'Tipo Actividad',
 M.materia as 'Materia',
 Uni.descripcion as 'Unidad'
  from Notas as N
 inner join Estudiante e on e.id_estudiante=N.id_estudiante
 inner join Integrantes_curso IC on IC.id_estudiante=e.id_estudiante
 inner join Curso c on c.id_curso=IC.id_curso
 inner join Persona p on p.rut_persona=e.rut_persona
 inner join Asignar_curso AC on AC.id_curso=c.id_curso
 inner join Usuario u on u.id_usuario=AC.id_usuario 
 INNER join Grado_discapacidad as GD on GD.id_grado_discapacidad=C.id_grado_discapacidad
 inner join Actividad as A on N.id_actividad=A.id_actividad
 inner join Tipo_actividad as TA on TA.id_tipo_actividad=A.id_tipo_actividad
 inner join Unidad as Uni on A.id_unidad=Uni.id_unidad
 inner join Materia as M on M.id_materia=Uni.id_materia
 where P.activo = 1";

                SqlDataSource2.SelectCommand = cons2;
                SqlDataSource2.Select(DataSourceSelectArguments.Empty);
                SqlDataSource2.DataBind();
                GridView.DataBind();

            }


            if (!IsPostBack)
            {



                if ((Session["role"].ToString() == "SYSAdmin") || (Session["role"].ToString() == "Admin"))
                {
                    string cons = @"select C.id_curso, C.nombre_curso from Curso as C
inner join Asignar_curso as A on C.id_curso=A.id_curso
inner join Usuario as U on U.id_usuario=A.id_usuario ";

                    SqlDataSourceC.SelectCommand = cons;
                    SqlDataSourceC.Select(DataSourceSelectArguments.Empty);
                    SqlDataSourceC.DataBind();
                    DropDownList1.DataBind();

                     

                }
            }

            
        }

        protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView GridView = e.Row.DataItem as DataRowView;
                if (Convert.ToInt32(GridView["Nota"]) < 40)
                {
                    e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;
                    // e.Row.BackColor = System.Drawing.Color.Red;
                }
                else
                {
                    e.Row.Cells[4].ForeColor = System.Drawing.Color.Green;
                    // e.Row.BackColor = System.Drawing.Color.Green;
                }
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            
            int curso = Convert.ToInt32(DropDownList1.SelectedValue);
            int materia = Convert.ToInt32(HttpUtility.HtmlDecode(DropDownList2.SelectedValue));
            int id_actividad = Convert.ToInt32(DropDownListActividad.SelectedValue);
            int id_alumno = Convert.ToInt32(DropDownListAlumno.SelectedValue);
            //int range = Convert.ToInt32(rangeCtrl.Value);

            Conexion con = new Conexion();

            if (btnAgregar.Text == "Editar")
            {
                 
                //ESTO NO ESTA HECHOOOOOO HACEEEEEEEEEEEEEEEEEEEEEEEEER

                //con.editar_alumnoP(txtRut.Text, txtNombre.Text, txtApellido.Text, txtcalendario.Text, cargo, tipoDisca);

                //Response.Write("<script>window.alert('El alumno se editó correctamente.');</script>"); //AGREEEEGAR PA EDICION!
                //Response.Redirect(Request.RawUrl);
            }
            else
            {
                
                    int nota = Convert.ToInt32(txtNota.Text);

                    string uidn = con.insertar_NotaP(id_actividad, id_alumno, nota, txtObservacion.Text, txtFecha.Text);
                    int ultimo_id_nota = Convert.ToInt32(uidn);

                    //INSERTAR NIVELES, HACER CON WHILE LUEGO
                        int id_tipo_nivel = 1;
                        int puntuacion = Convert.ToInt32(N1.Text);
                        con.insertar_NivelNotaSP(ultimo_id_nota, id_tipo_nivel, puntuacion);

                        id_tipo_nivel = 2;
                        puntuacion = Convert.ToInt32(N2.Text);
                        con.insertar_NivelNotaSP(ultimo_id_nota, id_tipo_nivel, puntuacion);

                        id_tipo_nivel = 3;
                        puntuacion = Convert.ToInt32(N3.Text);
                        con.insertar_NivelNotaSP(ultimo_id_nota, id_tipo_nivel, puntuacion);

                        id_tipo_nivel = 4;
                        puntuacion = Convert.ToInt32(N4.Text);
                        con.insertar_NivelNotaSP(ultimo_id_nota, id_tipo_nivel, puntuacion);

                        //se suman acciones 
                        int accion = Convert.ToInt32((int)(Session["accion"]));
                        int totalaccion = accion + 1;
                        Session["accion"] = totalaccion;
                        //termino de adición de acciones

                    Response.Write("<script>window.alert('Nota ingresada con exito.');</script>");
                    Response.Redirect(Request.RawUrl); 
               
            }



        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {

            Response.Redirect(Request.RawUrl);
        }

        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // If multiple ButtonField column fields are used, use the
            // CommandName property to determine which button was clicked.
            if (e.CommandName == "Editar")
            {

                labelTitulo.Text = "Editando Evaluación";
                btnAgregar.Text = "Editar";
                btnCancelar.Visible = true;

                // Convert the row index stored in the CommandArgument
                // property to an Integer.
                int index = Convert.ToInt32(e.CommandArgument);

                // Get the last name of the selected author from the appropriate
                // cell in the GridView control.
                GridViewRow selectedRow = GridView.Rows[index];

                TableCell id_act = selectedRow.Cells[0];

                //TableCell Nombres = selectedRow.Cells[1];
                //TableCell Apellidos = selectedRow.Cells[2];
                //TableCell FechaNAC = selectedRow.Cells[3];


                //id_mat.Text;
                // DropDownListActividad.SelectedIndex = Convert.ToInt32(id_act.Text);

                //txtNombre.Text = Nombres.Text;
                //txtApellido.Text = Apellidos.Text;

                //string fn = FechaNAC.Text;
                //txtcalendario.Text = fn;


            }

        }

        protected void GridView_RowCreated(object sender, GridViewRowEventArgs e)
        {
            //NO FUNCIONA CON PAGING EN GRIDVIEW
            ////id_nota
            //e.Row.Cells[0].Visible = false;
            ////boton editar
            //e.Row.Cells[10].Visible = false;
        }

        protected void checkboxNivel_CheckedChanged(object sender, EventArgs e)
        {
            if (checkboxNivel.Checked == true)
            {
                divNiveles.Visible = true;
            }
            else
            {
                divNiveles.Visible = false;
            }

        }

        protected void GridNivel_RowCreated(object sender, GridViewRowEventArgs e)
        {
            //id_nivel
            e.Row.Cells[0].Visible = false;
        }

        protected void ingresarNiveles()
        {
             
            // Get the last name of the selected author from the appropriate
            // cell in the GridView control.
            
 
            //for (int i = 0; i < GridNivel.PageCount; i++)
            //{
            //    GridViewRow selectedRow = GridNivel.Rows[i];

            //TableCell id_nivel = selectedRow.Cells[i];
            //string id = id_nivel.Text;
            //i++;
            //}


            //recorrer todo el gridview
            //foreach (GridViewRow row in GridNivel.Rows)
            //{
            //    for (int i = 0; i < GridNivel.Columns.Count; i++)
            //    {
            //        String header = GridNivel.Columns[i].HeaderText;
            //        String cellText = row.Cells[i].Text;
            //    }
            //}
        }

        private void getGridData()
        {
            
        }


    }
}