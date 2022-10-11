<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="AuLearn_Web.login" %>

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

    <div class="container">

       <form class="form-signin" id="form1" runat="server">
           <img src="Images/aulearn/aulearn icono.PNG" height="70" align="middle"><br /><br />
        <h2 class="form-signin-heading">Ingrese sus Datos</h2><br />

        <label for="inputUsuario" class="sr-only">Nombre de Usuario</label>
        <asp:TextBox ID="txtUsuario" runat="server"  class="form-control" placeholder="Nombre de Usuario" required autofocus ></asp:TextBox><br />

        <label for="inputPassword" class="sr-only">Contraseña</label>
        <asp:TextBox ID="txtPass" type="password" runat="server" MaxLength="20" class="form-control" placeholder="Contraseña" required ></asp:TextBox><br />

        <div >
           <asp:Button ID="btnIngresar"  runat="server" OnClick="Button1_Click" Text="Ingresar" class="btn btn-lg btn-primary btn-block"/>
        </div> 
           <%--<a class="btn btn-primary" href="index.html">Salir</a>--%>
   
        
      </form>
         <div class="text-center"> 
           <a class="d-block small" href="index.html">Volver</a>
        </div>
    </div> <!-- /container -->


    <!-- IE10 viewport hack for Surface/desktop Windows 8 bug -->
    <script src="Scripts/js/ie10-viewport-bug-workaround.js"></script>
  </body>
</html>