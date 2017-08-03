using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageMovement : MonoBehaviour {

    public float speed;
    GameManager gm;
    bool open_mic = false;
    bool close_mic = false;
    bool rec_mic = false;

    //Recording Stuff:
    //AudioRecorder recorder;

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
        if(other.tag == "open_mic" && !open_mic) // SE ENCIENDE EL MICROFONO
        {
            Debug.Log("MICROFONO ABIERTO=============");
            open_mic = true;
            //Recording thingy:
            AudioRecorder.Instance.StartRecorder(0.5f, 4f, "wordRecorded.wav");
        }
        else if (other.tag == "rec_mic" && !rec_mic)
        {
            Debug.Log("GRABANDO=============");
            rec_mic = true;
            gm.show_recording_panel();
            //
        }
        else if (!close_mic)
        {
            Debug.Log("MICROFONO CERRADO Y ENVIANDO=============");
            gm.is_moving = false;
            close_mic = true;
            gm.show_analyzing_panel();
            //
        }
    }
}
