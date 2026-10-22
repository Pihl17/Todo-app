using System.Diagnostics;
using System.IO;
using System.Text.Json;
using ToDo_App.Library.Models;

namespace ToDo_App.Library.Data;

public class JSONDataHandler : IDataHandler {

    public string Path { get; set; } = "../../../Files/Todoes.json";
    public string UserPreferences { get; set; } = "../../../Files/UserPreferences.json";

    public List<ToDo> ToDos { get; set; } = new List<ToDo>();

    /// <summary>
    /// Get the file path for the useres todo list json file
    /// </summary>
    public void GetPath()
    {
        Debug.WriteLine($"Current directory: {Directory.GetCurrentDirectory()}");
        Directory.CreateDirectory("../../../Files/");
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

    // Check Interface for summary
    public DataQueryResult LoadTodoList(out List<ToDo> todoList) {
        GetPath();
        if (File.Exists(Path)){
            var jsondata = File.ReadAllText(Path);
            try
            {
                ToDos = JsonSerializer.Deserialize<List<ToDo>>(jsondata);
                todoList = ToDos;
                return new DataQueryResult(true);
            } catch (Exception e)
            {
                todoList = [];
                return new DataQueryResult(false, e);
            }
        } else {
            List<ToDo> EmptyList = [];
            string jsonString = JsonSerializer.Serialize(EmptyList);
            File.WriteAllText(Path, jsonString);
            todoList = EmptyList;
            return new DataQueryResult(false, new Exception("No Todo list was found in the given Path: A new empty list has been created."));
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

    // Check Interface for summary
    public DataQueryResult AddTodo(ToDo todo)
    {
        if (ToDos.Contains(todo))
        {
            return new DataQueryResult(false, new Exception("ToDo already exists, cannot add dublicate"));
        }
        ToDos.Add(todo);
        UpdateList(ToDos);
        return new DataQueryResult(true);
    }

    // Check Interface for summary
    public DataQueryResult RemoveTodo(Guid todoId)
    {
        throw new NotImplementedException();
    }

    // Check Interface for summary
    public DataQueryResult UpdateTodo(ToDo updatedTodo)
    {
        throw new NotImplementedException();
    }

    // Check Interface for summary
    public DataQueryResult LoadTodo(Guid todoId, out ToDo todo)
    {
        throw new NotImplementedException();
    }

}