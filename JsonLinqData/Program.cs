using System;
using System.Linq;
using System.Text.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace LinqJsonData
{
    class Geometry
    {
        public string type { get; set; }
        public float[] coordinates { get; set; }
    }
    class Properties
    {
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string borough { get; set; }
        public string neighborhood { get; set; }
        public string country { get; set; }
    }
    class Features
    {
        public string type { get; set; }
        public Geometry geometry { get; set; }
        public Properties properties { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            displayNighborhoods();
            filterNighborhoods();
            removeDupNighborhoods();
        }
        public static void displayNighborhoods()
        {
            string fileName = "data.json";
            string jsonString = File.ReadAllText(fileName);

            JObject googleSearch = JObject.Parse(jsonString);
            int n = 0;
            IList<JToken> results = googleSearch["features"].Children().ToList();

            //Output all of the neighborhoods in this data list (Final Total: 147 neighborhoods)
            IList<Features> searchResults = new List<Features>();
            foreach (JToken result in results)
            {
                Features searchResult = result.ToObject<Features>();
                searchResults.Add(searchResult);
                Console.WriteLine(++n + ": " + searchResult.properties.neighborhood);
            }
            Console.WriteLine("=========================");
        }
        public static void filterNighborhoods()
        {
            string fileName = "data.json";
            string jsonString = File.ReadAllText(fileName);
            JObject googleSearch = JObject.Parse(jsonString);
            int n = 0;
            IList<JToken> results = googleSearch["features"].Children().ToList();

            //Output all of the neighborhoods in this data list (Final Total: 143 neighborhoods)
            IList<Features> searchResults = new List<Features>();
            foreach (JToken result in results)
            {
                Features searchResult = result.ToObject<Features>();
                searchResults.Add(searchResult);

                if (searchResult.properties.neighborhood != "")
                {
                    Console.WriteLine(++n + ": " + searchResult.properties.neighborhood + " has been Filtered");
                }
            }
            Console.WriteLine("======================");
        }
        public static void removeDupNighborhoods()
        {
            string fileName = "data.json";
            string jsonString = File.ReadAllText(fileName);
            JObject googleSearch = JObject.Parse(jsonString);

            IList<JToken> results = googleSearch["features"].Children().ToList();

            IList<Features> searchResults = new List<Features>();
            List<string?> hoodResult = new List<string?>();

            foreach (JToken result in results)
            {
                Features searchResult = result.ToObject<Features>();
                hoodResult.Add(searchResult.properties.neighborhood);
            }
            IEnumerable<string> neighborhoods = hoodResult.Distinct();

            int n = 0;


            foreach (var neighborhood in neighborhoods)
            {
                if (neighborhood == "")
                {
                    continue;
                }
                n++;
                Console.WriteLine($"{n}: {neighborhood}");
            }
        }
    }

}
