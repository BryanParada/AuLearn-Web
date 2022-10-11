<%@ Page Title="" Language="C#" MasterPageFile="~/SitioM.Master" AutoEventWireup="true" CodeBehind="subirGuia.aspx.cs" Inherits="AuLearn_Web.subirGuia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BreadCrumb" runat="server">
    Guías de Aprendizaje

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
     

        <div class="row">

            <div class="col-lg-5">

                <asp:label runat="server" id="labelTitulo" class="h2" >Subir Guías De Reforzamiento</asp:label>
                <br /><br />
                <asp:Label runat="server" ID="labelRut" Visible="false"></asp:Label>
                <label>Seleccione Curso </label>
                <br />
                <asp:DropDownList ID="DropDownCurso" runat="server" class="form-control" DataSourceID="SqlDataSource1" DataTextField="nombre_curso" DataValueField="id_curso" AutoPostBack="True" AppendDataBoundItems="true"></asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:aulearnConnectionString %>" SelectCommand="select C.id_curso, C.nombre_curso from Curso as C
inner join Asignar_curso as A on C.id_curso=A.id_curso
inner join Usuario as U on U.id_usuario=A.id_usuario
where U.rut_persona = @rut_persona">
                    <SelectParameters>
                                    <asp:ControlParameter ControlID="labelRut" Name="rut_persona" PropertyName="Text" />
                                     
                                </SelectParameters>
                </asp:SqlDataSource>
                <br />

                <label>Seleccione Materia</label><br />
                <asp:DropDownList ID="DropDownMateria" runat="server" class="form-control" DataSourceID="SqlDataSource2" DataTextField="materia" DataValueField="id_materia" AutoPostBack="True"></asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:aulearnConnectionString %>" SelectCommand="SELECT * FROM [Materia] WHERE ([id_curso] = @id_curso)">


                    <SelectParameters>
                        <asp:ControlParameter ControlID="DropDownCurso" Name="id_curso" PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <br />

                <label>Seleccione Unidad </label>
                <br />
                <asp:DropDownList ID="DropDownUnidad" runat="server" class="form-control" DataSourceID="SqlDataSource3" DataTextField="descripcion" DataValueField="id_unidad"></asp:DropDownList>


                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:aulearnConnectionString %>" SelectCommand="select U.id_unidad, descripcion, M.materia from Unidad as U
inner join Materia as M on U.id_materia=M.id_materia
inner join Curso C on C.id_curso=M.id_curso
where M.id_materia = @id_materia and C.id_curso = @id_curso">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="DropDownMateria" Name="id_materia" PropertyName="SelectedValue" Type="Int32" />
                        <asp:ControlParameter ControlID="DropDownCurso" Name="id_curso" PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <br />

                <asp:TextBox ID="txtDescripcion" runat="server" class="form-control" TextMode="multiline" Columns="50" Rows="5" placeholder="Descripción..."></asp:TextBox>

                <br />
                <asp:FileUpload ID="f" runat="server" accept=".pdf" />
                <br />
                <br />
                 <div class="col-lg-6">
                <asp:Button ID="btnAlFTP" runat="server" Text="subir" OnClick="btnAlFTP_Click" class="btn btn-lg btn-primary btn-block" AutoPostBack="True"/>
                     </div>

            </div>

            <div class="col-lg-6">
                <h2>Guías Subidas</h2>
                <br />

                <asp:GridView ID="GridViewAlumnos" OnRowCreated="GridViewAlumnos_RowCreated" AutoPostBack="True"  class="table table-bordered table-hover table-striped" runat="server" OnRowCommand="GridViewAlumnos_RowCommand" AutoGenerateColumns="False" DataKeyNames="id_guia" DataSourceID="SqlDataSourceGuias"   >
                    <Columns>
                        <asp:BoundField DataField="id_guia" HeaderText="id_guia" InsertVisible="False" ReadOnly="True" SortExpression="id_guia" />
                        <asp:BoundField DataField="Curso" HeaderText="Curso" SortExpression="Curso" />
                        <asp:BoundField DataField="Materia" HeaderText="Materia" SortExpression="Materia" />
                        <asp:BoundField DataField="Unidad" HeaderText="Unidad" SortExpression="Unidad" />
                        <asp:BoundField DataField="ruta_archivo" HeaderText="ruta_archivo" SortExpression="ruta_archivo" />
                        <asp:BoundField DataField="Nombre Archivo" HeaderText="Nombre Archivo" SortExpression="Nombre Archivo" />
                        <asp:BoundField DataField="Descripción" HeaderText="Descripción" SortExpression="Descripción" />
                        <asp:ButtonField ButtonType="Button" CommandName="Descargar" HeaderText="Descargar" Text="Descargar" ControlStyle-CssClass="btn btn-primary btn-block"  />
                        <asp:ButtonField ButtonType="Button" CommandName="Eliminar" HeaderText="Eliminar" Text="Eliminar" ControlStyle-CssClass="btn btn-primary btn-block"  />
                    </Columns>
                    
                    
                </asp:GridView>
                
                 
                <asp:SqlDataSource ID="SqlDataSourceGuias" runat="server" ConnectionString="<%$ ConnectionStrings:aulearnConnectionString %>" SelectCommand="select 
                    G.id_guia 'id_guia',
                     C.nombre_curso 'Curso',
                     M.materia 'Materia',
                     U.descripcion 'Unidad',
                     G.ruta_archivo 'ruta_archivo',
                     G.nombre_archivo 'Nombre Archivo',
                    G.descripcion 'Descripción'
                    from  Guia G
inner join Curso C on G.id_curso=C.id_curso
Inner join Materia M on G.id_materia=M.id_materia
inner join Unidad U on G.id_unidad=U.id_unidad
inner join Asignar_curso AC on AC.id_curso=C.id_curso
inner join Usuario usu on usu.id_usuario=AC.id_usuario
where usu.rut_persona = @rut_persona">
                    <SelectParameters>
                                    <asp:ControlParameter ControlID="labelRut" Name="rut_persona" PropertyName="Text" />
                                     
                                </SelectParameters>
                </asp:SqlDataSource>
                
                 
            </div>


        </div>
  <br />
                <br />
</asp:Content>
