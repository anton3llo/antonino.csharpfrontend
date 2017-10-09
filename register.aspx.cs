using HendoHealth.Library;
using HendoHealth.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HendoHealth
{
    public partial class register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void regButton_Click(object sender,EventArgs e)
        {
            using (var control = new medical_valuesEntities())
            {
                users utente = new users();
                utente.username = Request.Form[nameof(InputEmail)];
                utente.password = Hash.ComputeHash(Request.Form[nameof(InputPassword)],
                    "SHA256",
                    new UTF8Encoding().GetBytes(Request.Form[nameof(InputEmail)]));
                control.users.Add(utente);
                try
                {
                    control.SaveChanges();
                    HttpContext.Current.Response.Redirect(Properties.Resources.urlindex);
                }
                catch (DbEntityValidationException exception)
                {
                    System.Diagnostics.Debug.WriteLine(exception.EntityValidationErrors.First().ValidationErrors.First().ErrorMessage.ToString());
                }
            }
        }
        
    }
}