using ProjectFleetsOfDrones.Models;
using System.Text.Json;

namespace ProjectFleetsOfDrones.Helpers
{
    public static class Helper
    {
        public static string DronesPath = "Drones.txt";
        
        public static string FlightsPath = "Flights.txt";
        public static void Write(string path, string content)
        {
            System.IO.File.AppendAllText(path, content);
        }
        public static string Read(string path)
        {
            return System.IO.File.ReadAllText(path);
        }

        public static string Serialize(Object obj)
        {
            return JsonSerializer.Serialize(obj);
        }

        public static object Serialize(string s)
        {
            return JsonSerializer.Deserialize<object>(s);
        }
    }
}
