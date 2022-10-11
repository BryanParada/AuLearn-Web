<%@ Page Title="" Language="C#" MasterPageFile="~/SitioM.Master" AutoEventWireup="true" CodeBehind="Sugerencias.aspx.cs" Inherits="AuLearn_Web.Sugerencias" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BreadCrumb" runat="server">
    <i class="fa fa-commenting-o"></i> Sugerencias y Comentarios
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">


     <div class="row">
        <div class="col-lg-12" runat="server">
           
            <asp:GridView ID="GridTickets" runat="server"
                class="table table-bordered table-hover table-striped"
                AllowPaging="True"
                AllowSorting="True"
                AutoGenerateColumns="False"
                DataSourceID="SqlDataSourceSugerencias">

                <Columns>
                    <asp:BoundField DataField="Fecha Llegada" HeaderText="Fecha Llegada" SortExpression="Fecha Llegada"></asp:BoundField>
                    <asp:BoundField DataField="Rut Colegio" HeaderText="Rut Colegio" SortExpression="Rut Colegio"></asp:BoundField>
                    <asp:BoundField DataField="Colegio" HeaderText="Colegio" SortExpression="Colegio"></asp:BoundField>
                    <asp:BoundField DataField="Nombre Usuario" HeaderText="Nombre Usuario" ReadOnly="True" SortExpression="Nombre Usuario"></asp:BoundField>
                    <asp:BoundField DataField="Asunto" HeaderText="Asunto" SortExpression="Asunto"></asp:BoundField>
                    <asp:BoundField DataField="Sugerencia" HeaderText="Sugerencia" SortExpression="Sugerencia"></asp:BoundField>
                </Columns>
            </asp:GridView>



            <asp:SqlDataSource runat="server" ID="SqlDataSourceSugerencias" ConnectionString='<%$ ConnectionStrings:aulearnConnectionString %>' SelectCommand="select 
S.f_llegada 'Fecha Llegada',
S.rut_colegio 'Rut Colegio',
C.nombre_colegio 'Colegio',
p.nombre + ' ' + p.apellido 'Nombre Usuario',
S.asunto 'Asunto',
S.comentario 'Sugerencia'
from Sugerencia S
inner join Colegio C on C.rut_colegio=S.rut_colegio
inner join Usuario u on u.id_usuario=S.id_usuario
inner join Persona p on p.rut_persona=u.rut_persona"></asp:SqlDataSource>
        </div>
 
    </div>


    <br />
    <br />

</asp:Content>

