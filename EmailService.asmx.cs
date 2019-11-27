using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using UpdatePortal.Layers;
using System.Data;
using UpdatePortal.HelperClass;
using UpdatePortal.ErrorHandling;

namespace UpdatePortal.Webservices
{
    /// <summary>
    /// Summary description for EmailService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class EmailService : System.Web.Services.WebService
    {
        Datalayer dl = new Datalayer();
        Entity en = new Entity();
        SecureLayer sl = new SecureLayer();
        UpdateExceptions UPDexceptions = new UpdateExceptions();

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public string Checkemail(string EmailID)
        {
            en.checkemailFlag = "0";
            try
            {
                
               

                en.Emailid = EmailID;
                //DataSet ds = new DataSet();
                en.loginData = dl.runQuery("Users_SSP '" + "ResetPassword" + "','" + en.Emailid + "'"); //dl.runQuery("GetParetComment_SSP");
                if(en.loginData.Tables[0].Rows.Count>0)
                {
                    Random r = new Random();
                    int randomnum = r.Next(100000, 999999);
                    en.checkemailFlag = "1";
                    SecureLayer sl = new SecureLayer();
                    string EncryptedCode=  sl.Encrypt(Convert.ToString(randomnum));


                    dl.runQuery("Users_AuthRequest_Engine '" + "SecuritycodeINSERT" + "','" + en.Emailid +"','"+ EncryptedCode + "'");
                    MailHandlerClass mhc = new MailHandlerClass();

                    string DecryptedCode = sl.Decrypt(EncryptedCode);

                    string flag = mhc.SendSecurityCode(en.Emailid, DecryptedCode);
                    if (flag == "a")
                    {
                        dl.runQuery("Users_AuthRequest_Engine '" + "SecuritycodeSEND" + "','" + en.Emailid + "'");
                    }
                    else
                    {
                        dl.runQuery("Users_AuthRequest_Engine '" + "EmailNotSent" + "','" + en.Emailid + "'");
                    }




                }
                else
                {
                    en.checkemailFlag = "0";
                }

              

            }
            catch (Exception ex)
            {

                UPDexceptions.WriteException(Convert.ToString(ex.StackTrace), Convert.ToString(Session["username"]), Convert.ToString(HttpContext.Current.Request.Url.AbsoluteUri));
            }
            return en.checkemailFlag;
        }

        [WebMethod]
        public string ValidateSecurityCode(string EmailID, string SecurityCode)
        {
            string SCflag = "0";
            try
            {
                DataSet dsSc = new DataSet();
                //SecureLayer sl = new SecureLayer();
                
                //string emailID = context.Split( Convert.ToChar("@@@"))[0].ToString();
                //string SecurityCode= context.Split("@@@")[1].ToString();


                dsSc = dl.runQuery("Users_AuthRequest_Engine '" + "ValidateSecurityCode" + "','" + EmailID + "'");
                if (dsSc.Tables[0].Rows.Count > 0)
                {
                    string SCode = dsSc.Tables[0].Rows[0][0].ToString();
                    string OTPcodefromDB = sl.Decrypt(SCode);
                    if (OTPcodefromDB == SecurityCode)
                    {
                        SCflag = "1";
                    }


                }

            }
            catch (Exception ex)
            {

                UPDexceptions.WriteException(Convert.ToString(ex.StackTrace),Convert.ToString( Session["username"]), Convert.ToString(HttpContext.Current.Request.Url.AbsoluteUri));
            }
            return SCflag;



        }

        [WebMethod]
        public string ChangePassword (string EmailID,string ChngPassword)
        {
            //SecureLayer sl = new SecureLayer();
            string systemError = "";
            try
            {
                string encrypted = sl.Encrypt(ChngPassword);
                
                dl.runQuery("Users_AuthRequest_Engine '" + "ChangePassword" + "','" + EmailID + "','"+string.Empty+"','" + encrypted + "'");
            }
            catch(Exception ex)
            {
                systemError = "System Error ! Please Try Again Later";
                UPDexceptions.WriteException(Convert.ToString(ex.StackTrace), Convert.ToString(Session["username"]), Convert.ToString(HttpContext.Current.Request.Url.AbsoluteUri));
            }
            return systemError;



        }
    }
}
