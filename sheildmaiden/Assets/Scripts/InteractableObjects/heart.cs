using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heart : MonoBehaviour {


    //int playerHealth = GameObject.Find("Health").GetComponent<PlayerH>()._CurHealth;
    public void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.tag == "Player") //Checks for weapon hit
        {
            if (GameObject.Find("Health").GetComponent<PlayerH>()._CurHealth <= 4)
            {
                GameObject.Find("Health").GetComponent<PlayerH>()._CurHealth += 4;
            }
            else if (GameObject.Find("Health").GetComponent<PlayerH>()._CurHealth == 5)
            {
                GameObject.Find("Health").GetComponent<PlayerH>()._CurHealth += 3;
            }
            else if (GameObject.Find("Health").GetComponent<PlayerH>()._CurHealth == 6)
            {
                GameObject.Find("Health").GetComponent<PlayerH>()._CurHealth += 2;
            }
            else if (GameObject.Find("Health").GetComponent<PlayerH>()._CurHealth == 7)
            {
                GameObject.Find("Health").GetComponent<PlayerH>()._CurHealth += 1;
            }

            else
            {
                return;
            }
            Destroy(this.gameObject);
        }
    }
}
