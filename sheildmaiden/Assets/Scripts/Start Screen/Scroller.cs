using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class Scroller : MonoBehaviour {
	[SerializeField] private float speed = 0.1f;
	[SerializeField] private Text words;
	private Camera cam;
	private bool hasBeenDown = false;
	private float waitTime = 4f;
	private float waitTime2 = 8f;
	private int state;
	// Use this for initialization
	void Start () {
		cam =  Camera.main;
	}

	// Update is called once per frame
	void Update () {
		transform.Translate (new Vector3 (0, speed, 0) * Time.deltaTime);
		if (Input.anyKeyDown) {
			hasBeenDown = true;
			//start fade
			state = 9;
			words.text = "Loading in " + String.Format("{0:F2}", waitTime) + " seconds.";
		}
		if(hasBeenDown){
			waitTime -= Time.deltaTime;
			words.text = "Loading in " + String.Format("{0:F2}", waitTime) + " seconds.";
			if(waitTime <= 0){
				words.text = "Loading...";
				SceneManager.LoadScene("Start Screen", 0);
			}
		}
		if (waitTime2 <= 0) {
			state++;
			waitTime2 = 4f;
		} else {
			waitTime2 -= Time.deltaTime;
		}
		switch (state) {
		case 1:
			words.text = "aAAAAAAAAAAAAAAAAAAAA";
			break;	
		case 2:
			words.text = "aAAAAAAAAAAAAAAAAAAAA";
			break;
		case 3:
			words.text = "aAAAAAAAAAAAAAAAAAAAA";
			break;
		case 4:
			words.text = "aAAAAAAAAAAAAAAAAAAAA";
			break;
		case 5:
			words.text = "aAAAAAAAAAAAAAAAAAAAA";
			break;
		case 6:
			words.text = "aAAAAAAAAAAAAAAAAAAAA";
			break;
		case 7:
			words.text = "aAAAAAAAAAAAAAAAAAAAA";
			break;
		case 8:
			SceneManager.LoadScene("NeverUnload", 0);
			break;
		default:
			break;
		}
	}
}
