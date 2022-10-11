<%@ Page Title="" Language="C#" MasterPageFile="~/SitioM.Master" AutoEventWireup="true" CodeBehind="ResolverTicket.aspx.cs" Inherits="AuLearn_Web.ResolverTicket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BreadCrumb" runat="server">

    <i class="fa fa-check"></i>Resolver Ticket
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
     <div class="row">
        <div class="col-lg-12" runat="server">
            <asp:CheckBox Checked="true" ID="checkboxPrioridad" runat="server" Text="Mostrar sólo Pendientes" AutoPostBack="true" OnCheckedChanged="checkboxPrioridad_CheckedChanged"/>

            <br />

        </div>
    </div>

    <asp:Label ID="labelIDTecnico" runat="server" Visible="false"></asp:Label>

    <div class="row">
        <div class="col-lg-12" runat="server">


            <asp:GridView ID="GridTicketsTecnico" runat="server"
                class="table table-bordered table-hover table-striped"
                AllowPaging="True"
                PageSize="6"
                AllowSorting="True"
                AutoGenerateColumns="False"
                DataSourceID="SqlDataSourceTickets"
                OnRowCommand="GridTicketsTecnico_RowCommand"
                OnRowDataBound="GridTicketsTecnico_RowDataBound">

                <Columns>
                    <asp:BoundField DataField="ID Ticket" HeaderText="ID Ticket" ReadOnly="True" SortExpression="ID Ticket"></asp:BoundField>
                    <asp:BoundField DataField="Fecha Apertura" HeaderText="Fecha Apertura" ReadOnly="True" SortExpression="Fecha Apertura"></asp:BoundField>
                    <asp:BoundField DataField="Fecha Vencimiento" HeaderText="Fecha Vencimiento" ReadOnly="True" SortExpression="Fecha Vencimiento"></asp:BoundField>
                    <asp:BoundField DataField="Rut Colegio" HeaderText="Rut Colegio" SortExpression="Rut Colegio"></asp:BoundField>
                    <asp:BoundField DataField="Nombre usuario" HeaderText="Nombre usuario" ReadOnly="True" SortExpression="Nombre usuario"></asp:BoundField>
                    <asp:BoundField DataField="Prioridad" HeaderText="Prioridad" ReadOnly="True" SortExpression="Prioridad"></asp:BoundField>
                    <asp:BoundField DataField="M&#243;dulo" HeaderText="M&#243;dulo" SortExpression="M&#243;dulo"></asp:BoundField>
                    <asp:BoundField DataField="SubCategor&#237;a" HeaderText="SubCategor&#237;a" SortExpression="SubCategor&#237;a"></asp:BoundField>
                    <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado"></asp:BoundField>
                    <asp:BoundField DataField="Asunto" HeaderText="Asunto" SortExpression="Asunto"></asp:BoundField>
                    <asp:BoundField DataField="Comentario Usuario" HeaderText="Comentario Usuario" SortExpression="Comentario Usuario"></asp:BoundField>
                    <asp:BoundField DataField="Resoluci&#243;n" HeaderText="Resoluci&#243;n" SortExpression="Resoluci&#243;n"></asp:BoundField>
                    <asp:BoundField DataField="Fecha Resoluci&#243;n" HeaderText="Fecha Resoluci&#243;n" SortExpression="Fecha Resoluci&#243;n"></asp:BoundField>
                    <%-- <asp:BoundField DataField="Resoluci&#243;n" HeaderText="Resoluci&#243;n" ReadOnly="True" SortExpression="Resoluci&#243;n"></asp:BoundField>
                    <asp:BoundField DataField="Fecha Resoluci&#243;n" HeaderText="Fecha Resoluci&#243;n" ReadOnly="True" SortExpression="Fecha Resoluci&#243;n"></asp:BoundField>--%>
                    <asp:ButtonField ButtonType="Button" CommandName="Resolver" HeaderText="Resolver" Text="Resolver" ControlStyle-CssClass="btn btn-primary btn-block"  />

                </Columns>
            </asp:GridView>

            <asp:SqlDataSource runat="server" ID="SqlDataSourceTickets" ConnectionString='<%$ ConnectionStrings:aulearnConnectionString %>' SelectCommand="select
