using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Spawn : MonoBehaviour {


    private Vector2 Player_pos;
    private Vector2 Boss_pos;
    //private Vector2 Spawn_pos;
    private Vector2 going_to;

    [HideInInspector]
    public AudioSource[] sounds;
    [HideInInspector]
    public AudioSource noise1;

    private int speed;
    private bool firing;
    

	// Use this for initialization
	void Start () {

        sounds = GetComponents<AudioSource>();
        noise1 = sounds[0];
        Boss_pos = new Vector2(-109.4f, -306f);
        //Player_pos
        going_to = GameObject.Find("Player").transform.position;
        //Spawn_pos = new Vector2(-112.16f, -307.78f);
        speed = 3;
        //going_to = Player_pos;
        firing = true;
        this.transform.GetChild(0).GetComponent<CircleCollider2D>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Vector2.Distance(this.transform.position, going_to) != 0)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, going_to, speed * Time.deltaTime);
        }
        if (firing && Vector2.Distance(this.transform.position, going_to) == 0)
        {
            Explode();
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Weapon" || other.tag == "Arrow" || other.tag == "Arrow2")
        {
            going_to = Boss_pos;
            speed = 7;
        }
        if (other.tag == "Player")
        {
            going_to = this.transform.position;
            //GameObject.Find("Explosion").GetComponentInChildren<CircleCollider2D>().enabled = true;

            
            Explode();
        }
        if (other.tag == "Boss_Nest")
        {
            going_to = this.transform.position;
            

            Explode();

        }
        if (other.tag == "Wall")
        {
            Explode();
        }
        if (other.tag == "Blob_Missile")
        {
        }
    }
    private void Explode()
    {
        firing = false;
        this.transform.GetChild(0).GetComponent<CircleCollider2D>().enabled = true;
        noise1.Play();
        this.GetComponent<Animator>().SetBool("Explode", true);
        //GameObject.Find("Explosion").GetComponentInChildren<CircleCollider2D>().enabled = false;
        Object.Destroy(gameObject, .5f);
        GameObject.Find("Boss_Turret_Nest").GetComponent<Mother>().number_of -= 1;
    }
}
