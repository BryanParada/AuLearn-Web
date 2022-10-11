using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Security.Cryptography;
 
namespace AuLearn_Web
{
    public class Conexion
    {
        //parametros para la conexion
        static string DSource = "192.168.1.8";
        //static string DSource = "DESKTOP-DLGMCOK";
        static string NameCatalogo = "aulearn";
        static string NameUsuario = "aulearnuser";
        static string ClaveUsuario = "s0p0rt3.,";

        //instanciamos conexion

        static SqlConnection con = new SqlConnection();

        //parametros para conexion FTP
        static string userFTP = "bparada";
        static string passwordFTP = "S0p0rt3";
        static string direccionFTP = "ftp://192.168.1.8:21/";

        //abrimos la conexion
        public static void abrirConexion()
        {
            cerrarConexion();
            con.ConnectionString = @"Data Source = " + DSource + "; Initial Catalog = " + NameCatalogo + "; User ID = " + NameUsuario + "; password = " + ClaveUsuario + "";
            con.Open();
        }
        //cerramos conexion
        public static void cerrarConexion()
        {
            con.Close();
        }

        public string solicitarCredencialUser()
        {
            //WebClient request = new WebClient();
            //request.Credentials = new NetworkCredential(userFTP, passwordFTP);
            return userFTP;
        }

        public string solicitarCredencialPass()
        {
            return passwordFTP;
        }

        public string solicitarCredencialUrl()
        {
            return direccionFTP;
        }

        public string eliminarGuia(int id_guia, string ruta_archivo)
        {

            string mensaje = "";

            //METODO ELIMINAR ARCHIVO DE FTP
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ruta_archivo);

            //If you need to use network credentials
            request.Credentials = new NetworkCredential(userFTP, passwordFTP);
            //additionally, if you want to use the current user's network credentials, just use:
            //System.Net.CredentialCache.DefaultNetworkCredentials

            request.Method = WebRequestMethods.Ftp.DeleteFile;
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            Console.WriteLine("Delete status: {0}", response.StatusDescription);
            response.Close();

 
            //METODO ELIMINAR GUIA DE BDD
            //abrimos conexion
            abrirConexion();
 
            //instanciamos ejecucion en bd
            SqlCommand cmd2 = new SqlCommand();
            //buscamos el procedimiento agregarEstudiante
            cmd2.CommandText = "eliminarGuia";
            cmd2.Connection = con;
            //indicamos que es procedimiento almacenado
            cmd2.CommandType = CommandType.StoredProcedure;
            //entregamos parametros a procedimiento almacenado
            cmd2.Parameters.Add(new SqlParameter("@id_guia", id_guia)); 
            //ejecutamos el procedimiento
            cmd2.ExecuteNonQuery();


            //cerramos la conexion
            cerrarConexion();

            mensaje = "Guía Eliminada";
            return mensaje;

  
        
        }


        //metodo procedimiento almacenado
        public string iniciarSesion(string user, string pass)
        {
            string encryptedString = encrypt(pass);

            string mensaje = "";
            //abrimos conexion
            abrirConexion();
            //instanciamos ejecucion en bd
            SqlCommand cmd = new SqlCommand();
            //buscamos el procedimiento a ocupar
            cmd.CommandText = "entrar";
            cmd.Connection = con;
            //indicamos que es procedimiento almacenado
            cmd.CommandType = CommandType.StoredProcedure;
            //entregamos parametros a procedimiento almacenado
            cmd.Parameters.Add(new SqlParameter("@usuario", user));
            cmd.Parameters.Add(new SqlParameter("@pass", encryptedString));
            //mandamos el parametro mensaje aunque retorne, para que no de error
            SqlParameter mensaj = new SqlParameter("@mensaje", SqlDbType.VarChar);
            //decimos que mensaje va de salida
            mensaj.Direction = ParameterDirection.Output;
            //le indicamos el tamaño (en numero no es nesesario)
            mensaj.Size = 40;
            //agregamos al comando
            cmd.Parameters.Add(mensaj);
            //ejecutamos el procedimiento
            cmd.ExecuteNonQuery();
            //cerramos la conexion
            cerrarConexion();
            //traemos el valor de retorno
            mensaje = cmd.Parameters["@mensaje"].Value.ToString();

            //asi si es numerico
            //mensaje = Int32.Parse(cmd.Parameters["@mensaje"].Value.ToString());
            return mensaje;

        }

        public string iniciarGuias(string rut)
        {
            string mensaje = "";
            //abrimos conexion
            abrirConexion();
            //instanciamos ejecucion en bd
            SqlCommand cmd = new SqlCommand();
            //buscamos el procedimiento a ocupar
            cmd.CommandText = "entrarPadres";
            cmd.Connection = con;
            //indicamos que es procedimiento almacenado
            cmd.CommandType = CommandType.StoredProcedure;
            //entregamos parametros a procedimiento almacenado
            cmd.Parameters.Add(new SqlParameter("@rut", rut)); 
            //mandamos el parametro mensaje aunque retorne, para que no de error
            SqlParameter mensaj = new SqlParameter("@mensaje", SqlDbType.VarChar);
            //decimos que mensaje va de salida
            mensaj.Direction = ParameterDirection.Output;
            //le indicamos el tamaño (en numero no es nesesario)
            mensaj.Size = 40;
            //agregamos al comando
            cmd.Parameters.Add(mensaj);
            //ejecutamos el procedimiento
            cmd.ExecuteNonQuery();
            //cerramos la conexion
            cerrarConexion();
            //traemos el valor de retorno
            mensaje = cmd.Parameters["@mensaje"].Value.ToString();
            //asi si es numerico
            //mensaje = Int32.Parse(cmd.Parameters["@mensaje"].Value.ToString());
            return mensaje;

        }

        public string guardar_guia(int id_curso, int id_materia, int id_unidad, string descripcion, string ruta_archivo, string nombre_archivo)
        {
             string guardar = "";
            //abrimos conexion
            abrirConexion();
            //instanciamos ejecucion en bd
            SqlCommand cmd = new SqlCommand();
            //buscamos el procedimiento agregarGuia
            cmd.CommandText = "agregarGuia";
            cmd.Connection = con;
            //indicamos que es procedimiento almacenado
            cmd.CommandType = CommandType.StoredProcedure;
            //entregamos parametros a procedimiento almacenado
            cmd.Parameters.Add(new SqlParameter("@id_curso", id_curso));
            cmd.Parameters.Add(new SqlParameter("@id_materia", id_materia));
            cmd.Parameters.Add(new SqlParameter("@id_unidad", id_unidad));
            cmd.Parameters.Add(new SqlParameter("@descripcion", descripcion));
            cmd.Parameters.Add(new SqlParameter("@ruta_archivo", ruta_archivo));
            cmd.Parameters.Add(new SqlParameter("@nombre_archivo", nombre_archivo)); 
             
            //ejecutamos el procedimiento
            cmd.ExecuteNonQuery();
 
            //cerramos la conexion
            cerrarConexion();

            guardar = "Guía ingresada con éxito.";
            return guardar;
          
        }

