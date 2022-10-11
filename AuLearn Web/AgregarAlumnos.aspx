<%@ Page Title="" Language="C#" MasterPageFile="~/SitioM.Master" AutoEventWireup="true" CodeBehind="AgregarAlumnos.aspx.cs" Inherits="AuLearn_Web.AgregarAlumnos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BreadCrumb" runat="server">
    <i class="fa fa-users" ></i> Administración Alumnos

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">

   <div class="row">
        <div class="col-lg-5" runat="server">

            <div class="card mb-3">
                <div class="card-header">
                    <i class="fa fa-users" ></i><asp:label runat="server" id="labelTitulo"> Administración Alumnos</asp:label>
                </div>
                <div class="card-body">
                     
       
                    <div class="row">
     
                        <div class="col-lg-6">
                           
                            <label>Rut</label>
                            <asp:TextBox ID="txtRut" runat="server" class="form-control" placeholder="Rut" MaxLength="10"></asp:TextBox>
                            <asp:TextBox ID="txtRutOculto" Visible="false" disabled="true" runat="server" class="form-control" placeholder="Rut"></asp:TextBox>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidatorRut"
                                ControlToValidate="txtRut"
                                Display="Dynamic"
                                ErrorMessage="Debe ingresar un rut"
                                runat="server"
                                ForeColor="Red" /> 
                             
                            <asp:CustomValidator ID="CustomValidator1" runat="server" 
            ClientValidationFunction="validar_rut" ControlToValidate="txtRut" 
            Display="Dynamic" ErrorMessage="RUT incorrecto" SetFocusOnError="True" ForeColor="Red"></asp:CustomValidator>
                            <br />
                        </div>
                        <div class="col-lg-6">
                            <label>Fecha de nacimiento</label>
                            <asp:TextBox ID="txtcalendario" runat="server" class="form-control" TextMode="Date"  min="1910-01-01"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorFecha"
                                ControlToValidate="txtcalendario"
                                Display="Static"
                                ErrorMessage="Debe ingresar una Fecha válida"
                                runat="server"
                                ForeColor="Red" />
                            <br />
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-6">


                            <label>Nombres</label>
                            <asp:TextBox ID="txtNombre" runat="server" class="form-control col-xs-3" placeholder="Nombres" ></asp:TextBox>

                            <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidatorNombre" runat="server" ControlToValidate="txtNombre" ErrorMessage="Debe ingresar un nombre" ForeColor="Red" ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>--%>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorNombre"
                                ControlToValidate="txtNombre"
                                Display="Static"
                                ErrorMessage="Debe ingresar un nombre"
                                runat="server"
                                ForeColor="Red" />
                            <br />
                        </div>
                        <div class="col-lg-6">
                            <label>Apellidos</label>
                            <asp:TextBox ID="txtApellido" runat="server" class="form-control" placeholder="Apellidos" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorApellido"
                                ControlToValidate="txtApellido"
                                Display="Static"
                                ErrorMessage="Debe ingresar un apellido"
                                runat="server"
                                ForeColor="Red" />
                            <br />
                        </div>
                    </div>
                     <%-- <label>Previsualización nombre de usuario: </label>--%>
                   <%-- <asp:TextBox ID="txtPrev" runat="server" class="form-control col-xs-3" placeholder="Ecriba un nombre y apellido en los campos anteriores"></asp:TextBox>--%>
                    <%--<span id="lblPrev" style="color:blue" ></span>
                    <br />
                    <br /> --%>
                    <div id="divOtrosCargos" runat="server">

                        <label>Correo de Contacto</label>
                        <div class="form-group input-group">
                            <span class="input-group-addon">@</span>
                            <asp:TextBox ID="txtCorreo" runat="server" class="form-control" placeholder="Ejemplo@correo.cl" ></asp:TextBox>
                            <asp:RegularExpressionValidator Display="Dynamic" runat="server" ID="validaEmail" ControlToValidate="txtCorreo" ErrorMessage="Email Inválido" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>

                        </div>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                            ControlToValidate="txtCorreo"
                            Display="Static"
                            ErrorMessage="Debe ingresar un correo"
                            runat="server"
                            ForeColor="Red" />--%>
                        <br />

                        <label>Teléfono de Contacto</label>
                        <asp:TextBox ID="TextFono" runat="server" class="form-control" placeholder="Ejemplo: 9 1234 5678" MaxLength="9"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextFono" ErrorMessage="Ingrese sólo numeros" ForeColor="Red" ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>
                        <br />

                    </div>

                    <div id="divAlumno" runat="server">


                        <label>Tipo de Discapacidad</label><br />
                        <label for="inputPersona" class="sr-only">Tipo Discapacidad</label>
                        <asp:DropDownList ID="DropDownList2" runat="server" class="form-control" DataSourceID="SqlDataSourceT" DataTextField="tipo_discapacidad" DataValueField="id_tipo_discapacidad"></asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSourceT" runat="server" ConnectionString="<%$ ConnectionStrings:aulearnConnectionString %>" SelectCommand="SELECT [id_tipo_discapacidad], [tipo_discapacidad] FROM [Tipo_discapacidad]"></asp:SqlDataSource>
                        <br />

                    </div>

                    <div class="row">
                        <div class="col-lg-3">
                            <asp:Button ID="btnAgregar" OnClick="btnAgregar_Click" runat="server" Text="Agregar" class="btn btn-lg btn-primary btn-block" />
                        </div>
                        <div class="col-lg-3">
                            <asp:Button ID="btnCancelar" OnClick="btnCancelar_Click" runat="server" Text="Cancelar" class="btn btn-lg btn-danger btn-block" />
                        </div>
                    </div>
                    <br />





                </div>
            </div>
            <br />
            <br />
        </div>



        <div class="col-lg-6">
            <h2>Listado Alumnos</h2>
            <br />
            <asp:GridView ID="GridViewAlumnos" runat="server"
                class="table table-bordered table-hover table-striped"
                AllowPaging="True"
                PageSize="6"
                AllowSorting="True"
                AutoGenerateColumns="False"
                DataKeyNames="Rut"
                DataSourceID="SqlDataSourcePersonas"
                OnRowDataBound="GridViewAlumnos_RowDataBound"
                OnRowCommand="GridViewAlumnos_RowCommand">
                <Columns>
                    <asp:BoundField DataField="Rut" HeaderText="Rut" ReadOnly="True" SortExpression="Rut" />
                    <asp:BoundField DataField="Nombres" HeaderText="Nombres" ReadOnly="True" SortExpression="Nombres" />
                    <asp:BoundField DataField="Apellidos" HeaderText="Apellidos" ReadOnly="True" SortExpression="Apellidos" />
                    <asp:BoundField DataField="Fecha de Nacimiento" HeaderText="Fecha de Nacimiento" SortExpression="Fecha de Nacimiento" DataFormatString="{0:d}" HtmlEncode=false />
                    <asp:BoundField DataField="Edad" HeaderText="Edad" ReadOnly="True" SortExpression="Edad" />
                    <asp:BoundField DataField="Discapacidad" HeaderText="Discapacidad" SortExpression="Discapacidad" />
                    <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado" /> 
                    <asp:ButtonField ButtonType="Button" CommandName="Estado" HeaderText="Estado" Text="Cambiar Estado" ControlStyle-CssClass="fa-check-square" />
                    <asp:ButtonField ButtonType="Button" CommandName="Editar" HeaderText="Editar Alumno" Text="Editar" ControlStyle-CssClass="btn btn-primary btn-block"  />
                    <%--<asp:TemplateField>
                        <ItemTemplate>
                            <button type="button" class="btn btn-default btn-sm">
                                <span class="glyphicon glyphicon-trash"></span>Trash 
                            </button>
                        </ItemTemplate>
                    </asp:TemplateField>--%>

                </Columns>
            </asp:GridView>

            <asp:SqlDataSource ID="SqlDataSourcePersonas" runat="server" ConnectionString="<%$ ConnectionStrings:aulearnConnectionString %>" SelectCommand="select P.rut_persona as 'Rut',
