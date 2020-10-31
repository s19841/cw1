﻿using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cw1
{
     public class Program
    {
       public static async Task Main(string[] args)
       {
           string url = args.Length > 0 ? args[0] : "https://www.pja.edu.pl";
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var html = await response.Content.ReadAsStringAsync();
                var regex = new Regex("[a-z]+[a-z0-9]*@[a-z.]+");

                MatchCollection matches = regex.Matches(html);
                foreach (var i in matches)
                {
                    Console.WriteLine(i);
                }
                
            }
        }
    }
}
