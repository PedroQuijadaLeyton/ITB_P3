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
    Astronauta astronauta;

    bool is_result_good = false;

    public GameObject temp_correct;
    public GameObject temp_incorrect;

    // Use this for initialization
    void Start ()
    {
        astronauta = FindObjectOfType<Astronauta>();
        spawn_image();
	}
	
	// Update is called once per frame
	void Update ()
    {

        //Debug.Log(timepo += Time.deltaTime);
        //11.5
        

    }

    public void show_recording_panel()
    {
        astronauta.toggle_recording();
    }

    public void show_analyzing_panel(ImageManager im, bool result)
    {
        is_result_good = result;
        current_imagen = im;
        //recording.SetActive(false);
        astronauta.toggle_iddle();
        analyzing.SetActive(true);
        StartCoroutine(wait_for_sec_to_disable_analyzing());
    }

    IEnumerator wait_for_sec_to_disable_analyzing()
    {
        yield return new WaitForSeconds(2);
        analyzing.SetActive(false);
        current_imagen.set_result();
        if(is_result_good)
                astronauta.toggle_win();
    }

    public void spawn_image()
    {
        GameObject image_reference = Instantiate(image);
        image_reference.transform.position = spawning_point.position;
        image_reference.GetComponent<ImageManager>().init(images_therapy[images_therapy_index], 1f);
        images_therapy_index++;

        if (images_therapy_index == (images_therapy.Length - 1))
            images_therapy_index = 0;

    }

}
