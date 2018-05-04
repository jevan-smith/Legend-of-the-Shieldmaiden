using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobAI : MonoBehaviour
{


    public Transform[] patrolnodes;//Number and object set in unity
    [HideInInspector]
    public float speed;//Set in Unity
    public float run_speed;//Speed when playeris detected
    Transform CurrNode;//Node That the enemy will move to
    int CurrIndex;//Number that CurrNode is listed as in patrolnodes array
    private Transform target;//Player position
	private Transform target2;//Player position

    /* **Animation Section** */
    private Animator motion;//Gives access to animator component

    /* **Health** */
    [HideInInspector]
    public float dissolve;

    public int max_hp;
    public int curr_hp;
    public int damage;

    [HideInInspector]
    public bool hit_sound = false;

    [HideInInspector]
    public bool dead_sound = false;

    [HideInInspector]
    private bool dead;

    /* Is Attacking? */
    [HideInInspector]
    public bool Exploding;
    [HideInInspector]
    public bool Exploded;


    [HideInInspector]
    public SpriteRenderer spriteR;

    [HideInInspector]
    public AudioSource[] sounds;
    [HideInInspector]
    public AudioSource noise1;
    [HideInInspector]
    public AudioSource noise2;
    [HideInInspector]
    public AudioSource noise3;
    [HideInInspector]
    public bool canRunAudio = true;

    [HideInInspector]
    public bool blink = false; // if true enemy will flash a color

	[HideInInspector]
	public bool forceMove = false;


    public SpriteRenderer myRenderer;
    public Shader shaderGUItext;
    public Shader shaderSpritesDefault;

    [HideInInspector]
    public SpriteRenderer m_SpriteRenderer;
    [HideInInspector]
    Color m_NewColor;
    [HideInInspector]
    public static float timer = 0;

    [HideInInspector]
    public static bool onceAround = false;
    [HideInInspector]
    public static bool loopStopper = false;

    [HideInInspector]
    public static bool arrowHit2 = false;

    // Use this for initialization
    void Start()
    {

        //Fetch the SpriteRenderer from the GameObject
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        

        damage = 3;

        sounds = GetComponents<AudioSource>();
        noise1 = sounds[0];
        noise2 = sounds[1];
        noise3 = sounds[2];



        spriteR = gameObject.GetComponent<SpriteRenderer>();

        //At start first node is set
        CurrIndex = 0;
        CurrNode = patrolnodes[CurrIndex];
        target = null;//Target set to null(not detected)
		//target2 = GameObject.FindWithTag("Player").transform;


        //Animation Initialization
        motion = GetComponent<Animator>();

        //hp
        dissolve = 100 * Time.deltaTime;// Time until object will be deleted upon death
        max_hp = 10;//Enemy max hp
        curr_hp = 10;//current hp
        dead = false;//is it dead?
        Exploded = false;

        //attacking?
        Exploding = false;

        speed = .8f;//Dont delete..

        myRenderer = gameObject.GetComponent<SpriteRenderer>();
        shaderGUItext = Shader.Find("GUI/Text Shader");
        shaderSpritesDefault = Shader.Find("Sprites/Default"); // or whatever

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(timer);
        target2 = GameObject.FindWithTag("Player").transform;

        if (!Exploding)//If not attacking(attackin==false) do movement stuff below
        {

            /*if (Blob_On_Hit.arrow2_hit == true)
            {
                speed = run_speed - 0.5f;
            }*/


            //Moves enemy towards patrol node at set speed if not dead
            if (target == null && !dead && forceMove == false )
            {
                //print(CurrNode.position.x);
                SetDir(CurrNode.position.x);//Sets direction before move
                this.transform.position = Vector2.MoveTowards(this.transform.position, CurrNode.position, speed * Time.deltaTime);

                //Check if Patrol Node reached
                if (Vector2.Distance(this.transform.position, CurrNode.position) == 0)
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

			if (target != null && !dead && !blink) //Will move to player if detected and not dead
            {
                SetDir(target.position.x);//Sets direction before move 
                this.transform.position = Vector2.MoveTowards(this.transform.position, target.position, speed * Time.deltaTime);

            }
			if (forceMove == true && !dead && !blink && target == null) //Will force move to player if hit with arrow
			{
                
                if (arrowHit2 == true)
                {

                    if (onceAround == true)
                    {
                        timer += 2f;
                        onceAround = false;
                    }

                    timer -= Time.deltaTime;

                    speed = run_speed - 0.5f;

                    m_SpriteRenderer.color = Color.cyan;

                    if (timer <= 0)
                    {
                        arrowHit2 = false;
                        m_SpriteRenderer.color = Color.white;
                        timer = 2;
                    }
                }

                else
                {
                    speed = run_speed + 0.5f;
                }

				SetDir(target2.position.x);//Sets direction before move 
				this.transform.position = Vector2.MoveTowards(this.transform.position, target2.position, speed * Time.deltaTime);

			}
        }

        //if hp is <= 0 start death animation, dead==true, start dissolve timer, destroy enemy object
        if (curr_hp <= 0)
        {
            if (!Exploding)
            {
                if (!Exploded)
                {
                    motion.SetBool("Dead", true);//Starts death animation
                    
                }

            }
            if (canRunAudio == true && !Exploding && !Exploded)
                noise3.Play();
            else if (canRunAudio == true)
                noise2.Play();

            dead = true;//Stops the movement of Enemy Game Object



            if (dissolve > 0 && !Exploded)//checks if dissolve timer has time left
            {
                dissolve -= Time.deltaTime;//subtracts time
            }
            else if (dissolve > 0 && Exploded)
            {
                dissolve -= (Time.deltaTime) * 3;
            }
            else
            {
                Destroy(this.gameObject);//Time <= 0 game object is destroyed
            }
            canRunAudio = false;


        }

        if (blink == true)
        {
            StartCoroutine(turnRed());
        }
        if (hit_sound == true)
        {
            noise1.Play();
            hit_sound = false;
        }

    }

    /* *** PLAYER AND ENEMEY MUST HAVE RIGIDBODY + COLLIDER *** */

    // Checks for Enter of 2D circle collider placed on enemy object
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")//Checks for PLayer
        {
            target = other.transform;
            speed = run_speed;
            print("Detected");
        }

    }

    // Checks for Player exit of circle collider
    private void OnTriggerExit2D(Collider2D other)
    {
        //
        if (other.tag == "Player")
        {
            speed = 0.8f;
            target = null;
            //spriteR.color = Color.white;

        }

    }

    //Sets the direction of enemy
    private void SetDir(float heading)
    {
        //*heading* var is the target enemy is moving to(player or node)
        if ((transform.position.x - heading) < 0)
        {
            motion.SetInteger("direction", 2);//direction is 1(right)
            //"direction" is the parameter name in the animator window
        }
        else if ((transform.position.x - heading) > 0)
        {
            motion.SetInteger("direction", 1);//direction is 0(left/idle)
        }
        else
        {
            motion.SetInteger("direction", 0);
        }
    }

    void whiteSprite()
    {
        myRenderer.material.shader = shaderGUItext;
        myRenderer.color = Color.white;
    }

    void normalSprite()
    {
        myRenderer.material.shader = shaderSpritesDefault;
        myRenderer.color = Color.white;
    }

    public IEnumerator turnRed()
    {
        for (int i = 0; i < 3; i++)
        {
            whiteSprite();
            yield return new WaitForSeconds(.05f);
            normalSprite();
            yield return new WaitForSeconds(.05f);
        }
        blink = false;
    }
}
