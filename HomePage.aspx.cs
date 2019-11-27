using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UpdatePortal.Layers;
using UpdatePortal.HelperClass;
using System.Data;
using UpdatePortal.Sessions;
using UpdatePortal.ErrorHandling;

namespace BrollConnect
{
    public partial class HomePage : System.Web.UI.Page
    {
        Datalayer dl = new Datalayer();
        Entity en = new Entity();
        SecureLayer sl = new SecureLayer();
        App_Sessions sessions = new App_Sessions();
        UpdateExceptions UPDexceptions = new UpdateExceptions();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
               
                //Session["ResquestedURL"] = null;
            
                    HttpCookie cookie = Request.Cookies["LoginDetails"];
               


                if (!IsPostBack)
                {
                    string URL = "";

                    if (cookie != null)
                    {

                        en.username = cookie["username"];
                        en.Emailid = cookie["emailID"];
                        en.loginContext = cookie["Context"];
                      URL = sl.PageNavigateByCookies(en.username, en.loginContext, HttpContext.Current.Request.Url.AbsoluteUri);
                        sessions.UrlSession(Convert.ToString(HttpContext.Current.Request.Url.AbsoluteUri));

                        //if (!URL.Contains("Login.aspx"))
                        //{
                          


                        //    //continue with same page

                        //}
                        //else
                        //{
                            
                        //    //Response.Redirect("../Login.aspx");
                        //}

                    }
                    else if (Session["ResquestedURL"] != null)
                    {
                        //continue with same page

                    }
                    else
                    {

                        en.username = cookie["username"];
                        en.Emailid = cookie["emailID"];
                        en.loginContext = cookie["Context"];
                        URL = sl.PageNavigateByCookies(en.username, en.loginContext, HttpContext.Current.Request.Url.AbsoluteUri);
                        sessions.UrlSession(Convert.ToString(HttpContext.Current.Request.Url.AbsoluteUri));
                        //Response.Redirect("~/Login.aspx");

                    }
                }
            }
            catch (Exception ex)
            {
                UPDexceptions.WriteException(Convert.ToString(ex.StackTrace), Convert.ToString(Session["username"]), Convert.ToString(HttpContext.Current.Request.Url.AbsoluteUri));
            }


        }
    }
}