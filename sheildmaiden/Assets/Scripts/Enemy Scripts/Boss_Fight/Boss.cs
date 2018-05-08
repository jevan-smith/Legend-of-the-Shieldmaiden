using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
    private Vector2 Start_pos;
    private Vector2 Player_pos;
    [HideInInspector]
    public bool Nest_dead;
    private bool firstmove;
    private int speed;
    [HideInInspector]
    public Animator motion;
    [HideInInspector]
    public bool InRange;
	// Use this for initialization
	void Start () {
        speed = 3;
        firstmove = true;
        Nest_dead = false;
        Start_pos = new Vector2(-109.78f, -309.76f);
        motion = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        
        Player_pos = new Vector2 (GameObject.Find("Player").transform.position.x, GameObject.Find("Player").transform.position.y);
        if (!InRange)
        {
            if (Nest_dead && firstmove)
            {
                motion.SetInteger("Direction", 1);
                this.transform.position = Vector2.MoveTowards(this.transform.position, Start_pos, 2 * Time.deltaTime);
                if(Vector2.Distance(this.transform.position, Start_pos) == 0)
                {
                    firstmove = false;
                }

            }
            if (Nest_dead && !firstmove)
            {
                this.transform.position = Vector2.MoveTowards(this.transform.position, Player_pos, speed * Time.deltaTime);
            }
        }
	}
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            
            InRange = true;
        }
    }
}
