<%@ Page Title="" Language="C#" MasterPageFile="~/SitioM.Master" AutoEventWireup="true" CodeBehind="TESTActividades.aspx.cs" Inherits="AuLearn_Web.TESTActividades" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BreadCrumb" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">

    <script>function playSound() {
    document.getElementById('play').play();
}</script>
    <audio id="play" src="http://soundbible.com/grab.php?id=2180&type=wav"></audio>

<button onclick="playSound()">Play</button>

</asp:Content>
