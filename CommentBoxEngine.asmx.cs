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

namespace BrollConnect.WebServices
{
    /// <summary>
    /// Summary description for CommentBoxEngine
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class CommentBoxEngine : System.Web.Services.WebService
    {
        Datalayer dl = new Datalayer();
        Entity en = new Entity();
        SecureLayer sl = new SecureLayer();
        UpdateExceptions UPDexceptions = new UpdateExceptions();
        Business bl = new Business();

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]

        public Tuple<List<string>, List<string>, List<string>> PostComments(string Message,string Url,string username)
        {
            string eventID = bl.GetQueryStringParam(Url, "eventID");
            string ClientCode = bl.GetQueryStringParam(Url, "ClientID");
            if (ClientCode == null)
            {
                ClientCode = bl.GetQueryStringParam(Url, "Client");
            }
            if (ClientCode != null)
            {
                if (ClientCode == eventID)
                {
                    ClientCode = bl.GetQueryStringParam(Url, "Client");
                    if (ClientCode == eventID)
                    {
                        ClientCode = bl.GetQueryStringParam(Url, "ClientID");
                    }
                }
            }
            int userAccessable = bl.GetUserIndx(username);



            if (eventID != null) //Convert.ToInt32(eventID)
            {
                dl.runQuery("EventCommentsBox_ISP '" + username + "','" + Convert.ToInt32(eventID) + "','" + ClientCode +"','"+ Message +"','"+"|"+ userAccessable + "|"+"','"+ Url + "'");


                //bind comment after comment
                en.ds = dl.runQuery("EventCommentsBox_SSP '" + username + "','" + Convert.ToInt32(eventID) + "','" + ClientCode + "'");
                en.comments = en.ds.Tables[0].AsEnumerable().Select(r => r.Field<string>("MessageDetails")).ToList();
            }

            return new Tuple<List<string>, List<string>, List<string>>(en.comments, en.Users, en.EventGroupMembers); 
           


        }
        [WebMethod]
        public void UpdateUsersToGroup(string Url,string username,string flag,string ParentUsername="")
        {
            string eventID_Value = bl.GetQueryStringParam(Url, "eventID");
            string ClientCode_Value = bl.GetQueryStringParam(Url, "ClientID");
            if (ClientCode_Value == null)
            {
                ClientCode_Value = bl.GetQueryStringParam(Url, "Client");
            }
            if (ClientCode_Value != null)
            {
                if (ClientCode_Value == eventID_Value)
                {
                    ClientCode_Value = bl.GetQueryStringParam(Url, "Client");
                    if (ClientCode_Value == eventID_Value)
                    {
                        ClientCode_Value = bl.GetQueryStringParam(Url, "ClientID");
                    }
                }
            }
            int userAccessable = bl.GetUserIndx(username);


            
            if (eventID_Value != null) //Convert.ToInt32(eventID) Convert.ToString(Session["UserEmailID"])
            {
                dl.runQuery("EventAddUserToGroup_USP '" + flag + "','" + username + "','" +Convert.ToInt32( eventID_Value) + "','" + ClientCode_Value + "'");
                dl.runQuery("EventGrpupsBox_USP '" + flag + "','" + username + "','" + Convert.ToInt32(eventID_Value) + "','" + ClientCode_Value +"','"+""+"','"+ ParentUsername + "'");
            }


        }

        [WebMethod]
        public Tuple<List<string>, List<string>, List<string>,string,string> GetComments(string url, string username)


        {
            string eventID_Value = bl.GetQueryStringParam(url, "eventID");
            string ClientCode_Value = bl.GetQueryStringParam(url, "Client");
            if (ClientCode_Value == null)
            {
                ClientCode_Value = bl.GetQueryStringParam(url, "ClientID");
            }
            if(ClientCode_Value!=null)
            {
                if(ClientCode_Value== eventID_Value)
                {
                    ClientCode_Value = bl.GetQueryStringParam(url, "Client");
                    if(ClientCode_Value == eventID_Value)
                    {
                        ClientCode_Value = bl.GetQueryStringParam(url, "ClientID");
                    }
                }
            }
           
            if (eventID_Value != null) //Convert.ToInt32(eventID)
            {
                en.ds=dl.runQuery("EventCommentsBox_SSP '" + username + "','" + Convert.ToInt32(eventID_Value) + "','" + ClientCode_Value + "'");
                en.EventGroupDS= dl.runQuery("EventGroupBox_SSP '" + username + "','" + Convert.ToInt32(eventID_Value) + "','" + ClientCode_Value + "'");
                en.comments = en.ds.Tables[0].AsEnumerable().Select(r => r.Field<string>("MessageDetails")).ToList();
                en.Users=en.EventGroupDS.Tables[0].AsEnumerable().Select(r => r.Field<string>("UserID")).ToList();
                en.EventGroupMembers= en.EventGroupDS.Tables[1].AsEnumerable().Select(r => r.Field<string>("userID")).ToList();
                en.EventOwner = Convert.ToString(en.EventGroupDS.Tables[2].Rows[0]["EventOwner"]); //en.EventGroupDS.Tables[2].AsEnumerable().Select(r => r.Field<string>("EventOwner")).ToString();
            }

            return new Tuple<List<string>, List<string>, List<string>,string,string>(en.comments, en.Users, en.EventGroupMembers, eventID_Value,en.EventOwner);
       
        }

        [WebMethod]

        public List<string> GetHomePageComments(string url,string username)
        {
            try
            {
                string ClientCode_Value = bl.GetQueryStringParam(url, "ClientID");
                if(ClientCode_Value==null)
                {
                    ClientCode_Value = bl.GetQueryStringParam(url, "Client");
                }
               
                if (ClientCode_Value!=null)
                {
                    en.ds = dl.runQuery("EventCommentsBox_SSP '" + username + "','" + 0 + "','" + ClientCode_Value + "'");
                    en.comments = en.ds.Tables[0].AsEnumerable().Select(r => r.Field<string>("MessageDetails")).ToList();
                }


            }

            catch(Exception ex)
            {

            }

            return en.comments;



        }


    }
}
