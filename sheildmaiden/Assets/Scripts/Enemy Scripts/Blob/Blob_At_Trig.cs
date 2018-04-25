using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Blob_At_Trig : MonoBehaviour
{

    //public GameObject Parent;
    private Animator motion;

    private Color red = Color.red;

    private void Start()
    { 
        
        motion = GetComponentInParent<Animator>();//Sets motion to parent game object animator component.

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")//Checks for PLayer
        {
            /* **Make Explode** */
            GetComponentInParent<BlobAI>().Exploding = true;
            GetComponentInParent<BlobAI>().dissolve = 150 * Time.deltaTime;

            //new WaitForSeconds(.4f);
            StartCoroutine(turnRed());
            motion.SetBool("Explosion", true);
                
            GetComponentInParent<BlobAI>().curr_hp = 0;
            new WaitForSeconds(.2f);
            GetComponentInParent<BlobAI>().Exploded = true;
            GetComponentInParent<BlobAI>().Exploding = false;
            
        }

    }
    void redSprite()
    {
        GetComponentInParent<BlobAI>().myRenderer.material.shader = GetComponentInParent<BlobAI>().shaderGUItext;
        GetComponentInParent<BlobAI>().myRenderer.color = red;
    }

    void normalSprite()
    {
        GetComponentInParent<BlobAI>().myRenderer.material.shader = GetComponentInParent<BlobAI>().shaderSpritesDefault;
        GetComponentInParent<BlobAI>().myRenderer.color = Color.white;
    }

    public IEnumerator turnRed()
    { 
        for (int i = 0; i < 5; i++)
        {
            redSprite();
            yield return new WaitForSeconds(.1f);
            normalSprite();
            yield return new WaitForSeconds(.1f);
        }
        
    }

}

