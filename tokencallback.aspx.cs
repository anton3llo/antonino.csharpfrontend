using System;

namespace HendoHealth
{
    public partial class tokencallback : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*HttpContext
                .Current
                .Response
                .Redirect("http://localhost:1025/iHendoTotem/login.aspx?ours_code=" + 
                HttpContext.Current.Request.QueryString["code"]
                + "");*/
        }
    }
}