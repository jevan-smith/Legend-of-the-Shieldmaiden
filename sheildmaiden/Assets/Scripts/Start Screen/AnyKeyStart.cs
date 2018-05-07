using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AnyKeyStart : MonoBehaviour {

	[SerializeField] private Text textToUse;
	public Texture2D fadeOutTexture; // black image that will overlay the screen
	public float fadeSpeed = 0.8f;
	private int drawDepth = -1000;
	private float alpha = 1.0f;
	private int fadeDir = -1;

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


	void OnGUI()
	{
		alpha += fadeDir * fadeSpeed * Time.deltaTime;
		alpha = Mathf.Clamp01(alpha);

		GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
		GUI.depth = drawDepth;
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);
	}

	public float BeginFade (int direction)
	{
		fadeDir = direction;
		return (fadeSpeed);
	}

	private void OnLevelWasLoaded()
	{
		BeginFade(-1);
	}
}
