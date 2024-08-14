using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arcadeX.Models
{
    public class Game
    {
        public string Title { get; set; }
        public string Thumbnail { get; set; }
        public string Genre { get; set; }
        public string Platform { get; set; }
        public string Publisher { get; set; }
        public string Release_Date { get; set; }
    }
}