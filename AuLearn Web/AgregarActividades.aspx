<%@ Page Title="" Language="C#" MasterPageFile="~/SitioM.Master" AutoEventWireup="true" CodeBehind="AgregarActividades.aspx.cs" Inherits="AuLearn_Web.AgregarActividades" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BreadCrumb" runat="server">
    Agregar Actividades

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
    <!DOCTYPE html>

    <div class="row">
        <div class="col-lg-4">
            <div class="card mb-3">
                <div class="card-header">
                    <i class="fa fa-university" style="color: green"></i>
                    <asp:Label runat="server" ID="labelTitulo">Evaluación Actividades</asp:Label>
                </div>
                <div class="card-body">

                    <div class="row">
                        <div class="col-lg-3">
                            <asp:Label runat="server" ID="labelRut" Visible="false"></asp:Label>
                            <label>Curso </label>
                            <br />
                            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" class="form-control" DataSourceID="SqlDataSourceC" DataTextField="nombre_curso" DataValueField="id_curso"></asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSourceC" runat="server" ConnectionString="<%$ ConnectionStrings:aulearnConnectionString %>" SelectCommand="select C.id_curso, C.nombre_curso from Curso as C
inner join Asignar_curso as A on C.id_curso=A.id_curso
inner join Usuario as U on U.id_usuario=A.id_usuario
where U.rut_persona = @rut_persona 
 ">
                                 <SelectParameters>
                                    <asp:ControlParameter ControlID="labelRut" Name="rut_persona" PropertyName="Text" />
                                     
                                </SelectParameters>

                            </asp:SqlDataSource>


                        </div>
                        <div class="col-lg-5">

                            <label>Materia</label><br />
                            <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" class="form-control" DataSourceID="SqlDataSource1" DataTextField="materia" DataValueField="id_materia"></asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:aulearnConnectionString %>" SelectCommand="SELECT [materia], [id_materia] FROM [Materia] WHERE ([id_curso] = @id_curso)">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="DropDownList1" Name="id_curso" PropertyName="SelectedValue" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>


                        </div>
                        <div class="col-lg-4">
                            <label>Tipo</label><br />

                            <asp:DropDownList ID="DropDownList3" runat="server" class="form-control" DataSourceID="SqlDataSourceTipoAct" DataTextField="tipo_actividad" DataValueField="id_tipo_actividad"></asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSourceTipoAct" runat="server" ConnectionString="<%$ ConnectionStrings:aulearnConnectionString %>" SelectCommand="SELECT [id_tipo_actividad], [tipo_actividad] FROM [Tipo_actividad]"></asp:SqlDataSource>

                            <br />
                        </div>
                        
                    </div>

                    <div class="row">
                        <div class="col-lg-6">
                            <label>Unidad</label><br />

                            <asp:DropDownList ID="DropDownListUnidad" runat="server" AutoPostBack="True" class="form-control" DataSourceID="SqlDataSourceUnidad" DataTextField="descripcion" DataValueField="id_unidad"></asp:DropDownList>



                            <asp:SqlDataSource ID="SqlDataSourceUnidad" runat="server" ConnectionString="<%$ ConnectionStrings:aulearnConnectionString %>" SelectCommand="select U.id_unidad, descripcion, M.materia from Unidad as U
inner join Materia as M on U.id_materia=M.id_materia
inner join Curso C on C.id_curso=M.id_curso
where M.id_materia = @id_materia and C.id_curso = @id_curso">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="DropDownList2" Name="id_materia" PropertyName="SelectedValue" />
                                    <asp:ControlParameter ControlID="DropDownList1" Name="id_curso" PropertyName="SelectedValue" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>



                        </div>


                        <div class="col-lg-6">
                            <label>Actividad a Evaluar</label><br />

                            <asp:DropDownList ID="DropDownListActividad" runat="server" class="form-control" DataSourceID="SqlDataSourceActividad" DataTextField="descripcion" DataValueField="id_actividad"></asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSourceActividad" runat="server" ConnectionString="<%$ ConnectionStrings:aulearnConnectionString %>" SelectCommand="select A.id_actividad, A.descripcion, M.materia, U.id_materia, U.id_unidad, U.descripcion from Actividad A
