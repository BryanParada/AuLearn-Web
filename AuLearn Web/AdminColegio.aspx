<%@ Page Title="" Language="C#" MasterPageFile="~/SitioM.Master" AutoEventWireup="true" CodeBehind="AdminColegio.aspx.cs" Inherits="AuLearn_Web.AdminColegio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BreadCrumb" runat="server">
    <i class="fa fa-university"></i> Administración Colegio
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
    <!DOCTYPE html>

    <div class="row">
        <div class="col-lg-8" id="divBody" runat="server">

            <div class="card mb-3">
                <div class="card-header">
                    <i class="fa fa-university"></i> Administración Colegio
                </div>
                <div class="card-body">



                    <div class="row">
                        <div class="col-lg-5">
                            <asp:Label runat="server" Visible="false" ID="labelIDCOL"> </asp:Label>
                            <label>Rut Colegio</label>
                            <br />
                            <asp:Label ID="lblRUTOculto" runat="server" Visible="false"></asp:Label>
                            <asp:TextBox ID="txtRut" runat="server" class="form-control" placeholder="Rut Colegio"  disabled="true"></asp:TextBox>
                            
                             <asp:CustomValidator ID="CustomValidator2" runat="server" 
            ClientValidationFunction="validar_rut" ControlToValidate="txtRut" 
            Display="Dynamic" ErrorMessage="RUT incorrecto" SetFocusOnError="True" ForeColor="Red"></asp:CustomValidator>
                            <br />
                            <label>Nombre Colegio</label>
                            <br />
                            <asp:TextBox ID="txtNombre" runat="server" class="form-control" placeholder="Nombre Colegio"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                ControlToValidate="txtNombre"
                                Display="Static"
                                ErrorMessage="Debe ingresar un nombre"
                                runat="server"
                                ForeColor="Red" />
                            <br />
                            <label>Comuna</label>
                            <asp:DropDownList ID="DropDownListComuna" runat="server" class="form-control" DataSourceID="SqlDataSourceComunas" DataTextField="descripcion" DataValueField="id_comuna"></asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSourceComunas" runat="server" ConnectionString="<%$ ConnectionStrings:aulearnConnectionString %>" SelectCommand="SELECT * FROM [Comuna]"></asp:SqlDataSource>
                            <br />
                            <label>Dirección</label>
                            <br />
                            <asp:TextBox ID="txtDireccion" runat="server" class="form-control" placeholder="Dirección"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorDireccion"
                                ControlToValidate="txtDireccion"
                                Display="Static"
                                ErrorMessage="Debe ingresar una dirección"
                                runat="server"
                                ForeColor="Red" />
                            <br />

                            <label>Teléfono</label>
                            <asp:TextBox ID="TextFono" runat="server" class="form-control" placeholder="Ejemplo: 9 1234 5678" MaxLength="10"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextFono" ErrorMessage="Ingrese sólo números" ForeColor="Red" ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorTelefono"
                                ControlToValidate="TextFono"
                                Display="Static"
                                ErrorMessage="Debe ingresar un teléfono"
                                runat="server"
                                ForeColor="Red" />
                            <br />


                            <label>Email</label>
                            <div class="form-group input-group">
                                <span class="input-group-addon">@</span>
                                <asp:TextBox ID="txtMail" runat="server" class="form-control" placeholder="tucorreo@ejemplo.com"></asp:TextBox>
                            </div>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidatrorEmail"
                                ControlToValidate="txtMail"
                                Display="Dynamic"
                                ErrorMessage="Debe ingresar un correo"
                                runat="server" ForeColor="Red" />
                           
                          <asp:RegularExpressionValidator Display="Dynamic" runat="server" ID="validaEmail" ControlToValidate="txtMail" ErrorMessage="Email Inválido" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            <br />
                            <label>Sitio Web</label>
                            <asp:TextBox ID="txtSitio" runat="server" class="form-control" placeholder="Ejemplo: colegio.cl"></asp:TextBox>
                            <br />
                            <br />




                            <div class="row">
                                <div class="col-lg-6">
                                    <asp:Button ID="btnActualizar" OnClick="btnActualizar_Click" runat="server" Text="Actualizar" class="btn btn-lg btn-primary btn-block" />
                                </div>
                            </div>
                            <br />

                        </div>

                        <div class="col-lg-4">

                            <br />
                            <div class="card mb-3">
                                <div class="card-header">
                                    <i class="fa fa-file-image-o"></i> Logo Actual
                                </div>
                                <div class="card-body">
                                    <img width="150" height="150" src="traerImagen.ashx" />
                                </div>


                            </div>
                            <label>Cambiar Logo Colegio</label>
                            <asp:FileUpload ID="imgFileUpload" runat="server" onchange="showpreview(this);" accept=".png,.jpg,.jpeg"/>

                            <asp:CustomValidator ID="CustomValidator1"
                                ClientValidationFunction="ValidateFile" runat="server"
                                ControlToValidate="imgFileUpload"
                                Display="dynamic" ErrorMessage="Sólo se permiten imágenes">
                            </asp:CustomValidator>


                            <br />
                            <br />
                            <%--   <asp:Image runat="server" ID="logoCol" />--%>
                            <div class="card mb-3" id="divCardPrev" style="display: none">
                                <div class="card-header">
                                    <i class="fa fa-file-image-o"></i>Previsualización Logo
                                </div>
                                <div class="card-body">
                                    <img id="imgpreview" height="150" width="150" src="" style="border-width: 0px; visibility: hidden;" />
                                </div>


                            </div>

                            <br />
                            <br />


                        </div>
                        <br />
                        <br />


                    </div>


                </div>

            </div>


        </div>
    </div>



    <br />
    <br />

    <script type="text/javascript">
        function showpreview(input) {

            if (input.files && input.files[0]) {

                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#imgpreview').css('visibility', 'visible');
                    $('#imgpreview').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
                document.getElementById('divCardPrev').style.display = "block";
            }

        }

    </script>

    <script language="javascript">
        function ValidateFile(source, args) {
            try {
                var fileAndPath =
                   document.getElementById(source.controltovalidate).value;
                var lastPathDelimiter = fileAndPath.lastIndexOf("\\");
                var fileNameOnly = fileAndPath.substring(lastPathDelimiter + 1);
                var file_extDelimiter = fileNameOnly.lastIndexOf(".");
                var file_ext = fileNameOnly.substring(file_extDelimiter + 1).toLowerCase();
                if (file_ext != "jpg") {
                    args.IsValid = false;
                    if (file_ext != "gif")
                        args.IsValid = false;
                    if (file_ext != "png") {
                        args.IsValid = false;
                        return;
                    }
                }
            } catch (err) {
                txt = "There was an error on this page.\n\n";
                txt += "Error description: " + err.description + "\n\n";
                txt += "Click OK to continue.\n\n";
                txt += document.getElementById(source.controltovalidate).value;
                alert(txt);
            }

            args.IsValid = true;
        }
    </script>
    <script>
        function validar_rut(source, arguments) {
            var rut = arguments.Value; suma = 0; mul = 2; i = 0;

            for (i = rut.length - 3; i >= 0; i--) {
                suma = suma + parseInt(rut.charAt(i)) * mul;
                mul = mul == 7 ? 2 : mul + 1;
            }


            var dvr = '' + (11 - suma % 11);

            if (dvr == '10') dvr = 'K'; else if (dvr == '11') dvr = '0';
            {


                if (rut.charAt(rut.length - 2) != "-" || rut.charAt(rut.length - 1).toUpperCase() != dvr) {
                    arguments.IsValid = false;
                }

                else {
                    arguments.IsValid = true;
                }
            }


        }


    </script>


</asp:Content>
