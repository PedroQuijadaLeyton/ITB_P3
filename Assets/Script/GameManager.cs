using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    
    ImageManager current_imagen;
    Astronauta astronauta;
    TherapyManager therapy_manager;

    public GameObject recording;
    public GameObject analyzing;
    public GameObject image;
    public Transform spawning_point;
    float timepo;
    float time_to_spawn_and_death_time = 12f;
    public Sprite[] images_therapy;
    int images_therapy_index = 0;
    public GameObject well_done;

    bool is_result_good = false;

    public GameObject temp_correct;
    public GameObject temp_incorrect;
    int current_level = 0;
    
    List<ImageStruc> therapy_images_to_go = new List<ImageStruc>();
    List<ImageStruc> therapy_images_done = new List<ImageStruc>();
    public GameObject image_struc;
    public Transform image_struc_container;

    // Use this for initialization
    void Start ()
    {
        astronauta = FindObjectOfType<Astronauta>();
        therapy_manager = GetComponent<TherapyManager>();
        set_up_level();
        spawn_image_instance();
	}
	
    void set_up_level()
    {
        foreach(TherapyManager.Therapy_images current_image in therapy_manager.therapy_images)
        {
            if(current_image.get_level() == current_level)
            {
                for(int difficulty = 0; difficulty < 3; difficulty++)
                {
                    GameObject temp = Instantiate(image_struc);
                    temp.transform.SetParent(image_struc_container);
                    temp.GetComponent<ImageStruc>().image_instance = new ImageStruc.Image_instance(current_image.get_image_name(), current_level, difficulty, 0);
                    therapy_images_to_go.Add(temp.GetComponent<ImageStruc>());
                    Debug.Log(current_image.get_image_name()+" / " +current_level +" , " +difficulty);
                }
            }
        }

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
            astronauta.toggle_win();
            StartCoroutine(wait_for_sec_to_disable_well_done_message());
        }
    }

    public void spawn_image_instance()
    {
        Debug.Log(therapy_images_to_go[0].image_instance.get_image_name());
        GameObject image_reference = Instantiate(image);
        image_reference.transform.position = spawning_point.position;
        image_reference.GetComponent<ImageManager>().init(Resources.Load(therapy_images_to_go[0].image_instance.get_image_name(), typeof(Sprite)) as Sprite , string.Concat(therapy_images_to_go[0].image_instance.get_image_name(), ".", therapy_images_to_go[0].image_instance.get_difficulty().ToString()), this);
        //Debug.Log(therapy_images_to_go[0].image_instance.get_image_name() as Sprite);
        //ERASE
        //therapy_images_to_go.Add(therapy_images_to_go[0]);
        therapy_images_to_go.Remove(therapy_images_to_go[0]);
        //ERASE

        //if (images_therapy_index == (images_therapy.Length - 1))
        //    images_therapy_index = 0;

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


}
