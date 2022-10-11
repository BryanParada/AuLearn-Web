using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AuLearn_Web
{
    public partial class SitioM : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string usuario = Request.QueryString["user"];

            //--->COMENTAR DESDE AQUI
            //IMPORTANTE, DESCOMENTAR ESTO PARA MEDIDAS SEGURIDAD LOGIN
            if (!Page.IsPostBack)
            {
                if (Session["Var"] == null)
                {
                    Response.Redirect("login.aspx");
                }
                Response.AppendHeader("Cache-Control", "no-store");
            }
                ////IMPORTANTE, DESCOMENTAR ESTO PARA MEDIDAS SEGURIDAD LOGIN


            //IMPORTANTE PARA MOSTRAR LOS NAVBAR SEGUN TIPO DE USUARIO
            if (Session["role"].ToString() == "Admin")
            {
                //DETALLAR TODOS LOS NAV ITEM QUE DEBE VER EL ADMINISTRADOR
                
                liAdmin_Administracion.Visible = true;
                liAdmin_Alumnos.Visible = true;
                liUser_actividades.Visible = false;
                liSoporte.Visible = false;
                liUser_Applicacion.Visible = false;
                liUser_Guias.Visible = false;


                if (!Page.IsPostBack)
                {
                    Conexion con = new Conexion();
                    string rutActual = (string)(Session["rutAct"]);
                    DataTable tablaPromBMAGeneral = con.selectDatosPromBMAGeneral(rutActual);
                    if (tablaPromBMAGeneral.Rows.Count > 0)
                    {
                        for (int i = 0; i < tablaPromBMAGeneral.Rows.Count; i++)
                        {
                            promBMAGeneral.Value += tablaPromBMAGeneral.Rows[i]["Cantidad"].ToString() + ",";
                        }

                    }
                }
                
            }
            else if (Session["role"].ToString() == "User")
            {
                //DETALLAR TODOS LOS NAV ITEM QUE DEBE VER EL USUARIO NORMAL

                liAdmin_Administracion.Visible = false;
                liAdmin_Alumnos.Visible = false;
                liSoporte.Visible = false;

                if (!Page.IsPostBack)
                {
                    Conexion con = new Conexion();
                    string rutActual = (string)(Session["rutAct"]);
                    DataTable tablaPromBMA = con.selectDatosPromBMA(rutActual);
                    if (tablaPromBMA.Rows.Count > 0)
                    {
                        for (int i = 0; i < tablaPromBMA.Rows.Count; i++)
                        {
                            promBMA.Value += tablaPromBMA.Rows[i]["Cantidad"].ToString() + ",";
                        }

                    }
                }

            }
            else if (Session["role"].ToString() == "Soporte")
            {
                //DETALLAR TODOS LOS NAV ITEM QUE DEBE VER EL USUARIO NORMAL 
                liAsignar_Tickets.Visible = false; 
            }
            else if (Session["role"].ToString() == "SYSAdmin")
            {
                //DETALLAR TODOS LOS NAV ITEM QUE DEBE VER EL USUARIO NORMAL 
                liResolver_tickets.Visible = false;
            } 
            //IMPORTANTE PARA MOSTRAR LOS NAVBAR SEGUN TIPO DE USUARIO

            //<--- DESCOMENTAR DESDE AQUI
            
            //asociamos a la variable firstname el nombre de la sesion
            string firstname = (string)(Session["firstname"]);
            
            lblUser.Text = firstname;

            //SqlConnection cn = new SqlConnection("Data Source=172.16.115.244; Initial Catalog=aulearn; User ID=aulearn; password=S0p0rt3.,");
            //SqlCommand cmd = new SqlCommand("select P.nombre + ' ' + P.apellido + ' - ' + M.materia AS 'Alumno', avg(N.Nota) as 'Promedio Actual' from Notas N inner join Actividad A on A.id_actividad=N.id_actividad inner join Unidad as U on A.id_unidad=U.id_unidad inner join Materia as M on M.id_materia=U.id_materia  inner join Estudiante as E on E.id_estudiante=N.id_estudiante Inner join Persona as P on P.rut_persona=E.rut_persona  group by P.nombre, P.apellido, M.materia", cn);
            //cn.Open();
            //SqlDataReader dr = cmd.ExecuteReader();

            //if (dr.Read())
            //{
            //    //Console.WriteLine(Convert.ToString(dr[0));
            //    string valor0 = Convert.ToString(dr[0]);
            //    hdf_Test.Value = Convert.ToString(dr[1]);
            //    //string valor2 = Convert.ToString(dr[2]);

            //}

            if (!Page.IsPostBack)
            {
                Conexion con = new Conexion();
                string rutActual = (string)(Session["rutAct"]);
                DataTable tablaAl = con.selectDatosPromAlumnos(rutActual);


                if (tablaAl.Rows.Count > 0)
                {
                    for (int i = 0; i < tablaAl.Rows.Count; i++)
                    {
                        nombresal.Value += tablaAl.Rows[i]["Alumno"].ToString() + ",";
                        promal.Value += tablaAl.Rows[i]["Promedio Actual"].ToString() + ",";
                    }

                }

                DataTable tablaUSOP = con.selectDatosUsoProfes();
                if (tablaUSOP.Rows.Count > 0)
                {
                    for (int i = 0; i < tablaUSOP.Rows.Count; i++)
                    {
                        nombresprof.Value += tablaUSOP.Rows[i]["Nombre_Profesor"].ToString() + ",";
                        horasprof.Value += tablaUSOP.Rows[i]["Horas"].ToString() + ",";
                    }

                }

 

            }
            


        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            int id_usuario = Convert.ToInt32((string)(Session["id_usuarioAct"]));

 
            string año = DateTime.Now.ToString("yyyy");
            string mes = DateTime.Now.ToString("MMMM"); 

            //hora de inicio
            DateTime horaTermino = DateTime.Now;
            Session["hora_termino"] = horaTermino;

            DateTime hora_inicio = (DateTime)(Session["hora_inicio"]);

            TimeSpan tiempoTotal = horaTermino - hora_inicio;

            string HorasyMinutosTotales = tiempoTotal.ToString(@"hh\:mm\:ss");

            int minutosTot = (int)(tiempoTotal.TotalMinutes);

            //se traen total de acciones
            int cantAcciones = Convert.ToInt32((int)(Session["accion"]));
             

            Conexion con = new Conexion();
            con.ContadorSesion(id_usuario, hora_inicio, horaTermino, minutosTot, cantAcciones);

            Session.RemoveAll();
            Session.Abandon();
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
            Response.AppendHeader("Cache-Control", "no-store");
            Response.Redirect("index.html");
        }

        protected void ButtonEnviarSoporte_Click(object sender, EventArgs e)
        {


            DateTime FechaApertura = DateTime.Now;
            DateTime FechaVencimiento = DateTime.Now.AddHours(72);
            string rut_colegio = "5561274-9";
            int id_usuario = Convert.ToInt32((string)(Session["id_usuarioAct"]));
            int id_modulo = Convert.ToInt32(DropDownListModulo.SelectedValue);
            int id_subcategoria = Convert.ToInt32(DropDownListSubCategoria.SelectedValue);
            int id_estado_ticket = 1; //1 = Pendiente

            string comentario_usuario = txtComentarioSoporte.Text;


            Conexion con = new Conexion();
            con.AgregarTicketSP(FechaApertura, FechaVencimiento, rut_colegio, id_usuario, id_modulo, id_subcategoria, id_estado_ticket, comentario_usuario, txtAsuntoSoporte.Text);

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$(function() { $('#ModalConfirmacion').modal('show'); });</script>", false);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$(function() { $('#ModalConfirmacion').modal('show'); });</script>", false);
             
        }

        protected void btnEnviarSugerencia_Click(object sender, EventArgs e)
        {
            DateTime f_llegada = DateTime.Now;
            string rut_colegio = "5561274-9";
            int id_usuario = Convert.ToInt32((string)(Session["id_usuarioAct"]));

            Conexion con = new Conexion();
            con.AgregarSugerenciaSP(f_llegada, rut_colegio, id_usuario, txtAsuntoSugerencia.Text, txtComentarioSugerencia.Text);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$(function() { $('#ModalConfirmacionSugerencias').modal('show'); });</script>", false);
        }

      

        protected void btnConfControlUso_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void btnConfPass_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void btnConfEdPerfil_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void buttonConfirmacionAsignTick_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void btnConfResolucion_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void btnIrAColegio_Click(object sender, EventArgs e)
        {

            int id_usuario = Convert.ToInt32((string)(Session["id_usuarioAct"]));
            int subCategoria = 23;

            Conexion con = new Conexion();
            con.editar_TutorialSP(id_usuario, subCategoria);
            
            Response.Redirect("AdminColegio.aspx");
        }


      
  
    }
}