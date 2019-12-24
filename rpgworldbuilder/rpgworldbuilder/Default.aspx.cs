using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace rpgworldbuilder
{
    public partial class _Default : Page
    {
        /* Page_Load
         * On page load do nothing
         */
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        /* buildWorldButton_Click
         * A method which changes pages to the Build World Page
         */
        protected void buildWorldButton_Click(object sender, System.EventArgs e)
        {
            Response.Redirect("BuildWorld.aspx");
            return;
        }



        /* BrowseWorldsButton
         * A method which changes pages to the Browse Worlds Page
         */
        protected void browseWorldsButton_Click(object sender, System.EventArgs e)
        {
            Response.Redirect("BrowseWorlds.aspx");
            return;
        }

        
        
        
        /* ViewAPIButton
         * A method which changes pages to the View API Page
         */
        protected void viewAPIButton_Click(object sender, System.EventArgs e)
        {
            Response.Redirect("ViewAPI.aspx");
            return;
        }
    }
}