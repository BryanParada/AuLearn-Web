<%@ Page Title="" Language="C#" MasterPageFile="~/SitioF.Master" AutoEventWireup="true" CodeBehind="Contacto.aspx.cs" Inherits="AuLearn_Web.Contacto" %>
 
<asp:Content ID="Content3" ContentPlaceHolderID="Titulo" runat="server">
    Contáctanos

</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="CardHeader" runat="server">
    Contacto

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
    
    <asp:Label ID="Label1" runat="server" Text="Nombre:"></asp:Label>
    <asp:TextBox ID="TextNombre" runat="server"  class="form-control" Width="416px" placeholder="Nombre"></asp:TextBox>
    <br />
    <asp:Label ID="Label2" runat="server" Text="Teléfono:"></asp:Label>
    <asp:TextBox ID="TextFono" runat="server"  class="form-control" Width="413px" placeholder="Ejemplo: 9 1234 5678" MaxLength="10"></asp:TextBox>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextFono" ErrorMessage="Ingrese solo numeros" ForeColor="Red" ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>
    <br />
    <asp:Label ID="Label3" runat="server" Text="Email:"></asp:Label>
    <asp:TextBox ID="TextMail" runat="server"  class="form-control" Width="411px" placeholder="tucorreo@ejemplo.com"></asp:TextBox>
    <asp:RegularExpressionValidator Display="Dynamic" runat="server" ID="validaEmail" ControlToValidate="TextMail" ErrorMessage="Email Inválido" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
    <br />
    <asp:Label ID="Label4" runat="server" Text="Mensaje:"></asp:Label>
    <asp:TextBox ID="TextMensaje" runat="server"  class="form-control" Height="174px" TextMode="MultiLine" Width="582px"></asp:TextBox>
    <br />
    <asp:Button class="btn btn-primary btn-block" ID="Enviar" runat="server" Text="Enviar" OnClick="Enviar_Click" />
    <br /> 
    <%--<a class="btn btn-primary" href="index.html">Salir</a>--%>
    <div class="text-center"> 
           <a class="d-block small" href="index.html">Volver</a>
        </div>
   
</asp:Content>
