
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;
using static System.Windows.Forms.Design.AxImporter;
public class DataHandler {

    public string path = "C:\\Users\\SPAC-B-9\\Desktop\\Projekter\\Todo-app\\Todo Lists\\todo.json";

    public List<Class1> LoadList() {
        List<Class1> Testlist;
        if(File.Exists(path)){
            var jsondata = File.ReadAllText(path);
            Testlist = JsonSerializer.Deserialize<List<Class1>>(jsondata) ?? new List<Class1>();
        } else {
            Testlist = new List<Class1>();
        }
        return Testlist;
    }

    public void UpdateList(List<Class1> list) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(list, options);
        File.WriteAllText(path, jsonString);
    }
}