using HendoHealth.Library;
using HendoHealth.Model;
using System;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace HendoHealth
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void logBtn_Click(object sender, EventArgs e)
        {
            using (var control = new medical_valuesEntities())
            {
                var username = from item in control.users.ToList() where item.username.Equals(Request.Form[nameof(InputEmail)]) select item.password;
                //check if hash by salt is congruent with the one in local database
                if (Hash.VerifyHash(
                    Request.Form[nameof(InputPassword)],
                    "SHA256",
                    new UTF8Encoding().GetBytes(Request.Form[nameof(InputEmail)]),
                    username.First()))
                {
                    Session["username"] = Request.Form[nameof(InputEmail)];
                    //before login on cloud then redirect
                    /*Library.Connect iHealth = new Library.Connect();
                    iHealth.GetCode();
                    if (iHealth.GetAccessToken(HttpContext.Current.Request.QueryString["ours_code"], null, HttpContext.Current))*/
                    HttpContext.Current.Response.Redirect(Properties.Resources.urlmeasures);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "wrongCredentials", "AlertMessage();", true);
                }
            }
        }

        protected void regBtn_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Redirect(Properties.Resources.urlregister);
        }
    }
}