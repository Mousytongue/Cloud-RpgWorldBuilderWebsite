using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using System.Configuration;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Data.SqlClient;
using System.Text;

namespace rpgworldbuilder
{
    public partial class BuildWorld : Page  
    {
        //User ID string in case its needed for creating the world.
        string m_UserID;
        string m_UserName;

        //Map ID to be used in the redirect to editpage
        string m_MapID;

        string imgString;



        /* Page_Load
         * On page load, verify user is logged in, otherwise redirect to login page
         */
        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated)
            {
                m_UserID = User.Identity.GetUserId();
                m_UserName = User.Identity.GetUserName();
            }
            else
            {
                Response.Redirect("Account/Login.aspx?ReturnUrl=/BuildWorld.aspx",false);
                
                m_UserID = User.Identity.GetUserId();
                m_UserName = User.Identity.GetUserName();
            }
            MapUpload.Attributes["onchange"] = "UploadFile(this)";
        }


        /* btn_Upload_Click
         * Uploads image to page
         */
        protected void btn_Upload_Click(object sender, EventArgs e)
        {
            string folderPath = Server.MapPath("~/Files/");

            //Check whether directory exist
            if (!Directory.Exists(folderPath))
            {
                //If does not exist, create it
                Directory.CreateDirectory(folderPath);
            }


            //Checks if user has selected a file
            if (MapUpload.HasFile)
            {
                //checks file extension
                string fileExt = System.IO.Path.GetExtension(MapUpload.FileName);


                if (fileExt == ".jpeg" || fileExt == ".jpg" || fileExt == ".png" || fileExt == ".bmp")
                {
                    lbl_MapFolderPath.Text = folderPath;
                    lbl_MapFileName.Text = MapUpload.FileName;

                    if (File.Exists(folderPath + Path.GetFileName(MapUpload.FileName)))
                        File.Delete(folderPath + Path.GetFileName(MapUpload.FileName));
                    MapUpload.SaveAs(folderPath + Path.GetFileName(MapUpload.FileName));
                    img_Map.ImageUrl = "~/Files/" + Path.GetFileName(MapUpload.FileName);

                    HttpPostedFile postedFile = MapUpload.PostedFile;
                    Stream stream = postedFile.InputStream;
                    BinaryReader binaryReader = new BinaryReader(stream);
                    byte[] imgBytes = binaryReader.ReadBytes((int)stream.Length);
                    imgString = Convert.ToBase64String(imgBytes);

                    Session["imgstring"] = imgString;

                    //lbl_Message.Text = imgString;

                    lbl_Message.Visible = true;
                }
                else
                {
                    //invalid file extension
                    lbl_Message.Text = "Only .jpeg, .jpg, and .png files are allowed!";
                    lbl_Message.ForeColor = System.Drawing.Color.IndianRed;
                }
            }
            else
            {
                lbl_MapActionFeedback.Text = "No file selected";
            }
        }




        /* btn_SaveMap_Click
         * Saves map in both Blob Storage and SQL Database
         */
        protected void btn_SaveMap_Click(object sender, EventArgs e)
        {
            if (!checkFields())
            {
                return;
            }


            try
            {
                // Blob Storage Stuff
                if (!uploadMapToBlobStorage())
                { 
                    lbl_Message.Text = "Name already taken";
                    lbl_Message.ForeColor = System.Drawing.Color.IndianRed;
                    return;
                }

                //SQL Database Stuff
                if (!uploadMapToSQLStorage())
                { 
                    lbl_Message.Text += "Failed to save in SQL";
                    lbl_Message.ForeColor = System.Drawing.Color.IndianRed;
                    return;
                }
                //If reached here, both uploads succeeded.

                //after submitting Map into Database and Blob Storage, Map is official and user's can access it
                //User is redirected into the Edit World page where they can view their map, add POI notes, or delete it
                lbl_Message.ForeColor = System.Drawing.Color.Green;
                lbl_Message.Text = "Map successfully uploaded";

                Response.Redirect("EditWorld.aspx?MapID=" + m_MapID, true);

            }
            catch (Exception)
            {
                return;
            }
        }




        /* checkFields
         * Checks to see if User entered Map name field and if they selected a map image
         */
        protected bool checkFields()
        {
            //Check Name
            if (txt_MapName.Text == string.Empty || txt_MapName.Text == "")
            {
                lbl_Message.Text = "Missing Map name";
                lbl_Message.ForeColor = System.Drawing.Color.IndianRed;
                return false;
            }


            //Check Image exist
            if (img_Map.ImageUrl == string.Empty || img_Map.ImageUrl == "")
            {
                lbl_Message.Text = "No Image selected";
                lbl_Message.ForeColor = System.Drawing.Color.IndianRed;
                return false;
            }

            return true;
        }



        /* uploadMapToSQLStorage
         * Uploads map into SQL storage
         */
        protected bool uploadMapToSQLStorage()
        {
            try
            {
                //Connects to the SQL databased defined in the web.config connection string
                SqlConnection sql_Connection = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                StringBuilder sb = new StringBuilder();

                //All variables
                string MapName = txt_MapName.Text;
                string MapDesc = txt_MapDesc.Text;
                imgString = Session["imgstring"].ToString();

                sb.Append("INSERT INTO Map (MapImage, MapName, UserID, MapDescription, UserName) ");
                sb.Append("VALUES ('" + imgString + "','" + MapName + "', '" + m_UserID + "', '" + MapDesc + "', '" + m_UserName + "');");
                sb.Append("SELECT SCOPE_IDENTITY()");

                string sql = sb.ToString();
                sql_Connection.Open();
                SqlCommand cmd = new SqlCommand(sql, sql_Connection);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                m_MapID = reader.GetValue(0).ToString();
                sql_Connection.Close();

                return true;
            } catch (Exception ex)
            {
                lbl_Message.Text = "SQL Failed: " + ex;
                lbl_Message.ForeColor = System.Drawing.Color.IndianRed;
                throw new Exception();
            }
        }



        /* uploadMapToBlobStorage
         * Uploads map to the blob storage. Checks if file with same name already exists within the
         * storage. If a file already exists, doesn't overwrite or upload copy
         */
        protected bool uploadMapToBlobStorage()
        {
            try
            {
                //connects to storage
                ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["StorageConnection"];
                string connection = mySetting.ToString();
                CloudStorageAccount account = CloudStorageAccount.Parse(connection);
                CloudBlobClient serviceClient = account.CreateCloudBlobClient();


                // Create container. Name must be lower case
                var container = serviceClient.GetContainerReference("mycontainer");
                container.CreateIfNotExistsAsync().Wait();


                // write a blob to the container
                CloudBlockBlob blob = container.GetBlockBlobReference(txt_MapName.Text);


                //Checks if name already exists
                if (blob.Exists())
                {
                    return false;
                }


                // Upload the file
                FileStream file = File.OpenRead(lbl_MapFolderPath.Text + lbl_MapFileName.Text);
                blob.UploadFromStream(file);


                return true;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}