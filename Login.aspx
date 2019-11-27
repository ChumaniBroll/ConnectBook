<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="BrollConnect.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
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

    <%--<meta name="theme-color" content="#4C7FF0" />--%>

    <title>UPD 9 -Login</title>

    <!-- page stylesheets -->
    <!-- end page stylesheets -->

    <!-- build:css({.tmp,app}) styles/app.min.css -->
    <link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["delimeter"]%>vendor/bootstrap/dist/css/bootstrap.css" />
    <link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["delimeter"]%>vendor/pace/themes/blue/pace-theme-minimal.css" />
    <link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["delimeter"]%>vendor/font-awesome/css/font-awesome.css" />
                           
    <link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["delimeter"]%>vendor/animate.css/animate.css" />
    <link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["delimeter"]%>AppStyles/app.css" id="load_styles_before" />
    <link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["delimeter"]%>AppStyles/app.skins.css" />
    <!-- endbuild -->
    <!-- end initialize page scripts -->

    <script src="<%=ConfigurationManager.AppSettings["delimeter"]%>vendor/jquery/dist/jquery.js"></script>
    <script>

        function InvalidLogin() {

            $(function () {
                $("#divmsg").click();
            })


        }


    </script>
    <script type="text/javascript">
        function preventBack() {
            window.history.forward();
        }
        setTimeout("preventBack()", 0);
        window.onunload = function () {
            null
        };
    </script>


</head>
<body>

    <div class="app no-padding no-footer layout-static">
        <div class="session-panel">
            <div class="session">
                <div class="session-content">
                    <div class="card card-block form-layout">
                        <form role="form" id="validate" runat="server">
                            <h6 class="header">
                                <img src="<%=ConfigurationManager.AppSettings["delimeter"]%>images/city-skyline-silhouette.jpg" style="opacity:0.2" class="card-img-top img-fluid m-b-1" height="80" alt="" />
                            </h6>
                            <div class="text-xs-center m-b-3">
                                
                               <hr class="no-border" />
                                <h5>Welcome !
                                </h5>
                                <p class="text-muted">
                                    Sign in with your app id to continue.
                 
                                </p>
                            </div>
                            <fieldset class="form-group">
                                <label for="username">
                                    Enter your username
                 
                                </label>
                                <input type="text" class="form-control form-control-lg" name="username" placeholder="username" required />
                            </fieldset>
                            <fieldset class="form-group">
                                <label for="password">
                                    Enter your password
                 
                                </label>
                                <input type="password" class="form-control form-control-lg" name="password" placeholder="********" required />
                            </fieldset>
                            <fieldset class="form-group" style="display:none">
                                <label for="username">
                                    Save Credentials
                 <span>
                     <asp:CheckBox ID="chkStaySignedIn" CssClass="custom-control custom-checkbox m-b-1" runat="server" /></span>
                                </label>



                            </fieldset>

                            <asp:ScriptManager runat="server" EnablePageMethods="true" LoadScriptsBeforeUI="true"></asp:ScriptManager>
                            <asp:Button runat="server" ID="btlogin" CssClass="btn btn-primary btn-block btn-lg" Text="Login" OnClick="btlogin_Click" />
                            <fieldset class="form-group">

                                <select class="custom-select" hidden="hidden" id="messenger-type" style="width: 100%;">
                                    <option>Error</option>
                                </select>
                                <select class="custom-select" hidden="hidden" id="position" style="width: 100%;">
                                    <option value="topRight">Top right</option>

                                </select>
                                <textarea class="form-control m-b-1" hidden="hidden" id="message" placeholder="Enter a message ..." rows="2">Invalid Login! Please Contact System Administrator.</textarea>
                                <div id="divmsg" style="display: none" class="btn btn-primary btn-sm show-messenger">
                                </div>


                            </fieldset>
                            <%--  <div class="divider">
                  <span>
                    OR
                  </span>
                </div>
                <div class="text-xs-center">
                  <p>
                    Login with your social account
                  </p>
                  <button href="javascript:;" class="btn btn-icon-icon btn-facebook btn-lg m-b-1 m-r-1">
                    <i class="fa fa-facebook">
                    </i>
                  </button>
                  <button href="javascript:;" class="btn btn-icon-icon btn-github btn-lg m-b-1 m-r-1">
                    <i class="fa fa-github">
                    </i>
                  </button>
                  <button href="javascript:;" class="btn btn-icon-icon btn-google btn-lg m-b-1 m-r-1">
                    <i class="fa fa-google-plus">
                    </i>
                  </button>
                  <button href="javascript:;" class="btn btn-icon-icon btn-linkedin btn-lg m-b-1 m-r-1">
                    <i class="fa fa-linkedin">
                    </i>
                  </button>
                </div>--%>
                        </form>
                    </div>
                </div>
                <footer class="text-xs-center p-y-1">
                    <p>
                        <a href="<%=ConfigurationManager.AppSettings["delimeter"]%>Admin/ResetPassword.aspx">Forgot password?
                        </a>

                    </p>
                </footer>
            </div>

        </div>
    </div>

    <script type="text/javascript">
        window.paceOptions = {
            document: true,
            eventLag: true,
            restartOnPushState: true,
            restartOnRequestAfter: true,
            ajax: {
                trackMethods: ['POST', 'GET']
            }
        };
    </script>



    <!-- build:js({.tmp,app}) scripts/app.min.js -->
    <script src="<%=ConfigurationManager.AppSettings["delimeter"]%>vendor/jquery/dist/jquery.js"></script>
    <script src="<%=ConfigurationManager.AppSettings["delimeter"]%>vendor/pace/pace.js"></script>
    <script src="<%=ConfigurationManager.AppSettings["delimeter"]%>vendor/tether/dist/js/tether.js"></script>
    <script src="<%=ConfigurationManager.AppSettings["delimeter"]%>vendor/bootstrap/dist/js/bootstrap.js"></script>
    <script src="<%=ConfigurationManager.AppSettings["delimeter"]%>vendor/fastclick/lib/fastclick.js"></script>
    <script src="<%=ConfigurationManager.AppSettings["delimeter"]%>AppScripts/constants.js"></script>
    <script src="<%=ConfigurationManager.AppSettings["delimeter"]%>AppScripts/main.js"></script>
    <!-- endbuild -->

    <!-- page scripts -->
    <script src="<%=ConfigurationManager.AppSettings["delimeter"]%>vendor/noty/js/noty/packaged/jquery.noty.packaged.min.js"></script>
    <script src="<%=ConfigurationManager.AppSettings["delimeter"]%>AppScripts/helpers/noty-defaults.js"></script>
    <!-- end page scripts -->

    <!-- initialize page scripts -->
    <script src="<%=ConfigurationManager.AppSettings["delimeter"]%>AppScripts/ui/notifications.js"></script>
    <!-- end initialize page scripts -->
    <!-- disable settings button-->
    <style type="text/css">
        .configuration {
            display: none;
        }
    </style>
    <!-- initialize page scripts -->
    <script type="text/javascript">
        //$('#validate').validate();



    </script>
    <style type="text/css">
        .configuration {
            display: none;
        }
    </style>


</body>

</html>

