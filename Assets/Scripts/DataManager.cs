using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public TMP_InputField playerNameInput;
    public string playerName;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); //Singleton

    }

    public void ReadPlayerName(string p_playerName)
    {
        UpdateplayerName(p_playerName);
    }

    public void UpdateplayerName(string p_playerName)
    {
        playerName = p_playerName;
        Debug.Log(playerName);
    }

    [System.Serializable]
    class SaveData
    {
        public string dataPlayerName;
    }

    public void SaveName()
    {
        SaveData data = new SaveData();
        data.dataPlayerName = DataManager.instance.playerName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadColor()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            
            DataManager.instance.playerName = data.dataPlayerName;
        }
    }





}
