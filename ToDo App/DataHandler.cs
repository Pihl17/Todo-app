using System.Text.Json;
using ToDo_App.Models;
public class DataHandler {
    public List<ToDo> LoadList(string path) {
        List<ToDo> Testlist;
        if(File.Exists(path)){
            var jsondata = File.ReadAllText(path);
            Testlist = JsonSerializer.Deserialize<List<ToDo>>(jsondata) ?? new List<ToDo>();
        } else {
            Testlist = new List<ToDo>();
        }
        return Testlist;
    }

    public void UpdateList(List<ToDo> list, string path) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(list, options);
        File.WriteAllText(path, jsonString);
    }
}