using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRanking.Models
{
    public class WebItem
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public int Rank { get; set; }
        public bool ContainUrl { get; set; }
    }
}
