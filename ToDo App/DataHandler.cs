using System.Text.Json;
public class DataHandler {
    public List<Class1> LoadList(string path) {
        List<Class1> Testlist;
        if(File.Exists(path)){
            var jsondata = File.ReadAllText(path);
            Testlist = JsonSerializer.Deserialize<List<Class1>>(jsondata) ?? new List<Class1>();
        } else {
            Testlist = new List<Class1>();
        }
        return Testlist;
    }

    public void UpdateList(List<Class1> list, string path) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(list, options);
        File.WriteAllText(path, jsonString);
    }
}