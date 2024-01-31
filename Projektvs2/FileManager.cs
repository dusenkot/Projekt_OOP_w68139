using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

class FileManager
{
    public void SaveToFile<T>(string filePath, List<T> items)
    {
        try
        {
            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath);
            var json = JsonConvert.SerializeObject(items, Formatting.Indented);
            File.WriteAllText(fullPath, json);
            Console.WriteLine($"Data saved to {fullPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving data: {ex.Message}");
        }
    }

    public List<T> LoadFromFile<T>(string filePath)
    {
        try
        {
            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath);
            if (File.Exists(fullPath))
            {
                var json = File.ReadAllText(fullPath);
                Console.WriteLine($"Data loaded from {fullPath}");
                return JsonConvert.DeserializeObject<List<T>>(json);
            }
            Console.WriteLine($"File {fullPath} not found");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading data from {filePath}: {ex.Message}");
        }

        return new List<T>();
    }
}
