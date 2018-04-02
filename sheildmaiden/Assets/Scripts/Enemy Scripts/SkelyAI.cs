using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkelyAI : MonoBehaviour
{

    public Transform[] patrolnodes;//Number and object set in unity
    public float speed;//Set in Unity
    Transform CurrNode;//Node That the enemy will move to
    int CurrIndex;//Number that CurrNode is listed as in patrolnodes array

    private Transform target;//Player position

    // Use this for initialization
    void Start()
    {
        //At start first node is set
        CurrIndex = 0;
        CurrNode = patrolnodes[CurrIndex];
        target = null;//Target set to null(not detected)
    }

    // Update is called once per frame
    void Update()
    {
        //Moves enemy towards patrol node at set speed
        if (target == null)
        {
            transform.position = Vector2.MoveTowards(transform.position, CurrNode.position, speed * Time.deltaTime);
            
            //Check if Patrol Node reached
            if (Vector2.Distance(transform.position, CurrNode.position) == 0)
            {
                //Have reached Patrol Node - get next else start over
                if (CurrIndex + 1 < patrolnodes.Length)
                {
                    CurrIndex++;
                }
                else
                {
                    CurrIndex = 0;
                }
                CurrNode = patrolnodes[CurrIndex];

            }

        }
        if (target != null) //Will move to player if detected
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }

    }

    /* *** PLAYER AND ENMEY MUST HAVE RIGIDBODY + COLLIDER *** */

    // Checks for Enter of 2D circle collider placed on enemy object
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")//Checks for PLayer
        {
            target = other.transform;
            speed = 1.2f;
            print("Detected");
        }
    }

    // Checks for Player exit of circle collider
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            speed = 0.8f;
            target = null;
        }
    }
}
