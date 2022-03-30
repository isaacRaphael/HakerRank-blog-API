using System.Collections.Generic;


namespace Hackerrank_Api_Fetch
{
    public class Page
    {
        public int page { get; set; }
        public int Per_Page { get; set; }
        public int Total { get; set; }
        public int Total_Pages { get; set; }
        public List<Data> Data { get; set; } = new();

        public override string ToString()
        {
            return $"{page}, {Per_Page}, {Total}, {Total_Pages}, {string.Join("\n",Data)}";
        }

    }
}
