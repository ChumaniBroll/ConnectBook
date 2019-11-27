using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication_UPD8;
using System.Data;

namespace BrollConnect
{
    public partial class Default : System.Web.UI.Page
    {
        DataLayer dl = new DataLayer();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            DataSet loginData = new DataSet();
            string username = "";
            string password = "";
            string loginContext = "";

            loginContext = "";
            username = Request.Form["username"];
            password = Request.Form["password"];
            loginData = dl.runQuery("Users_ssp_login '" + username + "' , '" + password + "' , '" + loginContext + "'");

            if (Convert.ToString(loginData.Tables[0].Rows[0]["ClientAccess"]) != "")
            {
                //Session["username"] = Request.Form["username"];
                Session["username"] = "agadi";// username;//ddlUsers.SelectedItem.Text;
                Session["UserId"] = "7";//Convert.ToString(loginData.Tables[0].Rows[0]["Indx"]);
                //Response.Redirect("~/StartChat.aspx");
                Response.Redirect("~/StartChat.aspx");
            }
            else
            {
                //Page.ClientScript.RegisterStartupScript(Page.GetType(), "InvalidLogin", "InvalidLogin();", true);
                //Session["InvalidUser"] = "InvalidUser";
                //msg1.OnClientClick(null, null);
                //Button btn = (Button)Page.FindControl("msg");
                //Response.Write("<script language='javascript' type='text/javascript'>aa(););
                //Response.Write("</script>"")
                //ScriptManager.RegisterStartupScript(this, GetType(), "Message", "aa();", true);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "aa();", true);
                //divInvalidLogin.Visible = true;
                //ClientScript.RegisterStartupScript(GetType(), "hwa", "FnLoginFail();", true);
                //ScriptManager.RegisterClientScriptBlock(Page, typeof(System.Web.UI.Page), "Script", "FnLoginFail();", true);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "callFnLoginFail", "aa();", true);
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "aa", "<script type=\"text/javascript\">aa();</script>");
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "MyFun1", "FnLoginFail();", true);
            }
            //Session["UserName"] = ddlUsers.SelectedItem.Text;
            //Session["UserId"] = ddlUsers.SelectedValue;
            //Response.Redirect("~/StartChat.aspx");
        }
    }
}