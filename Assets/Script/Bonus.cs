using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.left * 100, ForceMode2D.Force);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "destroy")
        {
            Destroy(gameObject);
        }
    }

    void OnMouseDown()
    {
        Destroy(gameObject);
    }
}
