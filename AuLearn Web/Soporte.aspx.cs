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
    public partial class Soporte : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            divResolverTickets.Visible = false;
            
        }

        protected void GridTickets_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView GridView = e.Row.DataItem as DataRowView;

                if (Convert.ToString(GridView["Técnico Asignado"]) == "Sin Asignar")
                {
                    e.Row.Cells[5].ForeColor = System.Drawing.Color.Red; 
                }

                if (Convert.ToString(GridView["Prioridad"]) == "Sin Asignar")
                {
                    e.Row.Cells[6].ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    e.Row.Cells[6].ForeColor = System.Drawing.Color.Green;
                }

                if (Convert.ToString(GridView["Estado"]) == "Pendiente")
                {
                    e.Row.Cells[9].ForeColor = System.Drawing.Color.Red;
                }
                else if (Convert.ToString(GridView["Estado"]) == "En Desarrollo")
                {
                    e.Row.Cells[9].ForeColor = System.Drawing.Color.Orange;
                    Button btn = (Button)e.Row.Cells[12].Controls[0];
                    btn.Visible = false;
                }
                else if (Convert.ToString(GridView["Estado"]) == "Cerrado")
                {
                    e.Row.Cells[9].ForeColor = System.Drawing.Color.Green;

                    Button btn = (Button)e.Row.Cells[12].Controls[0];
                    btn.Visible = false;

                }

                if (Convert.ToString(GridView["Resolución"]) == "Sin Resolución")
                {
                    e.Row.Cells[12].ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        protected void GridTickets_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Asignar")
            {
                divResolverTickets.Visible = true;
                int index = Convert.ToInt32(e.CommandArgument);

                GridViewRow selectedRow = GridTickets.Rows[index];
                TableCell idticketCELL = selectedRow.Cells[0];
                TableCell FechaAperturaCell = selectedRow.Cells[1];
                TableCell tiempoVencimientoCell = selectedRow.Cells[2];
                TableCell rutColegioCell = selectedRow.Cells[3]; 
                TableCell ModuloCell = selectedRow.Cells[7];
                TableCell subCatCell = selectedRow.Cells[8];
                TableCell asuntoCell = selectedRow.Cells[10];
                TableCell comentCell = selectedRow.Cells[11];

                string tiempoVencimiento = Convert.ToDateTime(tiempoVencimientoCell.Text).ToString("yyyy-MM-dd");

                DateTime horaActual = DateTime.Now;

                TimeSpan tiempoTotalRestante = Convert.ToDateTime(tiempoVencimientoCell.Text) - horaActual;
               
                string diastotales = tiempoTotalRestante.ToString(@"dd");
                string horastotales = tiempoTotalRestante.ToString(@"hh");
                  
                tiempoRestante.Text = "Quedan " + diastotales + " días y " + horastotales + " horas para el vencimiento del Ticket.";
                tiempoRestante.Font.Bold = true;
                txtFechaApertura.Text = Convert.ToDateTime(FechaAperturaCell.Text).ToString("yyyy-MM-dd HH:mm");
                txtFechaVencimiento.Text = Convert.ToDateTime(tiempoVencimientoCell.Text).ToString("yyyy-MM-dd HH:mm");
                labelFechaVencimiento.Text = txtFechaVencimiento.Text;
                txtRutColegio.Text = HttpUtility.HtmlDecode(rutColegioCell.Text);

                Conexion con = new Conexion();
                DataTable tabla = con.selectDatosColegio();

                if (tabla.Rows.Count > 0)
                {
                    txtNombreColegio.Text = tabla.Rows[0]["nombre_colegio"].ToString();
                }

                 
                labelIDTICKET.Text = idticketCELL.Text;
                txtModulo.Text = HttpUtility.HtmlDecode(ModuloCell.Text);
                txtSubcategoria.Text = HttpUtility.HtmlDecode(subCatCell.Text);
                txtAsunto.Text = HttpUtility.HtmlDecode(asuntoCell.Text);
                txtComentarioUsuario.Text = HttpUtility.HtmlDecode(comentCell.Text);

  

                }


        }

        protected void btnAsignar_Click(object sender, EventArgs e)
        {

           
            int id_tecnico_asignado = Convert.ToInt32(DROPTecnico.SelectedItem.Value);
            int id_prioridad_ticket = Convert.ToInt32(DROPprioridad.SelectedItem.Value);

            Conexion con = new Conexion();
            //AGREGAR IF DEPENDIENDO PRIORIDAD BAJA 3 DIAS, MEDIA 2, ALTA 1, MODIFICAR SP AGREGANDO FVENCIMIENTO.

           

            if (DROPprioridad.SelectedItem.ToString() == "Baja")
            {

                con.editar_TicketSP(Convert.ToInt32(labelIDTICKET.Text), Convert.ToDateTime(labelFechaVencimiento.Text), id_tecnico_asignado, id_prioridad_ticket);

            } else if (DROPprioridad.SelectedItem.ToString() == "Media")
            {
                DateTime fechaV = Convert.ToDateTime(labelFechaVencimiento.Text);
                DateTime fechaBaja = fechaV.AddDays(-1);
                con.editar_TicketSP(Convert.ToInt32(labelIDTICKET.Text), fechaBaja, id_tecnico_asignado, id_prioridad_ticket);

            }
            else
            {
                DateTime fechaV = Convert.ToDateTime(labelFechaVencimiento.Text);
                DateTime fechaBaja = fechaV.AddDays(-2);
                con.editar_TicketSP(Convert.ToInt32(labelIDTICKET.Text), fechaBaja, id_tecnico_asignado, id_prioridad_ticket);
            }

             

            

            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#modalASIGNAR').modal('show');</script>", false);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$(function() { $('#modalASIGNAR').modal('show'); });</script>", false);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            Response.Redirect(Request.RawUrl);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {

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
 inner join Tipo_Estado_Ticket TE on TE.id_estado_ticket=T.id_estado_ticket where T.id_estado_ticket != 0";

                SqlDataSourceTickets.SelectCommand = cons;
                SqlDataSourceTickets.Select(DataSourceSelectArguments.Empty);
                SqlDataSourceTickets.DataBind();
                GridTickets.DataBind();
            }else {
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
 inner join Tipo_Estado_Ticket TE on TE.id_estado_ticket=T.id_estado_ticket where T.id_estado_ticket = 1";

                SqlDataSourceTickets.SelectCommand = cons;
                SqlDataSourceTickets.Select(DataSourceSelectArguments.Empty);
                SqlDataSourceTickets.DataBind();
                GridTickets.DataBind();
                //Response.Redirect(Request.RawUrl);
            }
            
        }

        

      
    }
}