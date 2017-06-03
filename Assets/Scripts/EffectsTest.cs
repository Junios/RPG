using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsTest : MonoBehaviour
{
    void OnEnable()
    {
        foreach(Animator a in GetComponentsInChildren<Animator>())
        {
            a.Rebind();
        }

//        foreach(Animation a in GetComponentsInChildren<Animation>())
//        {
//            a.enabled = false;
//            a.enabled = true;
//        }
    }
}
