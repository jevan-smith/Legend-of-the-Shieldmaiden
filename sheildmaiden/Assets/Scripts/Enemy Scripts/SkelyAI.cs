using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkelyAI : MonoBehaviour
{


    public Transform[] patrolnodes;//Number and object set in unity
    public float speed;//Set in Unity
    public float run_speed;//Speed when playeris detected
    Transform CurrNode;//Node That the enemy will move to
    int CurrIndex;//Number that CurrNode is listed as in patrolnodes array
    private Transform target;//Player position

   

    /* **Animation Section** */
    private Animator motion;//Gives access to animator component

    /* **Health** */
    private float dissolve;
    public int max_hp;
    public int curr_hp;
    private bool dead;

    [HideInInspector]
    public SpriteRenderer spriteR;

    [HideInInspector]
    public bool blink = false; // if tree enemy will flash a color


    private SpriteRenderer myRenderer;
    private Shader shaderGUItext;
    private Shader shaderSpritesDefault;

    // Use this for initialization
    void Start()
    {
        spriteR = gameObject.GetComponent<SpriteRenderer>(); 

        //At start first node is set
        CurrIndex = 0;
        CurrNode = patrolnodes[CurrIndex];
        target = null;//Target set to null(not detected)


        //Animation Initialization
        motion = GetComponent<Animator>();

        //hp
        dissolve = 200 * Time.deltaTime;// Time until object will be deleted upon death
        max_hp = 10;//Enemy max hp
        curr_hp = 10;//current hp
        dead = false;//is it dead?

        myRenderer = gameObject.GetComponent<SpriteRenderer>();
        shaderGUItext = Shader.Find("GUI/Text Shader");
        shaderSpritesDefault = Shader.Find("Sprites/Default"); // or whatever

    }

    // Update is called once per frame
    void Update()
    {
        //Moves enemy towards patrol node at set speed
        if (target == null && !dead)
        {
            SetDir(CurrNode.position.x);//Sets direction before move
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

        if (target != null && !dead) //Will move to player if detected
        {
            SetDir(target.position.x);//Sets direction before move 
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        }

        //if hp is <= 0 start death animation, dead==true, start dissolve timer, destroy enemy object
        if (curr_hp <= 0)
        {
            motion.SetBool("Dead", true);
            dead = true;
            if (dissolve > 0)
            {
                dissolve -= Time.deltaTime;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        if (blink == true)
        {
            StartCoroutine(turnRed());
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
            motion.SetInteger("direction", 1);//direction is 1(right)
            //"direction" is the parameter name in the animator window
        }
        else
        {
            motion.SetInteger("direction", 0);//direction is 0(left/idle)
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
