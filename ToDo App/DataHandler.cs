
using System.Text.Json;
using System.IO;
public class DataHandler {

    public int MinPlayers{get; set;}
    public int MaxPlayers{get; set;}

    public DataHandler(int max, int min)
    {
        MaxPlayers = max;
        MinPlayers = min;
    }

    public void CreateJSON() {
        Console.WriteLine("Testing");
        string path = "C:\\Users\\SPAC-B-9\\Desktop\\Projekter\\Todo-app\\Todo Lists\\todo.json";
        if (!File.Exists(path)) {
            Console.WriteLine("Creating");
            File.Create(path);
        }
    }


}