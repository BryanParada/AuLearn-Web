<%@ Page Title="" Language="C#" MasterPageFile="~/SitioM.Master" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="AuLearn_Web.Perfil" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BreadCrumb" runat="server">
     <i class="fa fa-user"></i> Mi Perfil
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
       <!DOCTYPE html>


    <div class="row">
        <div class="col-lg-5" runat="server">

            <div class="card mb-5">
                <div class="card-header">
                    <i class="fa fa-user" style="color: green"></i><asp:label runat="server" id="labelTitulo"> Mi Perfil</asp:label>
                </div>
                <div class="card-body">
                    <%--rut--%>
                    <asp:label runat="server" id="labelID" Visible="false"></asp:label>
                     <asp:label runat="server" id="labelCargo" Visible="false"></asp:label>

                    <div class="row">
                        <div class="col-lg-11">

                            <label>Nombres</label>
                            <asp:TextBox ID="txtNombre" runat="server" class="form-control" placeholder="Nombres"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorNombre"
                                ControlToValidate="txtNombre"
                                Display="Static"
                                ErrorMessage="Debe ingresar un nombre "
                                runat="server"
                                ForeColor="Red" />

                            <br />
                            
                            <label>Apellidos</label>
                            <asp:TextBox ID="txtApellido" runat="server" class="form-control" placeholder="Apellidos"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorApellido"
                                ControlToValidate="txtApellido"
                                Display="Static"
                                ErrorMessage="Debe ingresar un apellido "
                                runat="server"
                                ForeColor="Red" />
                             
                            <br />
                            <label>Fecha de nacimiento</label>
                            <asp:TextBox ID="txtcalendario" runat="server" class="form-control" TextMode="Date"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorFecha"
                                ControlToValidate="txtcalendario"
                                Display="Static"
                                ErrorMessage="Debe ingresar una Fecha válida"
                                runat="server"
                                ForeColor="Red" />
                            <br />

                            <label>Nombre de usuario</label>
                            <asp:TextBox ID="txtUsuario" runat="server" class="form-control" disabled="true"></asp:TextBox> 
                            <br />

                             <label>Email</label>
                            <div class="form-group input-group">
                                <span class="input-group-addon">@</span>
                                <asp:TextBox ID="txtMail" runat="server" class="form-control" placeholder="tucorreo@ejemplo.com"></asp:TextBox>
                            </div>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidatrorEmail"
                                ControlToValidate="txtMail"
                                Display="Static"
                                ErrorMessage="Debe ingresar un correo"
                                runat="server" ForeColor="Red" />
                            <br />
                            <label>Teléfono</label>
                        <asp:TextBox ID="txtTelefono" runat="server" class="form-control" placeholder="Ejemplo: 9 1234 5678" MaxLength="10"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtTelefono" ErrorMessage="Ingrese sólo numeros" ForeColor="Red" ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>
                        <br />
                        </div>
                    </div>



                    <div class="row">
                        <div class="col-lg-3">
                             <asp:Button ID="btnActualizar" OnClick="btnActualizar_Click" runat="server" Text="Actualizar" class="btn btn-lg btn-primary btn-block" />
                        </div> 
                    </div>
                    <br />





                </div>
            </div>

        </div>
        
           </div>


</asp:Content>
