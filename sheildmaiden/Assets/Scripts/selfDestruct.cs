using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfDestruct : MonoBehaviour {

    public float timer = 0.5f;

	// Update is called once per frame
	void Update () 
	{
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            Destroy(gameObject);
        }
			
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Enemy_Hit") //Checks for weapon hit
		{
			Destroy (gameObject);
		}
	}
}
