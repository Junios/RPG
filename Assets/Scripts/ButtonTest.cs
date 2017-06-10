using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTest : MonoBehaviour
{
    public void Click1()
    {
        Debug.Log("Click1");
    }

    public void Click2(int number)
    {
        Debug.Log("Click2 : " + number);
    }
}
