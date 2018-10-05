using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject objects;
    public int x;
    private int y;
    private int z;
    

    private float size;
	// Use this for initialization
	void Start () {
        size = objects.transform.localScale.x;
        Vector3 location = new Vector3(0, 0, 0);
        y = x;
        z = x;

        for (int _x = 0; _x < x; _x++)
        {
            location.y = objects.transform.localScale.y/2;
            for (int _y = 0; _y < y; _y++)
            {
                location.z = 0;
                for (int _z = 0; _z < z; _z++)
                {
                    GameObject temp = Instantiate(objects, transform);
                    
                    temp.transform.position = location;

                    location.z += size;
                    
                   
                }
                location.y += size;
            }
            location.x += size;
        }
	}
	
	
}
