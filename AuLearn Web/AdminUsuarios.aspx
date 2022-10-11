<%@ Page Title="" Language="C#" MasterPageFile="~/SitioM.Master" AutoEventWireup="true" CodeBehind="AdminUsuarios.aspx.cs" Inherits="AuLearn_Web.Faspx.AdminUsuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BreadCrumb" runat="server">
    Administración Usuarios
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
    <!DOCTYPE html>
  
     
       
        <div class="row">
            <div class="col-lg-5">

                <asp:label runat="server" id="labelTitulo" class="h2" >Nuevo Usuario</asp:label>
                <br /><br />
                 

                <asp:TextBox ID="txtRut" runat="server" class="form-control" placeholder="Rut" ></asp:TextBox>
                <asp:TextBox ID="txtRutOculto" Visible="false" disabled="true" runat="server" class="form-control" placeholder="Rut" ></asp:TextBox><br />

                <label for="inputPersona" class="sr-only">Nombres</label>
                <asp:TextBox ID="txtNombre" runat="server" class="form-control col-xs-3" placeholder="Nombres" ></asp:TextBox><br />

                <label for="inputPersona" class="sr-only">Apellidos</label>
                <asp:TextBox ID="txtApellido" runat="server" class="form-control" placeholder="Apellidos"></asp:TextBox><br />

                <label for="inputPersona" class="sr-only">Fecha de Nacimiento</label>
                <asp:TextBox ID="txtcalendario" runat="server" class="form-control" TextMode="Date" ></asp:TextBox><br /> 

                <div id="divCargo">
                <label>Cargo </label>
                <br />
                <label for="inputPersona" class="sr-only">Cargo</label>
                <asp:DropDownList ID="DropDownList1" runat="server" class="form-control" DataSourceID="SqlDataSource1" DataTextField="cargo" DataValueField="id_cargo"></asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:aulearnConnectionString %>" SelectCommand="SELECT [id_cargo], [cargo] FROM [Cargo]"></asp:SqlDataSource>
                <br />
                </div>

                <label>Tipo de Discapacidad</label><br />
                <label for="inputPersona" class="sr-only">Tipo Discapacidad</label>
                <asp:DropDownList ID="DropDownList2" runat="server" class="form-control" DataSourceID="SqlDataSourceT" DataTextField="tipo_discapacidad" DataValueField="id_tipo_discapacidad"></asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSourceT" runat="server" ConnectionString="<%$ ConnectionStrings:aulearnConnectionString %>" SelectCommand="SELECT [id_tipo_discapacidad], [tipo_discapacidad] FROM [Tipo_discapacidad]"></asp:SqlDataSource>
                <br />

                <div class="row">
                    <div class="col-lg-3">
                        <asp:Button ID="btnAgregar"   runat="server" Text="Agregar" class="btn btn-lg btn-primary btn-block" />
                    </div>
                    <div class="col-lg-3">
                        <asp:Button ID="btnCancelar"  runat="server" Text="Cancelar" class="btn btn-lg btn-danger btn-block" />
                    </div>
                </div> 
                <br />
            </div>

            <div class="col-lg-6">
                <h2>Listado Usuarios</h2>
                <br />

                 

                
                
            </div>
        </div>
 
   

</asp:Content>