        //METODO SIN PA
        public string insertar_alumno(string rut_persona, string nombre, string apellido, string fecha_nacimiento, int id_cargo, int id_tipo_discapacidad)
        {
            //METODO SIN SP, SOLO DE MUESTRA

            string guardar = "";
            //string fecha_nacimiento = '1991-10-10';

            SqlConnection sqlConnection1 = new SqlConnection( @"Data Source = " + DSource + "; Initial Catalog = " + NameCatalogo + "; User ID = " + NameUsuario + "; password = " + ClaveUsuario);
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "Insert into Persona (rut_persona, nombre, apellido, fecha_nacimiento, id_cargo) VALUES (@rut_persona, @nombre, @apellido, @fecha_nacimiento, @id_cargo)";
            
            cmd.Parameters.AddWithValue("@rut_persona", rut_persona);
            cmd.Parameters.AddWithValue("@nombre", nombre);
            cmd.Parameters.AddWithValue("@apellido", apellido);
            cmd.Parameters.AddWithValue("@fecha_nacimiento", fecha_nacimiento);
            cmd.Parameters.AddWithValue("@id_cargo", id_cargo); 

            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            guardar = "Nuevo Alumno ingresado";
            return guardar;
        }

        public string insertar_alumnoSP(string rut_persona, int id_tipo_discapacidad)
        {

            string guardar = "";
            //ESTUDIANTE DISCAPACIDAD

            //abrimos conexion
            abrirConexion();
            //instanciamos ejecucion en bd
            SqlCommand cmd2 = new SqlCommand();
            //buscamos el procedimiento agregarEstudiante
            cmd2.CommandText = "agregarEstudiante";
            cmd2.Connection = con;
            //indicamos que es procedimiento almacenado
            cmd2.CommandType = CommandType.StoredProcedure;
            //entregamos parametros a procedimiento almacenado
            cmd2.Parameters.Add(new SqlParameter("@rut", rut_persona));
            cmd2.Parameters.Add(new SqlParameter("@discapacidad", id_tipo_discapacidad));

            //ejecutamos el procedimiento
            cmd2.ExecuteNonQuery();


            //cerramos la conexion
            cerrarConexion();

            guardar = "Nuevo Alumno ingresado";
            return guardar;
          
         
        }

        public string insertar_personaSP(string rut_persona, string nombre, string apellido, string fecha_nacimiento, int id_cargo)
        {

            bool activo = true;
            string guardar = "";
            //abrimos conexion
            abrirConexion();
            //instanciamos ejecucion en bd
            SqlCommand cmd = new SqlCommand();
            //buscamos el procedimiento agregarPersona
            cmd.CommandText = "agregarPersona";
            cmd.Connection = con;
            //indicamos que es procedimiento almacenado
            cmd.CommandType = CommandType.StoredProcedure;
            //entregamos parametros a procedimiento almacenado
            cmd.Parameters.Add(new SqlParameter("@rut", rut_persona));
            cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
            cmd.Parameters.Add(new SqlParameter("@apellido", apellido));
            cmd.Parameters.Add(new SqlParameter("@fnacimiento", fecha_nacimiento));
            cmd.Parameters.Add(new SqlParameter("@cargo", id_cargo));
            cmd.Parameters.Add(new SqlParameter("@activo", activo));

            //ejecutamos el procedimiento
            cmd.ExecuteNonQuery();

            
            //cerramos la conexion
            cerrarConexion();

            guardar = "Nueva persona ingresada";
            return guardar;


        }



        public string editar_alumnoP(string rut_persona, string nombre, string apellido, string fecha_nacimiento, int id_cargo, int id_tipo_discapacidad)
        {
             
            string guardar = "";
           
            ////instanciamos ejecucion en bd
            //SqlCommand cmd = new SqlCommand();
            ////buscamos el procedimiento modificarPersona
            //cmd.CommandText = "modificarPersona";
            //cmd.Connection = con;
            ////indicamos que es procedimiento almacenado
            //cmd.CommandType = CommandType.StoredProcedure;
            ////entregamos parametros a procedimiento almacenado
            //cmd.Parameters.Add(new SqlParameter("@rut", rut_persona));
            //cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
            //cmd.Parameters.Add(new SqlParameter("@apellido", apellido));
            //cmd.Parameters.Add(new SqlParameter("@fnacimiento", fecha_nacimiento));
            //cmd.Parameters.Add(new SqlParameter("@cargo", id_cargo));

            
            //ejecutamos el procedimiento
            //cmd2.ExecuteNonQuery();

            //abrimos conexion
            abrirConexion();
            //ESTUDIANTE DISCAPACIDAD
            //instanciamos ejecucion en bd
            SqlCommand cmd2 = new SqlCommand();
            //buscamos el procedimiento agregarEstudiante
            cmd2.CommandText = "modificarEstudiante";
            cmd2.Connection = con;
            //indicamos que es procedimiento almacenado
            cmd2.CommandType = CommandType.StoredProcedure;
            //entregamos parametros a procedimiento almacenado
            cmd2.Parameters.Add(new SqlParameter("@rut", rut_persona));
            cmd2.Parameters.Add(new SqlParameter("@discapacidad", id_tipo_discapacidad));
            //ejecutamos el procedimiento
            cmd2.ExecuteNonQuery();


            //cerramos la conexion
            cerrarConexion();

            guardar = "Alumno modificado correctamente";
            return guardar;


        }

        public string editar_PersonaSP(string rut_persona, string nombre, string apellido, string fecha_nacimiento, int id_cargo)
        {
 
            string guardar = "";
            //abrimos conexion
            abrirConexion();
            //instanciamos ejecucion en bd
            SqlCommand cmd = new SqlCommand();
            //buscamos el procedimiento modificarPersona
            cmd.CommandText = "modificarPersona";
            cmd.Connection = con;
            //indicamos que es procedimiento almacenado
            cmd.CommandType = CommandType.StoredProcedure;
            //entregamos parametros a procedimiento almacenado
            cmd.Parameters.Add(new SqlParameter("@rut", rut_persona));
            cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
            cmd.Parameters.Add(new SqlParameter("@apellido", apellido));
            cmd.Parameters.Add(new SqlParameter("@fnacimiento", fecha_nacimiento));
            cmd.Parameters.Add(new SqlParameter("@cargo", id_cargo));

            //ejecutamos el procedimiento
            cmd.ExecuteNonQuery();
 
            //cerramos la conexion
            cerrarConexion();

            guardar = "Persona modificada correctamente";
            return guardar;


        }

        public string actdesPersonaSP(string rut_persona, bool activo)
        {
             
            string guardar = "";
            //abrimos conexion
            abrirConexion();
  
            //instanciamos ejecucion en bd
            SqlCommand cmd2 = new SqlCommand();
            //buscamos el procedimiento agregarEstudiante
            cmd2.CommandText = "act-desPersona";
            cmd2.Connection = con;
            //indicamos que es procedimiento almacenado
            cmd2.CommandType = CommandType.StoredProcedure;
            //entregamos parametros a procedimiento almacenado
            cmd2.Parameters.Add(new SqlParameter("@rut", rut_persona)); 
            cmd2.Parameters.Add(new SqlParameter("@activo", activo));
            //ejecutamos el procedimiento
            cmd2.ExecuteNonQuery();


            //cerramos la conexion
            cerrarConexion();

            guardar = "Alumno modificado correctamente";

            return guardar;


        }

