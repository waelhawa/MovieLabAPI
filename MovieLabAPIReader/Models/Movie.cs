using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieLabAPIReader.Models
{

    //public class Movie
    //{
    //    public Class1[] Property1 { get; set; }
    //}

    public class Movie
    {
        public int id { get; set; }
        public string title { get; set; }
        public string genre { get; set; }
        public object runtime { get; set; }
    }

}
