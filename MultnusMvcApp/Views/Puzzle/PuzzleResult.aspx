<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<MultunusMvcPuzzle.Models.TwitterDIVInfo>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Multnus Twitter Puzzle</title>
    <link rel="stylesheet" type="text/css" href="../../Content/Site.css" />
     <link rel="stylesheet" href="//code.jquery.com/ui/1.10.4/themes/blitzer/jquery-ui.css">
     <script src="../../Scripts/jquery-1.10.2.min.js" type="text/javascript" > </script>
    <script src="//code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
    <link rel="stylesheet" href="/resources/demos/style.css">    
    <script src="../../Scripts/puzzle.js" type="text/javascript" > </script>
    <script type="text/javascript">

        $(function () {
            $("#dialog-message").dialog({
                modal: true,
                width: 500,
                buttons: {
                    Ok: function () {
                        $(this).dialog("close");
                    }
                }
            });
        });
    </script>

</head>
<body background="../../Content/images/Main-Bgr.png" style="background-size: 100%;overflow : scroll;">
    <form action="PuzzleHome" method="post">
        <p align="right">
            <input type="submit" name="BtnTryAnotherHandle" id="BtnTryAnotherHandle" value ="TRY ANOTHER HANDLE" />
        </p> 
       <div id="main-Container" style="width:1024px;height:680px" >
            
            <div id="TwitterCirclePanel" style="width:500;height:400" >
             
             <% if (Model.MainCircleDIV != null && Model.RetweetedCircleDIV.Count > 0)
                { %>
                     <%= Model.MainCircleDIV%>
                     <% foreach (var ChildCircleInfo in Model.RetweetedCircleDIV)
                        {%>

                             <%= ChildCircleInfo%>
                      <%} %>
                
                <%}
                else
                { %>
                  <div id="dialog-message" title="Twitter Puzzle Error">
                        <p><b> Error Message:- </b> <%= Model.ErrorMessage %></p>
                        <p><b> Error Method:- </b> <%= Model.ErrorMethod %> </p>
                        
                 </div>
                    
                  
                  <% } %>              
            </div>
       </div>
       <%= Html.Hidden("HidTweetCircleCount", Model.TweetCircleCounts, new
          {
              style = "display:none"
          })%>
    </form>
</body>
</html>
