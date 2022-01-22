using UnityEngine;
using System.IO;

//ѕередаЄм обьекты между сценами не удал€€ их

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    public new string name;
    public int score;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    [System.Serializable]
    public class SaveData
    {
        public string Name;
        public int Score;
    }

    public void SaveRecord()
    {
        
        SaveData data = new SaveData();
        data.Name = name;
        data.Score = score;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadRecord()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            name = data.Name;
            score = data.Score;
        }
    }
}

