using Npgsql;
using Npgsql.NameTranslation;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using ToDo_App.Library.Models;
public class DataHandler {

    public string Path { get; set; } = "../../../Files/Todoes.json";
    public string UserPreferences { get; set; } = "../../../Files/UserPreferences.json";

    /// <summary>
    /// Get the file path for the useres todo list json file
    /// </summary>
    public void GetPath()
    {
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

    /// <summary>
    /// Returns a list of todos
    /// </summary>
    /// <returns>
    /// List<ToDo>
    /// </returns>
    public List<ToDo> LoadList() {

        string connectionString = "Host=localhost;Username=postgres;Password=password_here;Database=postgres";
        NpgsqlDataSourceBuilder dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
        dataSourceBuilder.MapEnum<ToDoStatus>();
        dataSourceBuilder.MapEnum<RepeatInterval>();
        using NpgsqlDataSource dataSource = dataSourceBuilder.Build();

        using NpgsqlCommand command = dataSource.CreateCommand("SELECT * FROM todos");
        using NpgsqlDataReader reader = command.ExecuteReader();

        List<ToDo> todoList = new List<ToDo>();
        ToDoHandler toDoHandler = new ToDoHandler();

        while (reader.Read())
        {
            todoList.Add(toDoHandler.Create(reader.GetString(1),
                desc: reader.GetString(2),
                priority: reader.GetInt32(4),
                repeat: reader.GetFieldValue<RepeatInterval>(8),
                choosenDeadLine: reader.IsDBNull(7) ? null : reader.GetDateTime(7)
            ));
        }

        return todoList;
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