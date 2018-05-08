using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight_Start : MonoBehaviour {
    [HideInInspector]
    public bool started;

	// Use this for initialization
	void Start () {
        started = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !started)
        {
            GameObject.Find("Boss_Turret_Nest").GetComponent<Mother>().shooting = true;
            started = true;
        }
    }
}