T.id_ticket 'ID Ticket', 
CONVERT(VARCHAR(19),T.f_apertura, 120) as 'Fecha Apertura',
CONVERT(VARCHAR(19),T.f_vencimiento, 120) 'Fecha Vencimiento',
T.rut_colegio 'Rut Colegio',
P.nombre + ' ' + P.apellido 'Nombre usuario',
CASE WHEN Convert(varchar(10), T.id_tecnico_asignado) IS NULL THEN 'Sin Asignar' ELSE Convert(varchar(10),P2.nombre + ' ' + P2.apellido) END as 'Técnico Asignado',
TPrio.descripcion_prioridad 'Prioridad',
M.descripcion_modulo 'Módulo',
SC.descripcion_subcategoria 'SubCategoría',
TE.descripcion_estado_ticket 'Estado',
T.asunto 'Asunto',
T.comentario_usuario 'Comentario Usuario',
CASE WHEN T.resolucion_conflicto IS NULL THEN 'Sin Resolución' ELSE +T.resolucion_conflicto END as 'Resolución',
(CASE WHEN T.f_resolucion IS NULL THEN 'Aún sin resolver' ELSE CONVERT(VARCHAR(19),T.f_resolucion, 120) END) as 'Fecha Resolución' 

 from Ticket T
 inner join Usuario U on U.id_usuario = T.id_usuario
 inner join Persona P on P.rut_persona=U.rut_persona
 left join Usuario U2 on T.id_tecnico_asignado=U2.id_usuario
 left join Persona P2 on P2.rut_persona=U2.rut_persona
 inner join Tipo_Prioridad_Ticket TPrio on TPrio.id_prioridad_ticket=T.id_prioridad_ticket
 inner join Modulo M on M.id_modulo=T.id_modulo
 inner join SubCategoria SC on SC.id_subcategoria=T.id_subcategoria
 inner join Tipo_Estado_Ticket TE on TE.id_estado_ticket=T.id_estado_ticket where T.id_tecnico_asignado = @id_tecnico_asignado">

                  <SelectParameters>
                                    <asp:ControlParameter ControlID="labelIDTecnico" Name="id_tecnico_asignado" PropertyName="Text" />
                                     
                                </SelectParameters>

            </asp:SqlDataSource>
            
        </div>

    </div>

    <div class="row" id="divResolverTickets" runat="server">
        <div class="col-lg-6" runat="server">

            <div class="card mb-3">
                <div class="card-header">
                    <i class="fa fa-check" style="color: green"></i>
                    <asp:Label runat="server" ID="labelTitulo"> Resolver Ticket</asp:Label>
                </div>
                <div class="card-body">

                    <div class="row">
                        <div class="col-lg-4">

                            <asp:Label ID="labelIDTICKET" runat="server" Visible="false"></asp:Label>

                            <label>Fecha Apertura</label>
                            <asp:TextBox ID="txtFechaApertura" runat="server" class="form-control" disabled="true"></asp:TextBox>
                            <br />
                        </div>
                        <div class="col-lg-4">


                            <label>Fecha Vencimiento</label>
                            <asp:TextBox ID="txtFechaVencimiento" runat="server" class="form-control" disabled="true"></asp:TextBox>
                            <br />

                        </div>
                        <div class="col-lg-4">
                            <label>Prioridad</label>
                            <asp:TextBox ID="txtPrioridad" runat="server" class="form-control" disabled="true"></asp:TextBox>
                            <br />
                        </div>
                    </div>


                    <div class="col-lg-12">
                        <asp:Label ID="tiempoRestante" runat="server"></asp:Label>
                        <br />
                    </div>
                    <br />

                    <div class="row">
                        <div class="col-lg-6">
                            <label>Rut Colegio</label>
                            <asp:TextBox ID="txtRutColegio" runat="server" class="form-control" disabled="true"></asp:TextBox>
                            <br />
                        </div>
                        <div class="col-lg-6">
                            <label>Nombre Colegio</label>
                            <asp:TextBox ID="txtNombreColegio" runat="server" class="form-control" disabled="true"></asp:TextBox>
                            <br />
                        </div>


                    </div>

                    <div class="row">
                        <div class="col-lg-6">
                            <label>Módulo</label>
                            <asp:TextBox ID="txtModulo" runat="server" class="form-control" disabled="true"></asp:TextBox>
                            <br />
                        </div>
                        <div class="col-lg-6">
                            <label>Sub Categoría</label>
                            <asp:TextBox ID="txtSubcategoria" runat="server" class="form-control" disabled="true"></asp:TextBox>
                            <br />
                        </div>

                    </div>


                    <label>Asunto</label>
                    <asp:TextBox ID="txtAsunto" runat="server" class="form-control" disabled="true"></asp:TextBox>
                    <br />


                    <label>Comentario Usuario</label>
                    <asp:TextBox ID="txtComentarioUsuario" runat="server" class="form-control" TextMode="multiline" Columns="70" Rows="5" disabled="true"></asp:TextBox>
                    <br />

                    <label>Especificar Resolución</label>
                    <br />
                    <asp:TextBox ID="txtResolucion" runat="server" class="form-control col-xs-3" placeholder="Resolución" TextMode="multiline" Columns="70" Rows="10"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorREsolucion"
                        ControlToValidate="txtResolucion"
                        Display="Static"
                        ErrorMessage="Debe ingresar una resolución"
                        runat="server"
                        ForeColor="Red" />
                    <br />
                    <br />
                    <div class="row"> 
                      
                                <div class="col-lg-6">
                                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" class="btn btn-lg btn-danger btn-block" OnClick="btnCancelar_Click" CausesValidation="false" />
                                </div>
                          <div class="col-lg-6">
                                   <asp:Button ID="btnResolucion" OnClick="btnResolucion_Click" runat="server" Text="Resolver Ticket" class="btn btn-lg btn-primary btn-block" />
                           
                                </div> 
                        <br />
                                
                                
                    </div>
                    <br />


                </div>
            </div>



        </div>
    </div>


    <br />
    <br />

</asp:Content>
