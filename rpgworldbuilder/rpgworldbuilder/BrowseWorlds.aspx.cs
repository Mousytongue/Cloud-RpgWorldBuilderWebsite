using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using rpgworldbuilder.Models;
using System.Data.SqlClient;
using System.Text;

namespace rpgworldbuilder
{
    public partial class BrowseWorlds : Page
    {
        /* Page_Load
         * Do nothing on page load
         */
        protected void Page_Load(object sender, EventArgs e)
        {
        }






        /* btn_QueryButton_Click
         * Queries the SQL database for the maps
         */
        protected void btn_QueryButton_Click(object sender, EventArgs e)
        {
            try
            {
                string searchPhrase;


                //search by map name and author name
                if ((txt_AuthorNameQuery.Text != string.Empty && txt_AuthorNameQuery.Text != "" && txt_AuthorNameQuery.Text != null) &&
                    (txt_MapNameQuery.Text != string.Empty && txt_MapNameQuery.Text != "" && txt_MapNameQuery.Text != null))
                {
                    searchPhrase = "SELECT * FROM Map WHERE MapName = '" + txt_MapNameQuery.Text + "' AND UserName = '" + txt_AuthorNameQuery.Text + "';";
                    sql_SqlDataSource1.SelectCommand = searchPhrase;
                }


                //search by author name only
                else if ((txt_AuthorNameQuery.Text != string.Empty && txt_AuthorNameQuery.Text != "" && txt_AuthorNameQuery.Text != null) &&
                    (txt_MapNameQuery.Text == string.Empty || txt_MapNameQuery.Text == "" || txt_MapNameQuery.Text == null))
                {
                    searchPhrase = "SELECT * FROM Map WHERE UserName = '" + txt_AuthorNameQuery.Text + "';";
                    sql_SqlDataSource1.SelectCommand = searchPhrase;
                }


                //search by map name only
                else if ((txt_AuthorNameQuery.Text == string.Empty || txt_AuthorNameQuery.Text == "" || txt_AuthorNameQuery.Text == null) &&
                    (txt_MapNameQuery.Text != string.Empty && txt_MapNameQuery.Text != "" && txt_MapNameQuery.Text != null))
                {
                    searchPhrase = "SELECT * FROM Map WHERE MapName = '" + txt_MapNameQuery.Text + "';";
                    sql_SqlDataSource1.SelectCommand = searchPhrase;
                }


                //no inputs entered - show all maps
                else
                {
                    searchPhrase = "SELECT * FROM Map;";
                    sql_SqlDataSource1.SelectCommand = searchPhrase;
                }
            }
            catch (Exception ex)
            {
                lbl_errorLabel.Text = ex.ToString();
            }
        }


        /* btn_Refresh_Click
         * Refreshes the page so the page shows all the maps again
         */
        protected void btn_RefreshButton_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("BrowseWorlds.aspx", true);
            }
            catch (Exception)
            { }
        }

    }
}