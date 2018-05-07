using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scroller : MonoBehaviour {
	[SerializeField] private float speed = 0.1f;
	[SerializeField] private Text words;
	private Camera cam;
	private bool hasBeenDown = false;
	private float waitTime = 4f;
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
		}
		if(hasBeenDown){
			waitTime -= Time.deltaTime;
			if(waitTime <= 0)
				SceneManager.LoadScene("Start Screen", 0);
		}
	}
}