inner join Unidad as U on A.id_unidad=U.id_unidad
inner join Materia as M on U.id_materia=M.id_materia
inner join Curso C on C.id_curso=M.id_curso
where U.id_unidad = @id_unidad and M.id_materia = @id_materia and C.id_curso = @id_curso">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="DropDownListUnidad" Name="id_unidad" PropertyName="SelectedValue" />
                                    <asp:ControlParameter ControlID="DropDownList2" Name="id_materia" PropertyName="SelectedValue" />
                                    <asp:ControlParameter ControlID="DropDownList1" Name="id_curso" PropertyName="SelectedValue" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <br />
                        </div>


                    </div>

                    <div class="row">
                        <div class="col-lg-6">
                            <label>Alumno</label><br />
                            <asp:DropDownList ID="DropDownListAlumno" runat="server" class="form-control" DataSourceID="SqlDataSourceAlumnos" DataTextField="Nombre Alumno" DataValueField="id_estudiante"></asp:DropDownList>


                            <asp:SqlDataSource ID="SqlDataSourceAlumnos" runat="server" ConnectionString="<%$ ConnectionStrings:aulearnConnectionString %>" SelectCommand="select E.id_estudiante, P.nombre + ' ' + P.apellido AS 'Nombre Alumno'
from Estudiante as E
INNER JOIN Persona AS P ON E.rut_persona = P.rut_persona 
inner join Integrantes_curso as IC on E.id_estudiante=IC.id_estudiante
inner join Curso as C on C.id_curso=IC.id_curso
inner join Materia as M on M.id_curso=C.id_curso
where M.id_materia = @id_materia and C.id_curso = @id_curso and P.activo = 1">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="DropDownList2" Name="id_materia" PropertyName="SelectedValue" Type="Int32" />
                                    <asp:ControlParameter ControlID="DropDownList1" Name="id_curso" PropertyName="SelectedValue" Type="Int32" />
                                </SelectParameters>

                            </asp:SqlDataSource>


                        </div>
                        <div class="col-lg-6">
                            <label>Fecha Evaluación</label><br />
                            <asp:TextBox ID="txtFecha" MaxLength="10" runat="server" class="form-control" placeholder="Fecha de la Evaluación" TextMode="Date"></asp:TextBox><br />
                        </div>
                    </div>

                    <label>Nota</label><br />
                    <asp:TextBox ID="txtNota" Text="0" type="number" min="10" max="70"  MaxLength="2" runat="server" class="form-control col-lg-2"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorNota"
                        ControlToValidate="txtNota"
                        Display="Static"
                        ErrorMessage="Debe ingresar una nota"
                        runat="server"
                        ForeColor="Red" />
                    <br />

                    <asp:TextBox ID="txtObservacion" runat="server" class="form-control" TextMode="multiline" Columns="50" Rows="5" placeholder="Observación..."></asp:TextBox><br />

                    <asp:CheckBox Checked="true" ID="checkboxNivel" runat="server" Text="Evaluar Niveles del alumno." OnCheckedChanged="checkboxNivel_CheckedChanged" AutoPostBack="true" />
                     <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#myModal" data-placement="top" title="Obtener ayuda">Ayuda <i class="fa fa-question-circle "></i></button>
                    <br />
                    <br />
                    <label >Niveles de Desempeño</label><br />
                    <div class="row" id="divNiveles" runat="server">
                        <div class="col-lg-3">

                            <label>Interés</label><br />
                            <%--<input id="rangeCtrl" type="range"  min="0" max="10" runat="server"/>--%>
                            <asp:TextBox ID="N1" runat="server" type="number" class="form-control" Text="5" min="0" max="10" MaxLength="1"> </asp:TextBox>
                            <br />

                        </div>

                        <div class="col-lg-3">
                            <label>Emocional</label><br />

                            <asp:TextBox ID="N2" runat="server" type="number" class="form-control" Text="5" min="0" max="10" MaxLength="1"> </asp:TextBox>
                            <br />
                        </div>

                        <div class="col-lg-3">
                            <label>Colaboración</label><br />

                            <asp:TextBox ID="N3" runat="server" type="number" class="form-control" Text="5" min="0" max="10" MaxLength="1"> </asp:TextBox>
                            <br />
                        </div>

                        <div class="col-lg-3">
                            <label>Autocontrol</label><br />

                            <asp:TextBox ID="N4" runat="server" type="number" class="form-control" Text="5" min="0" max="10" MaxLength="1"> </asp:TextBox>
                            <br />
                        </div>


                    </div>

                  <%--  <div class="row" id="divTEST" runat="server">
                        <div class="col-lg-5">

                            <asp:GridView ID="GridNivel" OnRowCreated="GridNivel_RowCreated" runat="server" class="table table-bordered table-hover table-striped" AutoGenerateColumns="False" DataKeyNames="id_tipo_nivel" DataSourceID="SqlDataSourceNiveles">
                                <Columns>
                                    <asp:BoundField DataField="id_tipo_nivel" HeaderText="id_tipo_nivel" InsertVisible="False" ReadOnly="True" SortExpression="id_tipo_nivel" />
                                    <asp:BoundField DataField="Descripción del Nivel" HeaderText="Descripción del Nivel" SortExpression="Descripción del Nivel" />
                                    <asp:TemplateField HeaderText="Evaluación">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNivelesG" runat="server" Text="Evaluación" Visible="true" />
                                            <asp:TextBox ID="txtN" runat="server" type="number" class="form-control" Text="5" min="0" max="10" MaxLength="1"> </asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSourceNiveles" runat="server" ConnectionString="<%$ ConnectionStrings:aulearnConnectionString %>" SelectCommand="select TN.id_tipo_nivel, TN.descripcion_nivel as 'Descripción del Nivel' from Tipo_nivel TN"></asp:SqlDataSource>
 
                        </div>
                    </div>--%>


                    
                    <br />

                    <div class="row">
                        <div class="col-lg-4">
                            <asp:Button ID="btnAgregar" OnClick="btnAgregar_Click" runat="server" Text="Evaluar" class="btn btn-lg btn-primary btn-block" />
                        </div>
                        <div class="col-lg-4">
                            <asp:Button ID="btnCancelar" OnClick="btnCancelar_Click" runat="server" Text="Cancelar" class="btn btn-lg btn-danger btn-block" />
                        </div>
                    </div>
                    <br />
                </div>
            </div>
        </div>

        <div class="col-lg-7">
            <h2>Listado Evaluaciones</h2>
            <br />

            <asp:GridView ID="GridView"
                OnRowCreated="GridView_RowCreated"
                OnRowCommand="GridView_RowCommand"
                runat="server"
                OnRowDataBound="GridView_RowDataBound"
                DataSourceID="SqlDataSource2"
                AutoGenerateColumns="False"
                class="table table-bordered table-hover table-striped"
                AllowPaging="True"
                PageSize="5"
                AllowSorting="true">
                <Columns>
                    <asp:BoundField DataField="id_nota" HeaderText="id_nota" SortExpression="id_nota" Visible="false" />

                    <asp:BoundField DataField="Curso" HeaderText="Curso" SortExpression="Curso" />
                    <asp:BoundField DataField="Grado Discapacidad" HeaderText="Grado Discapacidad" SortExpression="Grado Discapacidad" />
                    <asp:BoundField DataField="Nombre Alumno" HeaderText="Nombre Alumno" ReadOnly="True" SortExpression="Nombre Alumno" />
                    <asp:BoundField DataField="Nota" HeaderText="Nota" SortExpression="Nota" />
                    <asp:BoundField DataField="Observación" HeaderText="Observación" SortExpression="Observación" />

                    <asp:BoundField DataField="Descripción Actividad" HeaderText="Descripción Actividad" SortExpression="Descripción Actividad" />
                    <asp:BoundField DataField="Tipo Actividad" HeaderText="Tipo Actividad" SortExpression="Tipo Actividad" />
                    <asp:BoundField DataField="Materia" HeaderText="Materia" SortExpression="Materia" />
                    <asp:BoundField DataField="Unidad" HeaderText="Unidad" SortExpression="Unidad" />
                    <asp:ButtonField ButtonType="Button" CommandName="Editar" HeaderText="Editar Evaluación" Text="Editar" ControlStyle-CssClass="btn btn-primary btn-block" Visible="false" />

                </Columns>
            </asp:GridView>




            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:aulearnConnectionString %>" SelectCommand="select N.id_nota as id_nota, 
 C.nombre_curso as 'Curso',
 GD.grado_discapacidad as 'Grado Discapacidad',
 P.nombre + ' ' + P.apellido AS 'Nombre Alumno',
 N.nota as 'Nota', 
 N.observacion as 'Observación',
 A.descripcion as 'Descripción Actividad',
 TA.tipo_actividad as 'Tipo Actividad',
 M.materia as 'Materia',
 Uni.descripcion as 'Unidad'
  from Notas as N
 inner join Estudiante e on e.id_estudiante=N.id_estudiante
 inner join Integrantes_curso IC on IC.id_estudiante=e.id_estudiante
 inner join Curso c on c.id_curso=IC.id_curso
 inner join Persona p on p.rut_persona=e.rut_persona
 inner join Asignar_curso AC on AC.id_curso=c.id_curso
 inner join Usuario u on u.id_usuario=AC.id_usuario 
 INNER join Grado_discapacidad as GD on GD.id_grado_discapacidad=C.id_grado_discapacidad
 inner join Actividad as A on N.id_actividad=A.id_actividad
 inner join Tipo_actividad as TA on TA.id_tipo_actividad=A.id_tipo_actividad
 inner join Unidad as Uni on A.id_unidad=Uni.id_unidad
 inner join Materia as M on M.id_materia=Uni.id_materia
 where P.activo = 1 and u.rut_persona= @rut_persona">
                <SelectParameters>
                    <asp:ControlParameter ControlID="DropDownList2" Name="id_materia" PropertyName="SelectedValue" Type="Int32" />
                    <asp:ControlParameter ControlID="labelRut" Name="rut_persona" PropertyName="Text" />
                </SelectParameters>

            </asp:SqlDataSource>
        </div>


    </div>

    <br />
    <br />


    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Ayuda en los Niveles de Desempeño</h5>
                    <button id="btnCerrarSesion" type="button" class="close" data-dismiss="modal" aria-label="Close" runat="server">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    Los niveles permiten medir el desempeño del alumno en las actividades durante el semestre en un rango de 0 a 10. 
                    <br />
                    <br />
                    Éstos tienen como objetivo entregar resultados concretos, para así tomar decisiones importantes. 
                    La evaluación constante nos proporciona información vital sobre la evolución del alumno.
                    <br />
                    <br />
                    <div align="center">
                        <img src="Images/aulearn/emotions.png" height="120" />
                    </div>
                    <br />
                    <br />
                    <strong>Nivel de Interés:</strong>   Permite medir qué tanta devoción demostró al realizar la actividad.
                    <br />
                    <br />
                    <strong>Nivel Emocional:</strong>   Permite medir cuán feliz se sintió el alumno al realizar la actividad. 
                    <br />
                    <br />
                    <strong>Nivel Colaboración:</strong>   Permite medir cómo el alumno participó y/o se desenvolvió con sus demas compañeros.
                    <br />
                    <br />
                    <strong>Nivel Autocontrol:</strong>   Permite medir el comportamiento del alumno. Aquí entra la conducta y el comportamiento en la realización de la actividad.
                    <br />
                    <br />



                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" data-dismiss="modal">Ok</button>

                </div>
            </div>
        </div>
    </div>

</asp:Content>


