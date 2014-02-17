using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TweetSharp;
using MultunusMvcPuzzle.Models;
namespace MultunusMvcPuzzle.Controllers
{
    public class PuzzleController : Controller
    {
        //
        // GET: /Puzzle/

        public static int NUMBER_OF_RETWEETS = 10; // If the Count is 10, then TweetSharp will retrive the top 9 retweets through the API
        public static string CHILD_CIRLCE_IMGID = "img";
        public static string CHILD_CIRLCE_DIVID = "childcircle";
        public static string TWITTER_TOKEN_SEPERATOR = "@#$%^&*";
        public static string CHILD_DIV_CIRCLE_STYLE = "width: 75px; height:75px; position:absolute; border-radius:50%; background-size: cover; display: block;";
        
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PuzzleHome()
        {
            Response.Redirect("~/");
            return View();
        }


        public ActionResult PuzzleResult()
        {
            string twitterconsumerKey = "hURSVYMlVMPMYKKOgiBitQ";
            string twxitterCosnumerSecret = "TFaCy4fUUSbparCOF8o1jpFRwzXoQ49Zt9xeaUyBAMQ";
            string twitterAccessToken = "1872531062-9YB2m6RP5AnvwOjrklec6yBzVGNRLabHs5Gq8yc";
            string twitterAccessTokenSecret = "KI4NSeY9gwNlCX49AiyyOTqZxHfM1zpXmZFsnJQVIFUwg";
            string TwitterTokenKey = string.Empty;
            bool handleExist = false;
            TwitterDIVInfo TwitterDIVInfoObject = new TwitterDIVInfo();
            TwitterDIVInfoObject.ErrorController = "PuzzleController";
            TwitterDIVInfoObject.ErrorMethod = "PuzzleResult";

            try
            {
                if (Request.Form["txtTwitterHandle"] != null)
                {
                    string url = Request.Form["txtTwitterHandle"].ToString();
                    string[] URLContents = url.Split('/');

                    if (URLContents.Length >= 6)
                    {
                        Int64 status = Convert.ToInt64(URLContents[URLContents.Length - 1]);
                        string tweetedUser = URLContents[URLContents.Length - 3];

                        TwitterTokenKey = tweetedUser + TWITTER_TOKEN_SEPERATOR + status;

                        handleExist = TwitterSharedLib.IsTwitterHandleExist(TwitterTokenKey);

                        if (handleExist == false)
                        {

                            var service = new TwitterService(twitterconsumerKey, twxitterCosnumerSecret);
                            service.AuthenticateWith(twitterAccessToken, twitterAccessTokenSecret);

                            RetweetsOptions tweetOptions = new RetweetsOptions();
                            tweetOptions.Id = status;
                            tweetOptions.Count = NUMBER_OF_RETWEETS;

                            //Twitteru
                            GetTweetOptions tweetUser = new GetTweetOptions();
                            //tweetUser.IncludeEntities=true;
                            tweetUser.Id = status;

                            TwitterStatus tweetOwner = service.GetTweet(tweetUser);
                            string MainCircleURL = tweetOwner.Author.ProfileImageUrl.Replace("_normal", "");

                            List<TwitterStatus> tweets = (List<TwitterStatus>)service.Retweets(tweetOptions);

                            if (tweets != null)
                            {
                                int tweetCount = 9;

                                if (tweets.Count >= NUMBER_OF_RETWEETS)
                                {
                                    tweetCount = 10;
                                }
                                else
                                {
                                    tweetCount = tweets.Count;
                                }


                                List<TwitterStatus> Sortedtweets = (List<TwitterStatus>)tweets.OrderByDescending(tw => tw.User.FollowersCount).ToList().GetRange(0, tweetCount);

                                TwitterDIVInfoObject = SetTweetImages(MainCircleURL, Sortedtweets);

                                bool success = TwitterSharedLib.StoreTwitterHandle(MainCircleURL, Sortedtweets, TwitterTokenKey, DateTime.Now);

                            }
                            else
                            {

                                
                                TwitterDIVInfoObject.ErrorMessage = "Failled to get Tweitter Handle, Please try again later";
                              

                            }
                        }
                        else
                        {
                            TwitterSessionState twitterSession = TwitterSharedLib.GetTwitterHandle(TwitterTokenKey);
                            if (twitterSession != null)
                            {
                                TwitterDIVInfoObject = SetTweetImages(twitterSession.ProfileImageUrl, twitterSession.RetweetedStatusInformation);
                            }
                            else
                            {
                                Request.Form["txtTwitterHandle"] = null;
                                TwitterDIVInfoObject.ErrorMessage = "Unknown error occurred setting Twitter Images ";
                                


                            }
                        }
                    }
                    else
                    {

                        TwitterDIVInfoObject.ErrorMessage = "Please check the Tweet URL, Tweet id is missing";
                        
                    }

                }
                else
                {

                    TwitterDIVInfoObject.ErrorMessage = "URL is Empty , Please try again later";

                }
            }
            catch (Exception exp)
            {
                TwitterDIVInfoObject.ErrorMessage = "An Unknown error occured :- " + exp.Message;
                
            }
            return View(TwitterDIVInfoObject);
        }

        private TwitterDIVInfo SetTweetImages(string profileImageURL, List<TwitterStatus> listRetweetUserImageURL)
        {
            TwitterDIVInfo twitterDIVDetails = new TwitterDIVInfo();

            try
            {
                
                string mainCircleinnerHtml = "<div class=\"main-circle\"><img id=\"MainCircle\" src=\" " + profileImageURL + "\"/> </div>";
                List<string> retweetUserImageUrl = new List<string>();
                int startCount = 1;

                twitterDIVDetails.MainCircleDIV = mainCircleinnerHtml;
                twitterDIVDetails.TweetCircleCounts = listRetweetUserImageURL.Count;

                foreach (TwitterStatus twt in listRetweetUserImageURL)
                {
                    string childCircleImg = "<img id=\"img" + startCount + "\" src=\"" + twt.User.ProfileImageUrl.Replace("_normal", "") + "\" />";

                    string divChildCircleInf = "<div class=\"child-item tooltip\" id=\""+CHILD_CIRLCE_DIVID + startCount+"\" style=\""+CHILD_DIV_CIRCLE_STYLE+ "\" title=\""+ startCount.ToString()+"\">" + childCircleImg + "</div>";
                    retweetUserImageUrl.Add(divChildCircleInf);
                    startCount++;
                }
                twitterDIVDetails.RetweetedCircleDIV = retweetUserImageUrl;

                
                
            }
            catch (Exception exp)
            {
                twitterDIVDetails = null;
            }
            return twitterDIVDetails;

        }
    }
}
