using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AuLearn_Web
{
    public partial class GenerarInformeNotas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

                //http://escuelajuansandoval.webescuela.cl/sites/default/files/Untitled_8.png

                ReportViewer1.LocalReport.ReportPath = Server.MapPath("ReportInformeNota.rdlc");
                ReportViewer1.LocalReport.EnableExternalImages = true;
                //string imagePath = new Uri(Server.MapPath("Images/aulearn/pic1.jpg")).AbsoluteUri;
                string imagePath = new Uri(Server.MapPath("~/traerImagen.ashx")).AbsoluteUri;
               // string ur = @"http://escuelajuansandoval.webescuela.cl/sites/default/files/Untitled_8.png";
                ////version local
                string ur = new Uri(Server.MapPath("~/Images/aulearn/logo.png")).AbsoluteUri;
                ReportParameter parameter = new ReportParameter("ImagePath", ur);
                ReportViewer1.LocalReport.SetParameters(parameter);
                ReportViewer1.LocalReport.Refresh();

                this.ReportViewer1.LocalReport.DisplayName = "Informe de Notas";

                //ReportViewer1.LocalReport.EnableExternalImages = true;

                //ReportParameter param = new ReportParameter();

                //param.Name = "ImagePath";
                //param.Values.Add("ftp://aulearn:S0p0rt3@172.16.115.244:22/Colegio - Juan Sandoval/Logo/logo.png");
                //ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { param });
                //ReportViewer1.LocalReport.Refresh();
            }
        }

        protected void DropDownListNombre_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReportViewer1.LocalReport.Refresh();
        }

      
    }
}