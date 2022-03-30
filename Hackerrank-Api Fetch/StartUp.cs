using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using ConsoleTables;
using System.Threading;

namespace Hackerrank_Api_Fetch
{
    public static class StartUp
    {
        public async static Task App(bool page1 = true)
        {
            var page2 = false;
            var currentpage = page1 ? 1 : 2;
            Console.ForegroundColor = ConsoleColor.Yellow;
            var API_URL = page1?  "https://jsonmock.hackerrank.com/api/article_users/search" : "https://jsonmock.hackerrank.com/api/article_users/search?page=2";
            using var client = new HttpClient();
            using var response = await client.GetAsync(API_URL);
            using var content = response.Content;

            var data = await content.ReadAsStringAsync();
            if (data is not null)
            {
                var dataObj = JObject.Parse(data);
                var Page1 = new Page()
                {
                    page = (int)dataObj["page"],
                    Per_Page = (int)dataObj["per_page"],
                    Total = (int)dataObj["total"],
                    Total_Pages = (int)dataObj["total_pages"]
                };
                foreach (var i in dataObj["data"])
                {
                    Page1.Data.Add(new Data()
                    {
                        id = (int)i["id"],
                        UserName = (string)i["username"],
                        About = ((string)i["about"]).TruncateAbout(),
                        Submitted = (int)i["submitted"],
                        UpdatedAt = DateTime.Parse((string)i["updated_at"]).ToShortDateString(),
                        SubmissionCount = (int)i["submission_count"],
                        CommentCount = (int)i["comment_count"],
                        CreatedAt = UnixTimeToDateTime((long)i["created_at"])

                    });
                }

                Console.WriteLine("\n\n");
                Console.WriteLine($"Page {currentpage} Details");
                Console.WriteLine($"Page : {Page1.page}, Per_Page: {Page1.Per_Page}, Total: {Page1.Total}, Total Pages: {Page1.Total_Pages}");
                Console.WriteLine();
                Console.WriteLine($"___Page {currentpage}: Data Table___");

                var table = new ConsoleTable("Author Id", "Username", "About", "Updated At", "CommentCount", "Submitted", "SubmissionCount", "CreatedAt");
                Page1.Data.ForEach(datum =>
                {
                    table.AddRow(datum.id, datum.UserName, datum.About, datum.UpdatedAt, datum.CommentCount, datum.Submitted, datum.SubmissionCount, datum.CreatedAt);
                });
                Console.ForegroundColor = ConsoleColor.Cyan;
                table.Write();

                if (page1)
                {
                    await App(page2);
                }

                Console.ReadKey();
            }
        }
        
        public static string TruncateAbout(this string aboutMsg)
        {
            if (aboutMsg.Length > 50)
            {
                return aboutMsg.Substring(0, 50).Adjust() + "...";
            }

            return aboutMsg;
        }

        public static string Adjust(this string toAd)
        {
            string output = toAd;
            if(output.Contains(" "))
            {
                while (true)
                {
                    if (output[^1] == ' ')
                    {
                        break;
                    }
                    output = output.Remove(output.Length - 1);
                }
            }
            return output;
        }
        public static string UnixTimeToDateTime(long unixtime)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddMilliseconds(unixtime).ToLocalTime();
            return dtDateTime.ToShortDateString();
        }
    }
}
