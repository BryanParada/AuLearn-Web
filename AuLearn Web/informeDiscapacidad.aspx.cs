using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AuLearn_Web
{
    public partial class informeDiscapacidad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

                //http://escuelajuansandoval.webescuela.cl/sites/default/files/Untitled_8.png

                ReportViewer1.LocalReport.ReportPath = Server.MapPath("Reportes/ReportDiscapacidades.rdlc");
                ReportViewer1.LocalReport.EnableExternalImages = true;
                //string imagePath = new Uri(Server.MapPath("Images/aulearn/pic1.jpg")).AbsoluteUri;
                string imagePath = new Uri(Server.MapPath("~/traerImagen.ashx")).AbsoluteUri;
                //version FTP NO FUNCIONA
                //string ur = @"ftp://192.168.72.1/Colegio%20-%20Juan%20Sandoval/Logo/logo.png";
                ////version online
                //string ur = @"http://escuelajuansandoval.webescuela.cl/sites/default/files/Untitled_8.png";
                ////version local
                string ur = new Uri(Server.MapPath("~/Images/aulearn/logo.png")).AbsoluteUri;
                ReportParameter parameter = new ReportParameter("ImagePath", ur);
                ReportViewer1.LocalReport.SetParameters(parameter);
                ReportViewer1.LocalReport.Refresh();

                this.ReportViewer1.LocalReport.DisplayName = "Informe de Discapacidad";

                
            }
        }

        protected void DropDownCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReportViewer1.LocalReport.Refresh();
        }
    }
}