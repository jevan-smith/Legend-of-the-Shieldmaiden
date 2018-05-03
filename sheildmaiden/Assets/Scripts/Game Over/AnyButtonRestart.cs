using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnyButtonRestart : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Space))
        {
            int y = SceneManager.GetActiveScene().buildIndex;

            SceneManager.UnloadSceneAsync(y);
            SceneManager.LoadScene("NeverUnload", 0);
            //SceneManager.LoadScene("mainScene", LoadSceneMode.Additive);
        }
	}
}
