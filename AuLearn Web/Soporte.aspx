<%@ Page Title="" Language="C#" MasterPageFile="~/SitioM.Master" AutoEventWireup="true" CodeBehind="Soporte.aspx.cs" Inherits="AuLearn_Web.Soporte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BreadCrumb" runat="server">
    <i class="fa fa-tag"></i>Asignar Tickets
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">


    <div class="row">
        <div class="col-lg-12" runat="server">
            <asp:CheckBox Checked="true" ID="checkboxPrioridad" runat="server" Text="Mostrar sólo Pendientes" AutoPostBack="true" OnCheckedChanged="checkboxPrioridad_CheckedChanged" />

            <br />

        </div>
    </div>

    <div class="row">
        <div class="col-lg-12" runat="server">

           <%--AllowPaging="True"
                        PageSize="8"--%>
                    <asp:GridView ID="GridTickets" runat="server"
                        class="table table-bordered table-hover table-striped"
                        
                        AllowSorting="True"
                        AutoGenerateColumns="False"
                        DataSourceID="SqlDataSourceTickets"
                        OnRowDataBound="GridTickets_RowDataBound"
                        OnRowCommand="GridTickets_RowCommand"
                        >

                        <Columns>
                            <asp:BoundField DataField="ID Ticket" HeaderText="ID Ticket" ReadOnly="True" SortExpression="ID Ticket"></asp:BoundField>
                            <asp:BoundField DataField="Fecha Apertura" HeaderText="Fecha Apertura" ReadOnly="True" SortExpression="Fecha Apertura"></asp:BoundField>
                            <asp:BoundField DataField="Fecha Vencimiento" HeaderText="Fecha Vencimiento" ReadOnly="True" SortExpression="Fecha Vencimiento"></asp:BoundField>
                            <asp:BoundField DataField="Rut Colegio" HeaderText="Rut Colegio" SortExpression="Rut Colegio"></asp:BoundField>
                            <asp:BoundField DataField="Nombre usuario" HeaderText="Nombre usuario" ReadOnly="True" SortExpression="Nombre usuario"></asp:BoundField>
                            <asp:BoundField DataField="T&#233;cnico Asignado" HeaderText="T&#233;cnico Asignado" ReadOnly="True" SortExpression="T&#233;cnico Asignado"></asp:BoundField>
                            <asp:BoundField DataField="Prioridad" HeaderText="Prioridad" ReadOnly="True" SortExpression="Prioridad"></asp:BoundField>
                            <asp:BoundField DataField="M&#243;dulo" HeaderText="M&#243;dulo" SortExpression="M&#243;dulo"></asp:BoundField>
                            <asp:BoundField DataField="SubCategor&#237;a" HeaderText="SubCategor&#237;a" SortExpression="SubCategor&#237;a"></asp:BoundField>
                            <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado"></asp:BoundField>
                            <asp:BoundField DataField="Asunto" HeaderText="Asunto" SortExpression="Asunto"></asp:BoundField>
                            <asp:BoundField DataField="Comentario Usuario" HeaderText="Comentario Usuario" SortExpression="Comentario Usuario"></asp:BoundField>
                            <asp:ButtonField ButtonType="Button" CommandName="Asignar" HeaderText="Asignar" Text="Asignar" ControlStyle-CssClass="btn btn-primary btn-block" />

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
 inner join Tipo_Estado_Ticket TE on TE.id_estado_ticket=T.id_estado_ticket where T.id_estado_ticket = 1"></asp:SqlDataSource>
               
        </div>

    </div>

    <div class="row" id="divResolverTickets" runat="server">
        <div class="col-lg-6" runat="server">

            <div class="card mb-3">
                <div class="card-header">
                    <i class="fa fa-tag" style="color: green"></i>
                    <asp:Label runat="server" ID="labelTitulo"> Asignar Ticket</asp:Label>
                </div>
                <div class="card-body">

                    <div class="row">
                        <div class="col-lg-6">

                            <asp:Label ID="labelIDTICKET" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="labelFechaVencimiento" runat="server" Visible="false"></asp:Label>

                            <label>Fecha Apertura</label>
                            <asp:TextBox ID="txtFechaApertura" runat="server" class="form-control" disabled="true"></asp:TextBox>
                            <br />
                        </div>
                        <div class="col-lg-6">


                            <label>Fecha Vencimiento</label>
                            <asp:TextBox ID="txtFechaVencimiento" runat="server" class="form-control" disabled="true"></asp:TextBox>
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
                    <br />

                    <div class="row">
                        <div class="col-lg-6">

                            <label>Prioridad</label>
                            <asp:DropDownList ID="DROPprioridad" runat="server" class="form-control" DataSourceID="SqlDataSourcePrio" DataTextField="descripcion_prioridad" DataValueField="id_prioridad_ticket">
                            </asp:DropDownList>
                            <asp:SqlDataSource runat="server" ID="SqlDataSourcePrio" ConnectionString='<%$ ConnectionStrings:aulearnConnectionString %>' SelectCommand="select * from Tipo_Prioridad_Ticket where id_prioridad_ticket != 4"></asp:SqlDataSource>

                        </div>
                        <div class="col-lg-6">
                            <label>Asignar Técnico</label>
                            <asp:DropDownList ID="DROPTecnico" runat="server" DataSourceID="SqlDataSourceTecnicos" DataTextField="Nombre" DataValueField="id_usuario" class="form-control">
                            </asp:DropDownList>
                            <asp:SqlDataSource runat="server" ID="SqlDataSourceTecnicos" ConnectionString='<%$ ConnectionStrings:aulearnConnectionString %>' SelectCommand="select U.id_usuario, 
  SUBSTRING(P.nombre,1,(CHARINDEX(' ',P.nombre + ' ')-1)) + ' ' + SUBSTRING(P.apellido,1,(CHARINDEX(' ',P.apellido + ' ')-1)) 'Nombre'
   from Usuario U
inner join Persona P on P.rut_persona=U.rut_persona
where U.id_tipo_usuario = 1003

"></asp:SqlDataSource>
                        </div>
                    </div>
                    <br />

                    <div class="row">

                        <div class="col-lg-6">
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" class="btn btn-lg btn-danger btn-block" OnClick="btnCancelar_Click" CausesValidation="false" />
                        </div>
                        <div class="col-lg-6">
                            <asp:Button ID="btnAsignar" OnClick="btnAsignar_Click" runat="server" Text="Asignar Ticket" class="btn btn-lg btn-primary btn-block" />

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





