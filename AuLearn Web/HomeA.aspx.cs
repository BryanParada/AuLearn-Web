using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AuLearn_Web
{
    public partial class HomeA : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string rutActual = (string)(Session["rutAct"]);
            labelRut.Text = rutActual;


            ////IMPORTANTE PARA MOSTRAR LOS NAVBAR SEGUN TIPO DE USUARIO
            if (Session["role"].ToString() == "Admin")
            {
                // 

                BarChartPromedios.Visible = false; 
                CuadroADV.Visible = false;
                colPieChartSitAC.Visible = false;

                Conexion con = new Conexion();
                int id_usuario = Convert.ToInt32((string)(Session["id_usuarioAct"]));
                int subCategoria = 23;
                DataTable tabla = con.selectYavisto(id_usuario, subCategoria);
                

                if (tabla.Rows.Count > 0)
                {
                     
                     string YVstring = tabla.Rows[0]["id_yavisto"].ToString();


                     if (YVstring.Equals("False"))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$(function() { $('#ModalBienvenidaDirector').modal('show'); });</script>", false);
                    }
                    else
                    { 
                     //
                    }

                }
            }
            else if (Session["role"].ToString() == "User")
            {
                 
                //BarChartUSO.Visible = false;
                AdvertenciasDirector.Visible = false;
                 
            }
            else if (Session["role"].ToString() == "Soporte")
            {
                // 
                BarChartPromedios.Visible = false;
                PieChartSitAC.Visible = false;
                CuadroADV.Visible = false;
                BarChartUSO.Visible = false;
                AdvertenciasDirector.Visible = false;
            }
            else if (Session["role"].ToString() == "SYSAdmin")
            {
                // 
                BarChartPromedios.Visible = false;
                CuadroADV.Visible = false;
                colPieChartSitAC.Visible = false;
            }
            //IMPORTANTE PARA MOSTRAR LOS NAVBAR SEGUN TIPO DE USUARIO
           
        }

        protected void GridAdvertencias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Revisar")
            {
                int index = Convert.ToInt32(e.CommandArgument);

                GridViewRow selectedRow = GridAdvertencias.Rows[index];
                TableCell rut = selectedRow.Cells[0];
                TableCell nombrealumno = selectedRow.Cells[1];
                TableCell DescripNivel = selectedRow.Cells[2];
                TableCell NivelProm = selectedRow.Cells[3];
                TableCell materia = selectedRow.Cells[4];
                TableCell Cumplimiento = selectedRow.Cells[5];

                Response.Redirect("InformeNivel.aspx"); 

            }

        }

        

    }
}