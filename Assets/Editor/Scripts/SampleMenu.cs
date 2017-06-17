using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SampleMenu
{
    [MenuItem("Window/NewWindow #&%g")]
    public static void TestMenu()
    {
        Debug.Log("새 윈도우 열기");
    }
}
