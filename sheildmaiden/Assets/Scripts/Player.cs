using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Entity {



    [HideInInspector]
    public int Player_Health = 8;
    [HideInInspector]
    public bool audio_health = false;

    public int Player_Damage = 2;
    public int Player_Keys = 0;

    SpriteRenderer m_SpriteRenderer;
    Color m_NewColor;
    

    [HideInInspector]
    public bool blink = false; // if true enemy will flash a color

    [HideInInspector]
    public SpriteRenderer spriteR;

    private SpriteRenderer myRenderer;
    private Shader shaderGUItext;
    private Shader shaderSpritesDefault;

    [HideInInspector]
    public AudioSource[] sounds;
    public AudioSource noise1;
    public AudioSource noise2;
    public AudioSource noise3;
    public AudioSource noise4;

    [HideInInspector]
    public bool hit_sound = false;

    [HideInInspector]
    public bool pickup_sound = false;



    // Use this for initialization
    protected override void Start () 
	{
        Player_Keys = Global.KeysCollected;
        //sound = GetComponent<AudioSource>();

        sounds = GetComponents<AudioSource>();
        noise1 = sounds[0];
        noise2 = sounds[1];
        noise3 = sounds[2];
        noise4 = sounds[3];

        spriteR = gameObject.GetComponent<SpriteRenderer>();
        base.Start ();

        myRenderer = gameObject.GetComponent<SpriteRenderer>();
        shaderGUItext = Shader.Find("GUI/Text Shader");
        shaderSpritesDefault = Shader.Find("Sprites/Default"); // or whatever
    }
	
	// Update is called once per frame
	protected override void Update () 
	{
        if (Player_Health == 0)
        {
            //Destroy(this.gameObject);
            //SceneManager.LoadScene("mainScene");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        GetInput ();

		base.Update ();
	}

	private void GetInput()
	{
		direction = Vector2.zero;

        // Used for debuging HEAL to FULL
        if (Input.GetKeyDown(KeyCode.Alpha1) && isAttacking == false)
        {
            GameObject.Find("Health").GetComponent<PlayerH>()._CurHealth = 8;
            audio_health = true;
            if (audio_health == true)
            {
                noise4.Play();
                StartCoroutine(blinking_red());
                audio_health = false;
            }
        }

        // Used for debuging Add Keys
        if (Input.GetKeyDown(KeyCode.Alpha2) && isAttacking == false)
        {
            Global.KeysCollected += 1;
            Player_Keys = Global.KeysCollected;

            pickup_sound = true;
            if (pickup_sound == true)
            {
                noise3.Play();
                pickup_sound = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.J) && isAttacking == false)
        {
            attackRoutine = StartCoroutine(Attack());
        }

        if (Input.GetKeyDown(KeyCode.K) && isAttacking == false)
        {
            attackRoutine = StartCoroutine(Attack2());
        }

        if (isAttacking == false)
        { 
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                direction += Vector2.up;
            }
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                direction += Vector2.down;
            }
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                direction += Vector2.left;
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                direction += Vector2.right;
            }
        }

    }

    private IEnumerator Attack()
    {
        if (!isAttacking && !IsMoving)
        {
            noise2.Play();

            isAttacking = true;

            animator.SetBool("attack", isAttacking);
            animator.SetInteger("type", 1);

            yield return new WaitForSeconds(0.25F);

            //Debug.Log("attack was a success");

            StopAttack();
        }

    }

    private IEnumerator Attack2()
    {
        if (!isAttacking && !IsMoving)
        {
            //noise2.Play();

            isAttacking = true;

            animator.SetBool("attack", isAttacking);
            animator.SetInteger("type", 2);

            yield return new WaitForSeconds(0.25F);

            //Debug.Log("attack was a success");

            StopAttack();
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy_Hit") //Checks for weapon hit
        {
            if (Player_Health != 0)
            {

                //Player_Health -= GameObject.Find("Demo Enemy").GetComponent<SkelyAI>().damage;
                GameObject.Find("Health").GetComponent<PlayerH>()._CurHealth -= GameObject.Find("Demo Enemy").GetComponent<SkelyAI>().damage;
                Player_Health = GameObject.Find("Health").GetComponent<PlayerH>()._CurHealth;

                blink = true;
                if (blink == true)
                {
                    StartCoroutine(blinking());
                }
                hit_sound = true;
                if (hit_sound == true)
                {
                    noise1.Play();
                    hit_sound = false;
                }
            }    

        }
        
        else if (other.tag == "Explosion_Radius")
        {
            if (Player_Health != 0 && /*GameObject.Find("Exploding Blob").*/GetComponent<BlobAI>().Exploding)
            {
                GameObject.Find("Health").GetComponent<PlayerH>()._CurHealth -= /*GameObject.Find("Exploding Blob").*/GetComponent<BlobAI>().damage;
                Player_Health = GameObject.Find("Health").GetComponent<PlayerH>()._CurHealth;

                blink = true;
                if (blink == true)
                {
                    StartCoroutine(blinking());
                }
                hit_sound = true;
                if (hit_sound == true)
                {
                    noise1.Play();
                    hit_sound = false;
                }
            }
        }

        if (other.tag == "Key") //Checks for weapon hit
        {

            Global.KeysCollected += 1;
            Player_Keys = Global.KeysCollected;

            pickup_sound = true;
            if (pickup_sound == true)
            {
                noise3.Play();
                pickup_sound = false;
            }
        }

        if (other.tag == "heart") //Checks for weapon hit
        {
            if (Player_Health < 8)
            {
                audio_health = true;
                if (audio_health == true)
                {
                    noise4.Play();
                    StartCoroutine(blinking_red());
                    audio_health = false;
                }
            }
        }
    }


    void whiteSprite()
    {
        myRenderer.material.shader = shaderGUItext;
        myRenderer.color = Color.white;
    }

    void redSprite()
    {
        myRenderer.material.shader = shaderGUItext;
        myRenderer.color = Color.red;
    }

    void normalSprite()
    {
        myRenderer.material.shader = shaderSpritesDefault;
        myRenderer.color = Color.white;
    }

    public IEnumerator blinking()
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
    public IEnumerator blinking_red()
    {
        for (int i = 0; i < 2; i++)
        {
            redSprite();
            yield return new WaitForSeconds(.05f);
            normalSprite();
            yield return new WaitForSeconds(.05f);
        }
        blink = false;
    }

}
