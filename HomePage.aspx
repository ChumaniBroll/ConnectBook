<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="BrollConnect.HomePage" MasterPageFile="~/MasterPage.Master" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1, maximum-scale=1" />
    <meta name="msapplication-tap-highlight" content="no" />

    <meta name="mobile-web-app-capable" content="yes" />
    <meta name="application-name" content="Milestone" />

    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="apple-mobile-web-app-title" content="Milestone" />
  
   <style type="text/css">
        .configuration {
            display: none;
        }
    </style>

    <link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["delimeter"]%>vendor/bootstrap/dist/css/bootstrap.css" />
    <link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["delimeter"]%>vendor/pace/themes/blue/pace-theme-minimal.css" />
    <link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["delimeter"]%>vendor/font-awesome/css/font-awesome.css" />
    <link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["delimeter"]%>vendor/animate.css/animate.css" />
    <link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["delimeter"]%>AppStyles/app.css" id="load_styles_before" />
    <link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["delimeter"]%>AppStyles/app.skins.css" />
    <!-- endbuild -->
        <script>
            function updatetracker() {
                //debugger;
                document.getElementById("hdnprgs").value = document.getElementById("prgs").value;
                var d = parseInt(document.getElementById("hdnprgs").value) + 20;
                document.getElementById("prgs").value = d;//document.getElementById("hdnprgs").value + 10;

                if (parseInt(document.getElementById("prgs").value) > 10 && parseInt(document.getElementById("prgs").value) < 54) {
                    document.getElementById("prgs").className = "progress progress-striped progress-warning";
                }
                if (parseInt(document.getElementById("prgs").value) > 55 && parseInt(document.getElementById("prgs").value) < 74) {
                    document.getElementById("prgs").className = "progress progress-striped progress-info";
                }
                if (parseInt(document.getElementById("prgs").value) > 75 && parseInt(document.getElementById("prgs").value) < 99) {
                    document.getElementById("prgs").className = "progress progress-striped progress-primary";
                }
                if (parseInt(document.getElementById("prgs").value) >= 100) {
                    document.getElementById("prgs").className = "progress progress-striped progress-success";
                }
            }
        </script>

  <!-- main area -->
        <div class="main-content">
          <div class="content-view">

             
          </div>
          <!-- bottom footer -->
          <div class="content-footer">
            <nav class="footer-right">
              <ul class="nav">
                <li>
                  <a href="javascript:;">Feedback</a>
                </li>
              </ul>
            </nav>
            <nav class="footer-left">
              <ul class="nav">
                <li>
                  <a href="javascript:;">
                    <span>Copyright</span>
                    &copy; 2019 UPD
                  </a>
                </li>
                <li class="hidden-md-down">
                  <a href="javascript:;">Privacy</a>
                </li>
                <li class="hidden-md-down">
                  <a href="javascript:;">Terms</a>
                </li>
                <li class="hidden-md-down">
                  <a href="javascript:;">help</a>
                </li>
              </ul>
            </nav>
          </div>
          <!-- /bottom footer -->
        </div>
        <!-- /main area -->

</asp:Content>


