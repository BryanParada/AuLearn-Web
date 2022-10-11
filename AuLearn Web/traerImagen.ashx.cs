using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace AuLearn_Web
{
    /// <summary>
    /// Descripción breve de traerImagen
    /// </summary>
    public class traerImagen : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "texto/normal";
            //context.Response.Write("Hola a todos");

            bool fileExiste = ExisteArchivo();

            if (fileExiste == true)//si es que es falso se crea el directorio
            {

                var webClient = new WebClient();
                webClient.UseDefaultCredentials = true;
                Conexion con = new Conexion(); 
                webClient.Credentials = new NetworkCredential(con.solicitarCredencialUser(), con.solicitarCredencialPass());
                byte[] imageBytes = webClient.DownloadData(con.solicitarCredencialUrl() + "Colegio - Juan Sandoval/Logo/logo.png");


                context.Response.Buffer = true;
                context.Response.Charset = "";
                context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                context.Response.ContentType = "image/png";
                context.Response.AddHeader("content-disposition", "attachment;filename=logo.png");
                context.Response.BinaryWrite(imageBytes);
            }
            else {

                var webClient = new WebClient();
                byte[] imageBytes = webClient.DownloadData("http://portal.webdificio.com/documents/10197/0/tulogoaquifooter.png");


                context.Response.Buffer = true;
                context.Response.Charset = "";
                context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                context.Response.ContentType = "image/png";
                context.Response.AddHeader("content-disposition", "attachment;filename=logo.png");
                context.Response.BinaryWrite(imageBytes);
                 
            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public bool ExisteArchivo()
        {
            //valor por defecto en true como si existiera
            bool fileExiste = true;
 Conexion con = new Conexion(); 
            var request = (FtpWebRequest)WebRequest.Create
            (con.solicitarCredencialUrl() + "Colegio - Juan Sandoval/Logo/logo.png");
           
            request.Credentials = new NetworkCredential(con.solicitarCredencialUser(), con.solicitarCredencialPass());
            request.Method = WebRequestMethods.Ftp.GetFileSize;

            //si el archivo no existe, el catch convierte el bool a false
            try
            {
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                FtpWebResponse response = (FtpWebResponse)ex.Response;
                if (response.StatusCode ==
                    FtpStatusCode.ActionNotTakenFileUnavailable)
                {
                    //NO EXISTE
                    fileExiste = false;

                } 
            }

            return fileExiste;
 
        
        }


    }
}