
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;

public class DataHandler {

    public void CreateJSON() {
        Console.WriteLine("Testing");
        string path = "C:\\Users\\SPAC-B-9\\Desktop\\Projekter\\Todo-app\\Todo Lists\\todo.json";
        if (!File.Exists(path)) {
            Console.WriteLine("Creating");
            File.Create(path);
        }
    }

    public void UpdateList() {
        string path = "C:\\Users\\SPAC-B-9\\Desktop\\Projekter\\Todo-app\\Todo Lists\\todo.json";
        Random r = new Random();
        int rInt = r.Next(17, 100);
        var testobj = new Class1()
        {
            created = DateTime.Parse("2019-08-01"),
            value = rInt
        };
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(testobj, options);
        File.WriteAllText(path, jsonString);
    }


}