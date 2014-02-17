using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MultunusMvcPuzzle.Models
{
    public class TwitterDIVInfo
    {
        public string MainCircleDIV
        {
            get;
            set;
        }
        public List<string> RetweetedCircleDIV
        {
            get;
            set;
        }

        public int TweetCircleCounts
        {
            get;
            set;
        }

        // Error Related Properties

        public string ErrorMessage { get; set; }
        public string ErrorMethod { get; set; }
        public string ErrorController { get; set; }
        public string ErrorDIVInfo { get; set; }
    }
}