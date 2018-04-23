using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Key : MonoBehaviour
{

    public void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.tag == "Player") //Checks for weapon hit
        {
            Destroy(this.gameObject);
        }
    }
}
