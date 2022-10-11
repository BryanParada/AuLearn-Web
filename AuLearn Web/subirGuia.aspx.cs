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
    public partial class subirGuia : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            string rutActual = (string)(Session["rutAct"]);
            labelRut.Text = rutActual;

            if ((Session["role"].ToString() == "SYSAdmin") || (Session["role"].ToString() == "Admin"))
            {

                string cons2 = @"select 
                    G.id_guia 'id_guia',
                     C.nombre_curso 'Curso',
                     M.materia 'Materia',
                     U.descripcion 'Unidad',
                     G.ruta_archivo 'ruta_archivo',
                     G.nombre_archivo 'Nombre Archivo',
                    G.descripcion 'Descripción'
                    from  Guia G
inner join Curso C on G.id_curso=C.id_curso
Inner join Materia M on G.id_materia=M.id_materia
inner join Unidad U on G.id_unidad=U.id_unidad";

                SqlDataSourceGuias.SelectCommand = cons2;
                SqlDataSourceGuias.Select(DataSourceSelectArguments.Empty);
                SqlDataSourceGuias.DataBind();
                GridViewAlumnos.DataBind();

            }
            if (!IsPostBack)
            {
                if ((Session["role"].ToString() == "SYSAdmin") || (Session["role"].ToString() == "Admin"))
                {

                    string cons = @"select C.id_curso, C.nombre_curso from Curso as C
inner join Asignar_curso as A on C.id_curso=A.id_curso
inner join Usuario as U on U.id_usuario=A.id_usuario ";

                    SqlDataSource1.SelectCommand = cons;
                    SqlDataSource1.Select(DataSourceSelectArguments.Empty);
                    SqlDataSource1.DataBind();
                    DropDownCurso.DataBind();



                }
            }


        }

        protected void btnAlFTP_Click(object sender, EventArgs e)
        {
            //variables Dropdown
            string curso = DropDownCurso.SelectedItem.Text;
            string materia = DropDownMateria.SelectedItem.Text;
            string unidad = DropDownUnidad.SelectedItem.Text;


            if (this.f.HasFile) //pregunta si es que si se selecciono algo
            {
                // permite 2,100,000 bytes (aprox 2 MB) a ser subidos.
                if (f.PostedFile.ContentLength < 2100000) //800000 serian 800 kb
                {
                    //menor a 2 mb

                    if (Directory.Exists(@"C:\\SubirFtp")) //pregunta si es que existe el directorio
                    {
                        System.IO.Directory.Delete(@"C:\\SubirFtp", true);
                    }

                    System.IO.Directory.CreateDirectory(@"C:\\SubirFtp");//crea el directorio en c
                    this.f.SaveAs(@"C:\\SubirFtp\\" + this.f.FileName);//copia el archivo seleccionado al directorio creado

                    Conexion con = new Conexion();

                    string rutaC = con.solicitarCredencialUrl() + "Colegio - Juan Sandoval/" + curso + "/";
                    string ruta = "" + rutaC + f.FileName + "";

                    //crear directorio en FTP

                    bool Directorioexiste = DirectoryExists(rutaC); //envia ruta para ver si existe directorio

                    if (Directorioexiste == false)//si es que es falso se crea el directorio
                    {
                        WebRequest requestD = WebRequest.Create(rutaC);
                        requestD.Method = WebRequestMethods.Ftp.MakeDirectory;
                         
                        requestD.Credentials = new NetworkCredential(con.solicitarCredencialUser(), con.solicitarCredencialPass());
                        using (var resp = (FtpWebResponse)requestD.GetResponse())
                        {
                            Console.WriteLine(resp.StatusCode);
                        }

                        SubirFTP(ruta);//se envia la ruta para subir el archivo a ftp solo despues de haber creado el directorio.
                    }
                    else
                    {

                        SubirFTP(ruta);//se envia la ruta para subir el archivo a ftp, como ya existe el directorio, no hay necesidad de crear

                    }

                }
                else
                {
                    //mayor a 2 mb
                    Response.Write("<script>alert('Sólo se permiten archivos menores a 2 Megabytes.');</script>");
                }

            }
            else
            {
                Response.Write("<script>alert('Debe seleccionar archivo');</script>");
            }

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

                TableCell ruta_archivo = selectedRow.Cells[4];
                TableCell nombre_archivo = selectedRow.Cells[5];

                WebClient request = new WebClient();

                Conexion con = new Conexion();
                request.Credentials = new NetworkCredential(con.solicitarCredencialUser(), con.solicitarCredencialPass());
                //IMPPPPPPPPPPPPPPPPPPPOOOOOORTAAAANTE
                //Conexion con = new Conexion();
                //con.solicitarCredencialesFTP();

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
            else
            {


                int index = Convert.ToInt32(e.CommandArgument);

                // Get the last name of the selected author from the appropriate
                // cell in the GridView control.
                GridViewRow selectedRow = GridViewAlumnos.Rows[index];

                TableCell id_guia = selectedRow.Cells[0];
                TableCell ruta_archivo = selectedRow.Cells[4];

                Conexion con = new Conexion();
                con.eliminarGuia(Convert.ToInt32(id_guia.Text), ruta_archivo.Text);

                Response.Redirect(Request.RawUrl);
            }

        }

        protected void GridViewAlumnos_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[4].Visible = false;
        }

        public void SubirFTP(string ruta)
        {
            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(ruta); //se le indica al ftp en que ruta subir el archivo

            request.Method = WebRequestMethods.Ftp.UploadFile;//indica que se sube arhivos
            Conexion conC = new Conexion();
            request.Credentials = new NetworkCredential(conC.solicitarCredencialUser(), conC.solicitarCredencialPass());

            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = true;


            FileStream stream = File.OpenRead(@"C:\\SubirFtp\\" + f.FileName);//ruta del archivo que se va a subir
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            stream.Close();
            Stream reqStream = request.GetRequestStream();
            reqStream.Write(buffer, 0, buffer.Length);
            reqStream.Flush();
            reqStream.Close();


            System.IO.File.Delete(@"C:\\SubirFtp\\" + f.FileName);//elimina el archivo
            System.IO.Directory.Delete(@"C:\\SubirFtp");//elimina directorio creado


            //variables
            int id_curso = Convert.ToInt32(DropDownCurso.SelectedValue);
            int id_materia = Convert.ToInt32(DropDownMateria.SelectedValue);
            int id_unidad = Convert.ToInt32(DropDownUnidad.SelectedValue);


            Conexion con = new Conexion();
            con.guardar_guia(id_curso, id_materia, id_unidad, txtDescripcion.Text, ruta, f.FileName);

            //se suman acciones 
            int accion = Convert.ToInt32((int)(Session["accion"]));
            int totalaccion = accion + 1;
            Session["accion"] = totalaccion;
            //termino de adición de acciones

            Response.Write("<script>alert('El archivo se almaceno exitosamente');</script>");
            Response.Redirect(Request.RawUrl);



        }


        public bool DirectoryExists(string directory)
        {
            bool directoryExists;

            var request = (FtpWebRequest)WebRequest.Create(directory);
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            Conexion con = new Conexion();
            request.Credentials = new NetworkCredential(con.solicitarCredencialUser(), con.solicitarCredencialPass());

            try
            {
                using (request.GetResponse())
                {
                    //DIRECTORIO SI EXISTE
                    directoryExists = true;
                }
            }
            catch (WebException)
            {
                //DIRECTORIO NO EXISTE
                directoryExists = false;
            }

            return directoryExists;


        }







    }
}