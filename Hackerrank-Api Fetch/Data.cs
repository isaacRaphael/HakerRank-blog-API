using System;


namespace Hackerrank_Api_Fetch
{
    public class Data
    {
        public int id { get; set; }
        public string UserName { get; set; }
        public string About { get; set; }
        public int Submitted { get; set; }
        public string UpdatedAt { get; set; }
        public int SubmissionCount { get; set; }
        public int CommentCount { get; set; }
        public string CreatedAt { get; set; }


        public override string ToString()
        {
            return $"{id}, {UserName}";
        }
    }
}
