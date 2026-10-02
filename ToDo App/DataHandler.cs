
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;
using static System.Windows.Forms.Design.AxImporter;
public class DataHandler {

    public List<Class1> list;
    public string path = "C:\\Users\\SPAC-B-9\\Desktop\\Projekter\\Todo-app\\Todo Lists\\todo.json";

    public List<Class1> LoadList() {
        List<Class1> Testlist;
        if(File.Exists(path)){
            var jsondata = File.ReadAllText(path);
            Testlist = JsonSerializer.Deserialize<List<Class1>>(jsondata) ?? new List<Class1>();
            list = Testlist;
        } else {
            Testlist = new List<Class1>();
            list = Testlist;
        }
        return Testlist;
    }

    public void AddToList() {
        Random r = new Random();
        int rInt = r.Next(0, 100);
        var testobj = new Class1()
        {
            created = DateTime.Now,
            value = rInt
        };
        list.Add(testobj);
    }


    public void UpdateList() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(list, options);
        File.WriteAllText(path, jsonString);
    }
}