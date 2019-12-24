using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Http;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Text;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;

namespace rpgworldbuilder
{
    public partial class EditWorld : Page
    {
        //User ID
        string m_UserID;

        //Map ID
        string m_MapID;

        //Monsters API List
        static Rootobject monsters;

 
        
        /* Page_Load
         * On page load, checks if user's identity is authenticated
         * Sets Map ID based on the ID sent in the http query
         * Calls the page to be populated from that ID;
         */
        protected void Page_Load(object sender, EventArgs e)
        {
            ClearFeedback();

            if (this.IsPostBack)
            {
                TabName.Value = Request.Form[TabName.UniqueID];
            }
            if (User.Identity.IsAuthenticated)
            {
                m_UserID = User.Identity.GetUserId();
            }
            else
            {
                Response.Redirect("Account/Login.aspx?ReturnUrl=" + Request.Url.PathAndQuery, false);

                m_UserID = User.Identity.GetUserId();
            }

            try
            {
                m_MapID = Request.QueryString["MapID"];
            }
            catch (Exception ex)
            {
                lbl_MapName.Text = ex.ToString();
            }


            if (IsPostBack)
            {
                return;
            }

            LoadMapFromSQL();

            HttpClient client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate });
        
            //Sets the clients base address to the API
            client.BaseAddress = new Uri("http://www.dnd5eapi.co/");
            
