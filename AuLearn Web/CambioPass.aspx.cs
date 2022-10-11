using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AuLearn_Web
{
    public partial class CambioPass : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             if (!IsPostBack)
            {
                //string rutPOrmientras = "7698502-2";
                //labelID.Text = rutPOrmientras;

                //COMENTAR "RUTPORMIENTRAS" CUANDO SE APLIQUE SEGURIDAD
                string rutActual = (string)(Session["rutAct"]);
                labelID.Text = rutActual;
            }
        }

        protected void btnCambio_Click(object sender, EventArgs e)
        {
            labelVal.Visible = false;

            Conexion con = new Conexion();
           
            DataTable tabla = con.selectPass(labelID.Text);

            if (tabla.Rows.Count > 0)
            {
                string passActualEncriptada = tabla.Rows[0]["contraseña"].ToString();
                string passActualSis = con.Decrypt(passActualEncriptada);

                if (passActualSis != passActual.Text)
                {
                    labelVal.Visible = true;
                    labelVal.Text = " * La contraseña actual no coincide.";
                }
                else if (passActualSis == (txtNuevaPass.Text) || (passActualSis == (txtConfiPass.Text)))
                {
                //la contraseña nueva debe ser distinta a la antigua
                    labelVal.Visible = true;
                    labelVal.Text = " * La contraseña no puede ser la misma que la anterior.";
                }
                else if (txtNuevaPass.Text != txtConfiPass.Text)
                {
                    //confirmar contraseña
                    labelVal.Visible = true;
                    labelVal.Text = " * La contraseñas no coinciden.";
                }
                else {
                    con.modificar_Pass(labelID.Text, txtNuevaPass.Text);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$(function() { $('#ModalConfirmacionPass').modal('show'); });</script>", false);
                    //Response.Write("<script>window.alert('La contraseña se modificó correctamente.');</script>");
                    //Response.Redirect(Request.RawUrl);
                }
                 

            }
        }
    }
}