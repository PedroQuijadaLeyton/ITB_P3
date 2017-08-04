using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astronauta : MonoBehaviour {

    GameObject alert_image;
    GameObject record_image;
    GameObject procesing_image;
    GameObject score_image;
    SpriteRenderer bag;
    public Sprite astronauta_iddle;
    public Sprite astronauta_recording;
    public Sprite astronauta_win;
    public Sprite[] bag_level;
    int bag_index = 1;
    
    // Use this for initialization
    void Start ()
    {
        alert_image = transform.GetChild(0).gameObject;
        record_image = transform.GetChild(1).gameObject;
        procesing_image = transform.GetChild(2).gameObject;
        score_image = transform.GetChild(3).gameObject;
        bag = transform.GetChild(4).GetComponent<SpriteRenderer>();
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

    public void toggle_procesing()
    {
        procesing_image.SetActive(true);
        record_image.SetActive(false);
        alert_image.SetActive(false);
        //GetComponent<SpriteRenderer>().sprite = astronauta_iddle;
    }

    public void toggle_iddle()
    {
        record_image.SetActive(false);
        procesing_image.SetActive(false);
        alert_image.SetActive(false);
        score_image.SetActive(false);
        GetComponent<SpriteRenderer>().sprite = astronauta_iddle;
    }

    public void toggle_win()
    {
        bag.sprite = bag_level[bag_index];
        bag_index++;
        if (bag_index == 4)
            bag_index = 0;

        GetComponent<SpriteRenderer>().sprite = astronauta_win;
        score_image.SetActive(true);
        StartCoroutine(toggle_iddle_coru());
    }

    IEnumerator toggle_iddle_coru()
    {
        yield return new WaitForSeconds(2.0f);
        toggle_iddle();
    }
}
