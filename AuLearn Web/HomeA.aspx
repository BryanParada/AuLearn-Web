<%@ Page Title="" Language="C#" MasterPageFile="~/SitioM.Master" AutoEventWireup="true" CodeBehind="HomeA.aspx.cs" Inherits="AuLearn_Web.HomeA" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BreadCrumb" runat="server">
    Inicio

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">

     
    
     <%--<!-- Icon Cards -->
        <div class="row">
          <div class="col-xl-3 col-sm-6 mb-3">
            <div class="card text-white bg-primary o-hidden h-100">
              <div class="card-body">
                <div class="card-body-icon">
                  <i class="fa fa-fw fa-comments"></i>
                </div>
                <div class="mr-5">
                  26 New Messages!
                </div>
              </div>
              <a href="#" class="card-footer text-white clearfix small z-1">
                <span class="float-left">View Details</span>
                <span class="float-right">
                  <i class="fa fa-angle-right"></i>
                </span>
              </a>
            </div>
          </div>
          <div class="col-xl-3 col-sm-6 mb-3">
            <div class="card text-white bg-warning o-hidden h-100">
              <div class="card-body">
                <div class="card-body-icon">
                  <i class="fa fa-fw fa-list"></i>
                </div>
                <div class="mr-5">
                  11 New Tasks!
                </div>
              </div>
              <a href="#" class="card-footer text-white clearfix small z-1">
                <span class="float-left">View Details</span>
                <span class="float-right">
                  <i class="fa fa-angle-right"></i>
                </span>
              </a>
            </div>
          </div>
          <div class="col-xl-3 col-sm-6 mb-3">
            <div class="card text-white bg-success o-hidden h-100">
              <div class="card-body">
                <div class="card-body-icon">
                  <i class="fa fa-fw fa-shopping-cart"></i>
                </div>
                <div class="mr-5">
                  123 New Orders!
                </div>
              </div>
              <a href="#" class="card-footer text-white clearfix small z-1">
                <span class="float-left">View Details</span>
                <span class="float-right">
                  <i class="fa fa-angle-right"></i>
                </span>
              </a>
            </div>
          </div>
          <div class="col-xl-3 col-sm-6 mb-3">
            <div class="card text-white bg-danger o-hidden h-100">
              <div class="card-body">
                <div class="card-body-icon">
                  <i class="fa fa-fw fa-support"></i>
                </div>
                <div class="mr-5">
                  13 New Tickets!
                </div>
              </div>
              <a href="#" class="card-footer text-white clearfix small z-1">
                <span class="float-left">View Details</span>
                <span class="float-right">
                  <i class="fa fa-angle-right"></i>
                </span>
              </a>
            </div>
          </div>
        </div>--%>
          
       <%-- <div class="card mb-3">
          <div class="card-header">
            <i class="fa fa-area-chart"></i>
            Promedios actuales Alumnos
          </div>
          <div class="card-body">

            <canvas id="myAreaChart" width="100%" height="30">
            </canvas>


             
              
 
        <!-- Area Chart Example -->
          </div>
          <div class="card-footer small text-muted">
            Última actualización 11:59 PM
          </div>
        </div>--%>

        <div class="row">

             <div class="col-lg-12">
            <!-- Example Pie Chart Card -->
            <div class="card mb-3" id="CuadroADV" runat="server">
              <div class="card-header">
                <i class="fa fa-exclamation-triangle" style="color: red"></i>
                Advertencias
              </div>
              <div class="card-body">
                   <asp:Label runat="server" ID="labelRut" Visible="false"></asp:Label>
                  <asp:GridView ID="GridAdvertencias" runat="server" class="table table-bordered table-hover table-striped" 
                      AutoGenerateColumns="False" DataKeyNames="Rut" DataSourceID="SqlDataSourceAdvertencias"
                      OnRowCommand="GridAdvertencias_RowCommand">
                      <Columns>
                          <asp:BoundField DataField="Rut" HeaderText="Rut" ReadOnly="True" SortExpression="Rut" Visible="false"></asp:BoundField>
                          <asp:BoundField DataField="Nombre Alumno" HeaderText="Nombre Alumno" ReadOnly="True" SortExpression="Nombre Alumno" Visible="false"></asp:BoundField>
                          <asp:BoundField DataField="Descripci&#243;n del Nivel" HeaderText="Descripci&#243;n del Nivel" SortExpression="Descripci&#243;n del Nivel" Visible="false"></asp:BoundField>
                          <asp:BoundField DataField="Nivel Promedio" HeaderText="Nivel Promedio" ReadOnly="True" SortExpression="Nivel Promedio" Visible="false"></asp:BoundField>
                          <asp:BoundField DataField="materia" HeaderText="materia" SortExpression="materia" Visible="false"></asp:BoundField>
                          <asp:BoundField DataField="Cumplimiento" HeaderText="Cumplimiento" ReadOnly="True" SortExpression="Cumplimiento"></asp:BoundField>
                          <asp:ButtonField ButtonType="Button" CommandName="Revisar" HeaderText="Revisar" Text="Revisar" ControlStyle-CssClass="btn btn-primary btn-block" />
                      </Columns>
                  </asp:GridView>


                  <asp:SqlDataSource runat="server" ID="SqlDataSourceAdvertencias" ConnectionString='<%$ ConnectionStrings:aulearnConnectionString %>' SelectCommand="SELECT 
                      P.rut_persona AS 'Rut',
                       P.nombre + ' ' + P.apellido AS 'Nombre Alumno',
                       TN.descripcion_nivel AS 'Descripción del Nivel',
                       AVG(nl.puntuacion) AS 'Nivel Promedio',
                       M.materia,
                       (CASE 
                      WHEN Tn.descripcion_nivel = 'Emocional' 
                      THEN P.nombre + ' ' + P.apellido + ' ha bajado su nivel ' + Tn.descripcion_nivel + ' en la asignatura de ' + M.materia 
                      ELSE P.nombre + ' ' + P.apellido + ' ha bajado su nivel de ' + Tn.descripcion_nivel + ' en la asignatura de ' + M.materia END)
                       AS 'Cumplimiento' 
                      FROM Nivel AS nl 
                      INNER JOIN Notas AS N ON N.id_nota = nl.id_nota 
                      INNER JOIN Estudiante AS E ON E.id_estudiante = N.id_estudiante 
                      INNER JOIN Persona AS P ON P.rut_persona = E.rut_persona
                       INNER JOIN Tipo_nivel AS TN ON nl.id_tipo_nivel = TN.id_tipo_nivel
                       INNER JOIN Actividad AS A ON N.id_actividad = A.id_actividad
                       INNER JOIN Unidad AS U ON U.id_unidad = A.id_unidad
                       INNER JOIN Materia AS M ON M.id_materia = U.id_materia 
                      inner join Integrantes_curso IC on IC.id_estudiante=E.id_estudiante
                        inner join Curso C on C.id_curso=IC.id_curso
                        inner join Asignar_curso AC on AC.id_curso=C.id_curso
                        inner join Usuario Usu on Usu.id_usuario=AC.id_usuario
                        where Usu.rut_persona=@rut_persona
                      GROUP BY P.rut_persona, P.nombre, P.apellido, TN.descripcion_nivel, M.materia
                       HAVING (AVG(nl.puntuacion) < 5)">

                        <SelectParameters>
                                    <asp:ControlParameter ControlID="labelRut" Name="rut_persona" PropertyName="Text" />
                                     
                                </SelectParameters>

                  </asp:SqlDataSource>
              </div>
              <div class="card-footer small text-muted">
                Sólo se muestran las advertencias del semestre actual.
              </div>
            </div>
           
          </div>
              </div>
     <div class="row">
          <div class="col-lg-8"> 

            <!-- Example Bar Chart Card -->
            <div class="card mb-3" id="BarChartPromedios" runat="server">
              <div class="card-header">
                <i class="fa fa-bar-chart"></i>
                Promedio Actual Alumnos
              </div>
              <div class="card-body">
                  
                <div class="row"> 
                    <canvas id="myBarChart" width="100" height="50"></canvas>
 
                </div>
              </div>
              <div class="card-footer small text-muted">
                Los Promedios actuales detallan el progreso durante el Semestre Actual
              </div>
            </div>

              <div class="card mb-3" id="BarChartUSO" runat="server">
              <div class="card-header">
                <i class="fa fa-bar-chart"></i>
                Uso Sistema AuLearn en profesores
              </div>
              <div class="card-body">
                  
                <div class="row"> 
                    <canvas id="ChartUSO" width="100" height="50"></canvas> 
                </div>
              </div>
              <div class="card-footer small text-muted">
                El uso del sistema corresponde al mes actual
              </div>
            </div>

               

           

          </div>
          
          <div class="col-lg-4" id="colPieChartSitAC" runat="server">
            <!-- Example Pie Chart Card -->
            <div class="card mb-3" id="PieChartSitAC" runat="server">
              <div class="card-header">
                <i class="fa fa-pie-chart"></i>
               Cantidad alumnos según situación académica
              </div>
              <div class="card-body">
                <canvas id="myPieChart" width="100%" height="100"></canvas>
              </div>
              <div class="card-footer small text-muted">
                Sólo se muestran los alumnos al curso correspondiente del profesor
              </div>
            </div>
           
          </div>

         <div class="col-lg-4">
           
            <div class="card mb-3" id="AdvertenciasDirector" runat="server">
              <div class="card-header">
                <i class="fa fa-exclamation-triangle" style="color: red"></i>
                Advertencias en el control de uso
              </div>
              <div class="card-body">
                   <asp:Label runat="server" ID="label1" Visible="false"></asp:Label>
                <asp:GridView ID="GridControlUso" runat="server"
                class="table table-bordered table-hover table-striped"
                AllowPaging="True"
                PageSize="10"
                AllowSorting="True"
                AutoGenerateColumns="False" DataSourceID="SqlDataSourceDatosControlUso" >
                      <Columns>
                    <%--<asp:BoundField DataField="id" HeaderText="id" SortExpression="id" />--%>
                    <asp:BoundField DataField="Nombre Profesor" HeaderText="Nombre Profesor" ReadOnly="True" SortExpression="Nombre Profesor" />
                    <%--<asp:BoundField DataField="Cantidad Horas Asignadas" HeaderText="Cantidad Horas Asignadas" ReadOnly="True" SortExpression="Cantidad Horas Asignadas" />
                    <asp:BoundField DataField="Cantidad Acciones Asignadas" HeaderText="Cantidad Acciones Asignadas" ReadOnly="True" SortExpression="Cantidad Acciones Asignadas" />--%>
                    <asp:BoundField DataField="Mes" HeaderText="Mes" ReadOnly="True" SortExpression="Mes" />
                   <%-- <asp:BoundField DataField="Horas Totales de Uso" HeaderText="Horas Totales de Uso" ReadOnly="True" SortExpression="Horas Totales de Uso" />
                    <asp:BoundField DataField="Cantidad de Acciones" HeaderText="Cantidad de Acciones" ReadOnly="True" SortExpression="Cantidad de Acciones" />--%>
                    <%--<asp:BoundField DataField="Porcentaje Cumplimiento" HeaderText="Porcentaje Cumplimiento" ReadOnly="True" SortExpression="Porcentaje Cumplimiento" />--%>
                    <asp:BoundField DataField="Cumplimiento" HeaderText="Cumplimiento" ReadOnly="True" SortExpression="Cumplimiento" />
                </Columns>

            </asp:GridView>




            <asp:SqlDataSource ID="SqlDataSourceDatosControlUso" runat="server" ConnectionString="<%$ ConnectionStrings:aulearnConnectionString %>" SelectCommand="
                select C.id_usuario as 'id', P.nombre + ' ' + P.apellido as 'Nombre Profesor',
                
