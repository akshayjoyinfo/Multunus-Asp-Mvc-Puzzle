<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Multunus Twitter Puzzle </title>
     <link rel="stylesheet" type="text/css" href="../../Content/Site.css" />
    <script src="../../Scripts/jquery-1.10.2.min.js" type="text/javascript" > </script>
    <script src="../../Scripts/puzzle.js" type="text/javascript" > </script>
     <script language="javascript" type="text/javascript">

         $(document).ready(function () {

             $('#SampleTweetHandle').bind('click', function (event) {
                 // alert(event.target.id);
                 try {
                     var ImgSrc = event.target.id; // get id of first image
                     $('#txtTwitterHandle').val(ImgSrc);
                     $('#btnGetTweetCircle').trigger('click');

                 } catch (err) {
                     alert(err);

                 }
             });


             $("#form1").submit(function () {
                 var valid = true;
                 if (!btnGetTweetClientClik()) {
                     valid = false;
                 }
                 if (!valid) return false;
                                  
             });

         });

         function btnGetTweetClientClik() {
             try {

                 var URL = $("#txtTwitterHandle").val();
                 if (URL == '') {
                     alert("Please enter the URL ");
                     return false;
                 }

                 var pattern = /(ftp|http|https):\/\/(\w+:{0,1}\w*@)?(\S+)(:[0-9]+)?(\/|\/([\w#!:.?+=&%@!\-\/]))?/;
                 if (pattern.test(URL)) {
                 
                     return true;
                 }
                 alert("Url is not valid!");
                 
                 $('#txtTwitterHandle').val('');
                 return false;

             }
             catch (err) {
                 alert(err);
             }

         }

    </script>
</head>
<body background="../../Content/images/Bg-Screen.jpg" style="background-size: 100%;overflow : scroll;">
    <form action="Puzzle/PuzzleResult" id="form1" method="post">
    <div id="SampleTweetHandle" class="imageTweetHandler">
    
    <img src="https://pbs.twimg.com/profile_images/426158315781881856/sBsvBbjY.png" id="https://twitter.com/github/status/428654444993994752" />
    <img src="http://pbs.twimg.com/profile_images/2556368541/alng5gtlmjhrdlr3qxqv.jpeg" id="https://twitter.com/dhh/status/431806048035696640" />
    <img src="https://pbs.twimg.com/profile_images/79787739/mf-tg-sq.jpg" id="https://twitter.com/martinfowler/status/431211716811042816" />
    <img src="https://pbs.twimg.com/profile_images/424495004/GuidoAvatar.jpg" id="https://twitter.com/gvanrossum/status/427650653473615872" />
    <img src="https://pbs.twimg.com/profile_images/378800000091193257/fcb03c8d0a40048f2537df967239686f.jpeg" id="https://twitter.com/spolsky/status/430431156043935745" />
    <img src="https://pbs.twimg.com/profile_images/378800000324784929/1a4ee3fde80808a96ed268a7fb94682d.png" id="https://twitter.com/firefox/status/431125106081820672" />
    </div>
    
    <div id="main-Container" style="width:1024px;height:100%">
        
        <div id="main-data-query" style="margin-left:38%;margin-top:10%;">
            <span id="lblTwitterHandle">Enter a Valid Tweet Handle </span>
            <%= Html.TextBox("txtTwitterHandle", "https://twitter.com/firefox/status/431125106081820672")%>
            <input type="submit" id="btnGetTweetCircle" name="btnGetTweetCircle" 
                value="Get Tweet Circle"/> 
        </div>
            
    </div>
    </form>
</body>
</html>
