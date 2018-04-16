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

	/// <summary>
	/// The Entity's direction
	/// </summary>
	protected Vector2 direction;

	// Use this for initialization
	protected virtual void Start () 
	{
		animator = GetComponent<Animator> ();
        body = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	protected virtual void Update () 
	{
		Move ();
	}

	public void Move()
	{
        //transform.Translate (direction*speed*Time.deltaTime);
        body.MovePosition(new Vector2((transform.position.x + direction.x * speed),
               transform.position.y + direction.y * speed));

        if (direction.x != 0 || direction.y != 0) 
		{
			AnimateMovement (direction);
		}
		else 
		{
			animator.SetLayerWeight (1, 0);
		}


    }

	public void AnimateMovement(Vector2 direction)
	{

        animator.SetLayerWeight (1, 1);

		animator.SetFloat ("x", direction.x);
		animator.SetFloat ("y", direction.y);

    }

    public void ActivateLayer(string layerName)
    {
        for (int i = 0; i < animator.layerCount; i++)
        {
            animator.SetLayerWeight(i, 0);
        }

        animator.SetLayerWeight(animator.GetLayerIndex(layerName), 1);
    }
}
