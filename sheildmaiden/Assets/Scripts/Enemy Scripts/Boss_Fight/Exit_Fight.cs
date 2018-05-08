using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit_Fight : MonoBehaviour {

    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            GameObject.Find("Boss_Turret_Nest").GetComponent<Mother>().shooting = false;
            GameObject.Find("Fight_Start").GetComponent<Fight_Start>().started = false;
        }
    }
}