            //Calls the api with APIKey and query information
            HttpResponseMessage response = client.GetAsync("api/monsters/").Result;
            string result = response.Content.ReadAsStringAsync().Result;
            monsters = JsonConvert.DeserializeObject<Rootobject>(result);

        }



        /* LoadMapFromSQL
         * Loads map from SQL onto page, displaying all the information (Map image, names, etc.)
         */
        protected void LoadMapFromSQL()
        {
            ClearFeedback();

            try
            {
                SqlConnection sql_Connection = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                StringBuilder sb = new StringBuilder();

                sb.Append("SELECT * ");
                sb.Append("FROM Map ");
                sb.Append("WHERE MapID = '" + m_MapID + "'");

                string sql = sb.ToString();
                sql_Connection.Open();
                SqlCommand cmd = new SqlCommand(sql, sql_Connection);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                lbl_MapName.Text = reader.GetString(1);
                lbl_MapDesc.Text = reader.GetString(4);
                lbl_MapAuthor.Text = reader.GetString(6);
                if (!reader.IsDBNull(5))
                    PopulatePOIList(reader.GetString(5));

                img_Map.ImageUrl = "data:Image/png;base64," + reader.GetString(0);

                Session["ImgString"] = reader.GetString(0);
                Session["MapUserID"] = reader.GetString(3);
            }
            catch (Exception)
            {
                lbl_Feedback.ForeColor = System.Drawing.Color.IndianRed;
                lbl_Feedback.Text = "Failed to load Map ";
            }
        }





        /* PopulatePOIList
         * Populates POI with user's entries
         */
        protected void PopulatePOIList(string list)
        {
            ClearFeedback();
            if (list == string.Empty || list == "" || list == null)
                return;

            //Parse the poi list into useable fields for the checkbox list
            string[] lines = list.Split(new[] { "\n" }, StringSplitOptions.None);
            foreach (string entry in lines)
            {
                if (entry == string.Empty || entry == "" || entry == null)
                    continue;
                CheckBoxList1.Items.Add(entry);
            }
        }


        /* ClearFeedback
         * Clears all the labels
         */
        protected void ClearFeedback()
        {
            lbl_Feedback.ForeColor = System.Drawing.Color.IndianRed;
            lbl_Feedback.Text = "";
        }



        /* btn_DeletePoi_Click
         * Deletes selected POI from CheckBoxList and calls method to remove it from SQL Database
         */
        protected void btn_DeletePoi_Click(object sender, EventArgs e)
        {
            ClearFeedback();
            try
            {
                for (int i = 0; i < CheckBoxList1.Items.Count; i++)
                {
                    if (CheckBoxList1.Items[i].Selected == true)
                        CheckBoxList1.Items.RemoveAt(i);
                }
            }
            catch (Exception)
            {
                lbl_Feedback.ForeColor = System.Drawing.Color.IndianRed;
                lbl_Feedback.Text = "Unable to delete POI";
            }
        }




        /* btn_AddPoi_Click
         * Creates a string with coordinates attached to it indicating the position of the POI on the map
         */
        protected void btn_AddPoi_Click(object sender, EventArgs e)
        {
            ClearFeedback();
            try
            {
                //Returns if either coords or desc is empty.
                if (lbl_PointCoords.Text == null || lbl_PointCoords.Text == "")
                {
                    return;
                }
                if (txt_PointDesc.Text == null || txt_PointDesc.Text == "")
                {
                    return;
                }

                string n_POI = lbl_PointCoords.Text + " " + txt_PointDesc.Text;

                CheckBoxList1.Items.Add(n_POI);
                ListItem listItem = CheckBoxList1.Items.FindByText(n_POI);
                if (listItem != null) listItem.Selected = false;

                lbl_PointCoords.Text = null;
                txt_PointDesc.Text = null;
            }
            catch (Exception)
            {
                lbl_Feedback.ForeColor = System.Drawing.Color.IndianRed;
                lbl_Feedback.Text = "Unable to add POI";
            }
        }



        /* img_Map_Click
         * Upon clicking, changes the text in lbl_PointCoords to what the image coordinate clicked 
         */
        protected void img_Map_Click(object sender, ImageClickEventArgs e)
        {
            ClearFeedback();
            var input =
            lbl_PointCoords.Text = e.X + ", " + e.Y;
        }

        //This saves the map and its POI list to the sql database
        protected void btn_SavePoi_Click(object sender, EventArgs e)
        {
            ClearFeedback();

            string userID = "";
            if (User.Identity.IsAuthenticated)
            {
                userID = User.Identity.GetUserId();
            }
            if (Session["MapUserID"].ToString() != userID)
            {
                lbl_Feedback.ForeColor = System.Drawing.Color.IndianRed;
                lbl_Feedback.Text = "Not Able to Save - Not Your Map!";
                return;
            }

            string all_POIs = "";
            foreach (ListItem entry in CheckBoxList1.Items)
            {
                all_POIs += entry.Text + "\n";
            }

            try
            {

                SqlConnection sql_Connection = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                StringBuilder sb = new StringBuilder();

                string imgString = Session["imgString"].ToString();
                string MapName = lbl_MapName.Text;
                string MapDesc = lbl_MapDesc.Text;
                string m_UserName = lbl_MapAuthor.Text;

                sb.Append("UPDATE Map ");
                sb.Append("SET MapImage = '" + imgString + "', MapName = '" + MapName + "', UserID = '" + m_UserID + "', MapDescription = '" + MapDesc + "', PointsOfInterest = '" + all_POIs + "', UserName = '" + m_UserName + "' ");
                sb.Append("WHERE MapID = " + m_MapID + ";");
                string sql = sb.ToString();
                sql_Connection.Open();
                SqlCommand cmd = new SqlCommand(sql, sql_Connection);
                cmd.ExecuteNonQuery();
                sql_Connection.Close();

            }
            catch (Exception ex)
            {
                lbl_Feedback.ForeColor = System.Drawing.Color.IndianRed;
                lbl_Feedback.Text = "Failed to Save Map. Ensure MapName is valid";
            }
        }
        //Searches the static monster api call for matching labels and updates the listbox with the entries.
        protected void textbox_API_Textchanged(object sender, EventArgs e)
        {
            List<string> items = new List<string>();
            foreach (Result monster in monsters.results)
            {
                if (monster.name.ToLower().Contains(API_TextBox1.Text.ToLower()) == true)
                {
                    items.Add(monster.name);
                }
            }
            ListBox1.DataSource = items;
            ListBox1.DataBind();
        }
        //Uses the dnd 5e API to display information about a specific monster in a modal popup
        protected void listbox_index_changed(object sender, EventArgs e)
        {
            if (ListBox1.Items.Count == 0)
            {
                return;
            }
            string item = ListBox1.SelectedValue;
            Rootobject2 ret = null;
            foreach (Result monster in monsters.results)
            {
                if (monster.name.ToLower().Equals(item.ToLower()) == true)
                {

                    HttpClient client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate });
        
                    //Sets the clients base address to the API
                    //client.BaseAddress = new Uri(monster.url);
                    //Calls the api with APIKey and query information
                    HttpResponseMessage response = client.GetAsync(monster.url).Result;
                    string result = response.Content.ReadAsStringAsync().Result;
                    ret = JsonConvert.DeserializeObject<Rootobject2>(result);
                    break;
                }
            }
            if(ret == null)
            {
                return;
            }
            else
            {
                lbl_CreatureName.Text = ret.name;
                lbl_CreatureType.Text = ret.size + ", " + ret.alignment;
                lbl_ArmorClass.Text = "Armor Class "+ret.armor_class.ToString();
                lbl_HP.Text = "Hit Points "+ret.hit_points.ToString();
                lbl_Speed.Text = "Speed "+ret.speed;
                lbl_Senses.Text = "Senses " + ret.senses;
                lbl_Languages.Text = "Languages " + ret.languages;
                lbl_CR.Text = "Challenge " + ret.challenge_rating;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

        }
    }


    //DnD 5e API /api/monsters model template
    public class Rootobject
    {
        public int count { get; set; }
        public Result[] results { get; set; }
    }
    //One of the two objects contained in the /api/monsters return
    public class Result
    {
        public string name { get; set; }
        public string url { get; set; }
    }
    //Dnd 5e API specific monster model template
    public class Rootobject2
    {
        public string _id { get; set; }
        public int index { get; set; }
        public string name { get; set; }
        public string size { get; set; }
        public string type { get; set; }
        public string subtype { get; set; }
        public string alignment { get; set; }
        public int armor_class { get; set; }
        public int hit_points { get; set; }
        public string hit_dice { get; set; }
        public string speed { get; set; }
        public int strength { get; set; }
        public int dexterity { get; set; }
        public int constitution { get; set; }
        public int intelligence { get; set; }
        public int wisdom { get; set; }
        public int charisma { get; set; }
        public int dexterity_save { get; set; }
        public int constitution_save { get; set; }
        public int wisdom_save { get; set; }
        public int charisma_save { get; set; }
        public int insight { get; set; }
        public int perception { get; set; }
        public int persuasion { get; set; }
        public int stealth { get; set; }
        public string damage_vulnerabilities { get; set; }
        public string damage_resistances { get; set; }
        public string damage_immunities { get; set; }
        public string condition_immunities { get; set; }
        public string senses { get; set; }
        public string languages { get; set; }
        public float challenge_rating { get; set; }
        public Special_Abilities[] special_abilities { get; set; }
        public Action[] actions { get; set; }
        public Legendary_Actions[] legendary_actions { get; set; }
        public string url { get; set; }
    }

    public class Special_Abilities
    {
        public int attack_bonus { get; set; }
        public string desc { get; set; }
        public string name { get; set; }
    }

    public class Action
    {
        public int attack_bonus { get; set; }
        public string desc { get; set; }
        public string name { get; set; }
        public int damage_bonus { get; set; }
        public string damage_dice { get; set; }
    }

    public class Legendary_Actions
    {
        public int attack_bonus { get; set; }
        public string desc { get; set; }
        public string name { get; set; }
    }

}