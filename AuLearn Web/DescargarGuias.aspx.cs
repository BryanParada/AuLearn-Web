using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AuLearn_Web
{
    public partial class DescargarGuias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string usuario = Request.QueryString["user"];
            string Valor = Request.QueryString["valor"];
            loginPadres lp = new loginPadres();

            string rutE = Valor;
            //string rutE = "6257429-1";

            labelRut.Text = rutE;
            labelTitulo.Text = "Guías de Reforzamiento para " + usuario;
        }

        protected void GridViewAlumnos_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[3].Visible = false;

        }

        protected void GridViewAlumnos_RowCommand(object sender, GridViewCommandEventArgs e)
        {


            // If multiple ButtonField column fields are used, use the
            // CommandName property to determine which button was clicked.
            if (e.CommandName == "Descargar")
            {

                //txtRut.Attributes.Add("class", "form-control col-xs-3");
                // Convert the row index stored in the CommandArgument
                // property to an Integer.
                int index = Convert.ToInt32(e.CommandArgument);

                // Get the last name of the selected author from the appropriate
                // cell in the GridView control.
                GridViewRow selectedRow = GridViewAlumnos.Rows[index];

                TableCell ruta_archivo = selectedRow.Cells[3];
                TableCell nombre_archivo = selectedRow.Cells[4];

                WebClient request = new WebClient();

                Conexion con = new Conexion(); 
                request.Credentials = new NetworkCredential(con.solicitarCredencialUser(), con.solicitarCredencialPass());//entrega credenciales de sitio ftp

                ////busca el archivo en ftp
                //byte[] fileData = request.DownloadData(ruta_archivo.Text);

                //Response.AppendHeader("Content-Disposition", "attachment; filename=" + nombre_archivo.Text);
                //Response.ContentType = "application/pdf";

                //----------METODO DESCARGA AUTOMATICA
                Response.ContentType = "application/octet-stream";
                var cd = new System.Net.Mime.ContentDisposition();
                cd.Inline = false;
                cd.FileName = Path.GetFileName(nombre_archivo.Text);
                Response.AppendHeader("Content-Disposition", cd.ToString());

                byte[] fileData = request.DownloadData(ruta_archivo.Text);
                Response.OutputStream.Write(fileData, 0, fileData.Length);


                //-------------------**metodo antiguo
                //string path = @"C:\\output\\" + nombre_archivo.Text;
                //FileStream file = File.Create(path);

                //file.Write(fileData, 0, fileData.Length);

                //file.Close();
            }
        }
    }

}