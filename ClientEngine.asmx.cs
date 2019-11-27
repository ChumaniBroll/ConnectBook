using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using UpdatePortal.Layers;
using System.Data;
using UpdatePortal.HelperClass;
using BrollConnect.Layers;

namespace UpdatePortal.Webservices
{
    /// <summary>
    /// Summary description for ClientEngine
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class ClientEngine : System.Web.Services.WebService
    {
        Datalayer dl = new Datalayer();
        Entity en = new Entity();
        Business bl = new Business();

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]

        public DataSet GetClinetInfo(string username)
        {
            //string ClinetAssigned = "";
            en.ClientInfo=  dl.runQuery("Client_Info_SSP '" + "GetClinetInfo" + "','" + Session["UserEmailID"] + "'");
            if (en.ClientInfo.Tables[0].Rows.Count>0)
            {
                //ClinetAssigned = en.ClientInfo.Tables[0].Rows[0][0].ToString();
            }
            return en.ClientInfo;
        }

        [WebMethod]
        public void UpdateLoaginClientWorked(string EmailID,string FK_Client_Link, string Last_Client_Worked)
        {
            try
            {
              
                FK_Client_Link = bl.UpdateUserClient_Format(FK_Client_Link);
                //System.Threading.Thread.Sleep(1000); 
                dl.runQuery("Users_Client_USP '" + "UpdateClientLastClientWorked" + "','" + EmailID + "','" + FK_Client_Link + "','" + Last_Client_Worked + "'");
               
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
