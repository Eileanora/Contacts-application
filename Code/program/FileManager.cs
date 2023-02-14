using Newtonsoft.Json;

namespace Final_project;
public class FileManager
{
    public string FilePath { get; set; }

    public FileManager(string filePath)
    {
        FilePath = filePath;
    }

    public void LoadFriends(ref List<Friend> friends)
    {
        if (File.Exists(FilePath))
        {
            //If file exists, but empty, save empty settings to it
            if (new FileInfo(FilePath).Length == 0)
            {
                SaveSettings();
            }
            else
            {
                //Read json from file
                using (StreamReader r = new StreamReader(FilePath))
                {
                    string json = r.ReadToEnd();
                    //Convert json to list
                   friends = JsonConvert.DeserializeObject<List<Friend>>(json);
                }
            }
        }
        else
        {
            //Create file
            File.Create(FilePath).Close();

            //Wait for filesystem to create file
            while (!File.Exists(FilePath))
            {
                System.Threading.Thread.Sleep(100);
            }

            //Save empty settings to file
            SaveSettings();
        }
    }

    public void SaveSettings()
    {
        string json = JsonConvert.SerializeObject(Program.friends);

        File.AppendAllText(FilePath, json);
    }

    //Can save or update passed coin
    public void SaveFriend()
    {
        string json = JsonConvert.SerializeObject(Program.friends);
        File.WriteAllText(FilePath, json);
    }
    
}