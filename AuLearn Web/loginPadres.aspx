<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="loginPadres.aspx.cs" Inherits="AuLearn_Web.loginPadres" %>

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

       <form class="form-signin" id="form2" runat="server">
           <img src="Images/aulearn/aulearn icono.PNG" height="70" align="middle"><br />

        <h2 class="form-signin-heading">Ingrese Los Datos</h2><br />

           <label>Rut Estudiante</label> 
        <asp:TextBox ID="txtRut" runat="server"  class="form-control" placeholder="Ejemplo: 12345678-1" required autofocus ></asp:TextBox>

         <br />
        <div >
           <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" OnClick="btnIngresar_Click" class="btn btn-lg btn-primary btn-block"/>
        </div> 
        
      </form>

    </div> <!-- /container -->


    </div>
     
     <script src="Scripts/js/ie10-viewport-bug-workaround.js"></script>
</body>
</html>
