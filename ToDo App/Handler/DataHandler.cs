using System.Text.Json;
using System.IO;
using ToDo_App.Models;
using System.Diagnostics;
public class DataHandler {

    public string Path { get; set; } = "../../../Files/todoes.json";
    public string UserPreferences { get; set; } = "../../../Files/UserPreferences.json";

    // Get the file path for the useres todo list json file
    public void GetPath()
    {
        // check if file exists
        if (File.Exists(UserPreferences))
        {
            // set path to the content of the the user preference json file
            var jsondata = File.ReadAllText(UserPreferences);
            Path = JsonSerializer.Deserialize<string>(jsondata);
        } else {
            // else create a json file with the deafult file location as a string
            string jsonString = JsonSerializer.Serialize(Path);
            File.WriteAllText(UserPreferences, jsonString);
        }
    }

    public void SelectSavePath(string newpath)
    {
        //change the path for where the useres todo list will be saved
        string jsonString = JsonSerializer.Serialize(newpath);
        File.WriteAllText(UserPreferences, jsonString);
        Path = newpath;
    }

    public List<ToDo> LoadList() {
        // check if file exists
        if (File.Exists(Path)){
            var jsondata = File.ReadAllText(Path);
            //return list of todos based on the content of the json file
            return JsonSerializer.Deserialize<List<ToDo>>(jsondata);
        } else {
            //return a empty list
            return [];
        }
    }

    public void UpdateList(List<ToDo> list) {
        //update the content of the json file
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(list, options);
        File.WriteAllText(Path, jsonString);
    }
}