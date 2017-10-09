using System;
using System.Web;
using System.Web.UI.WebControls;

namespace HendoHealth
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {

            }
            
        }

        protected void getMeasures_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "progressBar", "fillinbox();", true);
        }

        protected void OnMenuItemDataBound(object sender, MenuEventArgs e)
        {
            if (SiteMap.CurrentNode != null)
            {
                if (e.Item.Text == SiteMap.CurrentNode.Title)
                {
                    if (e.Item.Parent != null)
                    {
                        e.Item.Parent.Selected = true;
                    }
                    else
                    {
                        e.Item.Selected = true;
                    }
                }
            }
        }

    }
}