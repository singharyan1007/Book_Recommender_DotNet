﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLoader
{
    public class User
    {
        public int UserId { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country
        { get; set; }

        public List<BookUserRating> Ratings { get; set; } = new List<BookUserRating>();
    }
}
