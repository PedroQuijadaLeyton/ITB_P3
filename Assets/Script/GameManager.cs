using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject well_done;

    bool is_result_good = false;

    public GameObject temp_correct;
    public GameObject temp_incorrect;

    public GameObject bonus_x2;
    public GameObject bonus_time;
    public GameObject alien_1;

    public Image current_bag_fill;
    public Text current_bag_value_text;
    public int bag_index = 1;
    int score_bag_value = 0;

    // Use this for initialization
    void Start ()
    {
        astronauta = FindObjectOfType<Astronauta>();
        spawn_image();
        //SPAWN BONUS
        InvokeRepeating("spawn_bonus_time", 2, 22);
        InvokeRepeating("spawn_bonus_x2", 6, 22);
        InvokeRepeating("spawn_alien_1", 13, 22);
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
        astronauta.toggle_procesing();
        analyzing.SetActive(true);
        StartCoroutine(wait_for_sec_to_disable_analyzing());
    }

    IEnumerator wait_for_sec_to_disable_analyzing()
    {
        yield return new WaitForSeconds(2);
        analyzing.SetActive(false);
        astronauta.toggle_iddle();
        current_imagen.set_result();
        if(is_result_good)
        {
            //VISUAL
            bag_index++;
            if (bag_index == 4)
            {
                bag_index = 0;
                current_bag_fill.fillAmount = 0;
                //LEVEL SCORE DISLAYED IN BAG - once full we count 
                score_bag_value++;
                current_bag_value_text.text = string.Concat("x ", score_bag_value.ToString());
            }

            current_bag_fill.fillAmount = 0.2f * bag_index;
            astronauta.toggle_win_and_update_bag(bag_index);


            StartCoroutine(wait_for_sec_to_disable_well_done_message());
        }
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

    IEnumerator wait_for_sec_to_disable_well_done_message()
    {
        well_done.SetActive(true);
        yield return new WaitForSeconds(2);
        well_done.SetActive(false);
    }

    public void is_correct_button()
    {
        FindObjectOfType<ImageManager>().is_correct_button();
    }

    public void is_wrong_button()
    {
        FindObjectOfType<ImageManager>().is_wrong_button();
    }

    public void spawn_bonus_x2()
    {
        Instantiate(bonus_x2);
    }

    public void spawn_bonus_time()
    {
        Instantiate(bonus_time);
    }

    public void spawn_alien_1()
    {
        Instantiate(alien_1);
    }

}
