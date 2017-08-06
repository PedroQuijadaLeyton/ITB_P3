using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour {

    float time_to_die;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position += transform.right * Time.deltaTime;

        if ((time_to_die += Time.deltaTime) >= 14f)
            Destroy(gameObject);
    }
}
