using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astronauta : MonoBehaviour {

    GameObject alert_image;
    GameObject record_image;
    public Sprite astronauta_iddle;
    public Sprite astronauta_recording;
    public Sprite astronauta_win;

    // Use this for initialization
    void Start ()
    {
        alert_image = transform.GetChild(0).gameObject;
        record_image = transform.GetChild(1).gameObject;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void toggle_recording()
    {
        alert_image.SetActive(true);
        StartCoroutine(toggle_off_alert_image());
    }

    IEnumerator toggle_off_alert_image()
    {
        yield return new WaitForSeconds(1.0f);
        GetComponent<SpriteRenderer>().sprite = astronauta_recording;
        alert_image.SetActive(false);
        record_image.SetActive(true);
    }
    
    public void toggle_iddle()
    {
        record_image.SetActive(false);
        alert_image.SetActive(false);
        GetComponent<SpriteRenderer>().sprite = astronauta_iddle;
    }

    public void toggle_win()
    {
        GetComponent<SpriteRenderer>().sprite = astronauta_win;
        StartCoroutine(toggle_iddle_coru());
    }

    IEnumerator toggle_iddle_coru()
    {
        yield return new WaitForSeconds(2.0f);
        toggle_iddle();
    }
}
