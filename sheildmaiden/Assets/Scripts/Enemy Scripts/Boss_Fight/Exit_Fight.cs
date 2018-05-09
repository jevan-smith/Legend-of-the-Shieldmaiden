using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit_Fight : MonoBehaviour {

    private float speed = 20.0f;
    private float newFoV = 0;
    
	// Use this for initialization
	void Start () {
        newFoV = Camera.main.orthographicSize;
	}
	
	// Update is called once per frame
	void Update () {
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, newFoV, .5f / speed);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        //print("Triggered1");
        if (other.tag == "Player")
        {
            //print("Triggered2");
            //GameObject.Find("Boss_Turret_Nest").GetComponent<Mother>().shooting = false;
            //GameObject.Find("Fight_Start").GetComponent<Fight_Start>().started = false;
            newFoV = 8f;
            
            
        }
    }

    
}
