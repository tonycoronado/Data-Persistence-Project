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
    class BestScoreData
    {
        public string dataPlayerName;
        public int dataBestScore;
    }

    public void SaveBestScoreData()
    {
        BestScoreData data = new BestScoreData();
        data.dataPlayerName = DataManager.instance.playerName;
        data.dataBestScore = MainManager.instance.BestScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadBestScoreData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            BestScoreData data = JsonUtility.FromJson<BestScoreData>(json);
            
            DataManager.instance.playerName = data.dataPlayerName;
            MainManager.instance.BestScore = data.dataBestScore;
        }
    }





}
