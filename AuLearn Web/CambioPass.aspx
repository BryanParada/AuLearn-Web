<%@ Page Title="" Language="C#" MasterPageFile="~/SitioM.Master" AutoEventWireup="true" CodeBehind="CambioPass.aspx.cs" Inherits="AuLearn_Web.CambioPass" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BreadCrumb" runat="server">
    Cambio de Contraseña
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
     <div class="row">
       <div class="col-lg-7" runat="server">
        <div class="card mb-5">
            <div class="card-header">
                <i class="fa fa-user" style="color: green"></i> Cambio de Contraseña
            </div>
            
            <div class="card-body">
                <%--rut--%>
                    <asp:label runat="server" id="labelID" Visible="false"></asp:label>
                <asp:label runat="server" id="labelVal" Visible="false" ForeColor="Red"></asp:label>

                <div class="col-lg-10">
                    <label>Contraseña Actual</label>
                            <asp:TextBox ID="passActual" runat="server" class="form-control" placeholder="Contraseña Actual" type="password"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                ControlToValidate="passActual"
                                Display="Static"
                                ErrorMessage="Debe ingresar Contraseña Actual "
                                runat="server"
                                ForeColor="Red" />

                            <br />
                    <label>Nueva Contraseña</label>
                            <asp:TextBox ID="txtNuevaPass" runat="server" class="form-control" placeholder="Nueva Contraseña" type="password"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                ControlToValidate="txtNuevaPass"
                                Display="Static"
                                ErrorMessage="Debe ingresar una Contraseña Nueva "
                                runat="server"
                                ForeColor="Red" />

                            <br />
                    <label>Confirmar Contraseña</label>
                            <asp:TextBox ID="txtConfiPass" runat="server" class="form-control" placeholder="Confirmar Contraseña" type="password"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                ControlToValidate="txtConfiPass"
                                Display="Static"
                                ErrorMessage="Dece confirmar su contraseña "
                                runat="server"
                                ForeColor="Red" />

                            <br />  
                    <div class="row">
                        <div class="col-lg-5">
                             <asp:Button ID="btnCambio" OnClick="btnCambio_Click" runat="server" Text="Cambio de Contraseña" class="btn btn-lg btn-primary btn-block" />
                        </div> 
                    </div>


                    <br />
                    <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="CustomValidator" ControlToValidate="passActual"></asp:CustomValidator>
                   <br />



                     </div>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
