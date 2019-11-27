using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using UpdatePortal.Layers;
using System.Data;
using UpdatePortal.HelperClass;
using UpdatePortal.ErrorHandling;
using BrollConnect.Layers;
using System.Xml;
using System.IO;
using System.Drawing;

namespace BrollConnect.WebServices
{
    /// <summary>
    /// Summary description for UserEngine
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class UserEngine : System.Web.Services.WebService
    {
        Datalayer dl = new Datalayer();
        Entity en = new Entity();
        SecureLayer sl = new SecureLayer();
        UpdateExceptions UPDexceptions = new UpdateExceptions();
        Business bl = new Business();
        HttpContext context = HttpContext.Current;


        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public Tuple<List<string>, List<string>, List<string>> GetUsers()
        {
            try
            {
                en.ds = dl.runQuery("Users_SSP '" + "" + "'");

                en.Users = en.ds.Tables[0].AsEnumerable().Select(r => r.Field<string>("EMail")).ToList();
                en.UserClients= en.ds.Tables[1].AsEnumerable().Select(r => r.Field<string>("ClientCode")).ToList();
                en.UserRoles= en.ds.Tables[2].AsEnumerable().Select(r => r.Field<string>("Code")).ToList();

            }
            catch(Exception ex)
            {
                UPDexceptions.WriteException(Convert.ToString(ex.StackTrace), Convert.ToString(Session["username"]), Convert.ToString(HttpContext.Current.Request.Url.AbsoluteUri));
            }

            //return en.Users;
            return new Tuple<List<string>, List<string>, List<string>>(en.Users,en.UserRoles, en.UserClients);

        }

        [WebMethod]
        public Tuple<List<string>,List<string>> GetUserRolesClients(string EmailID)
        {
            try
            {
                en.ds = dl.runQuery("Users_SSP '" + "GetUserRolesandClients" + "','" + EmailID + "'");

                if (en.ds.Tables[0].Rows.Count>0)
                {
                    en.UserRoles = en.ds.Tables[0].AsEnumerable().Select(r => r.Field<string>("UserRoleLinked")).ToList();
                }
                if(en.ds.Tables[1].Rows.Count>0)
                {
                    en.UserClients= en.ds.Tables[1].AsEnumerable().Select(r => r.Field<string>("ClientCode")).ToList();
                }
               

            }
            catch (Exception ex)
            {
                UPDexceptions.WriteException(Convert.ToString(ex.StackTrace), Convert.ToString(Session["username"]), Convert.ToString(HttpContext.Current.Request.Url.AbsoluteUri));
            }


            return new Tuple<List<string>, List<string>>(en.UserRoles, en.UserClients);
        }

        [WebMethod]
        public string UpdateUserDetails (string UserRoles ,string UserClients , string EmailID )
        {
            try
            {
                en.ClientCodes= bl.UpdateUserClient_Format(UserClients);

                en.UserIndx = bl.GetUserIndx(EmailID);

                dl.runQuery("Users_USP '" + en.UserIndx + "','" + EmailID + "','" + EmailID + "','" + EmailID + "','" + UserRoles +"','"+ "1" +"','"+ en.ClientCodes + "'");

               
            }
            catch(Exception ex)
            {
                UPDexceptions.WriteException(Convert.ToString(ex.StackTrace), Convert.ToString(Session["username"]), Convert.ToString(HttpContext.Current.Request.Url.AbsoluteUri));
            }

            return EmailID;


        }

        [WebMethod]
        public void InsertUser(string UserRoles, string UserClients, String EmailID)
        {
            try
            {
                //en.ClientCodes = bl.UpdateUserClient_Format(UserClients);

                string strRemovePipe = UserClients.Replace("|", "");
                

                XmlDocument doc = new XmlDocument();
                XmlElement el = (XmlElement)doc.AppendChild(doc.CreateElement("Root"));
                // el.SetAttribute("CR", "Clients & Roles");

                List<string> lstroles = new List<string>();
                lstroles = UserRoles.Split( Convert.ToChar("|")).Where(x=>x.Length>0).ToList();
                for(int i=0;i<=lstroles.Count-1;i++)
                {
                    
                    el.AppendChild(doc.CreateElement(strRemovePipe)).InnerText ="<Role>" +lstroles[i] +"</Role>";
                }
                UserRoles = doc.OuterXml;
                

                dl.runQuery("Users_ISP '" + EmailID + "','" + EmailID + "','" + EmailID + "','" + UserRoles + "','"  + UserClients + "'");

                //inserting Client and Roles assigned 
                //dl.runQuery("Users_CLIENTvsROLE_SSP '" + EmailID + "','" + strRemovePipe + "','" + EmailID + "','" + UserRoles + "','" + UserClients + "'");



            }
            catch (Exception ex)
            {
                UPDexceptions.WriteException(Convert.ToString(ex.StackTrace), Convert.ToString(Session["username"]), Convert.ToString(HttpContext.Current.Request.Url.AbsoluteUri));
            }

        }

        [WebMethod]
        public string CheckUserDetails(string EmailID)
        {
            string userFlag = "";
            try
{
               en.ds= dl.runQuery("Users_SSP '" + "GetUserRolesandClients" + "','" + EmailID + "'");
                if (en.ds.Tables[0].Rows.Count>0)
                {
                    userFlag = "Yes";
                }
                
            }
            catch(Exception ex)
            {
                UPDexceptions.WriteException(Convert.ToString(ex.StackTrace), Convert.ToString(Session["username"]), Convert.ToString(HttpContext.Current.Request.Url.AbsoluteUri));
            }

            return userFlag;
        }

        [WebMethod]
        public List<string> GetRoles_Editpage(string UserRoles, string UserClients, string EmailID,string flag="",string RevokeClientAccess="")
        {
            try
            {
                if(flag== "InsertRole")
                {
                    //insert role
                    en.ds = dl.runQuery("Users_CLIENTvsROLE_SSP '" + EmailID + "','" + UserClients + "','" + UserRoles + "','" + "InsertRole" + "'");



                    //update User  profile 
                    

                }
                if(flag== "DeleteRole")
                {
                    //remove role 
                    en.ds = dl.runQuery("Users_CLIENTvsROLE_SSP '" + EmailID + "','" + UserClients + "','" + UserRoles + "','" + "DeleteRole" + "'");
                    if(RevokeClientAccess=="True")
                    {


                        string a;

                        en.ds = dl.runQuery("Users_CLIENTvsROLE_SSP '" + EmailID + "','" + UserClients + "','" + UserRoles + "','" + "RevokeClinetAccess" + "'");
                    }
                }
                else
                {
                    //select query
                    en.ds = dl.runQuery("Users_CLIENTvsROLE_SSP '" + EmailID + "','" + UserClients + "','" + UserRoles + "','" + "SelectQuery" + "'");
                }

                if (en.ds.Tables[0].Rows.Count>0)
                {
                    en.UserRoles = en.ds.Tables[0].AsEnumerable().Select(r => r.Field<string>("Role")).ToList();
                }


                //string ClientCode_Value = bl.GetQueryStringParam(Convert.ToString(HttpContext.Current.Request.UrlReferrer), "ClientID");
                //if (ClientCode_Value == null)
                //{
                //    ClientCode_Value = bl.GetQueryStringParam(Convert.ToString(HttpContext.Current.Request.UrlReferrer), "Client");
                //}

                //en.ds = dl.runQuery("Get_UserInfo_SSP '" + "GetUserProfile" + "','" + ParentEmail + "','" + ClientCode_Value + "'");
                //en.UserRoles = en.ds.Tables[0].AsEnumerable().Select(r => r.Field<string>("Role")).ToList();

                //for (int i = 0; i <= en.UserRoles.Count - 1; i++)
                //{
                //    en.UserRole = en.UserRole + en.UserRoles[i] + " ,";
                //}
                //en.UserRole = en.UserRole.Remove(en.UserRole.Length - 1);
                //if (en.UserRole.Length == 0)
                //{

                //    context.Session["UserEmailID"] = "No Roles Assigned";
                //}
                //else
                //{
                //    context.Session["UserEmailID"] = en.UserRole;
                //}

            }
            catch(Exception ex)
            {
                UPDexceptions.WriteException(Convert.ToString(ex.StackTrace), "", Convert.ToString(HttpContext.Current.Request.UrlReferrer));
            }

            return en.UserRoles;
        }

        [WebMethod]
        public string InsertAvatar(string URL,string Username)
        {
            try
            {

                 byte[] avatar = File.ReadAllBytes(@"C:\Users\agadi\Desktop\absa.PNG");
                string str = Convert.ToBase64String(avatar);
                //Image r = Image.FromFile(@"C:\Users\agadi\Desktop\absa.PNG");
                //object obj = @"C:\Users\agadi\Desktop\absa.PNG";
                //byte[] bytes = (byte[])obj;
                //byte[] bytes = Convert.FromBase64String(@"C:\Users\agadi\Desktop\absa.PNG");
                //File.Copy(@"C:\Users\agadi\Desktop\absa.PNG", Server.MapPath("..") + @"\Avatar\" + Username + ".png");

                dl.runQuery("Users_GetAvatar_SSP '" + "InsertAvatar" + "','" + Username + "','" + str + "'");
            }
            catch(Exception ex)
            {

            }
           

            return "";
        }

        [WebMethod]

        public List<string> GetuserRoles(string Username,string Client)
        {
            try
            {
                en.ds = dl.runQuery("Users_GetInfo_SSP '" + "GetUserProfile" + "','" + Username + "','" + Client + "'");
                en.UserRoles= en.ds.Tables[0].AsEnumerable().Select(r => r.Field<string>("Role")).ToList();
            }
            catch(Exception ex)

            {

            }

            return en.UserRoles;
        }
     

    }
}
