<%@ Page Title="" Language="C#" MasterPageFile="~/SitioM.Master" AutoEventWireup="true" CodeBehind="Informe.aspx.cs" Inherits="AuLearn_Web.Informe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BreadCrumb" runat="server">
    Informes de Desempeño

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">

    <!DOCTYPE html>
    <asp:Label runat="server" ID="labelTitulo" class="h2">Informes de Desempeño</asp:Label>
    <br />
    <br />

    <div class="row">
        <div class="col-lg-2">

 
            <label>Seleccione Alumno</label><br />


            <asp:DropDownList ID="DropDownListNombre" runat="server" DataSourceID="SqlDataSource1" DataTextField="Column1" DataValueField="rut_persona" class="form-control">
            </asp:DropDownList>

            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:aulearnConnectionString %>" SelectCommand="select 
                e.rut_persona, p.nombre+' '+p.apellido from Estudiante e inner join Persona p on e.rut_persona = p.rut_persona"></asp:SqlDataSource>

        </div>
        <div class="col-md-2">
            <br />
            <asp:Button ID="Buscar" runat="server" Text="Buscar" OnClick="Buscar_Click" class="btn btn-lg btn-primary btn-block" />

        </div>
        <div class="col-lg-2">
            <br />
            <asp:HyperLink runat="server" href="javascript:pruebaDivAPdf()" id="ButtonPDF" class="btn btn-lg btn-primary btn-block">Descargar PDF</asp:HyperLink>

        </div>
    </div>

    <br />

    


    <%--DIIIV IMPRIMIR!--%>

  
    <div class="row" id="imprimir" >


          

        <div class="col-lg-6">
            <div style="margin-left: auto; margin-right: auto; text-align: center;">
                <asp:Label runat="server" ID="label1" class="h2">Informe de Desempeño 2018</asp:Label>
            </div>
            <br />
            <br />

            <label class="h3">Datos Alumno</label>
            <br />

            <asp:Label runat="server" ID="labelNombre"></asp:Label>
            <br />
            <asp:Label runat="server" ID="labelEdad"></asp:Label>
            <br />
            <asp:Label runat="server" ID="labelTD"></asp:Label>
            <br />
            <asp:Label runat="server" ID="labelGD"></asp:Label>
            <br />
            <label></label>
            <br />

            <br />

            <%--GRIIIID DATOOOOS--%>

    <asp:GridView ID="GridViewDatos" Visible="false" runat="server" AutoGenerateColumns="False" class="table table-bordered table-hover table-striped" DataSourceID="SqlDataSourceDatosAlumnos" >
        <Columns>
            <asp:BoundField DataField="Nombre Alumno" HeaderText="Nombre Alumno" ReadOnly="True" SortExpression="Nombre Alumno" />
            <asp:BoundField DataField="Edad" HeaderText="Edad" ReadOnly="True" SortExpression="Edad" />
            <asp:BoundField DataField="Tipo Discapacidad" HeaderText="Tipo Discapacidad" SortExpression="Tipo Discapacidad" />
            <asp:BoundField DataField="Grado de Discapacidad" HeaderText="Grado de Discapacidad" SortExpression="Grado de Discapacidad" />
        </Columns>

    </asp:GridView>

    <asp:SqlDataSource ID="SqlDataSourceDatosAlumnos" runat="server" ConnectionString="<%$ ConnectionStrings:aulearnConnectionString %>" SelectCommand="select  P.nombre + ' ' + P.apellido AS 'Nombre Alumno',
(select (cast(datediff(dd, P.fecha_nacimiento ,GETDATE()) / 365.25 as int))) as 'Edad',
TD.tipo_discapacidad as 'Tipo Discapacidad',
GD.grado_discapacidad as 'Grado de Discapacidad' 
 from Persona as P
 inner join Estudiante as E on  E.rut_persona=P.rut_persona
 inner join Tipo_discapacidad as TD on E.id_tipo_discapacidad=TD.id_tipo_discapacidad
 inner join Integrantes_curso as IC on IC.id_estudiante=E.id_estudiante
 inner join Curso as C on C.id_curso=IC.id_curso
 inner join Grado_discapacidad as GD on GD.id_grado_discapacidad=C.id_grado_discapacidad
 where E.rut_persona = @rut_persona">
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownListNombre" Name="rut_persona" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>


            <label class="h3">Resumen de Notas</label>
            <br />
            <%--GRIIIIIIID NOOOTAS--%>

            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" class="table table-bordered table-hover table-striped" DataSourceID="SqlDataSourceD1">
                <Columns>
                    <asp:BoundField DataField="Materia" HeaderText="Materia" SortExpression="Materia" />
                    <asp:BoundField DataField="Actividad" HeaderText="Actividad" SortExpression="Actividad" />
                    <asp:BoundField DataField="Nota" HeaderText="Nota" SortExpression="Nota" />
                </Columns>
            </asp:GridView>

            <asp:SqlDataSource ID="SqlDataSourceD1" runat="server" ConnectionString="<%$ ConnectionStrings:aulearnConnectionString %>" SelectCommand="select m.materia as 'Materia',
                  a.descripcion as 'Actividad',
                  n.nota as 'Nota'
				  from Notas n
				  inner join Estudiante E on E.id_estudiante=n.id_estudiante
				  inner join Actividad A on A.id_actividad=N.id_actividad
				  inner join Unidad U on U.id_unidad= A.id_unidad
				  inner join Materia M on M.id_materia=U.id_materia
				  where e.rut_persona = @rut_persona 
                  group by m.materia, a.descripcion, n.nota">
                <SelectParameters>
                    <asp:ControlParameter ControlID="DropDownListNombre" Name="rut_persona" PropertyName="SelectedValue" />
                </SelectParameters>
            </asp:SqlDataSource>

            <br />

            <label class="h3">Promedios Actuales</label>
            <br />
            <%--GRIIIIIID PROMEDIOS--%>


            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" class="table table-bordered table-hover table-striped" DataSourceID="SqlDataSourceD2">
                <Columns>
                    <asp:BoundField DataField="Materia" HeaderText="Materia" SortExpression="Materia" />
                    <asp:BoundField DataField="Promedio Actual" HeaderText="Promedio Actual" ReadOnly="True" SortExpression="Promedio Actual" />
                </Columns>
            </asp:GridView>

            <asp:SqlDataSource ID="SqlDataSourceD2" runat="server" ConnectionString="<%$ ConnectionStrings:aulearnConnectionString %>" SelectCommand="select
M.materia AS 'Materia', 
avg(N.Nota) as 'Promedio Actual'
from Notas N
inner join Actividad A on A.id_actividad=N.id_actividad
inner join Unidad as U on A.id_unidad=U.id_unidad
inner join Materia as M on M.id_materia=U.id_materia 
inner join Estudiante as E on E.id_estudiante=N.id_estudiante
Inner join Persona as P on P.rut_persona=E.rut_persona 
where E.rut_persona = @rut_persona
group by M.materia">
                <SelectParameters>
                    <asp:ControlParameter ControlID="DropDownListNombre" Name="rut_persona" PropertyName="SelectedValue" />
                </SelectParameters>
            </asp:SqlDataSource>

            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <label>Colegio Diferencial Juan Sandoval - Av. Gran Avenida José Miguel Carrera #11928 - Santiago</label>
             <img width="150" Height="150" src="traerImagen.ashx" />
            <br />
            <br />
            <br />

        </div>
    </div>


</asp:Content>
