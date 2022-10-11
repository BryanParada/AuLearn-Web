<%@ Page Title="" Language="C#" MasterPageFile="~/SitioM.Master" AutoEventWireup="true" CodeBehind="informeDiscapacidad.aspx.cs" Inherits="AuLearn_Web.informeDiscapacidad" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BreadCrumb" runat="server">
    Informes Discapacidad
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">

    <div class="row">
        <div class="col-lg-7">

            <div class="card mb-3">
                <div class="card-header">
                    <i class="fa fa-bar-chart"></i> Informes de Discapacidad
                </div>
                <div class="card-body">


                    <div class="row">
                        <div class="col-lg-5">

                              <label>Seleccione un Curso para generar el Reporte.</label>
                            <br />
                    <asp:DropDownList runat="server" ID="DropDownCurso" OnSelectedIndexChanged="DropDownCurso_SelectedIndexChanged" DataSourceID="SqlDataSourceDPC" DataTextField="nombre_curso" DataValueField="id_curso" AutoPostBack="true" class="form-control"></asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSourceDPC" runat="server" ConnectionString="<%$ ConnectionStrings:aulearnConnectionString %>" SelectCommand="SELECT [id_curso], [nombre_curso] FROM [Curso]"></asp:SqlDataSource>
                    <br />
                            <br />

                            </div>

                         
                        </div>
                        
                   

                    <div class="row">
                        <div class="col-lg-7">
                             
                       
                            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="454px" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="821px">
                                <LocalReport ReportPath="Reportes\ReportDiscapacidades.rdlc">
                                    <DataSources>
                                        <rsweb:ReportDataSource DataSourceId="ObjectDataSourceDiscapacidades" Name="DataSetDiscapacidades" />
                                        <rsweb:ReportDataSource DataSourceId="ObjectDataSourcePorcenDisca" Name="DataSetPorcenDisca" />
                                        <rsweb:ReportDataSource DataSourceId="ObjectDataSourceDatosColegio" Name="DataSetDatosColegio" />
                                    </DataSources>
                                </LocalReport>
                            </rsweb:ReportViewer>
                            <asp:ObjectDataSource ID="ObjectDataSourceDatosColegio" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataDatosColegioTEMP" TypeName="AuLearn_Web.DataSetReportesTableAdapters.select_Datos_ColegioTableAdapter"></asp:ObjectDataSource>
                            <asp:ObjectDataSource ID="ObjectDataSourcePorcenDisca" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataPorcentajeDiscapacidades" TypeName="AuLearn_Web.DataSetReportesTableAdapters.select_Porcentaje_discapacidadesTableAdapter">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="DropDownCurso" Name="id_curso" PropertyName="SelectedValue" Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            <asp:ObjectDataSource ID="ObjectDataSourceDiscapacidades" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataDatosDiscapacidad" TypeName="AuLearn_Web.DataSetReportesTableAdapters.selectDatosDiscapacidadTableAdapter">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="DropDownCurso" Name="id_curso" PropertyName="SelectedValue" Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </div>

                    </div>
                    

                </div>

            </div>
        </div>
    </div>


</asp:Content>
