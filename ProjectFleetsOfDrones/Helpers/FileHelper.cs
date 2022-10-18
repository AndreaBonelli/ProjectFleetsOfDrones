using ProjectFleetsOfDrones.Models;
using System.Collections.Generic;
using System.Text.Json;

namespace ProjectFleetsOfDrones.Helpers
{
    public static class FileHelper
    {
        public static string DronesPath = "Drones.txt";
        
        public static string FlightsPath = "Flights.txt";
        public static void Write<T>(string path, List<T> newlist)
        {
            string newText = Serialize(newlist);
            File.WriteAllText(path, newText);
        }


        public static IEnumerable<T> ReadAndDeserialize<T>(string path)
        {
            string json = File.ReadAllText(path);
            if(json == string.Empty)
                return new List<T>();
            IEnumerable<T> resultList = JsonSerializer.Deserialize<IEnumerable<T>>(json);
            return resultList;
        }




        public static string Read(string path)
        {
            return File.ReadAllText(path);
        }

        public static string Serialize<T>(T list)
        {
            return JsonSerializer.Serialize(list);
        }

        public static List<T> Deserialize<T>(string s)
        {
            return JsonSerializer.Deserialize<List<T>>(s);
        }
    }
}
