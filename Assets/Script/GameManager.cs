using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public bool is_moving = true;
    public GameObject recording;
    public GameObject analyzing;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void show_recording_panel() //"abre" microfono y empieza a grabar 
    {
        recording.SetActive(true);
    }

    public void show_analyzing_panel() // detienen la grabaciin y se enva a analizar 
    {
        recording.SetActive(false);
        analyzing.SetActive(true);
        StartCoroutine(wait_for_sec_to_disable_analyzing());
    }

    IEnumerator wait_for_sec_to_disable_analyzing()
    {
        yield return new WaitForSeconds(2);
        analyzing.SetActive(false);
        is_moving = true;
    }
}
