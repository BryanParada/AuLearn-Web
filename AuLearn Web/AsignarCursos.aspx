<%@ Page Title="" Language="C#" MasterPageFile="~/SitioM.Master" AutoEventWireup="true" CodeBehind="AsignarCursos.aspx.cs" Inherits="AuLearn_Web.AsignarCursos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BreadCrumb" runat="server">
     <i class="fa fa-table"></i> Asignar Cursos
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
    <!DOCTYPE html>
     
    <div class="row">
        <div class="col-lg-5" id="divBody" runat="server">

            <div class="card mb-3">
                <div class="card-header">
                    <i class="fa fa-table"></i> Asignar Cursos
                </div>
                <div class="card-body">

                    <asp:Label runat="server" ID="labelTitulo" class="h2">Asignar Cursos</asp:Label>
                    <br />
                    <br />


                    <div class="row">
                        <div class="col-lg-4">
                            <label>Alumnos Sin curso </label>
                            <br />
                            <asp:DropDownList ID="DropDownAlumnos" runat="server" AutoPostBack="True" class="form-control" DataSourceID="SqlDataSourceAlumnos" DataTextField="Nombre Alumno" DataValueField="id_estudiante"></asp:DropDownList>

                            <asp:SqlDataSource ID="SqlDataSourceAlumnos" runat="server" ConnectionString="<%$ ConnectionStrings:aulearnConnectionString %>" SelectCommand="Select E.id_estudiante ,
P.nombre + ' ' + P.apellido AS 'Nombre Alumno' from Estudiante E 
inner join Persona P on P.rut_persona=E.rut_persona
where not exists (select 1 from Integrantes_curso where Integrantes_curso.id_estudiante = E.id_estudiante)"></asp:SqlDataSource>

                        </div>
                       
                        <div class="col-lg-3">
                            <label>Curso a Asignar</label>
                            <br />
                            
                            <asp:DropDownList ID="DropDownCurso" runat="server" AutoPostBack="True" class="form-control" DataSourceID="SqlDataSourceC" DataTextField="nombre_curso" DataValueField="id_curso"></asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSourceC" runat="server" ConnectionString="<%$ ConnectionStrings:aulearnConnectionString %>" SelectCommand="select C.id_curso, C.nombre_curso from Curso as C"></asp:SqlDataSource>
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

        <div class="col-lg-5">

            <div class="card mb-3">
                <div class="card-header">
                    <i class="fa fa-table"></i> Asignar Cursos
                </div>
                <div class="card-body">


                    <h2>Listado Alumnos</h2>

                    <br />

                    <asp:GridView ID="GridViewListado" OnRowCommand="GridViewListado_RowCommand" runat="server" AutoGenerateColumns="False" class="table table-bordered table-hover table-striped" DataSourceID="SqlDataSourceListadoAlumnos">
                        <Columns>
                            <asp:BoundField DataField="Profesor a Cargo" HeaderText="Profesor a Cargo" ReadOnly="True" SortExpression="Profesor a Cargo" />
                            <asp:BoundField DataField="Curso" HeaderText="Curso" SortExpression="Curso" />
                            <asp:BoundField DataField="Nombre Alumno" HeaderText="Nombre Alumno" ReadOnly="True" SortExpression="Nombre Alumno" />
                            <asp:ButtonField ButtonType="Button" CommandName="Editar" HeaderText="Editar Curso Alumno" Text="Editar" ControlStyle-CssClass="btn btn-primary" />
                        </Columns>
                    </asp:GridView>

                    <asp:SqlDataSource ID="SqlDataSourceListadoAlumnos" runat="server" ConnectionString="<%$ ConnectionStrings:aulearnConnectionString %>" SelectCommand="select PU.nombre + ' ' + PU.apellido AS 'Profesor a Cargo',
 C.nombre_curso as 'Curso',
 P.nombre + ' ' + P.apellido AS 'Nombre Alumno'
 from Curso C
inner join Integrantes_curso IC on IC.id_curso = C.id_curso
inner join Estudiante E on E.id_estudiante=IC.id_estudiante
inner join Persona P on P.rut_persona=E.rut_persona
inner join Asignar_curso AC on AC.id_curso=C.id_curso
inner join Usuario U on U.id_usuario=AC.id_usuario
inner join Persona PU on PU.rut_persona=U.rut_persona"></asp:SqlDataSource>

                </div>

            </div>
        </div>


    </div>

     <br />
     <br />
    <br />





</asp:Content>
