using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AuLearn_Web
{
    public partial class loginPadres : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            string rutE = "6257429-1";

            

            Conexion con = new Conexion();

            string resultado = con.iniciarGuias(txtRut.Text);

            if (resultado.Equals(""))
            {
                Response.Write("<script>alert('El rut no existe.');</script>");

            }
            else
            {
                
                Response.Write("<script>alert('" + resultado + "');</script>");
                Response.Redirect("DescargarGuias.aspx?user=" + resultado + "&valor=" + rutE);

            }


        }

         


    }
}