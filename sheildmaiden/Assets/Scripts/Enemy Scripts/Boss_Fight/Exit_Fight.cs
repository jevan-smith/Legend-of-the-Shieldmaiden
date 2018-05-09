using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit_Fight : MonoBehaviour
{

    [HideInInspector]
    public AudioSource[] sounds;
    [HideInInspector]
    public AudioSource noise1;
    public AudioSource noise2;
    private float speed = 20.0f;
    private float newFoV = 0;
    [HideInInspector]
    bool once = true;

    // Use this for initialization
    void Start()
    {
        newFoV = Camera.main.orthographicSize;
        sounds = GetComponents<AudioSource>();
        noise1 = sounds[0];
        noise2 = sounds[1];
        noise2.Play();
    }

    // Update is called once per frame
    void Update()
    {
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, newFoV, .5f / speed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //print("Triggered1");
        if (other.tag == "Player")
        {
            if (once == true)
            {
                noise2.Stop();
                noise1.Play();
                once = false;
            }
            //print("Triggered2");
            //GameObject.Find("Boss_Turret_Nest").GetComponent<Mother>().shooting = false;
            //GameObject.Find("Fight_Start").GetComponent<Fight_Start>().started = false;
            newFoV = 8f;


        }
    }


}