using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLoader
{
    public class BookUserRating
    {
        public int Rating { get; set; }
        public string ISBN { get; set; }
        public int UserID { get; set; }

        public Book Book { get; set; }
        public User User { get; set; }
    }
}
