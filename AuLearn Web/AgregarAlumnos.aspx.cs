using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AuLearn_Web
{
    public partial class AgregarAlumnos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             
            divOtrosCargos.Visible = true;
            btnCancelar.Visible = false;
            txtRut.Visible = true;
           
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
                    int cargo = 1004;
                    int tipoDisca = Convert.ToInt32(DropDownList2.SelectedValue);
                    con.editar_PersonaSP(txtRut.Text, txtNombre.Text, txtApellido.Text, txtcalendario.Text, cargo);
                    con.editar_alumnoP(txtRut.Text, txtNombre.Text, txtApellido.Text, txtcalendario.Text, cargo, tipoDisca);

                    Response.Write("<script>window.alert('El alumno se editó correctamente.');</script>");
                    Response.Redirect(Request.RawUrl);
                     
                     
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
                    int cargo = 1004;
                    con.insertar_personaSP(txtRut.Text, txtNombre.Text, txtApellido.Text, txtcalendario.Text, cargo);

                    int tipoDisca = Convert.ToInt32(DropDownList2.SelectedValue);
                    //con.insertar_alumno(txtRut.Text, txtNombre.Text, txtApellido.Text, txtcalendario.Text, cargo, tipoDisca);
                    con.insertar_alumnoSP(txtRut.Text, tipoDisca);
                    int id_tipo_usuario = 1003; //otro
                    //con.insertar_UsuarioSP(txtRut.Text, usuario, contraseñaDefault, id_tipo_usuario, txtCorreo.Text, TextFono.Text);

                    Response.Write("<script>window.alert('Se añadió el usuario correctamente.');</script>");
                    Response.Redirect(Request.RawUrl);
                     


                }
            }

           
        }
 

        protected void btnCancelar_Click(object sender, EventArgs e)
        { 
            Response.Redirect(Request.RawUrl);
        }

        protected void GridViewAlumnos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Estado")
            {
                txtRut.Visible = false;
                txtRutOculto.Visible = true;
                
                int index = Convert.ToInt32(e.CommandArgument);

                GridViewRow selectedRow = GridViewAlumnos.Rows[index];
                TableCell rut = selectedRow.Cells[0];
                TableCell estadoG = selectedRow.Cells[6];

                txtRut.Text = rut.Text;
                txtRutOculto.Text = rut.Text;
                string estado = estadoG.Text;

                Conexion con = new Conexion();

                if (estado == "Activo")
                {
                    bool activo = false;
                    con.actdesPersonaSP(rut.Text, activo);
                    GridViewAlumnos.DataBind();
                    //Response.Redirect(Request.RawUrl);
                }
                else
                {
                    bool activo = true;
                    con.actdesPersonaSP(rut.Text, activo);
                    GridViewAlumnos.DataBind();
                    //Response.Redirect(Request.RawUrl);
                }


            }
            else if (e.CommandName == "Editar")
            {
                //LLENAR TEXTBOXS
                labelTitulo.Text = "Editando Alumno";
                btnAgregar.Text = "Editar";
                btnCancelar.Visible = true;
                txtRut.Visible = false;
                txtRutOculto.Visible = true;

                int index = Convert.ToInt32(e.CommandArgument);

                // Get the last name of the selected author from the appropriate
                // cell in the GridView control.
                GridViewRow selectedRow = GridViewAlumnos.Rows[index];
                TableCell rutAlumno = selectedRow.Cells[0];
                TableCell Nombres = selectedRow.Cells[1];
                TableCell Apellidos = selectedRow.Cells[2];
                TableCell FechaNAC = selectedRow.Cells[3];
                TableCell Discapacidad = selectedRow.Cells[5];


                txtRut.Text = rutAlumno.Text;
                txtRutOculto.Text = rutAlumno.Text;
                txtNombre.Text = HttpUtility.HtmlDecode(Nombres.Text);
                txtApellido.Text = HttpUtility.HtmlDecode(Apellidos.Text);
                txtcalendario.Text = Convert.ToDateTime(FechaNAC.Text).ToString("yyyy-MM-dd");

                DropDownList2.SelectedValue = DropDownList2.Items.FindByText(HttpUtility.HtmlDecode(Discapacidad.Text)).Value;

            }

        }

        protected void GridViewAlumnos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
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
    }
}