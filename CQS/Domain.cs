using System;
using System.Collections.Generic;
using System.Text;

namespace CQS.Domain
{
    public class Book
    {
        public string Title { get; set; }
        public string Authors { get; set; }
        public DateTime DatePublished { get; set; }
        public bool InMyPossession { get; set; }
    }
}
