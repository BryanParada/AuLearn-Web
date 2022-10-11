<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DescargarGuias.aspx.cs" Inherits="AuLearn_Web.DescargarGuias" %>

<!DOCTYPE html>
<html lang="en">
  <head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
    <meta name="description" content="">
    <meta name="author" content="">
    <link rel="icon" href="favicon.ico">

    <title>Acceso</title>

    <!-- Bootstrap core CSS -->
    <link href="Content/css/bootstrap.min.css" rel="stylesheet">

    <!-- IE10 viewport hack for Surface/desktop Windows 8 bug -->
    <link href="Content/css/ie10-viewport-bug-workaround.css" rel="stylesheet">

    <!-- Custom styles for this template -->
    <link href="Content/css/signin.css" rel="stylesheet">

    <!-- Just for debugging purposes. Don't actually copy these 2 lines! -->
    <!--[if lt IE 9]><script src="../../assets/js/ie8-responsive-file-warning.js"></script><![endif]-->
    <script src="Scripts/js/ie-emulation-modes-warning.js"></script>

    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
      <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->


  </head>

<body background="Images/aulearn/fondo.png">
    
    <div>
    
 
        <div class="container">



            <form id="form1" runat="server">
        
           <img src="Images/aulearn/aulearn icono.PNG" height="70" align="middle">
           <div class="row">
           <div class="col-lg-8">

               <div class="jumbotron">
    <h1><i class="fa fa-users" style="color:#bf6363"></i><strong>  <asp:Label runat="server" ID="labelTitulo" class="h2">Guías de Aprendizaje</asp:Label> </strong></h1>
     
    <p>A continuación se muestran las guías de aprendizaje, realizadas durante el semestre, con el fin de fomentar el reforzamiento en casa.</p>
    




               
                <br />
                <br />
               <asp:Label runat="server" ID="labelRut" Visible="false" class="h2">rut</asp:Label>
                <asp:GridView ID="GridViewAlumnos" OnRowCreated="GridViewAlumnos_RowCreated" AutoPostBack="True" class="table table-bordered bs-table table table-bordered" runat="server" OnRowCommand="GridViewAlumnos_RowCommand" AutoGenerateColumns="False" DataSourceID="SqlDataSourceGuias"   >
                    <Columns>
                        <asp:BoundField DataField="Curso" HeaderText="Curso" SortExpression="Curso" />
                        <asp:BoundField DataField="Materia" HeaderText="Materia" SortExpression="Materia" />
                        <asp:BoundField DataField="Unidad" HeaderText="Unidad" SortExpression="Unidad" />
                        <asp:BoundField DataField="ruta_archivo" HeaderText="ruta_archivo" SortExpression="ruta_archivo" />
                        <asp:BoundField DataField="Nombre Archivo" HeaderText="Nombre Archivo" SortExpression="Nombre Archivo" />
                        <asp:ButtonField ButtonType="Button" CommandName="Descargar" HeaderText="Descargar" Text="Descargar" ControlStyle-CssClass="btn btn-primary btn-block"  />
                    </Columns>
                    
                    
                </asp:GridView>
                
                 
                <asp:SqlDataSource ID="SqlDataSourceGuias" runat="server" ConnectionString="<%$ ConnectionStrings:aulearnConnectionString %>" SelectCommand="select C.nombre_curso 'Curso', M.materia 'Materia', U.descripcion 'Unidad', G.ruta_archivo 'ruta_archivo', G.nombre_archivo 'Nombre Archivo' from  Guia G
inner join Curso C on G.id_curso=C.id_curso
Inner join Materia M on G.id_materia=M.id_materia
inner join Unidad U on G.id_unidad=U.id_unidad
inner join Integrantes_curso IC on IC.id_curso=C.id_curso
inner join Estudiante E on IC.id_estudiante=E.id_estudiante
where E.rut_persona = @rut_persona">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="labelRut" Name="rut_persona" PropertyName="Text" />
                    </SelectParameters>
               </asp:SqlDataSource>
                
                 
            </div>

                 <%--<div class="jumbotron">
    <h1><i class="fa fa-users" style="color:#bf6363"></i><strong>  <asp:Label runat="server" ID="label1" class="h2">Informe de Notas</asp:Label> </strong></h1>
     
    <p>A continuación se muestran el progreso de </p>
    




               
                <br />
                <br />
               
                
                 
            </div>--%>


                </div>
        

                </div>
                <a class="btn btn-primary" href="index.html">Salir</a>
       </form>

    </div> <!-- /container -->

        
    </div>
      
    
 
 


     <script src="Scripts/js/ie10-viewport-bug-workaround.js"></script>
</body>
</html>
