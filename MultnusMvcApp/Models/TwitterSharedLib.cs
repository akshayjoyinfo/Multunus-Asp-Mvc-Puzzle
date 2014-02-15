using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Concurrent;
using System.Timers;
using TweetSharp;
namespace MultunusMvcPuzzle.Models
{
    public static class TwitterSharedLib
    {
        static ConcurrentDictionary<string, TwitterSessionState> sessionDictionary = new ConcurrentDictionary<string, TwitterSessionState>();
        public static Timer Time;

        /// <summary>
        /// Default Value in seconds, After this many seconds Application will Store the Retweet Information.
        /// </summary>
        public static int TIMER_ELAPSED_VALUE = 300;

        /// <summary TWITTER_RATE_LIMIT_COMPENSATOR>
        /// Default value in Minutes, It will kep all the TweetHandle till this time, It will remove the URL from the Dictionary and go for Fresh Tweet Handle
        /// </summary TWITTER_RATE_LIMIT_COMPENSATOR>
        public static int TWITTER_RATE_LIMIT_COMPENSATOR = 10;

        static TwitterSharedLib()
        {

            Time = new Timer(TIMER_ELAPSED_VALUE * 1000);
            Time.AutoReset = true;
            Time.Elapsed += new ElapsedEventHandler(Time_Elapsed);
            Time.Enabled = true;
        }

        public static bool StoreTwitterHandle(string prfImgUrl, List<TwitterStatus> retweets, string token, DateTime lstAcessTime)
        {
            bool success = false;
            try
            {
                TwitterSessionState sessionState = new TwitterSessionState(prfImgUrl, retweets, token, lstAcessTime);
                success = sessionDictionary.TryAdd(token, sessionState);
            }
            catch (Exception exp)
            {
                throw new Exception("Unale to Store the Twitter Handle in the Memory");
            }
            return success;
        }

        public static bool IsTwitterHandleExist(string token)
        {
            bool isHandleExist = false;
            try
            {
                if (sessionDictionary != null && sessionDictionary.Count > 0)
                {
                    var value = from sessionValues in sessionDictionary.Values.Where(p => p.TokenURL == token) select sessionValues.TokenURL;
                    if (value.Count() > 0)
                        isHandleExist = true;
                    else
                        isHandleExist = false;
                }
            }
            catch (Exception exp)
            {
                isHandleExist = false;
                throw new Exception("Unale to Get the Twitter Handle");
            }
            return isHandleExist;
        }

        public static TwitterSessionState GetTwitterHandle(string token)
        {
            TwitterSessionState twitterHandle = null;
            try
            {
                if (sessionDictionary != null)
                {
                    var value = from sessionValues in sessionDictionary.Values.Where(p => p.TokenURL == token) select sessionValues;
                    twitterHandle = value.FirstOrDefault();
                }
            }
            catch (Exception exp)
            {
                twitterHandle = null;
                throw new Exception("Unale to Get the Twitter Handle");
            }
            return twitterHandle;


        }

        public static void Time_Elapsed(object sender, ElapsedEventArgs e)
        {
            List<string> tokenKeys = new List<string>();

            try
            {
                if (sessionDictionary != null)
                {
                    foreach (var pair in sessionDictionary)
                    {
                        int minutediff = DateTime.Now.Subtract(pair.Value.LastAccessTime).Minutes;
                        if (minutediff >= TWITTER_RATE_LIMIT_COMPENSATOR)
                        {
                            tokenKeys.Add(pair.Key);
                        }
                    }

                    foreach (string key in tokenKeys)
                    {

                        TwitterSessionState s;
                        bool blnRemoved = sessionDictionary.TryRemove(key, out s);
                        if (!blnRemoved)
                        {
                            throw new Exception("Unable to Remove the Token from the Session Dictionary");
                        }
                    }
                }

            }
            catch (Exception exp)
            {

            }
        }
    }
}