CU.cantidad_horas as 'Cantidad Horas Asignadas',
CU.cantidad_acciones as 'Cantidad Acciones Asignadas',
DATEname(MONTH, C.hora_inicio) as 'Mes', 
 CONVERT(FLOAT,ROUND(SUM((C.tiempo_total_sesion)/60.0),1))  as 'Horas Totales de Uso',
 (SUM(C.cantidad_acciones)) as 'Cantidad de Acciones',
 CAST(
 (((SUM(C.tiempo_total_sesion)/60) * 100) / CU.cantidad_horas) AS NVARCHAR(255)
 ) + '%' as 'Porcentaje Cumplimiento',
  
 (case 
 when ((SUM(C.tiempo_total_sesion))/60) &gt;= CU.cantidad_horas and (SUM(C.cantidad_acciones)) &gt;= CU.cantidad_acciones 
 then 'Cumple'
 else 'No cumple'
 end) as 'Cumplimiento' 
  from Contador C

inner join Usuario U on U.id_usuario=C.id_usuario
inner join Persona P on P.rut_persona=U.rut_persona
inner join Control_de_uso CU on CU.id_usuario_a_controlar=U.id_usuario
 
	  group by C.id_usuario, P.nombre, P.apellido,
	  DATEname(MONTH, C.hora_inicio), 
	  DATEPART(MONTH, C.hora_inicio),
	  CU.cantidad_horas,
	  CU.cantidad_acciones 
	  order by P.nombre, P.apellido, DATEPART(MONTH, C.hora_inicio) "></asp:SqlDataSource>

              </div>
              <div class="card-footer small text-muted">
                Sólo se muestra el uso del sistema AuLearn en el semestre actual.
              </div>
            </div>
           
          </div>
          </div>   


            
 

        
        
     <br />
    <br />
    
     
    

</asp:Content>






