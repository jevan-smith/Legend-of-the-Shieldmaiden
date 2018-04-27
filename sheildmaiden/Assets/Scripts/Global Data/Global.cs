using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;



public class Global : MonoBehaviour {

    public static int KeysCollected = 0;
	public static int ArrowsCollected = 0;

    public void Update()
    {
        GameObject.Find("key_value").GetComponent<Text>().text = KeysCollected.ToString();
		GameObject.Find("arrow_value").GetComponent<Text>().text = ArrowsCollected.ToString();

    }

}
