using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mother : MonoBehaviour {
    [HideInInspector]
    public bool shooting;
    public GameObject Blob_Ammo;
    public GameObject Blob_Ammo2;
    private GameObject clone;
    private GameObject clone2;
    private Vector2 cannon1_pos;
    private Vector2 cannon2_pos;
    [HideInInspector]
    public int number_of;
    private int cannon_num;

	// Use this for initialization
	void Start () {
        shooting = false;
        cannon_num = 1;
        number_of = 0;
        cannon1_pos = new Vector2 (-112.17f, -307.75f);
        cannon2_pos = new Vector2(-106.95f, -307.75f);
	}
	
	// Update is called once per frame
	void Update () {
        if (shooting == true && GetComponent<Turret_Nest>().health >= 0)
        {
            StartCoroutine(Spawn());
        }
		
	}
    public IEnumerator Spawn()
    {
        shooting = false;
        if (number_of <= 4)
        {
            if (cannon_num == 1)
            {
                clone = Instantiate(Blob_Ammo, cannon1_pos, Quaternion.identity) as GameObject;
                cannon_num += 1;
                number_of++;

                yield return new WaitForSeconds(3);
                //shooting = true;
            }
            else if (cannon_num == 2)
            {
                clone2 = Instantiate(Blob_Ammo2, cannon2_pos, Quaternion.identity) as GameObject;
                cannon_num -= 1;
                number_of++;
                yield return new WaitForSeconds(3);
                //shooting = true;
            }

        }
        else
        {
            yield return new WaitForSeconds(3);
            //shooting = true;
        }
        shooting = true;
    }
}
