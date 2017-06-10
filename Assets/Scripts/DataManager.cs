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
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int maxHP = 100;
    public int currentHP = 100;
    public int exp = 0;
    public int gold = 0;
    public int level = 1;
}
