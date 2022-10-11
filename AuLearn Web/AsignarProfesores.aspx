<%@ Page Title="" Language="C#" MasterPageFile="~/SitioM.Master" AutoEventWireup="true" CodeBehind="AsignarProfesores.aspx.cs" Inherits="AuLearn_Web.AsignarProfesores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BreadCrumb" runat="server">
    <i class="fa fa-book"></i> Asignar Profesores a Cursos
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
     <!DOCTYPE html>
     
    <div class="row">
        <div class="col-lg-5" id="divBody" runat="server">

            <div class="card mb-3">
                <div class="card-header">
                    <i class="fa fa-book"></i> Asignar Cursos
                </div>
                <div class="card-body">

                    <asp:Label runat="server" ID="labelTitulo" class="h2">Asignar Cursos</asp:Label>
                    <br />
                    <br />
                     <div id="DivAdvertencia" runat="server" visible="false">
                         <asp:Label runat="server" ID="labelAdvertencia">Actualmente no hay profesores a los cuáles asignar un curso.</asp:Label>
                         </div>
                    <div id="DivBodyP" runat="server">
                    <div class="row">
                        <div class="col-lg-4">
                            <label>Seleccione Profesor</label>
                            <br />
                            <asp:DropDownList ID="DropDownProfes" AutoPostBack="true" runat="server" class="form-control" DataSourceID="SqlDataSourceAlumnos" DataTextField="Nombre Profesor" DataValueField="id_usuario"></asp:DropDownList>

                            <asp:SqlDataSource ID="SqlDataSourceAlumnos" runat="server" ConnectionString="<%$ ConnectionStrings:aulearnConnectionString %>" SelectCommand="select U.id_usuario,
P.nombre + ' ' + P.apellido AS 'Nombre Profesor' from Usuario U 
inner join Persona P on U.rut_persona=P.rut_persona
where P.id_cargo = 1003"></asp:SqlDataSource>

                            <%--Esta consulta muestra los profesores que no tienen curso--%>
                            <%--select U.id_usuario,
P.nombre + ' ' + P.apellido AS 'Nombre Profesor' from Usuario U 
inner join Persona P on U.rut_persona=P.rut_persona
where P.id_cargo = 1003 and not exists (select 1 from Asignar_curso where Asignar_curso.id_usuario = U.id_usuario)--%>

                        </div>
                       
                        <div class="col-lg-3">
                            <label>Curso a Asignar</label>
                            <br />
                            
                            <asp:DropDownList ID="DropDownCurso" runat="server" AutoPostBack="True" class="form-control" DataSourceID="SqlDataSourceC" DataTextField="nombre_curso" DataValueField="id_curso"></asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSourceC" runat="server" ConnectionString="<%$ ConnectionStrings:aulearnConnectionString %>" SelectCommand="
                                Select C.id_curso, C.nombre_curso from Curso as C  
 where not exists (
 select 1 
 from Asignar_curso 
 inner join Asignar_curso AC on AC.id_curso=C.id_curso 
 inner join Usuario U on U.id_usuario=AC.id_usuario
 inner join Persona P on P.rut_persona=U.rut_persona
 where Asignar_curso.id_curso = C.id_curso
 )
                                ">

<%--                                Select C.id_curso, C.nombre_curso from Curso as C  
 where not exists (
 select 1 
 from Asignar_curso 
 inner join Asignar_curso AC on AC.id_curso=C.id_curso 
 inner join Usuario U on U.id_usuario=AC.id_usuario
 inner join Persona P on P.rut_persona=U.rut_persona
 where U.id_usuario = @id_usuario
 and Asignar_curso.id_curso = C.id_curso
 )
            --%>                    

                                 <SelectParameters>
                                    <asp:ControlParameter ControlID="DropDownProfes" Name="id_usuario" PropertyName="SelectedValue" /> 
                                </SelectParameters>

                            </asp:SqlDataSource>
                        </div>
                    </div>
                    <br />
                    <br />


                    <div class="row">
                        <div class="col-lg-3">
                            <asp:Button ID="btnAsignar" OnClick="btnAsignar_Click" runat="server" Text="Asignar" class="btn btn-lg btn-primary btn-block" />
                        </div>
                    </div>
                    <br />
                        </div>


                </div>
            </div>

        </div>

        <div class="col-lg-5">

            <div class="card mb-3">
                <div class="card-header">
                    <i class="fa fa-book"></i> Asignar Cursos
                </div>
                <div class="card-body">


                    <h2>Listado Cursos y Profesores</h2>

                    <br />

                    <asp:GridView ID="GridViewListado" OnRowCommand="GridViewListado_RowCommand" runat="server" AutoGenerateColumns="False" class="table table-bordered table-hover table-striped" DataSourceID="SqlDataSourceListadoAlumnos">
                        <Columns>
                            <asp:BoundField DataField="id_asignar_curso" HeaderText="Identificador Asignación" ReadOnly="True" SortExpression="id_asignar_curso"  />
                            <asp:BoundField DataField="Profesor" HeaderText="Profesor" ReadOnly="True" SortExpression="Profesor" />
                            <asp:BoundField DataField="Curso" HeaderText="Curso" SortExpression="Curso" />
                            <asp:BoundField DataField="Grado Discapacidad Curso" HeaderText="Grado Discapacidad Curso" ReadOnly="True" SortExpression="Grado Discapacidad Curso" />
                            <asp:ButtonField ButtonType="Button" CommandName="Desvincular" HeaderText="Desvincular Curso" Text="Desvincular" ControlStyle-CssClass="btn btn-primary" />
                        </Columns>
                    </asp:GridView>

                    <asp:SqlDataSource ID="SqlDataSourceListadoAlumnos" runat="server" ConnectionString="<%$ ConnectionStrings:aulearnConnectionString %>" SelectCommand="
 select AC.id_asignar_curso as 'id_asignar_curso',
 P.nombre + ' ' + P.apellido AS 'Profesor',
 C.nombre_curso as 'Curso',
 GD.grado_discapacidad as 'Grado Discapacidad Curso'
 from Persona P
 inner join Usuario U on U.rut_persona=P.rut_persona
 inner join Asignar_curso AC on AC.id_usuario=U.id_usuario
 inner join Curso C on C.id_curso= AC.id_curso
 inner join Grado_discapacidad GD on GD.id_grado_discapacidad=C.id_grado_discapacidad where P.activo = 1"></asp:SqlDataSource>

                </div>

            </div>
        </div>


    </div>

</asp:Content>
