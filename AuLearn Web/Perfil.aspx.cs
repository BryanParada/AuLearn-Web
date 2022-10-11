using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AuLearn_Web
{
    public partial class Perfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //se suman acciones 
                    int accion = Convert.ToInt32((int)(Session["accion"]));
                    int totalaccion = accion + 1;
                    Session["accion"] = totalaccion;
                //termino de adición de acciones

                //COMENTAR "RUTPORMIENTRAS" CUANDO SE APLIQUE SEGURIDAD
                string rutActual = (string)(Session["rutAct"]);
              
                string rutPOrmientras = "7698502-2";

                Conexion con = new Conexion();
                DataTable tabla = con.selectDatosPersona(rutActual);

                if (tabla.Rows.Count > 0)
                {
                    labelID.Text = tabla.Rows[0]["rut_persona"].ToString();
                    txtNombre.Text = tabla.Rows[0]["nombre"].ToString();
                    txtApellido.Text = tabla.Rows[0]["apellido"].ToString();
                    string fecha = tabla.Rows[0]["fecha_nacimiento"].ToString();
                    labelCargo.Text = tabla.Rows[0]["id_cargo"].ToString();
 
                    string FechaArreglada = Convert.ToDateTime(fecha).ToString("yyyy-MM-dd"); //returns 25/09/2011

                    txtcalendario.Text = FechaArreglada;
                    
                }

                DataTable tablaU = con.selectDatosUsuario(rutActual);

                if (tablaU.Rows.Count > 0)
                {
                     
                    txtUsuario.Text = tablaU.Rows[0]["usuario"].ToString(); 
                    txtMail.Text = tablaU.Rows[0]["email"].ToString();
                    txtTelefono.Text = tablaU.Rows[0]["telefono"].ToString();
                }

            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            //editar persona
            int cargo = Convert.ToInt32(labelCargo.Text);
            Conexion con = new Conexion();
            con.editar_PersonaSP(labelID.Text, txtNombre.Text, txtApellido.Text, txtcalendario.Text, cargo);

            string mensaje = con.modificar_UsuarioSP_Perfil(labelID.Text, txtMail.Text, txtTelefono.Text);
            //Response.Write("<script>window.alert('" + mensaje + "');</script>");

            //LLENAR TEXTBOXUSUARIO DENUEVO
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$(function() { $('#ModalConfirmacionEdicionPerfil').modal('show'); });</script>", false);

            string rutActual = (string)(Session["rutAct"]);


            DataTable tablaU = con.selectDatosUsuario(rutActual);

            if (tablaU.Rows.Count > 0)
            {

                txtUsuario.Text = tablaU.Rows[0]["usuario"].ToString();
                
                 
            }


        }





    }
}