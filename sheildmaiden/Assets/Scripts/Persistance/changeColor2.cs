using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeColor2 : MonoBehaviour
{

    public Text theText;
    public static bool showType2 = false;

    // Update is called once per frame
    void Update()
    {

        if (showType2 == true)
        {
            theText.color = Color.yellow;
        }
        if (showType2 == false)
        {
            theText.color = Color.white;
        }

    }
}