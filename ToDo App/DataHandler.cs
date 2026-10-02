using System.Text.Json;
using ToDo_App.Models;
public class DataHandler {
    public List<ToDo> LoadList(string path) {
        if(File.Exists(path)){
            var jsondata = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<ToDo>>(jsondata) ?? [];
        } else {
            return [];
        }
    }

    public void UpdateList(List<ToDo> list, string path) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(list, options);
        File.WriteAllText(path, jsonString);
    }
}