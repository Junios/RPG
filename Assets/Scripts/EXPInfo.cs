using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EXPInfo : MonoBehaviour
{
    public Image exp;

    void Update()
    {
        exp.fillAmount = (float)(DataManager.Instance.exp % 30) / 30.0f;
    }
}
