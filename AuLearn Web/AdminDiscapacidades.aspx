<%@ Page Title="" Language="C#" MasterPageFile="~/SitioM.Master" AutoEventWireup="true" CodeBehind="AdminDiscapacidades.aspx.cs" Inherits="AuLearn_Web.AdminDiscapacidades" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BreadCrumb" runat="server">
    <i class="fa fa-wheelchair"></i>  Administrar Discapacidades
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">

    <!DOCTYPE html>


    <div class="row">
        <div class="col-lg-5" runat="server">

            <div class="card mb-5">
                <div class="card-header">
                    <i class="fa fa-wheelchair" style="color: green"></i><asp:label runat="server" id="labelTitulo"> Administración Discapacidades</asp:label>
                </div>
                <div class="card-body">


                    <div class="row">
                        <div class="col-lg-11">

                            <label>Discapacidad</label>
                            <asp:TextBox ID="txtNombre" runat="server" class="form-control" placeholder="Nombre de la Discapacidad"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorRut"
                                ControlToValidate="txtNombre"
                                Display="Static"
                                ErrorMessage="Debe ingresar un nombre "
                                runat="server"
                                ForeColor="Red" />

                            <br />
                            <asp:label runat="server" id="labelID" Visible="false"></asp:label>

                            <label>Descripción de la Discapacidad</label>
                            <asp:TextBox ID="txtDesc" runat="server" class="form-control" TextMode="multiline" Columns="50" Rows="5" placeholder="Descripción..."></asp:TextBox><br />
                            <br />

                        </div>
                    </div>



                    <div class="row">
                        <div class="col-lg-3">
                            <asp:Button ID="btnAgregar" OnClick="btnAgregar_Click" runat="server" Text="Agregar" class="btn btn-lg btn-primary btn-block" />
                        </div>
                        <div class="col-lg-3">
                            <asp:Button ID="btnCancelar" OnClick="btnCancelar_Click" runat="server" Text="Cancelar" class="btn btn-lg btn-danger btn-block" />
                        </div>
                    </div>
                    <br />





                </div>
            </div>

        </div>
        <div class="col-lg-7" runat="server">
        <div class="card mb-5">
            <div class="card-header">
                <i class="fa fa-wheelchair" style="color: green"></i> Listado Discapacidades
            </div>
            <div class="card-body">


                <div class="col-lg-10">

                    <br />
                    <asp:GridView ID="GridDisca" runat="server"
                        class="table table-bordered table-hover table-striped"
                        AllowPaging="True"
                        PageSize="6"
                        AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="id_tipo_discapacidad" DataSourceID="SqlDataSourceDiscapacidades"
                        OnRowCommand="GridDisca_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="id_tipo_discapacidad" HeaderText="Identificador" InsertVisible="False" ReadOnly="True" SortExpression="id_tipo_discapacidad" />
                            <asp:BoundField DataField="tipo_discapacidad" HeaderText="Tipo Discapacidad" SortExpression="tipo_discapacidad" />
                            <asp:BoundField DataField="descripcion" HeaderText="Descripción" SortExpression="descripcion" HeaderStyle-Width="100%"/>
                            <asp:ButtonField ButtonType="Button" CommandName="Editar" HeaderText="Editar" Text="Editar" ControlStyle-CssClass="btn btn-primary btn-block"  />
                        </Columns>

                    </asp:GridView>



                    <asp:SqlDataSource ID="SqlDataSourceDiscapacidades" runat="server" ConnectionString="<%$ ConnectionStrings:aulearnConnectionString %>" SelectCommand="SELECT * FROM [Tipo_discapacidad]"></asp:SqlDataSource>

                </div>
            </div>

        </div>
    </div>
           </div>

    <br />

</asp:Content>
