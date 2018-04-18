using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Attack_Trigger : MonoBehaviour {

    public GameObject Parent;
    private Animator motion;

    private void Start()
    {
        motion = Parent.GetComponent<Animator>();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")//Checks for PLayer
        {
            this.transform.parent.GetComponent<SkelyAI>().IsAttacking = true;
            motion.SetBool("Attack", true);
        }

    }

    // Checks for Player exit of circle collider
    private void OnTriggerExit2D(Collider2D other)
    {
        this.transform.parent.GetComponent<SkelyAI>().IsAttacking = false;
        motion.SetBool("Attack", false);

    }
}
