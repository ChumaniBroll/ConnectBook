using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UpdatePortal.Layers;
using UpdatePortal.HelperClass;
using System.Data;
using BrollConnect.Layers;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace BrollConnect
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        Datalayer dl = new Datalayer();
        Entity en = new Entity();
        //SecureLayer sl = new SecureLayer();
        Business bl = new Business();
        
        
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies["LoginDetails"];

            //add Client Param to URL
            //Convert.ToString(HttpContext.Current.Request.Url.AbsoluteUri)
         

            if (cookie != null)
            {
                Session["username"] = cookie["username"];
                Session["UserEmailID"] = cookie["emailID"];
            }
            //if (Response.Cookies["LoginDetails"]["username"] != null)
            //{
            //    Session["username"] = Response.Cookies["LoginDetails"]["username"];
            //    Session["UserEmailID"] = Response.Cookies["LoginDetails"]["Email"];//Convert.ToString(en.loginData.Tables[0].Rows[0]["EMail"]);
            //    //string PassDecrypt=sl.Decrypt()
            //    Response.Redirect("~/HomePage.aspx");
            //}
            //else
            //{
            //    Response.Redirect("~/Login.aspx");
            //}


            
                if (Session["UserEmailID"] != null)
                {
                    string ClientCodes = "";
                en.ClientInfo = dl.runQuery("Client_Info_SSP '" + "GetClinetInfo" + "','" + Session["UserEmailID"] + "'");

                string ClientCode_Value = bl.GetQueryStringParam(Convert.ToString(HttpContext.Current.Request.Url.AbsoluteUri), "ClientID");
                if (ClientCode_Value == null)
                {
                    ClientCode_Value = bl.GetQueryStringParam(Convert.ToString(HttpContext.Current.Request.Url.AbsoluteUri), "Client");
                }
                
                en.ds = dl.runQuery("Users_GetInfo_SSP '" + "GetUserProfile" + "','" + Session["UserEmailID"] + "','" + ClientCode_Value + "'");
                if(en.ds.Tables[0].Rows.Count>0)
                {
                    en.UserRoles = en.ds.Tables[0].AsEnumerable().Select(r => r.Field<string>("Role")).ToList();

                    for (int i = 0; i <= en.UserRoles.Count - 1; i++)
                    {
                        en.UserRole = en.UserRole + en.UserRoles[i] + " ,";
                    }
                    en.UserRole = en.UserRole.Remove(en.UserRole.Length - 1);
                    if (en.UserRole.Length == 0)
                    {
                        Session["userRoles"] = "No Roles Assigned";
                    }
                    else
                    {
                        Session["userRoles"] = en.UserRole;
                    }
                }
               





                foreach (DataRow dr in en.ClientInfo.Tables[0].Rows)
                    {
                        ClientCodes = ClientCodes + dr["ClientCode"].ToString() + "@" + dr["indx"].ToString() + "|";
                    }
               
                   Page.ClientScript.RegisterStartupScript(this.GetType(), "LoadClients", "LoadClients('" + ClientCodes +"','"+ en.UserRole + "')", true);
               
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "LoadClients", "LoadClients('" + ClientCodes + "');", true);


                // string client = dlclients.Value;
            }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }


            
        }

     
    }
}