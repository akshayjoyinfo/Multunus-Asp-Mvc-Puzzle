using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TweetSharp;
namespace MultunusMvcPuzzle.Models
{
    public class TwitterSessionState
    {

        string profileImageUrl;
        public string ProfileImageUrl
        {
            get { return profileImageUrl; }
            set { profileImageUrl = value; }
        }
        List<TwitterStatus> retweetedStatusInformation;
        public List<TwitterStatus> RetweetedStatusInformation
        {
            get { return retweetedStatusInformation; }
            set { retweetedStatusInformation = value; }
        }

        string tokenURL;

        public string TokenURL
        {
            get { return tokenURL; }
            set { tokenURL = value; }
        }

        DateTime lastAccessTime;

        public DateTime LastAccessTime
        {
            get { return lastAccessTime; }
            set { lastAccessTime = value; }
        }



        public TwitterSessionState(string prfImgUrl, List<TwitterStatus> retweets, string token, DateTime lstAcessTime)
        {
            this.profileImageUrl = prfImgUrl;
            this.retweetedStatusInformation = retweets;
            this.tokenURL = token;
            this.lastAccessTime = lstAcessTime;
        }
    }
}