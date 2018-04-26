using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour {

    private Vector2 volocity;
    public float smoothTimeY;
    public float smoothTimeX;

    public GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    private void FixedUpdate()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref volocity.x, smoothTimeX);
        float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref volocity.y, smoothTimeY);

        transform.position = new Vector3(posX, posY, transform.position.z);
    }

}
