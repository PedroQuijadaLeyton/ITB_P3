using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageMovement : MonoBehaviour {

    public float speed;
    GameManager gm;
    bool triggered_start = false;
    bool triggered_end = false;

    // Use this for initialization
    void Start ()
    {
        gm = FindObjectOfType<GameManager>();
    }
	
	// Update is called once per frame
	void Update ()
    { 
        if(gm.is_moving)
            transform.position += -transform.right * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("entroooooo");
        if(other.tag == "start" && !triggered_start)
        {
            triggered_start = true;
            gm.show_recording_panel();
        }
        else if(!triggered_end)
        {
            gm.is_moving = false;
            triggered_end = true;
            gm.show_analyzing_panel();
        }
    }
}
