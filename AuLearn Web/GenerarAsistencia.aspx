<%@ Page Title="" Language="C#" MasterPageFile="~/SitioM.Master" AutoEventWireup="true" CodeBehind="GenerarAsistencia.aspx.cs" Inherits="AuLearn_Web.GenerarAsistencia" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BreadCrumb" runat="server">
    Generar Asistencia
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
    <div class="row">
        <div class="col-lg-7">

            <div class="card mb-3">
                <div class="card-header">
                    <i class="fa fa-university"></i>Generación Asistencia
                </div>
                <div class="card-body">


                    <div class="row">
                        <div class="col-lg-3">

                            <asp:Label runat="server">Seleccione Curso</asp:Label>
                            <br />
                            <asp:DropDownList class="form-control" ID="dropDownCurso" runat="server" OnSelectedIndexChanged="dropDownCurso_SelectedIndexChanged" AutoPostBack="true" DataSourceID="SqlDataSourceCurso" DataTextField="nombre_curso" DataValueField="id_curso"></asp:DropDownList>

                            <asp:SqlDataSource ID="SqlDataSourceCurso" runat="server" ConnectionString="<%$ ConnectionStrings:aulearnConnectionString %>" SelectCommand="SELECT [id_curso], [nombre_curso] FROM [Curso]"></asp:SqlDataSource>


                            <br />


                        </div>
                        <br />


                    </div>

                    <div class="row">
                        <div class="col-lg-7">
                            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="837px">
                                <LocalReport ReportPath="ReportAsistencia.rdlc">
                                    <DataSources>
                                        <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="TablaAsistencia" />
                                        <rsweb:ReportDataSource DataSourceId="ObjectDataSourceDatosCol" Name="DataSetColegio" />
                                        <rsweb:ReportDataSource DataSourceId="ObjectDataSourceCALCP" Name="DataSetCALCP" />
                                    </DataSources>
                                </LocalReport>
                            </rsweb:ReportViewer>
                            <asp:ObjectDataSource ID="ObjectDataSourceCALCP" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataCALPC" TypeName="AuLearn_Web.DataSetAsistenciaTableAdapters.contar_al_curso_profeTableAdapter">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="dropDownCurso" Name="id_curso" PropertyName="SelectedValue" Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            <asp:ObjectDataSource ID="ObjectDataSourceDatosCol" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataDatosColegioTEMP" TypeName="AuLearn_Web.DataSetReportesTableAdapters.select_Datos_ColegioTableAdapter"></asp:ObjectDataSource>
                            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="AuLearn_Web.DataSetAsistenciaTableAdapters.generar_asistenciaTableAdapter" OldValuesParameterFormatString="original_{0}">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="dropDownCurso" Name="id_curso" PropertyName="SelectedValue" Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            <br />
                           

                        </div>

                    </div>
                    <div class="row">
                        <div class="col-lg-3">
                            
                        </div>
                    </div>

                </div>

            </div>
        </div>
    </div>

</asp:Content>