        public string insertar_UsuarioSP(string rut_persona, string usuario, string contraseña, int id_tipo_usuario, string email, string telefono)
        {

            string encryptedString = encrypt(contraseña);
 
            string guardar = "";
            //abrimos conexion
            abrirConexion();
 
            //instanciamos ejecucion en bd
            SqlCommand cmd2 = new SqlCommand();
            //buscamos el procedimiento agregarUsuario
            cmd2.CommandText = "agregarUsuario";
            cmd2.Connection = con;
            //indicamos que es procedimiento almacenado
            cmd2.CommandType = CommandType.StoredProcedure;
            //entregamos parametros a procedimiento almacenado
            cmd2.Parameters.Add(new SqlParameter("@rut_persona", rut_persona));
            cmd2.Parameters.Add(new SqlParameter("@usuario", usuario));
            cmd2.Parameters.Add(new SqlParameter("@contraseña", encryptedString)); //1580
            cmd2.Parameters.Add(new SqlParameter("@id_tipo_usuario", id_tipo_usuario));
            cmd2.Parameters.Add(new SqlParameter("@email", email));
            cmd2.Parameters.Add(new SqlParameter("@telefono", telefono));
 
 
            //ejecutamos el procedimiento
            cmd2.ExecuteNonQuery();
 
            //cerramos la conexion
            cerrarConexion();

            guardar = "Usuario ingresado correctamente";

            return guardar;


        }

        public string modificar_UsuarioSP(string rut_persona, string usuario, int id_tipo_usuario, string email, string telefono)
        {
           
            string guardar = "";
            //abrimos conexion
            abrirConexion();

            //instanciamos ejecucion en bd
            SqlCommand cmd2 = new SqlCommand();
            //buscamos el procedimiento modificarUsuario
            cmd2.CommandText = "modificarUsuario";
            cmd2.Connection = con;
            //indicamos que es procedimiento almacenado
            cmd2.CommandType = CommandType.StoredProcedure;
            //entregamos parametros a procedimiento almacenado
            cmd2.Parameters.Add(new SqlParameter("@rut_persona", rut_persona));
            cmd2.Parameters.Add(new SqlParameter("@usuario", usuario));
            cmd2.Parameters.Add(new SqlParameter("@id_tipo_usuario", id_tipo_usuario));
            cmd2.Parameters.Add(new SqlParameter("@email", email));
            cmd2.Parameters.Add(new SqlParameter("@telefono", telefono));
 
            //ejecutamos el procedimiento
            cmd2.ExecuteNonQuery();
 
            //cerramos la conexion
            cerrarConexion();

            guardar = "Usuario editado correctamente";

            return guardar;
 
        }

        public string modificar_UsuarioSP_Perfil(string rut_persona, string email, string telefono)
        {

            string guardar = "";
            //abrimos conexion
            abrirConexion();

            //instanciamos ejecucion en bd
            SqlCommand cmd2 = new SqlCommand();
            //buscamos el procedimiento modificarUsuario
            cmd2.CommandText = "modificarUsuarioPerfil";
            cmd2.Connection = con;
            //indicamos que es procedimiento almacenado
            cmd2.CommandType = CommandType.StoredProcedure;
            //entregamos parametros a procedimiento almacenado
            cmd2.Parameters.Add(new SqlParameter("@rut_persona", rut_persona)); 
            cmd2.Parameters.Add(new SqlParameter("@email", email));
            cmd2.Parameters.Add(new SqlParameter("@telefono", telefono));


            //ejecutamos el procedimiento
            cmd2.ExecuteNonQuery();


            //cerramos la conexion
            cerrarConexion();

            guardar = "Usuario editado correctamente";

            return guardar;


        }

        public string modificar_Pass(string rut_persona, string pass)
        {

            string encryptedString = encrypt(pass);

            string guardar = "";
            //abrimos conexion
            abrirConexion();

            //instanciamos ejecucion en bd
            SqlCommand cmd2 = new SqlCommand();
            //buscamos el procedimiento modificarUsuario
            cmd2.CommandText = "modificarPass";
            cmd2.Connection = con;
            //indicamos que es procedimiento almacenado
            cmd2.CommandType = CommandType.StoredProcedure;
            //entregamos parametros a procedimiento almacenado
            cmd2.Parameters.Add(new SqlParameter("@rut_persona", rut_persona));
            cmd2.Parameters.Add(new SqlParameter("@contraseña", encryptedString));


            //ejecutamos el procedimiento
            cmd2.ExecuteNonQuery();


            //cerramos la conexion
            cerrarConexion();

            guardar = "Contraseña modificada correctamente";

            return guardar;


        }

        public string editar_TutorialSP(int id_usuario, int id_subcategoria)
        {

            bool id_yavisto = true;

            string guardar = "";
            //abrimos conexion
            abrirConexion();
            //instanciamos ejecucion en bd
            SqlCommand cmd = new SqlCommand();
            //buscamos el procedimiento modificarPersona
            cmd.CommandText = "modificarTutorial";
            cmd.Connection = con;
            //indicamos que es procedimiento almacenado
            cmd.CommandType = CommandType.StoredProcedure;
            //entregamos parametros a procedimiento almacenado
            cmd.Parameters.Add(new SqlParameter("@id_usuario", id_usuario));
            cmd.Parameters.Add(new SqlParameter("@id_subcategoria", id_subcategoria));
            cmd.Parameters.Add(new SqlParameter("@id_yavisto", id_yavisto)); 
            //ejecutamos el procedimiento
            cmd.ExecuteNonQuery();

            //cerramos la conexion
            cerrarConexion();

            guardar = "Tutorial ha cambiado a ya visto";
            return guardar;


        }

        public DataTable selectYavisto(int id_usuario, int subCategoria)
        {
            //bool yavisto = new bool();

            //se declara una variable de tipo SqlConnection
            SqlConnection con = new SqlConnection();
            //se indica la cadena de conexion
            con.ConnectionString = @"Data Source = " + DSource + "; Initial Catalog = " + NameCatalogo + "; User ID = " + NameUsuario + "; password = " + ClaveUsuario + "";
            //codigo para llenar el comboBox
            DataSet ds = new DataSet();
            DataTable tabla = new DataTable();
            //endicamos la consulta en SQL
            SqlDataAdapter da = new SqlDataAdapter("select id_yavisto from Tutorial where id_usuario = " + id_usuario + " and id_subcategoria =" + subCategoria + " ", con);
            //se indica el nombre de la tabla
            da.Fill(tabla);

            return tabla;
        }



        public string insertar_NotaP(int id_actividad, int id_alumno, int nota, string Observacion, string fecha)
        {

            string mensaje = "";
            //abrimos conexion
            abrirConexion();
            //instanciamos ejecucion en bd
            SqlCommand cmd = new SqlCommand();
            //buscamos el procedimiento a ocupar
            cmd.CommandText = "agregarNota";
            cmd.Connection = con;
            //indicamos que es procedimiento almacenado
            cmd.CommandType = CommandType.StoredProcedure;
            //entregamos parametros a procedimiento almacenado
            cmd.Parameters.Add(new SqlParameter("@id_actividad", id_actividad));
            cmd.Parameters.Add(new SqlParameter("@id_estudiante", id_alumno));
            cmd.Parameters.Add(new SqlParameter("@nota", nota));
            cmd.Parameters.Add(new SqlParameter("@observacion", Observacion));
            cmd.Parameters.Add(new SqlParameter("@fecha", fecha)); 
            //mandamos el parametro mensaje aunque retorne, para que no de error
            SqlParameter mensaj = new SqlParameter("@mensaje", SqlDbType.VarChar);
            //decimos que mensaje va de salida
            mensaj.Direction = ParameterDirection.Output;
            //le indicamos el tamaño (en numero no es nesesario)
            mensaj.Size = 40;
            //agregamos al comando
            cmd.Parameters.Add(mensaj);
            //ejecutamos el procedimiento
            cmd.ExecuteNonQuery();
            //cerramos la conexion
            cerrarConexion();
            //traemos el valor de retorno
            mensaje = cmd.Parameters["@mensaje"].Value.ToString();

            //asi si es numerico
            //mensaje = Int32.Parse(cmd.Parameters["@mensaje"].Value.ToString());
            return mensaje;
 

        }

