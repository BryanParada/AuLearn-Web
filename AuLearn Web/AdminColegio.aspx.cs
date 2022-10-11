using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AuLearn_Web
{
    public partial class AdminColegio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //if (Session["role"].ToString() == "User")
                //{
                //    Response.Redirect("HomeA.aspx");
                //}

                Conexion con = new Conexion();
                DataTable tabla = con.selectDatosColegio();

                if (tabla.Rows.Count > 0)
                {
                    //for (int i = 0; i < tabla.Rows.Count;i++ )
                    //{
                    //    txtRut.Text += tabla.Rows[i]["rut_colegio"].ToString() + ",";
                    //}

                    txtRut.Text = tabla.Rows[0]["rut_colegio"].ToString();
                    lblRUTOculto.Text = tabla.Rows[0]["rut_colegio"].ToString();
                    txtNombre.Text = tabla.Rows[0]["nombre_colegio"].ToString();
                    DropDownListComuna.SelectedValue = tabla.Rows[0]["id_comuna"].ToString();
                    txtDireccion.Text = tabla.Rows[0]["direccion"].ToString();
                    TextFono.Text = tabla.Rows[0]["telefono"].ToString();
                    txtMail.Text = tabla.Rows[0]["email"].ToString();
                    txtSitio.Text = tabla.Rows[0]["sitio_web"].ToString();
                }
            }


            //logoCol.ImageUrl = "ftp://aulearn:S0p0rt3., @ftp://192.168.102.129:23/Colegio - Juan Sandoval/Logo/logo.png";


            //using (WebClient request = new WebClient())
            //{

            //    request.Credentials = new NetworkCredential("base", "s0p0rt3.,");
            //    byte[] imageBytes = request.DownloadData("ftp://192.168.102.129:23/Colegio - Juan Sandoval/Logo/logo.png");

            //    Response.Buffer = true;
            //    Response.Charset = "";
            //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //    Response.ContentType = "image/png";
            //    Response.AddHeader("content-disposition", "attachment;filename=logo.png");
            //    Response.BinaryWrite(imageBytes);
            //}


            //using (WebClient request = new WebClient())
            //{
            //    ftp://172.16.115.244:22/
            //    request.Credentials = new NetworkCredential("base", "s0p0rt3.,");
            //    byte[] fileData = request.DownloadData(string.Format("ftp://172.16.115.244:22/Colegio - Juan Sandoval/Logo/logo.png"));

            //    using (FileStream file = File.Create(Server.MapPath("ftp://172.16.115.244:22/Colegio - Juan Sandoval/Logo/")))
            //    {
            //        file.Write(fileData, 0, fileData.Length);
            //    }
            //}

            //// Download image
            //ftpclass.Download("/images/myimage.jpg", "server-address", "user", "pass", "/mysavedimage.jpg");

            //// Now link to the image
            //logoCol.ImageUrl = "/mysavedimage.jpg";




        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {

            string rut_col = lblRUTOculto.Text;
            string nombre = txtNombre.Text;
            int comuna = Convert.ToInt32(DropDownListComuna.SelectedValue);
            string direccion = txtDireccion.Text;
            string telefono = TextFono.Text;
            string email = txtMail.Text;
            string sitio = txtSitio.Text;
            //string logo_dir = "ftp://192.168.102.129:23/Colegio - Juan Sandoval/Logo/logo.png";

            string logo_dir = subirLogo();

            Conexion con = new Conexion();
            con.editar_ColegioSP(rut_col, nombre, comuna, direccion, telefono, email, sitio, logo_dir);


           
            DataTable tabla = con.selectDatosColegio();

            if (tabla.Rows.Count > 0)
            {
                txtRut.Text = tabla.Rows[0]["rut_colegio"].ToString();
                txtNombre.Text = tabla.Rows[0]["nombre_colegio"].ToString();
                DropDownListComuna.SelectedValue = tabla.Rows[0]["id_comuna"].ToString();
                txtDireccion.Text = tabla.Rows[0]["direccion"].ToString();
                TextFono.Text = tabla.Rows[0]["telefono"].ToString();
                txtMail.Text = tabla.Rows[0]["email"].ToString();
                txtSitio.Text = tabla.Rows[0]["sitio_web"].ToString();
            }

           
           
        }

        public string subirLogo()
        {
            string filePath = "";

            if (imgFileUpload.HasFile)
            {
                //obtener nombre de archivo
                //string FileName = imgFileUpload.PostedFile.FileName;
                string FileName = "logo.png";

                imgFileUpload.SaveAs("C:\\Windows\\Temp\\" + FileName);
                Conexion con = new Conexion();
                
                filePath = con.solicitarCredencialUrl() + "Colegio - Juan Sandoval/Logo/";
                filePath += FileName;
                string fileLocation = "C:\\Windows\\Temp\\" + FileName;
                 
                UploadToFTP(filePath, fileLocation, con.solicitarCredencialUser(), con.solicitarCredencialPass());



                //logoCol.ImageUrl = "ftp://aulearn:S0p0rt3., @ftp://192.168.102.129:23/Colegio - Juan Sandoval/Logo/" + FileName;
            }

            return filePath;

        }

        private bool UploadToFTP(string strFTPFilePath, string strLocalFilePath, string strUserName, string strPassword)
        {
            try
            {
                //Create a FTP Request Object and Specfiy a Complete Path
                FtpWebRequest reqObj = (FtpWebRequest)WebRequest.Create(strFTPFilePath);

                //Call A FileUpload Method of FTP Request Object
                reqObj.Method = WebRequestMethods.Ftp.UploadFile;

                //If you want to access Resourse Protected,give UserName and PWD
                reqObj.Credentials = new NetworkCredential(strUserName, strPassword);

                // Copy the contents of the file to the byte array.
                byte[] fileContents = File.ReadAllBytes(strLocalFilePath);
                reqObj.ContentLength = fileContents.Length;

                //Upload File to FTPServer
                Stream requestStream = reqObj.GetRequestStream();
                requestStream.Write(fileContents, 0, fileContents.Length);
                requestStream.Close();
                FtpWebResponse response = (FtpWebResponse)reqObj.GetResponse();
                response.Close();
            }

            catch (Exception Ex)
            {
                throw Ex;
            }
            return true;
        }

    }
}