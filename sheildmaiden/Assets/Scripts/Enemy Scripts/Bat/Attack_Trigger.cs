using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Attack_Trigger : MonoBehaviour {

    //public GameObject Parent;
    private Animator motion;

    private void Start()
    {
        motion = this.transform.parent.GetComponent<Animator>();//Sets motion to parent game object animator component.

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")//Checks for PLayer
        {
            //Enemy is attacking
            this.transform.parent.GetComponent<SkelyAI>().attacking = true;

            //motion.SetBool("Attack", true);
            StartCoroutine(pauseAction());
        }

    }

    // Checks for Player exit of circle collider
    private void OnTriggerExit2D(Collider2D other)
    {
        //Enemy is NOT attacking
        this.transform.parent.GetComponent<SkelyAI>().attacking = false;

        motion.SetBool("Attack", false);//Stops attacking animation

    }

    private IEnumerator pauseAction()
    {
        // Loop to continue attack animation while attacking.
        while (this.transform.parent.GetComponent<SkelyAI>().attacking)
        {
            motion.SetBool("Attack", true);
            yield return new WaitForSeconds(.5f);//Waits .5 seconds to set animation to not attacking
            motion.SetBool("Attack", false);
            yield return new WaitForSeconds(1);//Waits 1 scond before starting loop again.

        }
    }
}