        public string insertar_NivelNotaSP(int id_nota, int id_tipo_nivel, int puntuacion)
        {


            string guardar = "";
            //abrimos conexion
            abrirConexion();
            //instanciamos ejecucion en bd
            SqlCommand cmd = new SqlCommand();
            //buscamos el procedimiento agregarNivelNota
            cmd.CommandText = "agregarNivelNota";
            cmd.Connection = con;
            //indicamos que es procedimiento almacenado
            cmd.CommandType = CommandType.StoredProcedure;
            //entregamos parametros a procedimiento almacenado
            cmd.Parameters.Add(new SqlParameter("@id_nota", id_nota));
            cmd.Parameters.Add(new SqlParameter("@id_tipo_nivel", id_tipo_nivel));
            cmd.Parameters.Add(new SqlParameter("@puntuacion", puntuacion)); 
            //ejecutamos el procedimiento
            cmd.ExecuteNonQuery();


            //cerramos la conexion
            cerrarConexion();

            guardar = "Nivel Ingresado con exito";
            return guardar;


        }

        public bool validarRut(string rut)
        {

            bool validacion = false;
            try
            {
                rut = rut.ToUpper();
                rut = rut.Replace(".", "");
                rut = rut.Replace("-", "");
                int rutAux = int.Parse(rut.Substring(0, rut.Length - 1));

                char dv = char.Parse(rut.Substring(rut.Length - 1, 1));

                int m = 0, s = 1;
                for (; rutAux != 0; rutAux /= 10)
                {
                    s = (s + rutAux % 10 * (9 - m++ % 6)) % 11;
                }
                if (dv == (char)(s != 0 ? s + 47 : 75))
                {
                    validacion = true;
                }
            }
            catch (Exception)
            {
            }
            return validacion;
        }

