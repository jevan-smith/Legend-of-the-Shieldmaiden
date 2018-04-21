using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AnyManager : MonoBehaviour
{

    public static AnyManager anyManager;
	public string level_to_load = "mainScene";

    bool gameStart;

    void Awake()
    {
        if (!gameStart)
        {
            anyManager = this;

			SceneManager.LoadSceneAsync(level_to_load, LoadSceneMode.Additive);

            gameStart = true;
        }
    }

    public void UnloadScene(string scene)
    {
        StartCoroutine(Unload(scene));
    }

    IEnumerator Unload(string scene)
    {
        yield return null;

        SceneManager.UnloadSceneAsync(scene);
    }




}
