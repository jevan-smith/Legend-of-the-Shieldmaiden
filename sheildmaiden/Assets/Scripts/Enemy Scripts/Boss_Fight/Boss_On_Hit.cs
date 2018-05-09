using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_On_Hit : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Weapon")
        {//Checks for weapon hit
            if (this.transform.parent.GetComponent<Boss>().health != 0 && GameObject.Find("Boss_Turret_Nest").GetComponent<Turret_Nest>().dead == true)
            {//gets variable from main script
                this.transform.parent.GetComponent<Boss>().health -= GameObject.Find("Player").GetComponent<Player>().Player_Damage;//changes variable from parent object
                this.transform.parent.GetComponent<Boss>().blink = true;
                this.transform.parent.GetComponent<Boss>().hit_sound = true;
            }
        }
        if (other.tag == "Arrow")//Checks for weapon hit
        {
            if (this.transform.parent.GetComponent<Boss>().health != 0 && GameObject.Find("Boss_Turret_Nest").GetComponent<Turret_Nest>().dead == true)//gets variable from main script
            {
                this.transform.parent.GetComponent<Boss>().health -= GameObject.Find("Player").GetComponent<Player>().Player_Damage;//changes variable from parent object
                this.transform.parent.GetComponent<Boss>().blink = true;
                this.transform.parent.GetComponent<Boss>().hit_sound = true;
                //this.transform.parent.GetComponent<Boss>().forceMove = true;

            }
        }
        if (other.tag == "Arrow2")//Checks for weapon hit
        {
            if (this.transform.parent.GetComponent<Boss>().health != 0 && GameObject.Find("Boss_Turret_Nest").GetComponent<Turret_Nest>().dead == true)//gets variable from main script
            {
                Boss.arrowHit2 = true;
                Boss.onceAround = true;
                this.transform.parent.GetComponent<Boss>().health -= GameObject.Find("Player").GetComponent<Player>().Player_Damage;//changes variable from parent object
                this.transform.parent.GetComponent<Boss>().blink = true;
                this.transform.parent.GetComponent<Boss>().hit_sound = true;
                //this.transform.parent.GetComponent<Boss>().forceMove = true;

            }
        }
        if(this.transform.parent.GetComponent<Boss>().health <=0)
        {
            this.transform.parent.GetComponent<Boss>().dead = true;
        }
    }
}
