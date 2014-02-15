<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<MultunusMvcPuzzle.Models.TwitterDIVInfo>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Multnus Twitter Puzzle</title>
    <link rel="stylesheet" type="text/css" href="../../Content/Site.css" />
    <script src="../../Scripts/jquery-1.10.2.min.js" type="text/javascript" > </script>
    <script src="../../Scripts/puzzle.js" type="text/javascript" > </script>
</head>
<body background="../../Content/images/Main-Bgr.png" style="background-size: 100%;overflow : scroll;">
    <form action="PuzzleHome" method="post">
        <p align="right">
            <input type="submit" name="BtnTryAnotherHandle" id="BtnTryAnotherHandle" value ="TRY ANOTHER HANDLE" />
        </p> 
       <div id="main-Container" style="width:1024px;height:680px" >
            
            <div id="TwitterCirclePanel" style="width:500;height:400" >
             
             <%= Model.MainCircleDIV %>
             <% foreach (var ChildCircleInfo in Model.RetweetedCircleDIV)
                {%>

                     <%= ChildCircleInfo%>
              <%} %>
                
                                
            </div>
       </div>
       <%= Html.Hidden("HidTweetCircleCount", Model.TweetCircleCounts, new
          {
              style = "display:none"
          })%>
    </form>
</body>
</html>
