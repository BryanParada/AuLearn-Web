using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AuLearn_Web
{
    public partial class Contacto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void Enviar_Click(object sender, EventArgs e)
        {
            string name = TextNombre.Text;
            string fono = TextFono.Text;
            string mail = TextMail.Text;
            string mensaje = TextMensaje.Text;
            if (name=="")
            {
                Response.Write("<script>alert('Debe ingresar su nombre.');</script>");
            }
            else if (fono=="")
            {
                Response.Write("<script>alert('Debe ingresar su numero de telefono.');</script>");
            }
            else if (mail == "")
            {
                Response.Write("<script>alert('Debe ingresar su correo electronico.');</script>");
            }
            else if (mensaje == "")
            {
                Response.Write("<script>alert('Debe ingresar su mensaje.');</script>");
            }
            else
            {
                Conexion cn = new Conexion();
                Boolean valor = cn.SendMail(name, fono, mail, mensaje);
                cn.SendMailCliente(name, mail);
                if (valor == true)
                {
                    Response.Write("<script>alert('Su mensaje ha sido enviado exitosamente. Lo contactaremos a la brevedad.');</script>");

                    TextNombre.Text = ""; 
                    TextFono.Text = ""; 
                    TextMail.Text = ""; 
                    TextMensaje.Text = ""; 
                }
                else if (valor == false)
                {
                    Response.Write("<script>alert('Su mensaje no ha sido enviado. Intentelo mas tarde.');</script>");
                }
            }
            
        }
    }
}