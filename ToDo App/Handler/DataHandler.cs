using System.Text.Json;
using System.IO;
using ToDo_App.Models;
using System.Diagnostics;
public class DataHandler {

    public string Path { get; set; }
    public string UserPreferences { get; set; } = "../../../Files/UserPreferences.json";

    public void GetPath()
    {
        if (File.Exists(UserPreferences))
        {
            var jsondata = File.ReadAllText(UserPreferences);
            Path = JsonSerializer.Deserialize<string>(jsondata);
        } else {
            string setDeafult = "../../../Files/todoes.json";
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(setDeafult, options);
            File.WriteAllText(UserPreferences, jsonString);
            Path = "../../../Files/todoes.json";
        }
    }

    public List<ToDo> LoadList() {
        if(File.Exists(Path)){
            var jsondata = File.ReadAllText(Path);
            return JsonSerializer.Deserialize<List<ToDo>>(jsondata) ?? [];
        } else {
            return [];
        }
    }

    public void UpdateList(List<ToDo> list) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(list, options);
        File.WriteAllText(Path, jsonString);
    }

    public void SelectSavePath(string newpath) {

    }
}