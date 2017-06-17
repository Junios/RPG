using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static DataManager instance;

    public static DataManager Instance
    {
        get
        {
            return instance;
        }
    }

//    public static DataManager Instance()
//    {
//        return instance;
//    }

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
            Load();

            string JSONString = Resources.Load<TextAsset>("Data/PlayerLevelData").text;
            playerData = SimpleJson.SimpleJson.DeserializeObject<PlayerLevelData[]>(JSONString);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Save()
    {
        PlayerPrefs.SetInt("gold", gold);
        PlayerPrefs.SetInt("exp", exp);
        PlayerPrefs.SetInt("level", level);
        PlayerPrefs.Save();

        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.DeleteKey("gold");
//        PlayerPrefsX.
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("gold"))
        {
            gold = PlayerPrefs.GetInt("gold");
        }

        if (PlayerPrefs.HasKey("exp"))
        {
            exp = PlayerPrefs.GetInt("exp");
        }

        if (PlayerPrefs.HasKey("level"))
        {
            level = PlayerPrefs.GetInt("level");
        }
    }

    public PlayerLevelData GetCurrentPlayerData()
    {
        return playerData[level - 1];
    }

    public int maxHP = 100;
    public int currentHP = 100;
    public int exp = 0;
    public int gold = 0;
    public int level = 1;

    public string nextSceneName;

    //public List<PlayerLevelData> playerData = new List<PlayerLevelData>();
    public PlayerLevelData[] playerData;
}
