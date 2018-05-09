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
	public int Player_Arrows = 0;
    public int Player2_Arrows = 0;

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
    public AudioSource noise5;

    [HideInInspector]
    public bool hit_sound = false;

    [HideInInspector]
    public bool pickup_sound = false;

    public GameObject openDoorPrefab;
    public GameObject openDoorPrefab2;

    public GameObject arrowPrefab;
    Quaternion rot;
    public GameObject arrow2Prefab;

    [HideInInspector]
    public static int arrow_swtich = 0;

    [HideInInspector]
    public bool switch_sound;

    public Rigidbody player;

    [HideInInspector]
    public bool unloaded = false;

    [HideInInspector]
    public bool door1 = false; //when true door is open
    [HideInInspector]
    public bool door2 = false; //when true door is open



    // Use this for initialization
    protected override void Start () 
	{
        Player_Keys = Global.KeysCollected;
		Player_Arrows = Global.ArrowsCollected;
        Player2_Arrows = Global.Arrows2Collected;
        //sound = GetComponent<AudioSource>();



        animator = GetComponent<Animator>();

        sounds = GetComponents<AudioSource>();
        noise1 = sounds[0];
        noise2 = sounds[1];
        noise3 = sounds[2];
        noise4 = sounds[3];
        noise5 = sounds[4];

        spriteR = gameObject.GetComponent<SpriteRenderer>();
        base.Start ();

        myRenderer = gameObject.GetComponent<SpriteRenderer>();
        shaderGUItext = Shader.Find("GUI/Text Shader");
        shaderSpritesDefault = Shader.Find("Sprites/Default"); // or whatever
    }
	
	// Update is called once per frame
	protected override void Update () 
	{
        int y = SceneManager.GetActiveScene().buildIndex;
        Player_Health = GameObject.Find("Health").GetComponent<PlayerH>()._CurHealth;

        if (Player_Health <= 0)
        {
            SceneManager.UnloadSceneAsync(y);
            SceneManager.UnloadSceneAsync("NeverUnload");
            SceneManager.LoadSceneAsync("Death_Screen", 0);
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

		if (Input.GetKeyDown(KeyCode.Alpha3) && isAttacking == false)
		{
			Global.ArrowsCollected += 1;
			Player_Arrows = Global.ArrowsCollected;

			//pickup_sound = true;
			if (pickup_sound == true)
			{
				noise3.Play();
				pickup_sound = false;
			}
		}

        if (Input.GetKeyDown(KeyCode.Alpha4) && isAttacking == false)
        {
            Global.Arrows2Collected += 1;
            Player2_Arrows = Global.Arrows2Collected;

            //pickup_sound = true;
            if (pickup_sound == true)
            {
                noise3.Play();
                pickup_sound = false;
            }
        }


        //Transports to castle entrance ****DELETE FOR FINAL*****
        if(Input.GetKeyDown(KeyCode.Alpha7) && isAttacking == false)
        {
            this.transform.position = new Vector2(76.83f, -87.22f);
        }
        //*********************************************************


        if (Input.GetKeyDown(KeyCode.J) && isAttacking == false)
        {
            attackRoutine = StartCoroutine(Attack());
        }

        if (Input.GetKeyDown(KeyCode.K) && isAttacking == false)
        {
            attackRoutine = StartCoroutine(Attack2());
        }

        if (Input.GetKeyDown(KeyCode.L) && isAttacking == false)
        {
            if (arrow_swtich == 0)
            {
                arrow_swtich = 1;
                changeColor.showType = false;
                changeColor2.showType2 = true;
                switch_sound = true;
                if (switch_sound == true)
                {
                    noise5.Play();
                    switch_sound = false;
                }
            }
            else if (arrow_swtich == 1)
            {
                arrow_swtich = 0;
                changeColor.showType = true;
                changeColor2.showType2 = false;
                switch_sound = true;
                if (switch_sound == true)
                {
                    noise5.Play();
                    switch_sound = false;
                }
            }
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
		if (!isAttacking && !IsMoving && (Player_Arrows > 0 && arrow_swtich == 0) ||
            (Player2_Arrows > 0 && arrow_swtich == 1))
        {
            if (arrow_swtich == 0)
            {
                Global.ArrowsCollected -= 1;
                Player_Arrows = Global.ArrowsCollected;
            }
            if (arrow_swtich == 1)
            {
                Global.Arrows2Collected -= 1;
                Player2_Arrows = Global.Arrows2Collected;
            }

            Vector3 arrowOffset = transform.position;
            
            //noise2.Play();

            //facing right
            if (animator.GetFloat("x") == 1.0 && animator.GetFloat("y") == 0.0)
            {
                arrowOffset = transform.rotation * new Vector3(0.4f, 0, 0);
                rot = Quaternion.Euler(0, 0, 45);
            }
            //facing left
            else if (animator.GetFloat("x") == -1.0 && animator.GetFloat("y") == 0.0)
            {
                arrowOffset = transform.rotation * new Vector3(-0.4f, 0, 0);
                rot = Quaternion.Euler(0, 0, 225);
            }
            //facing up
            else if (animator.GetFloat("x") == 0.0 && animator.GetFloat("y") == 1.0)
            {
                arrowOffset = transform.rotation * new Vector3(0, 0.4f, 0);
                rot = Quaternion.Euler(0, 0, -225);
            }
            //facing down
            else if (animator.GetFloat("x") == 0.0 && animator.GetFloat("y") == -1.0)
            {
                arrowOffset = transform.rotation * new Vector3(0, -0.4f, 0);
                rot = Quaternion.Euler(0, 0, -45);
            }

            if (arrow_swtich == 0)
            {
                Instantiate(arrowPrefab, transform.position + arrowOffset, rot);
            }
            else if (arrow_swtich == 1)
            {
                Instantiate(arrow2Prefab, transform.position + arrowOffset, rot);
            }
            


            isAttacking = true;

           
            animator.SetBool("attack", isAttacking);
            animator.SetInteger("type", 2);
            

            

            yield return new WaitForSeconds(0.25F);

            //Debug.Log("attack was a success");

            StopAttack();
        }

    }

    public void OnTriggerEnter2D(Collider2D other)
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

        if (other.tag == "Explosion_Radius")
        {
            //print("Exploded1");
            if (Player_Health != 0)
            {
                //print("Exploded2");
                GameObject.Find("Health").GetComponent<PlayerH>()._CurHealth -= GameObject.Find("Exploding Blob").GetComponent<BlobAI>().damage;
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


        if (other.tag == "Boss_Hit")
        {
            
            if (Player_Health != 0)
            {
                
                GameObject.Find("Health").GetComponent<PlayerH>()._CurHealth -= GameObject.Find("Boss").GetComponent<Boss>().damage;
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

        if (other.tag == "Arrow_Pickup") //Checks for weapon hit
        {

            Global.ArrowsCollected += 10;
            Player_Arrows = Global.ArrowsCollected;

            //pickup_sound = true;
            if (pickup_sound == true)
            {
                noise3.Play();
                pickup_sound = false;
            }
        }

        if (other.tag == "Arrow2_Pickup") //Checks for weapon hit
        {

            Global.Arrows2Collected += 10;
            Player2_Arrows = Global.Arrows2Collected;

            //pickup_sound = true;
            if (pickup_sound == true)
            {
                noise3.Play();
                pickup_sound = false;
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

        if (other.tag == "heart")
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

        if (other.tag == "door") //Checks for weapon hit
        {
            
            if (Player_Keys >= 10)
            {
                if (door1 == false)
                {
                    door1 = true;
                    Destroy(GameObject.Find("closed_door"));
                    Instantiate(openDoorPrefab);
                    Global.KeysCollected -= 10;
                    Player_Keys = Global.KeysCollected;

                }
            }
        }

        if (other.tag == "door2") //Checks for weapon hit
        {

            if (Player_Keys >= 10)
            {
                if (door2 == false)
                {
                    door2 = true;
                    Destroy(GameObject.Find("closed_door2"));
                    Instantiate(openDoorPrefab2);
                    Global.KeysCollected -= 10;
                    Player_Keys = Global.KeysCollected;

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
