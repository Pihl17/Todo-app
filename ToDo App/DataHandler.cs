
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;
using static System.Windows.Forms.Design.AxImporter;
public class DataHandler {

    public void CreateJSON() {
        Console.WriteLine("Testing");
        string path = "C:\\Users\\SPAC-B-9\\Desktop\\Projekter\\Todo-app\\Todo Lists\\todo.json";
        if (!File.Exists(path)) {
            List<string> emptyList = new List<string>();
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(emptyList, options);
            File.WriteAllText(path, jsonString);
        }
    }

    public void UpdateList() {
        string path = "C:\\Users\\SPAC-B-9\\Desktop\\Projekter\\Todo-app\\Todo Lists\\todo.json";
        var jsondata = File.ReadAllText(path);
        List<Class1> Testlist = JsonSerializer.Deserialize<List<Class1>>(jsondata) ?? new List<Class1>();
        Random r = new Random();
        int rInt = r.Next(17, 100);
        var testobj = new Class1()
        {
            created = DateTime.Now,
            value = rInt
        };
        Testlist.Add(testobj);
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(Testlist, options);
        File.WriteAllText(path, jsonString);
    }


}