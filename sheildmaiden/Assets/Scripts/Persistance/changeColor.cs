using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeColor : MonoBehaviour
{

    public Text theText;
    public static bool showType = true;

    // Update is called once per frame
    void Update()
    {

        if (showType == true)
        {
            theText.color = Color.yellow;
        }
        if (showType == false)
        {
            theText.color = Color.white;
        }

    }
}