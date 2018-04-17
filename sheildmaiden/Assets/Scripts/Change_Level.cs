using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Change_Level : MonoBehaviour {

    public string levelToLoad;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //SceneManager.MoveGameObjectToScene(Main Camera.gameObject, levelToLoad);

            SceneManager.LoadScene(levelToLoad);
        }
    }

}