        public Boolean SendMail(string name, string fono, string email, string mensaje)
        {
            try
            { 
                //correo a empresa
                //Configuración del Mensaje
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                //Especificamos el correo desde el que se enviará el Email y el nombre de la persona que lo envía
                mail.From = new MailAddress("aulearn2017@gmail.com", name, Encoding.UTF8);
                //Aquí ponemos el asunto del correo
                mail.Subject = "Solicitud de informacion";
                //colocar mensaje en html
                mail.IsBodyHtml = true;
                //Aquí ponemos el mensaje que incluirá el correo
                mail.Body = "<center><table border='1'><tr><td><table border ='0'><tr><td height='38' colspan='2' align='center'><strong>DATOS PERSONALES DEL SOLICITANTE</strong></td></tr> <tr><td width='193' align='center'>nombre:</td><td width='180'>" + name + "</td></tr><tr><td align='center'>correo:</td><td>" + email + "</td></tr><tr><td align='center'>fono:</td><td>" + fono + "</td></tr></table> </td></tr></table><br/><br/><table border='1'><tr><td><table border='0'><tr><td height='34'><strong>MENSAJE DE SOLICITUD</strong>:</td></tr><tr><td><div align='center'>" + mensaje + "</div></td></tr></table></td></tr></table </center>";
                //Especificamos a quien enviaremos el Email, no es necesario que sea Gmail, puede ser cualquier otro proveedor
                mail.To.Add("aulearn2017@gmail.com");
                //Si queremos enviar archivos adjuntos tenemos que especificar la ruta en donde se encuentran
                //mail.Attachments.Add(new Attachment(@"C:\Documentos\carta.docx"));

                //Configuracion del SMTP
                SmtpServer.Port = 587; //Puerto que utiliza Gmail para sus servicios
                //Especificamos las credenciales con las que enviaremos el mail
                SmtpServer.Credentials = new System.Net.NetworkCredential("aulearn2017@gmail.com", "s0p0rt3.,");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Boolean SendMailCliente(string nombre, string email)
        {
            try
            {

                string mensajeCorreo = @"<center><td align='center' valign='top'><img src='http://oi64.tinypic.com/1zve6vo.jpg' width='512' height='296' vspace='10'></td> <p><br/><br/> <div style='color:#3482ad; font-size:19px;'>
<p>El equipo de AuLearn agradece tu interés.  </p>
  </div>
  Estimado Sr(a): " + nombre + ", su solicitud se encuentra en proceso. </p><p>Lo contactaremos a la brevedad.</p></center>";


                //correo a empresa
                //Configuración del Mensaje
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                //Especificamos el correo desde el que se enviará el Email y el nombre de la persona que lo envía
                mail.From = new MailAddress("aulearn2017@gmail.com", "Aulearn", Encoding.UTF8);
                //Aquí ponemos el asunto del correo
                mail.Subject = "Solicitud de informacion";
                //colocar mensaje en html
                mail.IsBodyHtml = true;
                //Aquí ponemos el mensaje que incluirá el correo 

                mail.Body = mensajeCorreo; 
                 
                //mail.Body = "Estimado Sr(a): " + nombre + ", su solicitud se encuentra en proceso. Lo contactaremos a la brevedad.";
                //Especificamos a quien enviaremos el Email, no es necesario que sea Gmail, puede ser cualquier otro proveedor
                mail.To.Add(email);
                //Si queremos enviar archivos adjuntos tenemos que especificar la ruta en donde se encuentran
                //mail.Attachments.Add(new Attachment(@"C:\Documentos\carta.docx"));

                //Configuracion del SMTP
                SmtpServer.Port = 587; //Puerto que utiliza Gmail para sus servicios
                //Especificamos las credenciales con las que enviaremos el mail
                SmtpServer.Credentials = new System.Net.NetworkCredential("aulearn2017@gmail.com", "s0p0rt3.,");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Boolean SendMailBienvenida(string nombre, string usuario, string contraseña, string email)
        {
            try
            {

                string mensajeCorreo = @"<center><td align='center' valign='top'><img src='http://oi64.tinypic.com/1zve6vo.jpg' width='512' height='296' vspace='10'></td> <p><br/><br/> <div style='color:#3482ad; font-size:19px;'>
<p>El equipo de AuLearn te da la Bienvenida.  </p>
  </div>
  Estimado Sr(a): " + nombre + ", le damos la bienvenida. </p> <p>Su nombre de usuario es: <strong>" + usuario + "</strong></p> <p>Su contraseña es: <strong>" + contraseña + "</strong></p> Le aconsejamos cambiar su contraseña una vez haya ingresado al sistema.</center>";


                //correo a empresa
                //Configuración del Mensaje
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                //Especificamos el correo desde el que se enviará el Email y el nombre de la persona que lo envía
                mail.From = new MailAddress("aulearn2017@gmail.com", "Aulearn", Encoding.UTF8);
                //Aquí ponemos el asunto del correo
                mail.Subject = "Bienvenido a la Plataforma Aulearn";
                //colocar mensaje en html
                mail.IsBodyHtml = true;
                //Aquí ponemos el mensaje que incluirá el correo 

                mail.Body = mensajeCorreo;

                //mail.Body = "Estimado Sr(a): " + nombre + ", su solicitud se encuentra en proceso. Lo contactaremos a la brevedad.";
                //Especificamos a quien enviaremos el Email, no es necesario que sea Gmail, puede ser cualquier otro proveedor
                mail.To.Add(email);
                //Si queremos enviar archivos adjuntos tenemos que especificar la ruta en donde se encuentran
                //mail.Attachments.Add(new Attachment(@"C:\Documentos\carta.docx"));

                //Configuracion del SMTP
                SmtpServer.Port = 587; //Puerto que utiliza Gmail para sus servicios
                //Especificamos las credenciales con las que enviaremos el mail
                SmtpServer.Credentials = new System.Net.NetworkCredential("aulearn2017@gmail.com", "s0p0rt3.,");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public string asignar_cursoSP(int id_curso, int id_estudiante)
        {

           
            string guardar = "";
            //abrimos conexion
            abrirConexion();
            //instanciamos ejecucion en bd
            SqlCommand cmd = new SqlCommand();
            //buscamos el procedimiento asignarCurso
            cmd.CommandText = "asignarCurso";
            cmd.Connection = con;
            //indicamos que es procedimiento almacenado
            cmd.CommandType = CommandType.StoredProcedure;
            //entregamos parametros a procedimiento almacenado
            cmd.Parameters.Add(new SqlParameter("@id_curso", id_curso));
            cmd.Parameters.Add(new SqlParameter("@id_estudiante", id_estudiante)); 

            //ejecutamos el procedimiento
            cmd.ExecuteNonQuery();
 

            //cerramos la conexion
            cerrarConexion();

            guardar = "Asignación Exitosa.";
            return guardar;


        }

        public string asignar_curso_ProfesorSP(int id_curso, int id_usuario)
        {


            string guardar = "";
            //abrimos conexion
            abrirConexion();
            //instanciamos ejecucion en bd
            SqlCommand cmd = new SqlCommand();
            //buscamos el procedimiento asignarCursoProfesor
            cmd.CommandText = "asignarCursoProfesor";
            cmd.Connection = con;
            //indicamos que es procedimiento almacenado
            cmd.CommandType = CommandType.StoredProcedure;
            //entregamos parametros a procedimiento almacenado
            cmd.Parameters.Add(new SqlParameter("@id_curso", id_curso));
            cmd.Parameters.Add(new SqlParameter("@id_usuario", id_usuario));

            //ejecutamos el procedimiento
            cmd.ExecuteNonQuery();


            //cerramos la conexion
            cerrarConexion();

            guardar = "Asignación Exitosa.";
            return guardar;


        }

        public string entrarTipoUsuarioSP(string usuario)
        {
            string mensaje = "";
            //abrimos conexion
            abrirConexion();
            //instanciamos ejecucion en bd
            SqlCommand cmd = new SqlCommand();
            //buscamos el procedimiento a ocupar
            cmd.CommandText = "entrarTipoUsuario";
            cmd.Connection = con;
            //indicamos que es procedimiento almacenado
            cmd.CommandType = CommandType.StoredProcedure;
            //entregamos parametros a procedimiento almacenado
            cmd.Parameters.Add(new SqlParameter("@usuario", usuario)); 
            //mandamos el parametro mensaje aunque retorne, para que no de error
            SqlParameter mensaj = new SqlParameter("@mensaje", SqlDbType.VarChar);
            //decimos que mensaje va de salida
            mensaj.Direction = ParameterDirection.Output;
            //le indicamos el tamaño (en numero no es nesesario)
            mensaj.Size = 40;
            //agregamos al comando
            cmd.Parameters.Add(mensaj);
            //ejecutamos el procedimiento
            cmd.ExecuteNonQuery();
            //cerramos la conexion
            cerrarConexion();
            //traemos el valor de retorno
            mensaje = cmd.Parameters["@mensaje"].Value.ToString();

            //asi si es numerico
            //mensaje = Int32.Parse(cmd.Parameters["@mensaje"].Value.ToString());
            return mensaje;

        }

        public DataTable selectDatosColegio()
        {
            DataTable DT = new DataTable();

            //se declara una variable de tipo SqlConnection
            SqlConnection con = new SqlConnection();
            //se indica la cadena de conexion
            con.ConnectionString = @"Data Source = " + DSource + "; Initial Catalog = " + NameCatalogo + "; User ID = " + NameUsuario + "; password = " + ClaveUsuario + "";
            //codigo para llenar el comboBox
            DataSet ds = new DataSet();
            DataTable tabla = new DataTable();
            //endicamos la consulta en SQL
            SqlDataAdapter da = new SqlDataAdapter("select * from Colegio", con);
            //se indica el nombre de la tabla
            da.Fill(tabla);

            return tabla;
 
        
        }

        public DataTable selectDatosPersona(string rut_persona)
        {
            DataTable DT = new DataTable();

            //se declara una variable de tipo SqlConnection
            SqlConnection con = new SqlConnection();
            //se indica la cadena de conexion
            con.ConnectionString = @"Data Source = " + DSource + "; Initial Catalog = " + NameCatalogo + "; User ID = " + NameUsuario + "; password = " + ClaveUsuario + "";
            //codigo para llenar el comboBox
            DataSet ds = new DataSet();
            DataTable tabla = new DataTable();
            //endicamos la consulta en SQL
            SqlDataAdapter da = new SqlDataAdapter("select P.rut_persona, P.nombre, P.apellido, P.fecha_nacimiento, P.id_cargo, Cargo.cargo from Persona P inner join Cargo on Cargo.id_cargo=P.id_cargo  where P.rut_persona = '" + rut_persona + "'", con);
            //se indica el nombre de la tabla
            da.Fill(tabla);

            return tabla; 
        }

        public DataTable selectDatosUsuario(string rut_persona)
        {
            DataTable DT = new DataTable();

            //se declara una variable de tipo SqlConnection
            SqlConnection con = new SqlConnection();
            //se indica la cadena de conexion
            con.ConnectionString = @"Data Source = " + DSource + "; Initial Catalog = " + NameCatalogo + "; User ID = " + NameUsuario + "; password = " + ClaveUsuario + "";
            //codigo para llenar el comboBox
            DataSet ds = new DataSet();
            DataTable tabla = new DataTable();
            //endicamos la consulta en SQL
            SqlDataAdapter da = new SqlDataAdapter("select * from Usuario where rut_persona = '" + rut_persona + "'", con);
            //se indica el nombre de la tabla
            da.Fill(tabla);

            return tabla;
 
        }

        public DataTable selectDatosUsuarioWU(string usuario)
        {
            DataTable DT = new DataTable();

            //se declara una variable de tipo SqlConnection
            SqlConnection con = new SqlConnection();
            //se indica la cadena de conexion
            con.ConnectionString = @"Data Source = " + DSource + "; Initial Catalog = " + NameCatalogo + "; User ID = " + NameUsuario + "; password = " + ClaveUsuario + "";
            //codigo para llenar el comboBox
            DataSet ds = new DataSet();
            DataTable tabla = new DataTable();
            //endicamos la consulta en SQL
            SqlDataAdapter da = new SqlDataAdapter("select * from Usuario where usuario = '" + usuario + "'", con);
            //se indica el nombre de la tabla
            da.Fill(tabla);

            return tabla;

        }
        public DataTable selectDatosControl_uso()
                {
                    DataTable DT = new DataTable();

                    //se declara una variable de tipo SqlConnection
                    SqlConnection con = new SqlConnection();
                    //se indica la cadena de conexion
                    con.ConnectionString = @"Data Source = " + DSource + "; Initial Catalog = " + NameCatalogo + "; User ID = " + NameUsuario + "; password = " + ClaveUsuario + "";
                    //codigo para llenar el comboBox
                    DataSet ds = new DataSet();
                    DataTable tabla = new DataTable();
                    //endicamos la consulta en SQL
                    SqlDataAdapter da = new SqlDataAdapter("select * from Control_de_uso", con);
                    //se indica el nombre de la tabla
                    da.Fill(tabla);

                    return tabla;

                }

        public DataTable selectDatosPromAlumnos(string rut_persona)
                        {
                            DataTable DT = new DataTable();

                            //se declara una variable de tipo SqlConnection
                            SqlConnection con = new SqlConnection();
                            //se indica la cadena de conexion
                            con.ConnectionString = @"Data Source = " + DSource + "; Initial Catalog = " + NameCatalogo + "; User ID = " + NameUsuario + "; password = " + ClaveUsuario + "";
                            //codigo para llenar el comboBox
                            DataSet ds = new DataSet();
                            DataTable tabla = new DataTable();
                            //endicamos la consulta en SQL
                            SqlDataAdapter da = new SqlDataAdapter(@"select
P.nombre + ' ' + P.apellido AS 'Alumno', 
avg(N.Nota) as 'Promedio Actual'
from Notas N
inner join Estudiante E on N.id_estudiante=E.id_estudiante
inner join Persona P on E.rut_persona=P.rut_persona
inner join Integrantes_curso IC on IC.id_estudiante=E.id_estudiante
inner join Curso C on C.id_curso=IC.id_curso
inner join Asignar_curso AC on AC.id_curso=C.id_curso
inner join Usuario U on U.id_usuario=AC.id_usuario
where U.rut_persona = '" + rut_persona + "' group by P.nombre, P.apellido", con);
                            //se indica el nombre de la tabla
                            da.Fill(tabla);

                            return tabla;

                        }

        public DataTable selectDatosUsoProfes()
        {
            DataTable DT = new DataTable();

            //se declara una variable de tipo SqlConnection
            SqlConnection con = new SqlConnection();
            //se indica la cadena de conexion
            con.ConnectionString = @"Data Source = " + DSource + "; Initial Catalog = " + NameCatalogo + "; User ID = " + NameUsuario + "; password = " + ClaveUsuario + "";
            //codigo para llenar el comboBox
            DataSet ds = new DataSet();
            DataTable tabla = new DataTable();
            //endicamos la consulta en SQL
            SqlDataAdapter da = new SqlDataAdapter(@"select C.id_usuario as 'id', P.nombre + ' ' + P.apellido as 'Nombre_Profesor',
             (Sum(C.tiempo_total_sesion))/60 as 'Horas',
             (SUM(C.cantidad_acciones)) as 'Cantidad de Acciones',

             (case 
             when ((SUM(C.tiempo_total_sesion))/60) >= CU.cantidad_horas and (SUM(C.cantidad_acciones)) >= CU.cantidad_acciones 
             then 'Cumple'
             else 'No cumple'
             end) as 'Cumplimiento' 
              from Contador C

            inner join Usuario U on U.id_usuario=C.id_usuario
            inner join Persona P on P.rut_persona=U.rut_persona
            inner join Control_de_uso CU on CU.id_usuario_a_controlar=U.id_usuario
 
	          group by C.id_usuario, P.nombre, P.apellido,
	          CU.cantidad_horas,
	          CU.cantidad_acciones 
	          order by P.nombre, P.apellido", con);
            //se indica el nombre de la tabla
            da.Fill(tabla);

            return tabla;

        }

        public DataTable selectDatosPromBMA(string rut_persona)
        {
            DataTable DT = new DataTable();

            //se declara una variable de tipo SqlConnection
            SqlConnection con = new SqlConnection();
            //se indica la cadena de conexion
            con.ConnectionString = @"Data Source = " + DSource + "; Initial Catalog = " + NameCatalogo + "; User ID = " + NameUsuario + "; password = " + ClaveUsuario + "";
  
                SqlDataAdapter da = new SqlDataAdapter("ListarPromBMA", con); // Using a Store Procedure.
                //SqlDataAdapter da = new SqlDataAdapter("SELECT 'this is a test text' as test", con); To use a hard coded query.
                da.SelectCommand.CommandType = CommandType.StoredProcedure; // Comment if using hard coded query.
                DataTable ds = new DataTable(); // Definition: Memory representation of the database.
                da.SelectCommand.Parameters.AddWithValue("@rut_persona", rut_persona); // Repeat for each parameter present in the Store Procedure.

                da.Fill(ds); // Fill the dataset with the query data

                return ds;


//                DataTable DT = new DataTable();

//                //se declara una variable de tipo SqlConnection
//                SqlConnection con = new SqlConnection();
//                //se indica la cadena de conexion
//                con.ConnectionString = @"Data Source = " + DSource + "; Initial Catalog = " + NameCatalogo + "; User ID = " + NameUsuario + "; password = " + ClaveUsuario + "";
//                //codigo para llenar el comboBox
//                DataSet ds = new DataSet();
//                DataTable tabla = new DataTable();
//                //endicamos la consulta en SQL
//                SqlDataAdapter da = new SqlDataAdapter(@"select  count(SC.[Promedio Actual]) as 'Cantidad' , 'Peligro' as 'situacion'
//from  
//(select
//P.nombre + ' ' + P.apellido AS 'Alumno', 
//avg(N.Nota) as 'Promedio Actual'
//from Notas N
//inner join Estudiante E on N.id_estudiante=E.id_estudiante
//inner join Persona P on E.rut_persona=P.rut_persona
//inner join Integrantes_curso IC on IC.id_estudiante=E.id_estudiante
//inner join Curso C on C.id_curso=IC.id_curso
//inner join Asignar_curso AC on AC.id_curso=C.id_curso
//inner join Usuario U on U.id_usuario=AC.id_usuario where U.rut_persona = '" + rut_persona + "' group by P.nombre, P.apellido ) as SC where SC.[Promedio Actual] between 10 and 39 union select  count(SC.[Promedio Actual]) as 'Cantidad' , 'Cuidado' as 'situacion' from (select P.nombre + ' ' + P.apellido AS 'Alumno', avg(N.Nota) as 'Promedio Actual' from Notas N inner join Estudiante E on N.id_estudiante=E.id_estudiante inner join Persona P on E.rut_persona=P.rut_persona inner join Integrantes_curso IC on IC.id_estudiante=E.id_estudiante inner join Curso C on C.id_curso=IC.id_curso inner join Asignar_curso AC on AC.id_curso=C.id_curso inner join Usuario U on U.id_usuario=AC.id_usuario where U.rut_persona = '" + rut_persona + "' group by P.nombre, P.apellido ) as SC where SC.[Promedio Actual] between 40 and 59 union select  count(SC.[Promedio Actual]) as 'Cantidad', 'Normal' as 'situacion' from (select P.nombre + ' ' + P.apellido AS 'Alumno', avg(N.Nota) as 'Promedio Actual' from Notas N inner join Estudiante E on N.id_estudiante=E.id_estudiante inner join Persona P on E.rut_persona=P.rut_persona inner join Integrantes_curso IC on IC.id_estudiante=E.id_estudiante inner join Curso C on C.id_curso=IC.id_curso inner join Asignar_curso AC on AC.id_curso=C.id_curso inner join Usuario U on U.id_usuario=AC.id_usuario where U.rut_persona = '" + rut_persona + "' group by P.nombre, P.apellido ) as SC where SC.[Promedio Actual] between 60 and 70", con);
//                //se indica el nombre de la tabla
//                da.Fill(tabla);

//                return tabla;
 
 

            
 
        }

        public DataTable selectDatosPromBMAGeneral(string rut_persona)
        {
            DataTable DT = new DataTable();

            //se declara una variable de tipo SqlConnection
            SqlConnection con = new SqlConnection();
            //se indica la cadena de conexion
            con.ConnectionString = @"Data Source = " + DSource + "; Initial Catalog = " + NameCatalogo + "; User ID = " + NameUsuario + "; password = " + ClaveUsuario + "";

            SqlDataAdapter da = new SqlDataAdapter("ListarPromBMAGeneral", con); // Using a Store Procedure.
            //SqlDataAdapter da = new SqlDataAdapter("SELECT 'this is a test text' as test", con); To use a hard coded query.
            da.SelectCommand.CommandType = CommandType.StoredProcedure; // Comment if using hard coded query.
            DataTable ds = new DataTable(); // Definition: Memory representation of the database.
            da.SelectCommand.Parameters.AddWithValue("@rut_persona", rut_persona); // Repeat for each parameter present in the Store Procedure.

            da.Fill(ds); // Fill the dataset with the query data

            return ds; 
        }


        public DataTable selectPass(string rut_persona)
        {
            DataTable DT = new DataTable();

            //se declara una variable de tipo SqlConnection
            SqlConnection con = new SqlConnection();
            //se indica la cadena de conexion
            con.ConnectionString = @"Data Source = " + DSource + "; Initial Catalog = " + NameCatalogo + "; User ID = " + NameUsuario + "; password = " + ClaveUsuario + "";
            //codigo para llenar el comboBox
            DataSet ds = new DataSet();
            DataTable tabla = new DataTable();
            //endicamos la consulta en SQL
            SqlDataAdapter da = new SqlDataAdapter("select contraseña from Usuario where rut_persona = '" + rut_persona + "'", con);
            //se indica el nombre de la tabla
            da.Fill(tabla);

            return tabla;


        }

        public string editar_ColegioSP(string rut_col, string nombre, int comuna, string direccion, string telefono, string email, string sitio, string logo_dir)
        {
 
 
            string guardar = "";
            //abrimos conexion
            abrirConexion();
            //instanciamos ejecucion en bd
            SqlCommand cmd = new SqlCommand();
            //buscamos el procedimiento modificarColegio
            cmd.CommandText = "modificarColegio";
            cmd.Connection = con;
            //indicamos que es procedimiento almacenado
            cmd.CommandType = CommandType.StoredProcedure;
            //entregamos parametros a procedimiento almacenado
            cmd.Parameters.Add(new SqlParameter("@rut_colegio", rut_col));
            cmd.Parameters.Add(new SqlParameter("@nombre_colegio", nombre));
            cmd.Parameters.Add(new SqlParameter("@id_comuna", comuna));
            cmd.Parameters.Add(new SqlParameter("@direccion", direccion));
            cmd.Parameters.Add(new SqlParameter("@telefono", telefono));
            cmd.Parameters.Add(new SqlParameter("@email", email));
            cmd.Parameters.Add(new SqlParameter("@sitio_web", sitio));
            cmd.Parameters.Add(new SqlParameter("@logo_dir", logo_dir));

            //ejecutamos el procedimiento
            cmd.ExecuteNonQuery();

 
            //cerramos la conexion
            cerrarConexion();

            guardar = "Colegio modificado correctamente";
            return guardar;


        }

        public string ingresar_DiscaSP(string tipo_discapacidad, string descripcion)
        {
 
            string guardar = "";
            //abrimos conexion
            abrirConexion();
            //instanciamos ejecucion en bd
            SqlCommand cmd = new SqlCommand();
            //buscamos el procedimiento agregarDiscapacidad
            cmd.CommandText = "agregarDiscapacidad";
            cmd.Connection = con;
            //indicamos que es procedimiento almacenado
            cmd.CommandType = CommandType.StoredProcedure;
            //entregamos parametros a procedimiento almacenado
            cmd.Parameters.Add(new SqlParameter("@tipo_discapacidad", tipo_discapacidad));
            cmd.Parameters.Add(new SqlParameter("@descripcion", descripcion));
             
            //ejecutamos el procedimiento
            cmd.ExecuteNonQuery();


            //cerramos la conexion
            cerrarConexion();

            guardar = "Discapacidad ingresada correctamente";
            return guardar;
 
        }

        public string modificar_DiscaSP(int id_tipo_discapacidad, string tipo_discapacidad, string descripcion)
        {

            string guardar = "";
            //abrimos conexion
            abrirConexion();
            //instanciamos ejecucion en bd
            SqlCommand cmd = new SqlCommand();
            //buscamos el procedimiento agregarDiscapacidad
            cmd.CommandText = "modificarDiscapacidad";
            cmd.Connection = con;
            //indicamos que es procedimiento almacenado
            cmd.CommandType = CommandType.StoredProcedure;
            //entregamos parametros a procedimiento almacenado
            cmd.Parameters.Add(new SqlParameter("@id_tipo_discapacidad", id_tipo_discapacidad));
            cmd.Parameters.Add(new SqlParameter("@tipo_discapacidad", tipo_discapacidad));
            cmd.Parameters.Add(new SqlParameter("@descripcion", descripcion));

            //ejecutamos el procedimiento
            cmd.ExecuteNonQuery();


            //cerramos la conexion
            cerrarConexion();

            guardar = "Discapacidad modificada correctamente";
            return guardar;

        }

        public string encrypt(string encryptString)
        {
            // usar
            // string encryptedString = Encrypt(sourceString);   


            string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {  
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76  
        });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    encryptString = Convert.ToBase64String(ms.ToArray());
                }
            }
            return encryptString;
        }

        public string Decrypt(string cipherText)
        {

            // usar
            // string decryptedString = Decrypt(encryptedString); 

            string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {  
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76  
        });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        public string desvincular_curso(string id_asignar_curso)
        {
            string guardar = "";

            SqlConnection sqlConnection1 = new SqlConnection(@"Data Source = " + DSource + "; Initial Catalog = " + NameCatalogo + "; User ID = " + NameUsuario + "; password = " + ClaveUsuario);
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "delete from Asignar_curso where id_asignar_curso = " + id_asignar_curso;
             
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            guardar = "El profesor ha sido desvinculado del curso.";
            return guardar;
        }

        public string ContadorSesion(int id_usuario, DateTime hora_inicio, DateTime hora_termino, int tiempo_total_sesion, int cantidad_acciones)
        {
            string guardar = "";
            //abrimos conexion
            abrirConexion();
            //instanciamos ejecucion en bd
            SqlCommand cmd = new SqlCommand();
            //buscamos el procedimiento agregarContador
            cmd.CommandText = "agregarContador";
            cmd.Connection = con;
            //indicamos que es procedimiento almacenado
            cmd.CommandType = CommandType.StoredProcedure;
            //entregamos parametros a procedimiento almacenado
            cmd.Parameters.Add(new SqlParameter("@id_usuario", id_usuario));
            cmd.Parameters.Add(new SqlParameter("@hora_inicio", hora_inicio));
            cmd.Parameters.Add(new SqlParameter("@hora_termino", hora_termino));
            cmd.Parameters.Add(new SqlParameter("@tiempo_total_sesion", tiempo_total_sesion));
            cmd.Parameters.Add(new SqlParameter("@cantidad_acciones", cantidad_acciones));
            //ejecutamos el procedimiento
            cmd.ExecuteNonQuery();


            //cerramos la conexion
            cerrarConexion();

            guardar = "Contador agregado";
            return guardar;
        
        }

        public string ControlUso(int id_usuario_a_controlar, int id_usuario_controlador, int cantidad_horas, int cantidad_acciones)
        {
            string guardar = "";
            //abrimos conexion
            abrirConexion();
            //instanciamos ejecucion en bd
            SqlCommand cmd = new SqlCommand();
            //buscamos el procedimiento agregarControlUso
            cmd.CommandText = "agregarControlUso";
            cmd.Connection = con;
            //indicamos que es procedimiento almacenado
            cmd.CommandType = CommandType.StoredProcedure;
            //entregamos parametros a procedimiento almacenado
            cmd.Parameters.Add(new SqlParameter("@id_usuario_a_controlar", id_usuario_a_controlar));
            cmd.Parameters.Add(new SqlParameter("@id_usuario_controlador", id_usuario_controlador));
            cmd.Parameters.Add(new SqlParameter("@cantidad_horas", cantidad_horas));
            cmd.Parameters.Add(new SqlParameter("@cantidad_acciones", cantidad_acciones)); 
            //ejecutamos el procedimiento
            cmd.ExecuteNonQuery();


            //cerramos la conexion
            cerrarConexion();

            guardar = "Control agregado";
            return guardar;

        }

        public string AgregarTicketSP(DateTime f_apertura, DateTime f_vencimiento, string rut_colegio, int id_usuario, int id_modulo, int id_subcategoria, int id_estado_ticket, string comentario_usuario, string asunto)
        {

            int id_prioridad_ticket = 4;// pendiente
            string guardar = "";
            //abrimos conexion
            abrirConexion();
            //instanciamos ejecucion en bd
            SqlCommand cmd = new SqlCommand();
            //buscamos el procedimiento agregarTicket
            cmd.CommandText = "agregarTicket";
            cmd.Connection = con;
            //indicamos que es procedimiento almacenado
            cmd.CommandType = CommandType.StoredProcedure;
            //entregamos parametros a procedimiento almacenado
            cmd.Parameters.Add(new SqlParameter("@f_apertura", f_apertura));
            cmd.Parameters.Add(new SqlParameter("@f_vencimiento", f_vencimiento));
            cmd.Parameters.Add(new SqlParameter("@rut_colegio", rut_colegio));
            cmd.Parameters.Add(new SqlParameter("@id_usuario", id_usuario));
            cmd.Parameters.Add(new SqlParameter("@id_modulo", id_modulo));
            cmd.Parameters.Add(new SqlParameter("@id_subcategoria", id_subcategoria));
            cmd.Parameters.Add(new SqlParameter("@id_estado_ticket", id_estado_ticket));
            cmd.Parameters.Add(new SqlParameter("@comentario_usuario", comentario_usuario));
            cmd.Parameters.Add(new SqlParameter("@asunto", asunto));
            cmd.Parameters.Add(new SqlParameter("@id_prioridad_ticket", id_prioridad_ticket));
            //ejecutamos el procedimiento
            cmd.ExecuteNonQuery();


            //cerramos la conexion
            cerrarConexion();

            guardar = "Ticket agregado";
            return guardar;

        }

        public string editar_TicketSP(int id_ticket, DateTime f_vencimiento, int id_tecnico_asignado, int id_prioridad_ticket)
        {

            int id_estado_ticket = 2; //en desarrollo

            string guardar = "";
            //abrimos conexion
            abrirConexion();
            //instanciamos ejecucion en bd
            SqlCommand cmd = new SqlCommand();
            //buscamos el procedimiento modificarTicket
            cmd.CommandText = "modificarTicket";
            cmd.Connection = con;
            //indicamos que es procedimiento almacenado
            cmd.CommandType = CommandType.StoredProcedure;
            //entregamos parametros a procedimiento almacenado
            cmd.Parameters.Add(new SqlParameter("@id_ticket", id_ticket));
            cmd.Parameters.Add(new SqlParameter("@f_vencimiento", f_vencimiento));
            cmd.Parameters.Add(new SqlParameter("@id_tecnico_asignado", id_tecnico_asignado));
            cmd.Parameters.Add(new SqlParameter("@id_prioridad_ticket", id_prioridad_ticket));
            cmd.Parameters.Add(new SqlParameter("@id_estado_ticket", id_estado_ticket)); 
            //ejecutamos el procedimiento
            cmd.ExecuteNonQuery();

            //cerramos la conexion
            cerrarConexion();

            guardar = "Ticket modificado correctamente";
            return guardar;


        }

        public string editar_TicketTECNSP(int id_ticket, string resolucion_conflicto, DateTime f_resolucion)
        {

            int id_estado_ticket = 3; //en desarrollo

            string guardar = "";
            //abrimos conexion
            abrirConexion();
            //instanciamos ejecucion en bd
            SqlCommand cmd = new SqlCommand();
            //buscamos el procedimiento modificarTicketTecnico
            cmd.CommandText = "modificarTicketTecnico";
            cmd.Connection = con;
            //indicamos que es procedimiento almacenado
            cmd.CommandType = CommandType.StoredProcedure;
            //entregamos parametros a procedimiento almacenado
            cmd.Parameters.Add(new SqlParameter("@id_ticket", id_ticket)); 
            cmd.Parameters.Add(new SqlParameter("@id_estado_ticket", id_estado_ticket));
            cmd.Parameters.Add(new SqlParameter("@resolucion_conflicto", resolucion_conflicto));
            cmd.Parameters.Add(new SqlParameter("@f_resolucion", f_resolucion));
            //ejecutamos el procedimiento
            cmd.ExecuteNonQuery();

            //cerramos la conexion
            cerrarConexion();

            guardar = "Ticket resuelto correctamente";
            return guardar;


        }

        public string AgregarSugerenciaSP(DateTime f_llegada, string rut_colegio, int id_usuario, string asunto, string comentario)
        {

             
            string guardar = "";
            //abrimos conexion
            abrirConexion();
            //instanciamos ejecucion en bd
            SqlCommand cmd = new SqlCommand();
            //buscamos el procedimiento agregarSugerencia
            cmd.CommandText = "agregarSugerencia";
            cmd.Connection = con;
            //indicamos que es procedimiento almacenado
            cmd.CommandType = CommandType.StoredProcedure;
            //entregamos parametros a procedimiento almacenado
            cmd.Parameters.Add(new SqlParameter("@f_llegada", f_llegada));
            cmd.Parameters.Add(new SqlParameter("@rut_colegio", rut_colegio));
            cmd.Parameters.Add(new SqlParameter("@id_usuario", id_usuario));
            cmd.Parameters.Add(new SqlParameter("@asunto", asunto));
            cmd.Parameters.Add(new SqlParameter("@comentario", comentario)); 
            //ejecutamos el procedimiento
            cmd.ExecuteNonQuery();


            //cerramos la conexion
            cerrarConexion();

            guardar = "Sugerencia agregada";
            return guardar;

        }
    }

}