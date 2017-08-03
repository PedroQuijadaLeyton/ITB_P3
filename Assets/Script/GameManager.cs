using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    
    public GameObject recording;
    public GameObject analyzing;
    public GameObject image;
    public Transform spawning_point;
    float timepo;
    float time_to_spawn_and_death_time = 12f;
    public Sprite[] images_therapy;
    int images_therapy_index = 0;
    ImageManager current_imagen;

    public GameObject temp_correct;
    public GameObject temp_incorrect;

    // Use this for initialization
    void Start ()
    {
        InvokeRepeating("spawn_image", 0, time_to_spawn_and_death_time);
	}
	
	// Update is called once per frame
	void Update ()
    {

        //Debug.Log(timepo += Time.deltaTime);
        //11.5
        

    }

    public void show_recording_panel()
    {
        recording.SetActive(true);
    }

    public void show_analyzing_panel(ImageManager im)
    {
        current_imagen = im;
        recording.SetActive(false);
        analyzing.SetActive(true);
        StartCoroutine(wait_for_sec_to_disable_analyzing());
    }

    IEnumerator wait_for_sec_to_disable_analyzing()
    {
        yield return new WaitForSeconds(2);
        analyzing.SetActive(false);
        current_imagen.set_result();
    }

    void spawn_image()
    {
        GameObject image_reference = Instantiate(image);
        image_reference.transform.position = spawning_point.position;
        image_reference.GetComponent<ImageManager>().init(images_therapy[images_therapy_index], time_to_spawn_and_death_time, 2);
        images_therapy_index++;

        if (images_therapy_index == (images_therapy.Length - 1))
            images_therapy_index = 0;

    }
}
