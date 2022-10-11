<%@ Page Title="" Language="C#" MasterPageFile="~/SitioM.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="AuLearn_Web.WebForm1" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="BreadCrumb" runat="server">
    Webform de prueba - grafico

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="Server">
    <!DOCTYPE html>



        <div class="row">
            <div class="col-lg-5">

                <asp:Chart ID="Chart1" runat="server" Height="400px" Width="500px" DataSourceID="SqlDataSourceGrafico">
                    <Series>
                        <asp:Series Name="Series1" ChartType="Line" ChartArea="ChartArea1" XValueMember="fecha_evaluacion" YValueMembers="fecha_evaluacion">
                        </asp:Series>
                        <asp:Series Name="Series2" ChartType="Line" ChartArea="ChartArea1">
                        </asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1">
                        </asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>

                <asp:SqlDataSource ID="SqlDataSourceGrafico" runat="server" ConnectionString="<%$ ConnectionStrings:aulearnConnectionString %>" SelectCommand="SELECT [nota], [fecha_evaluacion] FROM [Notas]"></asp:SqlDataSource>

            </div>
        </div>






</asp:Content>






