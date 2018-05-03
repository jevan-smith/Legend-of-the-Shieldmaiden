using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class displayKeys : MonoBehaviour {

    public GameObject DisplayKey;
    public GameObject createdObject;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		

	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            createdObject = Instantiate(DisplayKey);
        }

    }

    // Checks for Player exit of circle collider
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Destroy(createdObject);
        }

    }
}
