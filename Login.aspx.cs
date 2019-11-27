using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UpdatePortal.Layers;
using UpdatePortal.HelperClass;
using System.Configuration;
using UpdatePortal.Sessions;
using UpdatePortal.ErrorHandling;
using BrollConnect.Layers;

namespace BrollConnect
{
    public partial class Login : System.Web.UI.Page
    {
        Datalayer dl = new Datalayer();
        Entity en = new Entity();
        SecureLayer sl = new SecureLayer();
        App_Sessions sessions = new App_Sessions();
        UpdateExceptions UPDexceptions = new UpdateExceptions();
        Business bl = new Business();

        string BrollconnectPath= ConfigurationManager.AppSettings["AddBrollConnect"].ToString();
        //string RedirectURL = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                //HttpCookie cookie = Request.Cookies["LoginDetails"];
                //Session["username"] = null;
                //Session["UserEmailID"] = null;
                //if (cookie != null)
                //{
                //    cookie.Value = null;
                //    cookie.Expires = DateTime.Now.AddDays(-1);

                //}
                if (Session["username"] != null)
                {
                    Session["username"] = null;
                }
                if (Session["UserEmailID"] != null)
                {
                    Session["UserEmailID"] = null;
                }
                //HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //HttpContext.Current.Response.Cache.SetNoServerCaching();
                //HttpContext.Current.Response.Cache.SetNoStore();
                //Session.Abandon();
            }


        }


        protected void btlogin_Click(object sender, EventArgs e)
        {

            
            try
            {
                
                if (Session["username"] != null)
                {
                    Session["username"] = null;
                }
                if (Session["UserEmailID"] != null)
                {
                    Session["UserEmailID"] = null;
                }

                en.loginContext = "";
                en.username = Request.Form["username"];
                en.password = Request.Form["password"];
                en.password = sl.Encrypt(en.password);
                en.loginData = dl.runQuery("Users_ssp_login '" + en.username + "' , '" + en.password + "' , '" + en.loginContext + "'");
                string ServerURL = HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace(HttpContext.Current.Request.RawUrl, "");

                if (Convert.ToString(en.loginData.Tables[0].Rows[0]["ClientAccess"]) != "")
                {

                    //assgin sessions
                    sessions.UserSession(Request.Form["username"]);
                    sessions.UserEmailSession(Convert.ToString(en.loginData.Tables[0].Rows[0]["EMail"]));
                    sessions.UserIDSession(Convert.ToString(en.loginData.Tables[0].Rows[0]["Indx"]));

              

                    //if (Session["ResquestedURL"] == null) Session["ResquestedURL"] = ServerURL+ "/HomePage.aspx";

                    HttpCookie mycookie = new HttpCookie("LoginDetails");
                   

                    mycookie["username"] = Request.Form["username"];
                    mycookie["emailID"] = Convert.ToString(en.loginData.Tables[0].Rows[0]["EMail"]);
                    mycookie["Context"] = Convert.ToString(en.loginData.Tables[0].Rows[0]["Context"]);
                    en.ClientCodes = Convert.ToString(en.loginData.Tables[0].Rows[0]["FK_Last_Client"]);

                    if(en.ClientCodes.Length>0)
                    {
                        en.ClientCodes = bl.UpdateUserClient_Format_Code(en.ClientCodes);
                    }
                    else
                    {
                        en.ClientCodes = "1";
                    }

                    mycookie.Expires = System.DateTime.Now.AddDays(1);
                    Response.Cookies.Add(mycookie);

                    HttpContext.Current.Response.Cookies.Add(mycookie);
                    

                    if (Session["ResquestedURL"] != null && !Convert.ToString(Session["ResquestedURL"]).Contains("ResetPassword"))
                    {
                        string RedirectURL = Convert.ToString(Session["ResquestedURL"]);
                        Session["ResquestedURL"] = null;
                        if (!RedirectURL.Contains("?"))
                        {
                            en.ParamURL_Value = bl.AddQueryString(RedirectURL, en.ClientCodes);
                            //Session["ResquestedURL"] = en.ParamURL_Value;
                            RedirectURL = en.ParamURL_Value;

                            Response.Redirect(RedirectURL);
                        }
                        else
                        {
                            Response.Redirect(RedirectURL);
                        }
                       
                    }
                    else
                    {
                        if(BrollconnectPath== "/BrollConnect")
                        {
                            ServerURL = ServerURL + BrollconnectPath;
                        }
                        
                        en.ParamURL_Value = bl.AddQueryString(ServerURL + "/HomePage.aspx", en.ClientCodes);
                        Session["ResquestedURL"] = en.ParamURL_Value;

                        Response.Redirect(Session["ResquestedURL"].ToString());
                        // Response.Redirect("HomePage.aspx");
                    }

                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "InvalidLogin", "InvalidLogin();", true);

                }
            }
            catch (Exception ex)
            {
                UPDexceptions.WriteException(Convert.ToString(ex.StackTrace), Convert.ToString(Session["username"]), Convert.ToString(HttpContext.Current.Request.Url.AbsoluteUri));
            }


        }

        private void Btn_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}