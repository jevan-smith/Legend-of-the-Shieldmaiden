using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity {

    //protected Animator myAnimator;
    protected bool isAttacking = false;

	// Use this for initialization
	protected override void Start () 
	{
		base.Start ();
	}
	
	// Update is called once per frame
	protected override void Update () 
	{
		GetInput ();

		base.Update ();
	}

	private void GetInput()
	{
		direction = Vector2.zero;

		if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) 
		{
			direction += Vector2.up;
		}
		else if (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)) 
		{
			direction += Vector2.down;
		}
		else if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) 
		{
			direction += Vector2.left;
		}
		else if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) 
		{
			direction += Vector2.right;
		}
        if (Input.GetKeyDown(KeyCode.J))
        {

            isAttacking = true;
            StartCoroutine(Attack());

        }
    }

    private IEnumerator Attack()
    {
        if (isAttacking)
        {
            ActivateLayer("attackLayer");
        }


        animator.SetBool("attack", true);

        

        yield return new WaitForSeconds(0.001F);

        

        Debug.Log("attack");



        stopAttack();


    }

    public void stopAttack()
    {
        isAttacking = false;
        animator.SetBool("attack", false);
       
    }

}
