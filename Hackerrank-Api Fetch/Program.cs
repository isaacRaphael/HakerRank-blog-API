using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using ConsoleTables;
using System.Threading;

namespace Hackerrank_Api_Fetch
{
    public class Program
    {
        
        async static Task Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Program Started");
            Thread.Sleep(1000);
            Console.WriteLine("Press F to Fetch Hackerrank API and Print table");
            var resp = Console.ReadLine();

            if (resp.ToLower() == "f")
            {
                try
                {
                    await StartUp.App();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
                
        }
    }
}
