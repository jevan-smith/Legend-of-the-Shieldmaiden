using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AnyManager : MonoBehaviour
{

    public static AnyManager anyManager;
	public int level_to_load = 1;

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

    public void UnloadScene(int scene)
    {
        StartCoroutine(Unload(scene));
    }

    IEnumerator Unload(int scene)
    {
        yield return null;

        SceneManager.UnloadSceneAsync(scene);
    }




}
