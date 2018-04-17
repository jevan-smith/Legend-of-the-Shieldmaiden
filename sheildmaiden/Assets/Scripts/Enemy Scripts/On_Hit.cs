
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class On_Hit : MonoBehaviour {

   

    // Use this for initialization
    void Start ()
    {
       
    }

	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Weapon")//Checks for weapon hit
        {
            if (this.transform.parent.GetComponent<SkelyAI>().curr_hp != 0)
            {

                this.transform.parent.GetComponent<SkelyAI>().curr_hp -= 2;
            }

        }
    }
}
