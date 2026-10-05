using System.Text.Json;
using System.IO;
using ToDo_App.Models;
using System.Diagnostics;
public class DataHandler {

    public string Path { get; set; } = "../../../Files/todoes.json";
    public string UserPreferences { get; set; } = "../../../Files/UserPreferences.json";

    public void GetPath()
    {
        if (File.Exists(UserPreferences))
        {
            var jsondata = File.ReadAllText(UserPreferences);
            Path = JsonSerializer.Deserialize<string>(jsondata);
        } else {
            string jsonString = JsonSerializer.Serialize(Path);
            File.WriteAllText(UserPreferences, jsonString);
        }
    }

    public void SelectSavePath(string newpath)
    {
        string jsonString = JsonSerializer.Serialize(newpath);
        File.WriteAllText(UserPreferences, jsonString);
        GetPath();
    }

    public List<ToDo> LoadList() {
        if(File.Exists(Path)){
            var jsondata = File.ReadAllText(Path);
            return JsonSerializer.Deserialize<List<ToDo>>(jsondata);
        } else {
            return [];
        }
    }

    public void UpdateList(List<ToDo> list) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(list, options);
        File.WriteAllText(Path, jsonString);
    }
}