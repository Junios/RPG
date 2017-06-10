using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    public Image HPGauage;
    public Text level;
    public Text gold;
    public Text HP;

	void Update()
    {
        HPGauage.fillAmount = (float)DataManager.Instance.currentHP /
            (float)DataManager.Instance.maxHP;
        HP.text = DataManager.Instance.currentHP.ToString() +
            "/" + DataManager.Instance.maxHP.ToString();
        level.text = DataManager.Instance.level.ToString();
        gold.text = DataManager.Instance.gold.ToString();
	}
}
