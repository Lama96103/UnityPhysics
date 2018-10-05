using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fluid : MonoBehaviour {

    public GameObject objects;
    public int number;

	// Use this for initialization
	void Start () {
        StartCoroutine(Spawn());
	}

    IEnumerator Spawn()
    {
        for (int i = 0; i < number; i++)
        {
            Instantiate(objects, transform);
            yield return new WaitForSeconds(0.1f);
        }
        print("Reached the target.");

        yield return new WaitForSeconds(3f);

        print("MyCoroutine is now finished.");
        
    }

    
}
