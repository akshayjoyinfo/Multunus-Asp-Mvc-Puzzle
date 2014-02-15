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
    }
}