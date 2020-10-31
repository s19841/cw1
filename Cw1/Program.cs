using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cw1
{
     public class Program
    {
       public static async Task Main(string[] args)
       {
           //Wersja dla galezi dodatkowe wymagania
           if (args.Length == 0) throw new ArgumentException("Brak adresu URL");

           var urlRegex = new Regex("^(https?://)?[0 9a-zA-Z].[-_0-9a-zA-Z].[0-9a-zA-Z]+$");

            string url = args.Length > 0 ? args[0] : "https://www.pja.edu.pl";
           if (urlRegex.Matches(url).Count == 0) throw new ArgumentException();

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            var zbior = new HashSet<string>();

            if (response.IsSuccessStatusCode)
            {
                var html = await response.Content.ReadAsStringAsync();
                var regex = new Regex("[a-z]+[a-z0-9]*@[a-z.]+");

                MatchCollection matches = regex.Matches(html);
                if (matches.Count == 0)
                {
                    Console.WriteLine("Nie znaleziono adresów email");
                }
                else
                {
                    foreach (var i in matches)
                    {
                        zbior.Add(i.ToString());
                        
                    }

                    var zbior2 = zbior.Distinct();
                    foreach (var i in zbior2)
                    {
                        Console.WriteLine(i);
                    }
                }

            }
            else
            {
                Console.WriteLine("Błąd wczasie pobierania strony");
                return;
            }
            httpClient.Dispose();
        }
    }
}
