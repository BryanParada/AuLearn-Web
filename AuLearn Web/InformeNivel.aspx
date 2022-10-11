<%@ Page Title="" Language="C#" MasterPageFile="~/SitioM.Master" AutoEventWireup="true" CodeBehind="InformeNivel.aspx.cs" Inherits="AuLearn_Web.Reportes.InformeNivel" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BreadCrumb" runat="server">
    <i class="fa fa-bar-chart"></i> Informes de Desempeño
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
    <div class="row">
        <div class="col-lg-10">

            <div class="card mb-3">
                <div class="card-header">
                    <i class="fa fa-bar-chart"></i> Informes de Desempeño
                </div>
                <div class="card-body">
                    <asp:Label runat="server" ID="labelRut" Visible="false"></asp:Label>

                    <div class="row">
                        <div class="col-lg-5">

                             <asp:Label runat="server">Seleccione Alumno</asp:Label>
                            <br />
                            <asp:DropDownList ID="DropDownListNombre" OnSelectedIndexChanged="DropDownListNombre_SelectedIndexChanged"  AutoPostBack="true" runat="server" DataSourceID="SqlDataSource1" DataTextField="Column1" DataValueField="rut_persona" class="form-control">
                            </asp:DropDownList>

                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:aulearnConnectionString %>" SelectCommand="select e.rut_persona, p.nombre+' '+p.apellido 
from Estudiante e 
inner join Persona p on e.rut_persona = p.rut_persona 
inner join Integrantes_curso IC on IC.id_estudiante=e.id_estudiante
inner join Curso C on C.id_curso=IC.id_curso
inner join Asignar_curso AC on AC.id_curso=C.id_curso
inner join Usuario U on U.id_usuario=AC.id_usuario
where P.activo = 1 and U.rut_persona = @rut_persona"> 
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="labelRut" Name="rut_persona" PropertyName="Text" />
                                     
                                </SelectParameters></asp:SqlDataSource>
                            <br />
                            <br />

                            </div>

                        <div class="col-lg-5">
                            <asp:Label runat="server">Seleccione Nivel</asp:Label>

                            <br />
                             <asp:DropDownList ID="DropDownListTipoNivel" OnSelectedIndexChanged="DropDownListTipoNivel_SelectedIndexChanged"  AutoPostBack="true" runat="server" class="form-control" DataSourceID="SqlDataSourceTipoNivel" DataTextField="descripcion_nivel" DataValueField="id_tipo_nivel"> </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSourceTipoNivel" runat="server" ConnectionString="<%$ ConnectionStrings:aulearnConnectionString %>" SelectCommand="SELECT * FROM [Tipo_nivel]"></asp:SqlDataSource>
                            </div>
                        </div>
                       
                   

                    <div class="row">
                        <div class="col-lg-7">
                          
                            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="576px" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="1056px">
                                <LocalReport ReportPath="Reportes\ReportNivel.rdlc">
                                    <DataSources>
                                        <rsweb:ReportDataSource DataSourceId="ObjectDataSourceNivelesFecha" Name="DataSetNiveles_fecha" />
                                        <rsweb:ReportDataSource DataSourceId="ObjectDataSourceVerNivel" Name="DataSetVeredNiveles" />
                                        <rsweb:ReportDataSource DataSourceId="ObjectDataSourceDatosAlumno" Name="DataSetDatosAlumno" />
                                        <rsweb:ReportDataSource DataSourceId="ObjectDataSourceDatosColegio" Name="DataSetDatosColegio" />
                                    </DataSources>
                                </LocalReport>
                            </rsweb:ReportViewer>

                            <asp:ObjectDataSource ID="ObjectDataSourceDatosColegio" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataDatosColegioTEMP" TypeName="AuLearn_Web.DataSetReportesTableAdapters.select_Datos_ColegioTableAdapter"></asp:ObjectDataSource>

                            <asp:ObjectDataSource ID="ObjectDataSourceDatosAlumno" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataDatosAlumno" TypeName="AuLearn_Web.Reportes.DataSetAlumnoTableAdapters.select_DatosAlumnoTableAdapter">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="DropDownListNombre" Name="rut_persona" PropertyName="SelectedValue" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>

                            <asp:ObjectDataSource ID="ObjectDataSourceVerNivel" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataVEREDNivel" TypeName="AuLearn_Web.Reportes.DataSetNivelesTableAdapters.Ver_nivelesTableAdapter">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="DropDownListNombre" Name="rut_persona" PropertyName="SelectedValue" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>

                            <asp:ObjectDataSource ID="ObjectDataSourceNivelesFecha" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataNIVELES_FECHAS" TypeName="AuLearn_Web.Reportes.DataSetNivelesTableAdapters.select_niveles_fechaTableAdapter">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="DropDownListNombre" Name="rut_persona" PropertyName="SelectedValue" Type="String" />
                                    <asp:ControlParameter ControlID="DropDownListTipoNivel" Name="id_tipo_nivel" PropertyName="SelectedValue" Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>

                        </div>

                    </div>
                    <div class="row">
                    </div>

                </div>

            </div>
        </div>
    </div>


</asp:Content>
