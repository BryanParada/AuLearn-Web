using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;

using System.Drawing; 
using System.Text;
using System.Threading.Tasks;
 

namespace AuLearn_Web
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["Var"] != null)
                {
                    Response.Redirect("HomeA.aspx"); //pagina a la que redirigir
                }
                Response.AppendHeader("Cache-Control", "no-store");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            
            
            Conexion con = new Conexion();

            //metodo iniciar sesion, traer el nombre y apellido del usuario
            string resultado = con.iniciarSesion(txtUsuario.Text, txtPass.Text);
            
            Session["UserAuthentication"] = txtUsuario.Text;
            //declaramos el resultado a una sesion
            Session["firstname"] = resultado;
            
            //-.-.-.-.-.-.-.-.-.-Traer rut persona a traves del nombre de usuario
            DataTable tabla = con.selectDatosUsuarioWU(txtUsuario.Text);

            if (tabla.Rows.Count > 0)
            {
                string rutActual = tabla.Rows[0]["rut_persona"].ToString();
                string id_usuarioActual = tabla.Rows[0]["id_usuario"].ToString();

                Session["rutAct"] = rutActual;
                Session["id_usuarioAct"] = id_usuarioActual;

            }

            //se inicializa accion
            int accion = 0;
            Session["accion"] = accion;
 
            //metodo trae el tipo de usuario
            
            string resultadoTipoUsuario = con.entrarTipoUsuarioSP(txtUsuario.Text);

            //hacer validacion si el tipo es admin o usuario normal
            //Session["role"] = "Admin";
            Session["role"] = resultadoTipoUsuario;

            if (resultado.Equals(""))
            {
                Response.Write("<script>alert('Usuario o Contraseña Incorrecta.');</script>");

            }
            else
            {
                Session["Var"] = "SI";
                //Response.Write("<script>alert('" + resultado + "');</script>");
                //Response.Redirect("HomeA.aspx?user=" + resultado);

                //hora de inicio
                DateTime horaInicioSesion = DateTime.Now;
                Session["hora_inicio"] = horaInicioSesion;


                Response.Redirect("HomeA.aspx");

              



            }

             
                
                







        }



    }
}