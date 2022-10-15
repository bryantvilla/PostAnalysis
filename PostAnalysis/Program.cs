using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace PostAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {

            GetHtmlAsync();
            Console.ReadLine();

        }
        private static async void GetHtmlAsync()
        {
            var url = "https://www.glassdoor.com/Job/washington-software-engineer-jobs-SRCH_IL.0,10_IC1138213_KO11,28.htm";

            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var JobsHtml = htmlDocument.DocumentNode.Descendants("ul")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("hover p-0  css-7ry9k1 exy0tjh5")).ToList();

            var ListJobListings = JobsHtml[0].Descendants("li")
                .Where(node => node.GetAttributeValue("class","")
                .Contains("react-job-listing")).ToList();

            foreach(var JobListListing in ListJobListings)
            {
                Console.WriteLine(JobListListing.GetAttributeValue("class", ""));

                Console.WriteLine(JobListListing.Descendants("div")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Equals("d-flex justify-content-between align-items-start")).FirstOrDefault().InnerText
                    );

                Console.WriteLine();

            }




            Console.WriteLine();
        }
    }
}