P.nombre AS 'Nombres',
P.apellido  AS 'Apellidos',
P.fecha_nacimiento as 'Fecha de Nacimiento',
(select (cast(datediff(dd, P.fecha_nacimiento ,GETDATE()) / 365.25 as int))) as 'Edad',
TD.tipo_discapacidad as 'Discapacidad',
P.activo as 'Estado'
 from Persona P
inner join Cargo C on C.id_cargo=P.id_cargo
inner join Estudiante E on E.rut_persona=P.rut_persona
inner join Tipo_discapacidad TD on TD.id_tipo_discapacidad=E.id_tipo_discapacidad "></asp:SqlDataSource>


        </div>
    </div>

    <br />

    <script>
        function edValueKeyPress() {
            var valorTxtNombre = document.getElementById('<%= txtNombre.ClientID %>');
            var nombre = valorTxtNombre.value;

            var primernombre = nombre.split(' ')[0];

            var valorTxtApellido = document.getElementById('<%= txtApellido.ClientID %>');
            var Apellido = valorTxtApellido.value;

            var primerApellido = Apellido.split(' ')[0];
            var nombreusuario = primernombre + "." + primerApellido;

            var lblValue = document.getElementById("lblPrev");
            //PARA TEXTBOX ES .value
            //lblValue.value = "Previsualización nombre de usuario: " + primernombre + "." + primerApellido;

            lblValue.innerText = "Previsualización nombre de usuario: " + nombreusuario;

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


