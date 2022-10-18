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
            string text = Read(path);
            if(text.Length!=0)
            {
                List<T> list = Deserialize<T>(text);
                foreach (T item in list)
                    newlist.Add(item);
            }
            string newText = Serialize(newlist);
            System.IO.File.WriteAllText(path, newText);

        }
        public static string Read(string path)
        {
            return System.IO.File.ReadAllText(path);
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
