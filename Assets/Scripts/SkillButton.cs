using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    public Image standBy;
    public Text coolTimeText;

    public float coolTime = 3.0f;

    void Awake()
    {
        coolTimeText.gameObject.SetActive(false);
        standBy.fillAmount = 0;
    }

    public void UseSkill()
    {
        if (standBy.fillAmount == 0)
        {
            standBy.fillAmount = 1.0f;
            GameObject.FindGameObjectWithTag("Player").
                GetComponent<PlayerFSM>().SetState(CharacterState.Skill1);
            coolTimeText.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if (standBy.fillAmount != 0)
        {
            standBy.fillAmount -= Time.deltaTime / coolTime;

            float temp = standBy.fillAmount * coolTime;

            if (temp > 1f)
            {
                coolTimeText.text = string.Format("{0:0}", temp);
            }
            else
            {
                coolTimeText.text = string.Format("{0:0.0}", temp);
            }
        }
        else
        {
            coolTimeText.gameObject.SetActive(false);
        }
    }
}
