using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AnyKeyStart : MonoBehaviour {

	[SerializeField] private Text textToUse;
	// Use this for initialization
	void Start () {
		textToUse = GetComponent<Text>();

	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.anyKeyDown)
		{
			textToUse.text = "Loading...";
			SceneManager.LoadScene("NeverUnload", 0);
		}
	}
}
