using System.Diagnostics;
using System.IO;
using System.Text.Json;
using ToDo_App.Models;
public class DataHandler {

    public string Path { get; set; } = "../../../Files/Todoes.json";
    public string UserPreferences { get; set; } = "../../../Files/UserPreferences.json";

    /// <summary>
    /// Get the file path for the useres todo list json file
    /// </summary>
    public void GetPath()
    {
        if (File.Exists(UserPreferences))
        {
            var jsondata = File.ReadAllText(UserPreferences);
            try
            {
                Path = JsonSerializer.Deserialize<string>(jsondata);
            } catch
            {
                
            }
        } else {
            string jsonString = JsonSerializer.Serialize(Path);
            File.WriteAllText(UserPreferences, jsonString);
        }
    }

    /// <summary>
    /// updades the path for where the useres todolist gets saved and move it to the new location
    /// </summary>
    /// <param name="newpath">string</param>
    public void SelectSavePath(string newpath)
    {
        string jsonString = JsonSerializer.Serialize(newpath);
        File.WriteAllText(UserPreferences, jsonString);
        File.Move(Path, newpath);
        Path = newpath;
    }

    /// <summary>
    /// Returns a list of todos
    /// </summary>
    /// <returns>
    /// List<ToDo>
    /// </returns>
    public List<ToDo> LoadList() {
        Directory.CreateDirectory("../../../Files/");
        GetPath();
        if (File.Exists(Path)){
            var jsondata = File.ReadAllText(Path);
            try
            {
                return JsonSerializer.Deserialize<List<ToDo>>(jsondata);
            } catch
            {
                return [];
            }
        } else {
            List<ToDo> EmptyList = [];
            string jsonString = JsonSerializer.Serialize(EmptyList);
            File.WriteAllText(Path, jsonString);
            return EmptyList;
        }
    }

    /// <summary>
    /// update the content of the json file, replacing the old list with a new list
    /// </summary>
    /// <param name="list">List<ToDo></param>
    public void UpdateList(List<ToDo> list) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(list, options);
        File.WriteAllText(Path, jsonString);
    }
}