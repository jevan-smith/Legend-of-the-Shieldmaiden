using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour 
{
    private Rigidbody2D body;

	/// <summary>
	/// The Entity's movement speed
	/// </summary>
	[SerializeField]
	private float speed;

	protected Animator animator;

    protected Coroutine attackRoutine;

	/// <summary>
	/// The Entity's direction
	/// </summary>
	protected Vector2 direction;

    protected bool isAttacking = false;

    public bool IsMoving
    {
        get
        {
            return direction.x != 0 || direction.y != 0;
        }
    }

	// Use this for initialization
	protected virtual void Start () 
	{
		animator = GetComponent<Animator> ();
        body = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	protected virtual void Update () 
	{
        HandleLayers();
	}

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
	{ 
        body.velocity = direction.normalized * speed;
    }

    public void HandleLayers()
    {
        if (IsMoving && !isAttacking)
        {
            ActivateLayer("walkLayer");

            animator.SetFloat("x", direction.x);
            animator.SetFloat("y", direction.y);

            //StopAttack();
        }
        else if (isAttacking)
        {
            ActivateLayer("attackLayer");
        }
        else
        {
            ActivateLayer("idleLayer");
        }
    }


    public void ActivateLayer(string layerName)
    {
        for (int i = 0; i < animator.layerCount; i++)
        {
            animator.SetLayerWeight(i, 0);
        }

        animator.SetLayerWeight(animator.GetLayerIndex(layerName), 1);
    }

    public void StopAttack()
    {
        if (attackRoutine != null)
        {
            StopCoroutine(attackRoutine);
            isAttacking = false;
            animator.SetBool("attack", isAttacking);
        }
        isAttacking = false;
    }
}
