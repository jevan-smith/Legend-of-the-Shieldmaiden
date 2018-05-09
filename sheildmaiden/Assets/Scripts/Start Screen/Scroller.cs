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
	private float waitTime2 = 0f;
	private int state;
	// Use this for initialization
	void Start () {
		cam =  Camera.main;
	}

	// Update is called once per frame
	void Update () {
		transform.Translate (new Vector3 (0, speed, 0) * Time.deltaTime);
		if (Input.GetKeyDown(KeyCode.Space)) {
			hasBeenDown = true;
			//start fade
			state = 99;
			words.text = "Loading in " + String.Format("{0:F2}", waitTime) + " seconds.";
		}
		if(hasBeenDown){
			waitTime -= Time.deltaTime;
			words.text = "Loading in " + String.Format("{0:F2}", waitTime) + " seconds.";
			if(waitTime <= 0){
				words.text = "Loading...";
				SceneManager.LoadScene("NeverUnload", 0);
			}
		}
		if (waitTime2 <= 0) {
			state++;
			waitTime2 = 10f;
		} else {
			waitTime2 -= Time.deltaTime;
		}

		switch (state) {
		case 1:
			words.text = "There lives an evil" + "\r\n" +
				"dictator, who rules" + "\r\n" +
				"over the land of" + "\r\n" +
				"Enslagsted with an" + "\r\n" +
				"iron fist.";
			break;	
		case 2:
			words.text = "One day you decided " + "\r\n" +
				"that enough was enough" + "\r\n" +
				"and you would stand" + "\r\n" +
				"up to his rule.";
			break;
		case 3:
			words.text = "Brimming with courage" + "\r\n" +
				"and a sense of" + "\r\n" +
				"responsibility you set" + "\r\n" +
				"out to end him and " + "\r\n" +
				"restore justice to the"  + "\r\n" +
				"land." ;
			break;
		case 4:
			words.text = "You barely escaped with" + "\r\n" +
				"your life.";
			break;
		case 5:
			words.text = "Enraged at your attack," + "\r\n" +
				"he decided his best" + "\r\n" +
				"course of action would " + "\r\n" +
				"be to burn down your" + "\r\n" +
				"village, and the " + "\r\n" +
				"surrounding towns for" + "\r\n" +
				"good measure.";
			break;
		case 6:
			words.text = "It has been 2 years" + "\r\n" +
				"since that day. You've" + "\r\n" +
				"trained assiduously " + "\r\n" +
				"and in the intervening"  + "\r\n" +
				"time managed to acquire " + "\r\n" +
				"a sailing ship.";
			break;
		case 7:
			words.text = "You've managed to track" + "\r\n" +
				"the villain's second " + "\r\n" +
				"lieutenant to an island" + "\r\n" +
				"just north of where " + "\r\n" +
				"you've been hiding out." ;
			break;
		case 8:
			words.text = "Go forth, and bring an" + "\r\n" +
			"end to their evil ways!";
			break;
		case 9:
                int y = SceneManager.GetActiveScene().buildIndex;

                SceneManager.UnloadSceneAsync(y);
                SceneManager.LoadScene("NeverUnload", 0);
                break;
		default:
			break;
		}
	}
}
