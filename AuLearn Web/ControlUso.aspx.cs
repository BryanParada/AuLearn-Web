using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AuLearn_Web
{
    public partial class ControlUso : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGenerarControl_Click(object sender, EventArgs e)
        {
            int id_usuario_controlador = Convert.ToInt32((string)(Session["id_usuarioAct"]));

            int txtCantHoras = Convert.ToInt32(CantHoras.Text);
            int txtCantAcciones = Convert.ToInt32(CantAcciones.Text);
            int id_usuario_a_controlar = Convert.ToInt32(DropDownProfes.SelectedValue);

            Conexion con = new Conexion();
            con.ControlUso(id_usuario_a_controlar, id_usuario_controlador, txtCantHoras, txtCantAcciones);

            DateTime horaDefault = DateTime.Now;
            int tiempo_total_sesion = 0;
            int cantidad_acciones = 0;
            con.ContadorSesion(id_usuario_a_controlar, horaDefault, horaDefault, tiempo_total_sesion, cantidad_acciones);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$(function() { $('#modalConfControlUso').modal('show'); });</script>", false);


            Response.Write("<script>window.alert('Control registrado con éxito.');</script>");
            Response.Redirect(Request.RawUrl);



        }

        protected void GridControlUso_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Conexion con = new Conexion();

                DataTable tabla = con.selectDatosControl_uso();

                if (tabla.Rows.Count > 0)
                {
                    int TabcantHoras = Convert.ToInt32(tabla.Rows[0]["cantidad_horas"]);
                    int TabcantAcciones = Convert.ToInt32(tabla.Rows[0]["cantidad_acciones"]);
                

                DataRowView GridView = e.Row.DataItem as DataRowView;

                if (Math.Round((Convert.ToDecimal(GridView["Horas Totales de Uso"]))) >= Convert.ToDecimal(TabcantHoras))
                {
                    e.Row.Cells[5].ForeColor = System.Drawing.Color.Green;
                   
                    // e.Row.BackColor = System.Drawing.Color.Red;
                }
                else  
                {
                    e.Row.Cells[5].ForeColor = System.Drawing.Color.Red;
                    // e.Row.BackColor = System.Drawing.Color.Green;
                }


                if (Convert.ToInt32(GridView["Cantidad de Acciones"]) >= TabcantAcciones)
                {
                    e.Row.Cells[6].ForeColor = System.Drawing.Color.Green;

                    // e.Row.BackColor = System.Drawing.Color.Red;
                }
                else
                {
                    e.Row.Cells[6].ForeColor = System.Drawing.Color.Red;
                    // e.Row.BackColor = System.Drawing.Color.Green;
                }

                if (Convert.ToString(GridView["Cumplimiento"]) == "Cumple")
                {
                    e.Row.Cells[7].ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[7].BackColor = System.Drawing.Color.Green;
                }
                else
                {
                    e.Row.Cells[7].ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[7].BackColor = System.Drawing.ColorTranslator.FromHtml("#c43e3e");
                }
                

                }
            }
        }

        protected void GridControlUso_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void GridControlUso_DataBound(object sender, EventArgs e)
        {
            //id_usuario
            GridControlUso.Columns[0].Visible = false;
        }
    }
}