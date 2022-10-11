using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AuLearn_Web
{
    public partial class AgregarPersonas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            divAlumno.Visible = false;
            divOtrosCargos.Visible = true;
            btnCancelar.Visible = false;
        }

        protected void DropDownListCargo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownListCargo.SelectedItem.ToString() == "Alumno")
            {
                divAlumno.Visible = true;
                divOtrosCargos.Visible = false;
            }
            else
            {
                divAlumno.Visible = false;
                divOtrosCargos.Visible = true;
            }

        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {


            Conexion con = new Conexion();

            //EDITAAAAR
            if (btnAgregar.Text == "Editar")
            {
                Boolean resul = con.validarRut(txtRut.Text);
                if (resul == false)
                {

                    Response.Write("<script>window.alert('Rut inválido, por favor ingrese un rut válido');</script>");
                }
                else
                {

                    DataTable tabla = con.selectDatosPersona(txtRut.Text);

                    if (tabla.Rows.Count > 0)
                    {
                        string cargoActual = tabla.Rows[0]["cargo"].ToString();
                        string dropCargo = DropDownListCargo.SelectedItem.ToString();

                        if (dropCargo == "Alumno" && cargoActual == "Alumno")
                        {
                        int cargoE = Convert.ToInt32(DropDownListCargo.SelectedValue);
                        int tipoDisca = Convert.ToInt32(DropDownList2.SelectedValue);
                        con.editar_PersonaSP(txtRut.Text, txtNombre.Text, txtApellido.Text, txtcalendario.Text, cargoE);
                        con.editar_alumnoP(txtRut.Text, txtNombre.Text, txtApellido.Text, txtcalendario.Text, cargoE, tipoDisca);

                        Response.Write("<script>window.alert('El alumno se editó correctamente.');</script>");
                        Response.Redirect(Request.RawUrl);
                        }
                        else if ((dropCargo == "Profesor" && cargoActual == "Profesor") || (dropCargo == "Director" && cargoActual == "Director") || (dropCargo == "Profesor" && cargoActual == "Director") || (dropCargo == "Director" && cargoActual == "Profesor"))
                        {
                             
                            int cargo = Convert.ToInt32(DropDownListCargo.SelectedValue);

                            //se edita persona
                            string mensaje = con.editar_PersonaSP(txtRut.Text, txtNombre.Text, txtApellido.Text, txtcalendario.Text, cargo);

                            //se dividen nombres
                            string[] tokens = txtNombre.Text.Split(' ');
                            string primerNombre = tokens[0];

                            string[] tokens2 = txtApellido.Text.Split(' ');
                            string primerApellido = tokens2[0];

                            string usuario = primerNombre + "." + primerApellido;

                            string contraseñaDefault = "1580";

                            if (DropDownListCargo.SelectedItem.ToString() == "Director")
                            {
                                int id_tipo_usuario = 2;
                                con.modificar_UsuarioSP(txtRut.Text, usuario, id_tipo_usuario, txtCorreo.Text, TextFono.Text);

                                Response.Write("<script>window.alert('" + mensaje + "');</script>");
                                Response.Redirect(Request.RawUrl);
                            }
                            else if (DropDownListCargo.SelectedItem.ToString() == "Profesor")
                            {
                                int id_tipo_usuario = 3;
                                con.modificar_UsuarioSP(txtRut.Text, usuario, id_tipo_usuario, txtCorreo.Text, TextFono.Text);

                                Response.Write("<script>window.alert('" + mensaje + "');</script>");
                                Response.Redirect(Request.RawUrl);
                            }
                        }
                        else if (dropCargo == "Alumno" && (cargoActual == "Profesor" || cargoActual == "Director")) //si se quiere transformar de director/profe a ->alumno
                        {
                        //si se quiere cambiar de profesor/director a alumno
                        //deberia borrarse el usuario asociado al profesor/director?

                            int cargoE = Convert.ToInt32(DropDownListCargo.SelectedValue);
                            //se edita persona
                            string mensaje = con.editar_PersonaSP(txtRut.Text, txtNombre.Text, txtApellido.Text, txtcalendario.Text, cargoE);


                            int tipoDisca = Convert.ToInt32(DropDownList2.SelectedValue);
                            //con.insertar_alumno(txtRut.Text, txtNombre.Text, txtApellido.Text, txtcalendario.Text, cargo, tipoDisca);
                            con.insertar_alumnoSP(txtRut.Text, tipoDisca);
 
                            //se dividen nombres
                            string[] tokens = txtNombre.Text.Split(' ');
                            string primerNombre = tokens[0];

                            string[] tokens2 = txtApellido.Text.Split(' ');
                            string primerApellido = tokens2[0];

                            string usuario = primerNombre + "." + primerApellido;

                            string contraseñaDefault = "1580";
 
                            int id_tipo_usuario = 1003; //otro

                            con.modificar_UsuarioSP(txtRut.Text, usuario, id_tipo_usuario, txtCorreo.Text, TextFono.Text);
                             
                            Response.Write("<script>window.alert('Advertencia, no es posible convertir un '"+cargoActual+"' en '"+dropCargo+"');</script>");

                        }
                        else if ((dropCargo == "Profesor" || dropCargo == "Director") && (cargoActual == "Alumno"))//si se quiere transformar alumno a director/profe
                        {

                            int cargo2 = Convert.ToInt32(DropDownListCargo.SelectedValue);

                            //se edita persona
                            string mensaje = con.editar_PersonaSP(txtRut.Text, txtNombre.Text, txtApellido.Text, txtcalendario.Text, cargo2);

                            //se dividen nombres
                            string[] tokens = txtNombre.Text.Split(' ');
                            string primerNombre = tokens[0];

                            string[] tokens2 = txtApellido.Text.Split(' ');
                            string primerApellido = tokens2[0];

                            string usuario = primerNombre + "." + primerApellido;

                            string contraseñaDefault = "1580";

                           
                            int tipoDisca = Convert.ToInt32(DropDownList2.SelectedValue);
                            int id_tipo_usuario = 1003; //otro

                            con.modificar_UsuarioSP(txtRut.Text, usuario, id_tipo_usuario, txtCorreo.Text, TextFono.Text);
                            //con.editar_alumnoP(txtRut.Text, txtNombre.Text, txtApellido.Text, txtcalendario.Text, cargoE, tipoDisca);
                            Response.Write("<script>window.alert('Advertencia, no es posible convertir un '" + cargoActual + "' en '" + dropCargo + "');</script>");
                        }
                    }

                    
                }

            }
            else //AGREEEGAR
            {

                Boolean resul = con.validarRut(txtRut.Text);
                if (resul == false)
                {

                    Response.Write("<script>window.alert('Rut inválido, por favor ingrese un rut válido');</script>");
                }
                else
                {
                     
                        int cargo = Convert.ToInt32(DropDownListCargo.SelectedValue);

                        //se inserta persona
                        string mensaje = con.insertar_personaSP(txtRut.Text, txtNombre.Text, txtApellido.Text, txtcalendario.Text, cargo);

                        //se dividen nombres
                        string[] tokens = txtNombre.Text.Split(' ');
                        string primerNombre = tokens[0];

                        string[] tokens2 = txtApellido.Text.Split(' ');
                        string primerApellido = tokens2[0];

                        string usuario = primerNombre + "." + primerApellido;

                        string contraseñaDefault = "aulearn";
                        int _min = 1000;
                        int _max = 9999;
                        Random _rdm = new Random();
                        string contraseñaRandom = Convert.ToString(_rdm.Next(_min, _max));

                        if (DropDownListCargo.SelectedItem.ToString() == "Director")
                        {
                            int id_tipo_usuario = 2;
                            con.insertar_UsuarioSP(txtRut.Text, usuario, contraseñaDefault, id_tipo_usuario, txtCorreo.Text, TextFono.Text);

                            Response.Write("<script>window.alert('" + mensaje + "');</script>");
                            Response.Redirect(Request.RawUrl);
                        }
                        else if (DropDownListCargo.SelectedItem.ToString() == "Profesor")
                        {
                            int id_tipo_usuario = 3;
                            con.insertar_UsuarioSP(txtRut.Text, usuario, contraseñaDefault, id_tipo_usuario, txtCorreo.Text, TextFono.Text);
                            con.SendMailBienvenida(txtNombre.Text, usuario, contraseñaDefault, txtCorreo.Text);
                            Response.Write("<script>window.alert('" + mensaje + "');</script>");
                            Response.Redirect(Request.RawUrl);
                        }
                        else if (DropDownListCargo.SelectedItem.ToString() == "Alumno")
                        {
                        
                        int tipoDisca = Convert.ToInt32(DropDownList2.SelectedValue);
                        //con.insertar_alumno(txtRut.Text, txtNombre.Text, txtApellido.Text, txtcalendario.Text, cargo, tipoDisca);
                        con.insertar_alumnoSP(txtRut.Text, tipoDisca);
                        int id_tipo_usuario = 1003; //otro
                        con.insertar_UsuarioSP(txtRut.Text, usuario, contraseñaDefault, id_tipo_usuario, txtCorreo.Text, TextFono.Text);

                        Response.Write("<script>window.alert('Se añadió el usuario correctamente.');</script>");
                        Response.Redirect(Request.RawUrl);
                        }


                   
                }
            }



        }

        protected void GridPersonas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView GridView = e.Row.DataItem as DataRowView;

                if (Convert.ToString(GridView["Cargo"]) == "Alumno")
                {

                    e.Row.Cells[5].BackColor = System.Drawing.ColorTranslator.FromHtml("#c9ddff");
                    // e.Row.BackColor = System.Drawing.Color.Red;
                }
                else if (Convert.ToString(GridView["Cargo"]) == "Profesor")
                {

                    e.Row.Cells[5].BackColor = System.Drawing.ColorTranslator.FromHtml("#e3f7e5");

                    // e.Row.BackColor = System.Drawing.Color.Green;
                }
                else if (Convert.ToString(GridView["Cargo"]) == "Director")
                {

                    e.Row.Cells[5].BackColor = System.Drawing.ColorTranslator.FromHtml("#f7efe3");
                }

                if (e.Row.Cells[6].Text == "True")
                {

                    e.Row.Cells[6].Text = "Activo";
                    //e.Row.Cells[5].BackColor = System.Drawing.Color.Green;
                    e.Row.Cells[6].ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    e.Row.Cells[6].Text = "Inactivo";
                    e.Row.Cells[6].ForeColor = System.Drawing.Color.Red;
                }




            }

        }

        protected void GridPersonas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Estado")
            {
                int index = Convert.ToInt32(e.CommandArgument);

                GridViewRow selectedRow = GridPersonas.Rows[index];
                TableCell rut = selectedRow.Cells[0];
                TableCell estadoG = selectedRow.Cells[6];

                string estado = estadoG.Text;

                Conexion con = new Conexion();

                if (estado == "Activo")
                {
                    bool activo = false;
                    con.actdesPersonaSP(rut.Text, activo);
                    GridPersonas.DataBind();
                    //Response.Redirect(Request.RawUrl);
                }
                else
                {
                    bool activo = true;
                    con.actdesPersonaSP(rut.Text, activo);
                    GridPersonas.DataBind();
                    //Response.Redirect(Request.RawUrl);
                }


            }
            else if (e.CommandName == "Editar")
            {
                //LLENAR TEXTBOXS
                labelTitulo.Text = "Editando Persona";
                btnAgregar.Text = "Editar";
                btnCancelar.Visible = true;

                int index = Convert.ToInt32(e.CommandArgument);

                // Get the last name of the selected author from the appropriate
                // cell in the GridView control.
                GridViewRow selectedRow = GridPersonas.Rows[index];
                TableCell rutAlumno = selectedRow.Cells[0];
                TableCell Nombres = selectedRow.Cells[1];
                TableCell Apellidos = selectedRow.Cells[2];
                TableCell FechaNAC = selectedRow.Cells[3];
                TableCell Cargo = selectedRow.Cells[5];


                txtRut.Text = rutAlumno.Text;
                txtRutOculto.Text = rutAlumno.Text;
                txtNombre.Text = HttpUtility.HtmlDecode(Nombres.Text);
                txtApellido.Text = HttpUtility.HtmlDecode(Apellidos.Text);
                txtcalendario.Text = Convert.ToDateTime(FechaNAC.Text).ToString("yyyy-MM-dd"); 
 
                DropDownListCargo.SelectedValue = DropDownListCargo.Items.FindByText(Cargo.Text).Value;

                if (DropDownListCargo.SelectedItem.ToString() == "Alumno")
                {
                    divAlumno.Visible = true;
                    divOtrosCargos.Visible = false;
                }
                else
                {
                    divAlumno.Visible = false;
                    divOtrosCargos.Visible = true;
                }

                Conexion con = new Conexion();
                DataTable tablaUsuario = con.selectDatosUsuario(txtRut.Text);

                if (tablaUsuario.Rows.Count > 0)
                {
                    txtCorreo.Text = tablaUsuario.Rows[0]["email"].ToString();
                    TextFono.Text = tablaUsuario.Rows[0]["telefono"].ToString();
                }


            }



        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }




    }
}
