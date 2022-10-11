using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls; 

namespace AuLearn_Web
{
    public partial class ResolverTicket : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int id_usuarioTecnico = Convert.ToInt32((string)(Session["id_usuarioAct"]));
            labelIDTecnico.Text = Convert.ToString(id_usuarioTecnico);

            divResolverTickets.Visible = false;
        }

        protected void GridTicketsTecnico_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Resolver" || e.CommandName == "Editar")
            {
                divResolverTickets.Visible = true;
                int index = Convert.ToInt32(e.CommandArgument);

                GridViewRow selectedRow = GridTicketsTecnico.Rows[index];
                TableCell idticketCELL = selectedRow.Cells[0];
                TableCell FechaAperturaCell = selectedRow.Cells[1];
                TableCell tiempoVencimientoCell = selectedRow.Cells[2];
                TableCell rutColegioCell = selectedRow.Cells[3];
                TableCell PrioridadCell = selectedRow.Cells[5];
                TableCell ModuloCell = selectedRow.Cells[6];
                TableCell subCatCell = selectedRow.Cells[7];
                TableCell asuntoCell = selectedRow.Cells[9];
                TableCell comentCell = selectedRow.Cells[10];

                string tiempoVencimiento = Convert.ToDateTime(tiempoVencimientoCell.Text).ToString("yyyy-MM-dd");

                DateTime horaActual = DateTime.Now;
 
                TimeSpan tiempoTotalRestante = Convert.ToDateTime(tiempoVencimientoCell.Text) - horaActual;
                //string HorasyMinutosTotales = tiempoTotalRestante.ToString(@"dd\:hh\:mm\:ss");
                string diastotales = tiempoTotalRestante.ToString(@"dd");
                string horastotales = tiempoTotalRestante.ToString(@"hh");

                //int minutosTot = (int)(tiempoTotalRestante.TotalMinutes);
                //int horasTot = (int)(tiempoTotalRestante.TotalHours);
                //int diasTot = (int)(tiempoTotalRestante.TotalDays);

                tiempoRestante.Text = "Quedan " + diastotales + " días y " + horastotales + " horas para el vencimiento del Ticket.";
                tiempoRestante.Font.Bold = true;
                txtFechaApertura.Text = Convert.ToDateTime(FechaAperturaCell.Text).ToString("yyyy-MM-dd HH:mm");
                txtFechaVencimiento.Text = Convert.ToDateTime(tiempoVencimientoCell.Text).ToString("yyyy-MM-dd HH:mm");
                txtRutColegio.Text = HttpUtility.HtmlDecode(rutColegioCell.Text);

                Conexion con = new Conexion();
                DataTable tabla = con.selectDatosColegio();

                if (tabla.Rows.Count > 0)
                { 
                    txtNombreColegio.Text = tabla.Rows[0]["nombre_colegio"].ToString(); 
                }

                 
                txtPrioridad.Text = PrioridadCell.Text;


                if (txtPrioridad.Text == "Baja")
                {
                    txtPrioridad.Attributes["style"] = "color:green; font-weight:bold;";
                }
                else if (txtPrioridad.Text == "Media")
                {
                    txtPrioridad.Attributes["style"] = "color:orange; font-weight:bold;";
                }
                else if (txtPrioridad.Text == "Alta")
                {
                    txtPrioridad.Attributes["style"] = "color:red; font-weight:bold;";

                }

                labelIDTICKET.Text = idticketCELL.Text;
                txtModulo.Text = HttpUtility.HtmlDecode(ModuloCell.Text);
                txtSubcategoria.Text = HttpUtility.HtmlDecode(subCatCell.Text);
                txtAsunto.Text = HttpUtility.HtmlDecode(asuntoCell.Text);
                txtComentarioUsuario.Text = HttpUtility.HtmlDecode(comentCell.Text);

            }
        }

        protected void GridTicketsTecnico_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView GridView = e.Row.DataItem as DataRowView;


                if (Convert.ToString(GridView["Prioridad"]) == "Baja")
                {
                    e.Row.Cells[5].ForeColor = System.Drawing.Color.Green;
                }
                else if (Convert.ToString(GridView["Prioridad"]) == "Media")
                {
                    e.Row.Cells[5].ForeColor = System.Drawing.Color.Orange;
                }
                else if (Convert.ToString(GridView["Prioridad"]) == "Alta")
                {
                    e.Row.Cells[5].ForeColor = System.Drawing.Color.Red;

                }




                if (Convert.ToString(GridView["Estado"]) == "En Desarrollo")
                {
                    e.Row.Cells[8].ForeColor = System.Drawing.Color.Blue;
                }
                else if (Convert.ToString(GridView["Estado"]) == "Cerrado")
                {
                    e.Row.Cells[8].ForeColor = System.Drawing.Color.Green;

                    Button btn = (Button)e.Row.Cells[13].Controls[0];
                    btn.Text = "Editar";
                    btn.CommandName = "Editar"; 

                }

                if (Convert.ToString(GridView["Resolución"]) == "Sin Resolución")
                {
                    e.Row.Cells[11].ForeColor = System.Drawing.Color.Red;
                }

                if (Convert.ToString(GridView["Fecha Resolución"]) == "Aún sin resolver")
                {
                    e.Row.Cells[12].ForeColor = System.Drawing.Color.Red;
                }
            }
        }

 

        protected void btnCancelar_Click(object sender, EventArgs e)
        {

             
            Response.Redirect(Request.RawUrl);
        }

        protected void btnResolucion_Click(object sender, EventArgs e)
        {
            DateTime horaActual = DateTime.Now;

            Conexion con = new Conexion();
            con.editar_TicketTECNSP(Convert.ToInt32(labelIDTICKET.Text), txtResolucion.Text, horaActual);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$(function() { $('#modalResolucionTicket').modal('show'); });</script>", false);

            Response.Redirect(Request.RawUrl);
        }

        protected void checkboxPrioridad_CheckedChanged(object sender, EventArgs e)
        {
            if (checkboxPrioridad.Checked == false)
            {
                string cons = @"select
T.id_ticket 'ID Ticket', 
CONVERT(VARCHAR(19),T.f_apertura, 120) as 'Fecha Apertura',
CONVERT(VARCHAR(19),T.f_vencimiento, 120) 'Fecha Vencimiento',
T.rut_colegio 'Rut Colegio',
P.nombre + ' ' + P.apellido 'Nombre usuario',
CASE WHEN Convert(varchar(10), T.id_tecnico_asignado) IS NULL THEN 'Sin Asignar' ELSE Convert(varchar(10),P2.nombre + ' ' + P2.apellido) END as 'Técnico Asignado',
TPrio.descripcion_prioridad 'Prioridad',
M.descripcion_modulo 'Módulo',
SC.descripcion_subcategoria 'SubCategoría',
TE.descripcion_estado_ticket 'Estado',
T.asunto 'Asunto',
T.comentario_usuario 'Comentario Usuario',
CASE WHEN T.resolucion_conflicto IS NULL THEN 'Sin Resolución' ELSE +T.resolucion_conflicto END as 'Resolución',
(CASE WHEN T.f_resolucion IS NULL THEN 'Aún sin resolver' ELSE CONVERT(VARCHAR(19),T.f_resolucion, 120) END) as 'Fecha Resolución' 

 from Ticket T
 inner join Usuario U on U.id_usuario = T.id_usuario
 inner join Persona P on P.rut_persona=U.rut_persona
 left join Usuario U2 on T.id_tecnico_asignado=U2.id_usuario
 left join Persona P2 on P2.rut_persona=U2.rut_persona
 inner join Tipo_Prioridad_Ticket TPrio on TPrio.id_prioridad_ticket=T.id_prioridad_ticket
 inner join Modulo M on M.id_modulo=T.id_modulo
 inner join SubCategoria SC on SC.id_subcategoria=T.id_subcategoria
 inner join Tipo_Estado_Ticket TE on TE.id_estado_ticket=T.id_estado_ticket where T.id_tecnico_asignado = @id_tecnico_asignado and T.id_estado_ticket = 2";

                SqlDataSourceTickets.SelectCommand = cons;
                SqlDataSourceTickets.Select(DataSourceSelectArguments.Empty);
                SqlDataSourceTickets.DataBind();
                GridTicketsTecnico.DataBind();
            }
            else
            {
                Response.Redirect(Request.RawUrl);
            }
            
        }

        
    }
}