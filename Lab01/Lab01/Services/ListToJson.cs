using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lab01.Services
{
    public class ListToJson
    {
        public static void SaveToFile<T>(string fileName, T data)
        {
            //serialize the data to json format
            try
            {
                string jsonString = JsonSerializer.Serialize(data, new JsonSerializerOptions
                {
                    WriteIndented = true
                });
                //write the json string to a file
                File.WriteAllText(fileName, jsonString);
                Console.WriteLine($"Data succesfully saved to {fileName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured while saving data to {fileName}: {ex.Message}");
            }
        }

        public static List<T> LoadFromFile<T>(string filePath) where T : class
        {
            try
            {
                string jsonString = File.ReadAllText(filePath);
                List<T> dataList = JsonSerializer.Deserialize<List<T>>(jsonString);
                Console.WriteLine($"Data sucessfully loaded from {filePath}");
                return dataList;
            }
            catch (Exception ex) {
                Console.WriteLine($"An error occurred while loading data from {filePath}: {ex.Message}");
                return new List<T>();//return an empty list in case of error
            }
        }
    }
}
