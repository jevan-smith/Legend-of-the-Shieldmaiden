using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Change_Level : MonoBehaviour
{

    public int scene_to_load;
    public int scene_to_unload;

    [HideInInspector]
    public bool unloaded;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {

            SceneManager.LoadSceneAsync(scene_to_load, LoadSceneMode.Additive);
        }
        if (!unloaded)
        {
            unloaded = true;

            AnyManager.anyManager.UnloadScene(scene_to_unload);
        }
    }
}
