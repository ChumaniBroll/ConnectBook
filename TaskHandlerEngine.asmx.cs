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
using Newtonsoft.Json;

namespace BrollConnect.WebServices
{
    /// <summary>
    /// Summary description for TaskHandlerEngine
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class TaskHandlerEngine : System.Web.Services.WebService
    {
        Entity en = new Entity();
        Datalayer dl = new Datalayer();

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]

        public List<string> Getcontrols(string Username, string ClientCode)
        {
            try
            {
                en.ds = dl.runQuery("ACQ_GetControls_SSP '" + Username + "','"  + ClientCode + "'");
                if(en.ds.Tables[0].Rows.Count>0)
                {
                 en.ACQ_ControlsList= en.ds.Tables[0].AsEnumerable().Select(r => r.Field<string>("Controls")).ToList();
                }
            }
            catch(Exception ex)
            {

            }

            return en.ACQ_ControlsList;

        }

        [WebMethod]
        public string InsertACQ(object obj,string ClientCode,string Username)
        {
            int ACQ_count = 0;
            int eachEntry = 0;
            string ACQconfirm = "";
            string ACQEntry = "No Entries";
            string datetime = Convert.ToString(DateTime.Now);
            string ACQ_mappingIndx = "";
            try
            {
               
            
                var result  = JsonConvert.DeserializeObject<Dictionary<string, string>>(obj.ToString());
                ACQ_count = result.Count;
                foreach (var e in result)
                {
                    string k = e.Key.Replace("_", " ");
                    string v = e.Value;
                    if(v.Length>0)
                    {
                        if(ClientCode=="ABSA")
                        {
                            if(k.ToString().ToLower()!= "conflict")
                            {
                                ACQEntry = "Has Entries";
                            }
                            
                        }
                        

                    }
                }


                if(ACQEntry== "Has Entries")
                {
                  en.ds=  dl.runQuery("ACQ_InsertToACQMappingTable '" +  Username + "','" + ClientCode + "','" + datetime + "'");
                    if(en.ds.Tables[0].Rows.Count>0)
                    {
                        ACQ_mappingIndx = Convert.ToString(en.ds.Tables[0].Rows[0]["ACQ_mappingIndx"]);
                    }
                   
                }

                    foreach (var e in result)
                {
                    
                   string k = e.Key.Replace("_"," ");
                    string v = e.Value;
           
                    if(k.Length>0 && ACQEntry== "Has Entries")
                        {
                         dl.runQuery("ACQ_Data_ISP '" + k +"','"+v+ "','" + Username + "','" + ClientCode +"','"+ datetime +"','"+Convert.ToInt32(ACQ_mappingIndx )+ "'");
                         eachEntry = eachEntry + 1;
                        }
                   
                }

                if(eachEntry==ACQ_count)
                {
                    ACQconfirm = "Success";
                }
                if(eachEntry==0)
                {
                    ACQconfirm = "No Entries";
                }
                

               
            }
            catch(Exception ex)
            {
                if (eachEntry == ACQ_count - 1 && eachEntry!=0 && ACQ_count!=0)
                {
                    ACQconfirm = "Failed";
                }

            }
            //var dt= JsonConvert.DeserializeObject<DataTable>(obj.ToString());
         

            return ACQconfirm;
        }
    }

    
}
