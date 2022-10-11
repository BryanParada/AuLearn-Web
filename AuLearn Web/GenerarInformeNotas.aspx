<%@ Page Title="" Language="C#" MasterPageFile="~/SitioM.Master" AutoEventWireup="true" CodeBehind="GenerarInformeNotas.aspx.cs" Inherits="AuLearn_Web.GenerarInformeNotas" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BreadCrumb" runat="server">

    Generación Informe de Notas
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">

   <div class="row">
        <div class="col-lg-7">

            <div class="card mb-3">
                <div class="card-header">
                    <i class="fa fa-bar-chart"></i> Generación Informe de Notas
                </div>
                <div class="card-body">


                    <div class="row">
                        <div class="col-lg-5">

                             <asp:Label runat="server">Seleccione Alumno</asp:Label>
                            <br />
                           <asp:DropDownList ID="DropDownListNombre" AutoPostBack="true" OnSelectedIndexChanged="DropDownListNombre_SelectedIndexChanged" runat="server" DataSourceID="SqlDataSource1" DataTextField="Column1" DataValueField="rut_persona" class="form-control">
                            </asp:DropDownList>

                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:aulearnConnectionString %>" SelectCommand="select 
                e.rut_persona, p.nombre+' '+p.apellido from Estudiante e inner join Persona p on e.rut_persona = p.rut_persona where P.activo = 1"></asp:SqlDataSource>
                            <br />
                            <br />

                            </div>

                         
                        </div>
                        <br /> 
                   

                    <div class="row">
                        <div class="col-lg-7">
                          
                            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="509px" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="853px">
                                <LocalReport ReportPath="ReportInformeNota.rdlc">
                                    <DataSources>
                                        <rsweb:ReportDataSource DataSourceId="ObjectDataSourceResNotas" Name="DataSetResumenNotas" />
                                        <rsweb:ReportDataSource DataSourceId="ObjectDataSourceProm" Name="DataSetPromedios" />
                                        <rsweb:ReportDataSource DataSourceId="ObjectDataSourcedatAl" Name="DataSetDatosAl" />
                                        <rsweb:ReportDataSource DataSourceId="ObjectDataSourceDatcol" Name="DataSetDatosColegio" />
                                    </DataSources>
                                </LocalReport>
                            </rsweb:ReportViewer> 
                            <asp:ObjectDataSource ID="ObjectDataSourceDatcol" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataDatosColegioTEMP" TypeName="AuLearn_Web.DataSetReportesTableAdapters.select_Datos_ColegioTableAdapter"></asp:ObjectDataSource>
                            <asp:ObjectDataSource ID="ObjectDataSourcedatAl" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataDatosAlumno" TypeName="AuLearn_Web.Reportes.DataSetAlumnoTableAdapters.select_DatosAlumnoTableAdapter">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="DropDownListNombre" Name="rut_persona" PropertyName="SelectedValue" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            <asp:ObjectDataSource ID="ObjectDataSourceProm" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataPromedio" TypeName="AuLearn_Web.DataSetReportesTableAdapters.select_PromediosTableAdapter">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="DropDownListNombre" Name="rut_persona" PropertyName="SelectedValue" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            <asp:ObjectDataSource ID="ObjectDataSourceResNotas" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataResumenNotas" TypeName="AuLearn_Web.DataSetReportesTableAdapters.select_ResumenNotasTableAdapter">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="DropDownListNombre" Name="rut_persona" PropertyName="SelectedValue" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>

                        </div>

                    </div>
                    

                </div>

            </div>
        </div>
    </div>

     

           

        

     

     

  
   
</asp:Content>
