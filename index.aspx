<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="HendoHealth.index" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
    <title>HendoHealth</title>
    <script type="text/javascript" src="Scripts/jquery-1.9.1.js" ></script>
    <script type="text/javascript" src="Scripts/jquery-migrate-1.4.1.js"></script>
    <script type="text/javascript" src="Scripts/bowser.js"></script>
    <script type="text/javascript" src="Scripts/mediaelement/mediaelement-and-player.min.js" ></script>
    <script type="text/javascript" src="Scripts/progress_bar.js"></script>
    <script type="text/javascript" src="Scripts/sleep.js"></script>
    <link rel="stylesheet" href="Scripts/mediaelement/mediaelementplayer.css" />
    <link rel="stylesheet" href="Content/bootstrap.min.css" />
    <link rel="stylesheet" href="Css/index.css" />
    <link rel="stylesheet" href="Css/loadingbox.css" />
    <script type="text/javascript">
        jQuery(document).ready(function ($) {
            $('#iHealth_video').mediaelementplayer({
                success: function (mediaElement, originalNode, instance) {
                    instance.enterFullScreen();
                    
                    //$('#iHealth_video').click();
                },
                error: function () {
                    //handle error
                }
            });
        });
        function fillinbox() {
            $.showprogress('Getting Measures', 'Loading.....', '<img src="App_Data/image.gif" />');
            sleepTimer();
        }
    </script>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="start box">
            <video id="iHealth_video" autoplay="autoplay" loop="loop" style="max-width: 100%" poster="App_Data/ihealth.jpg">
                <source type="video/youtube" src="https://www.youtube.com/watch?v=_lZ8jQZUKXQ" />
            </video>
        </div>
        <div class="slideshow box">
            <iframe src="App_Data/BpManual.pdf" style="min-width: 100%; min-height: 100%"></iframe>
            <asp:SiteMapDataSource ID="SiteMapDataSource1" runat="server" ShowStartingNode="true" />
            <asp:Menu ID="Menu" runat="server" DataSourceID="SiteMapDataSource1" Orientation="Horizontal"
                OnMenuItemDataBound="OnMenuItemDataBound">
                <LevelMenuItemStyles>
                    <asp:MenuItemStyle CssClass="main_menu" />
                </LevelMenuItemStyles>
            </asp:Menu>
        </div>
        <div class="measurements box">
            <img src="App_Data/ihealth.jpg" style="width: 100%; height: 100%" />
        </div>
        <div class="leads box">
            <div class="col col-md-4 col-md-offset-4" style="margin-top: 15px">
                <asp:Button ID="measures" runat="server" Text="Effetttua Misure" OnClick="getMeasures_Click" OnClientClick="return fillinbox();" />
            </div>
        </div>
    </form>
</body>
</html>
