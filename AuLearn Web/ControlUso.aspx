<%@ Page Title="" Language="C#" MasterPageFile="~/SitioM.Master" AutoEventWireup="true" CodeBehind="ControlUso.aspx.cs" Inherits="AuLearn_Web.ControlUso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BreadCrumb" runat="server">
    <i class="fa fa-search-plus"></i>Control de Uso 

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">

    <!DOCTYPE html>

    <div class="row">
        <div class="col-lg-6" id="divBody" runat="server">

            <div class="card mb-3">
                <div class="card-header">
                    <i class="fa fa-search-plus"></i>Control de Uso 
                </div>
                <div class="card-body">
                    <asp:Label runat="server" ID="labelTitulo" class="h2">Control de Uso </asp:Label>
                    <br />
                    <br />

                    <div class="container">
  <div class="jumbotron">
    <h2>¿Para qué sirve el Control de Uso?</h2>      
    <p>El control de uso permite a los administradores controlar cuántas horas deben cumplir los profesores utilizando la plataforma AuLearn. </p>
      <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#myModal" data-placement="top" title="Obtener ayuda">Ayuda <i class="fa fa-question-circle "></i></button>
  </div>

                    <div class="row">
                        <div class="col-lg-6">
                            <label>Seleccione Profesor</label>
                            <br />
                            <asp:DropDownList ID="DropDownProfes" AutoPostBack="true" runat="server" class="form-control" DataSourceID="SqlDataSourceAlumnos" DataTextField="Nombre Profesor" DataValueField="id_usuario"></asp:DropDownList>

                            <asp:SqlDataSource ID="SqlDataSourceAlumnos" runat="server" ConnectionString="<%$ ConnectionStrings:aulearnConnectionString %>" SelectCommand="select U.id_usuario,
P.nombre + ' ' + P.apellido AS 'Nombre Profesor' from Usuario U 
inner join Persona P on U.rut_persona=P.rut_persona
where P.id_cargo = 1003 and not exists (select 1 from Control_de_uso where Control_de_uso.id_usuario_a_controlar = U.id_usuario)"></asp:SqlDataSource>

                        </div> 
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-6">
                            <label>Horas Semanales de Uso a controlar</label>
                            <br />
                            <asp:TextBox ID="CantHoras" runat="server" type="number" class="form-control" Text="0" min="1" max="10" MaxLength="1" onKeyPress="edValueKeyPress()" onKeyUp="edValueKeyPress()"> </asp:TextBox>
                            <br>
                        </div>
                        <div class="col-lg-6">
                          <label>Cantidad Mínima de acciones</label>
                            <br />
                            <asp:TextBox ID="CantAcciones" runat="server" type="number" class="form-control" Text="0" min="1" max="160" MaxLength="3"> </asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-6">
                            <label><strong>Cantidad de horas al mes:</strong> </label>
                            <br />
                            <span id="lblPrev" style="color: blue"></span>
                        </div>
                         <div class="col-lg-6">
                            <label><strong>Cantidad acciones recomendadas:</strong> </label>
                            <br />
                           <span id="lblPrevAcc" style="color: blue"></span>
                        </div>

                    </div>
                    <br />
                    <br />




                    


                    <div class="row">
                        <div class="col-lg-6">
                            <asp:Button ID="btnGenerarControl" OnClick="btnGenerarControl_Click" runat="server" Text="Generar Control" class="btn btn-lg btn-primary btn-block" />

                        </div>
                    </div>
                    <br />
                </div>


            </div>
        </div>
  </div>
        <div class="col-lg-6">
            <h2>Cumplimiento</h2>
            <br />
            <asp:GridView ID="GridControlUso" runat="server"
                class="table table-bordered table-hover table-striped"
                AllowPaging="True"
                PageSize="10"
                AllowSorting="True"
                AutoGenerateColumns="False" DataSourceID="SqlDataSourceDatosControlUso"
                OnRowDataBound="GridControlUso_RowDataBound"
                OnRowCommand="GridControlUso_RowCommand"
                OnDataBound="GridControlUso_DataBound">
                <Columns>
                    <asp:BoundField DataField="id" HeaderText="id" SortExpression="id" />
                    <asp:BoundField DataField="Nombre Profesor" HeaderText="Nombre Profesor" ReadOnly="True" SortExpression="Nombre Profesor" />
                    <asp:BoundField DataField="Cantidad Horas Asignadas" HeaderText="Cantidad Horas Asignadas" ReadOnly="True" SortExpression="Cantidad Horas Asignadas" />
                    <asp:BoundField DataField="Cantidad Acciones Asignadas" HeaderText="Cantidad Acciones Asignadas" ReadOnly="True" SortExpression="Cantidad Acciones Asignadas" />
                    <asp:BoundField DataField="Mes" HeaderText="Mes" ReadOnly="True" SortExpression="Mes" />
                    <asp:BoundField DataField="Horas Totales de Uso" HeaderText="Horas Totales de Uso" ReadOnly="True" SortExpression="Horas Totales de Uso" />
                    <asp:BoundField DataField="Cantidad de Acciones" HeaderText="Cantidad de Acciones" ReadOnly="True" SortExpression="Cantidad de Acciones" />
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


        <br />
    </div>
       

    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Ayuda en el Control de Uso</h5>
                    <button id="btnCerrarSesion" type="button" class="close" data-dismiss="modal" aria-label="Close" runat="server">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    El control de uso permite a los administradores controlar cuántas horas deben cumplir los profesores utilizando la plataforma AuLearn.
                    <br />
                    <br />
                    Con el fin de hacer un seguimiento, es posible obtener el cumplimiento tanto en horas como en la cantidad de actividades que se realizan. 
                    <br />
                    <br />
                    <div align="center">
                       <img src="Images/aulearn/pc.jpg" />
                    </div>
                    <br />
                    <br />
                    <strong>Ejemplo 1:</strong> Si se agregan 2 horas semanales, esto significa que el usuario/profesor tendrá que utilizar el sistema al menos 8 horas mensuales para cumplir con los objetivos de la institución.
                    <br />
                    <br />
                    <strong>Ejemplo 2:</strong> Si se agregan 5 acciones, esto significa que el usuario/profesor tendrá que realizar al menos 5 acciones en el sistema, entiéndase a "acciones" como: Editar perfil, evaluar alumnos, subir guías, generar reportes.
                    <br />
                    <br />



                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" data-dismiss="modal">Ok</button>

                </div>
            </div>
        </div>
    </div>


    <script>
        function edValueKeyPress() {
            var valorCantHoras = document.getElementById('<%= CantHoras.ClientID %>');
            var valorTotal = valorCantHoras.value * 4


            var lblValue = document.getElementById("lblPrev");
            var lblValueACC = document.getElementById("lblPrevAcc");
            //PARA TEXTBOX ES .value
            //lblValue.value = "Previsualización nombre de usuario: " + primernombre + "." + primerApellido;

            lblValue.innerText = valorTotal + " Horas.";
            lblValueACC.innerText = valorTotal * 4;
        }

    </script>
    <br />
    <br />
</asp:Content>
