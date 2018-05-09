using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Nest : MonoBehaviour {

    public int health;
    public bool dead;
    [HideInInspector]
    public bool played;

	// Use this for initialization
	void Start () {
        played = false;
        if (health == 0)
        {
            health = 5;
        }

        dead = false;
	}
	
	// Update is called once per frame
	void Update () {
        
		
	}
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Blob_Missile")
        {
            health -= 1;
            
            if (health <= 0)
            {
                dead = true;
               
            }
        }
        if (dead)
        {
            GetComponent<Mother>().shooting = false;
            GameObject.Find("Boss").GetComponent<Boss>().Nest_dead = true;
            GameObject.Find("Boss").GetComponent<Animator>().SetBool("tdead", true);
            Destroy(GameObject.Find("Blob Ammo"));
            if (!played)
            {
                GetComponent<Mother>().noise2.Play();
                played = true;
            }
        }
    }
}
