using System;

namespace MoviesSystem
{
    public class MovieInfo
    {
        public string[] directors { get; set; }
        public string release_date { get; set; }
        public float rating { get; set; }
        public string[] genres { get; set; }
        public string image_url { get; set; }
        public string plot { get; set; }
        public float rank { get; set; }
        public string[] actors { get; set; }
    }

    public class HelperClass
    {
        public ushort year { get; set; }
        public string title { get; set; }
        public bool Registered { get; set; }
        public MovieInfo info { get; set; }
        }

    }