using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    private Vector2 Start_pos;
    private Vector2 Player_pos;
    [HideInInspector]
    public bool Nest_dead;
    private bool firstmove;
    private float speed;
    [HideInInspector]
    public Animator motion;
    [HideInInspector]
    public bool InRange;

    [HideInInspector]
    public int health;
    [HideInInspector]
    public int damage;

    [HideInInspector]
    public bool blink = false; // if true enemy will flash a color

    public SpriteRenderer myRenderer;
    public Shader shaderGUItext;
    public Shader shaderSpritesDefault;

    [HideInInspector]
    public SpriteRenderer m_SpriteRenderer;

    [HideInInspector]
    public bool dead;
    [HideInInspector]
    public static float timer = 0;

    [HideInInspector]
    public static bool onceAround = false;
    [HideInInspector]
    public static bool loopStopper = false;

    [HideInInspector]
    public static bool arrowHit2 = false;

    [HideInInspector]
    public AudioSource[] sounds;
    [HideInInspector]
    public AudioSource noise1;
    [HideInInspector]
    public AudioSource noise2;
    [HideInInspector]
    public bool hit_sound = false;

    [HideInInspector]
    public bool dead_sound = false;

    [HideInInspector]
    public bool canRunAudio = true;
    [HideInInspector]
    public bool attacking = false;

    // Use this for initialization
    void Start()
    {
        //Fetch the SpriteRenderer from the GameObject
        this.transform.GetChild(1).GetComponent<CircleCollider2D>().enabled = false;
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        dead = false;

        sounds = GetComponents<AudioSource>();
        noise1 = sounds[0];
        noise2 = sounds[1];


        damage = 3;
        health = 40;
        speed = 1.0f;
        firstmove = true;
        Nest_dead = false;
        Start_pos = new Vector2(-109.78f, -309.76f);
        motion = GetComponent<Animator>();

        myRenderer = gameObject.GetComponent<SpriteRenderer>();
        shaderGUItext = Shader.Find("GUI/Text Shader");
        shaderSpritesDefault = Shader.Find("Sprites/Default"); // or whatever
    }

    // Update is called once per frame
    void Update()
    {

        int y = SceneManager.GetActiveScene().buildIndex;
        Player_pos = new Vector2(GameObject.Find("Player").transform.position.x, GameObject.Find("Player").transform.position.y);
        if (!InRange && !dead && !attacking)
        {
            if (Nest_dead && firstmove)
            {
                motion.SetInteger("Direction", 1);
                this.transform.position = Vector2.MoveTowards(this.transform.position, Start_pos, .7f * Time.deltaTime);
                if (Vector2.Distance(this.transform.position, Start_pos) == 0)
                {
                    firstmove = false;
                }

            }
            else if (Nest_dead && !firstmove && !dead && !attacking)
            {
                SetDir(Player_pos.x, Player_pos.y);
                this.transform.position = Vector2.MoveTowards(this.transform.position, Player_pos, speed * Time.deltaTime);

                if (!dead && !blink)
                {

                    if (arrowHit2 == true)
                    {

                        if (onceAround == true)
                        {
                            timer += 2f;
                            onceAround = false;
                        }

                        timer -= Time.deltaTime;
                        if (speed == 1)
                        {
                            speed -= 0.5f;
                        }

                        m_SpriteRenderer.color = Color.cyan;

                        if (timer <= 0)
                        {
                            arrowHit2 = false;
                            m_SpriteRenderer.color = Color.white;
                            timer = 2;
                            if (speed < 1)
                            {
                                speed += 0.5f;
                            }
                        }
                    }


                    //SetDir(target2.position.x);//Sets direction before move 
                    //this.transform.position = Vector2.MoveTowards(this.transform.position, target2.position, speed * Time.deltaTime);

                }
            }
        }
        if (InRange && !dead)
        {

            SetDir(Player_pos.x, Player_pos.y);


        }
        if (blink == true)
        {
            StartCoroutine(turnRed());

        }
        if (hit_sound == true)
        {
            noise2.Play();
            hit_sound = false;
        }
        if (dead)
        {
            if (!dead_sound)
            {
                noise1.Play();
                dead_sound = true;
            }
            motion.SetBool("Dead", true);
            Object.Destroy(gameObject, 2);
            SceneManager.UnloadSceneAsync(y);
            StartCoroutine(action());
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {

            InRange = true;
            attacking = true;
            StartCoroutine(pauseAction());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            attacking = false;
            InRange = false;
            motion.SetBool("Attack", false);
        }
    }

    private void SetDir(float headingx, float headingy)
    {
        float x = transform.position.x - headingx;
        float y = transform.position.y - headingy;
        if (x == 0 || x < 1 && x > -1)
        {
            if (y > 0)
            {
                motion.SetInteger("Direction", 1);
            }
            else if (y < 0)
            {
                motion.SetInteger("Direction", 3);
            }
        }
        else if (x < 0)
        {
            if (y < 1 && y > -1)
            {
                motion.SetInteger("Direction", 2);
            }
            else if (y > 0)
            {
                motion.SetInteger("Direction", 7);
            }
            else if (y < 0)
            {
                motion.SetInteger("Direction", 6);
            }
        }
        else if (x > 0)
        {
            if (y < 1 && y > -1)
            {
                motion.SetInteger("Direction", 4);
            }
            else if (y > 0)
            {
                motion.SetInteger("Direction", 8);
            }
            else if (y < 0)
            {
                motion.SetInteger("Direction", 5);
            }
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

    private IEnumerator pauseAction()
    {
        // Loop to continue attack animation while attacking.
        while (attacking)
        {
            motion.SetBool("Attack", true);
            this.transform.GetChild(1).GetComponent<CircleCollider2D>().enabled = true;
            yield return new WaitForSeconds(.5f);//Waits .5 seconds to set animation to not attacking
            this.transform.GetChild(1).GetComponent<CircleCollider2D>().enabled = false;
            motion.SetBool("Attack", false);
            yield return new WaitForSeconds(1);//Waits 1 scond before starting loop again.

        }
    }
    private IEnumerator action()
    {
        SceneManager.UnloadSceneAsync("NeverUnload");
        SceneManager.LoadSceneAsync("Credits", 0);
        yield return new WaitForSeconds(1);
    }
}
