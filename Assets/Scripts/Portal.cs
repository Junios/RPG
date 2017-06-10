﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string nextSceneName;

    public float stayTime = 0;

    void OnTriggerStay(Collider other)
    {
        stayTime += Time.deltaTime;

        if (stayTime >= 2.0f)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
