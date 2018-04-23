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
            //motion.SetBool("Attack", true);
            StartCoroutine(pauseAction());

            //Enemy is attacking
            this.transform.parent.GetComponent<SkelyAI>().attacking = true;
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
        motion.SetBool("Attack", true);
        yield return new WaitForSeconds(1);
        motion.SetBool("Attack", false);
    }
}
