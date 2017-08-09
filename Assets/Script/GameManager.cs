using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    
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

   // bool is_result_good = false;

    public GameObject temp_correct;
    public GameObject temp_incorrect;
    int current_level = 0;
    
    List<ImageStruc> therapy_images_to_go = new List<ImageStruc>();
    List<ImageStruc> therapy_images_done = new List<ImageStruc>();
    public GameObject image_struc;
    public Transform image_struc_container;

    public bool image_therapy_result = false;

    public AudioSource speaker;

    GameObject therapy_instance;

    float timer_debug = 0;
    bool start_timer_debug = false;

    // Use this for initialization
    void Start ()
    {
        astronauta = FindObjectOfType<Astronauta>();
        therapy_manager = GetComponent<TherapyManager>();
        set_up_level();
        spawn_therapy_instance();
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
                    //Debug.Log(current_image.get_image_name()+" / " +current_level +" , " +difficulty);
                }
            }
        }

    }

	// Update is called once per frame
	void Update ()
    {
        if(start_timer_debug)
        {
            Debug.Log(timer_debug += Time.deltaTime);
        }
        //Debug.Log(timepo += Time.deltaTime);
        //11.5
        

    }
    
    public void toggle_question()
    {
        astronauta.toggle_question();
    }

    public void play_cue_and_record()
    {
        if (therapy_images_to_go[0].image_instance.get_difficulty() == 0 || therapy_images_to_go[0].image_instance.get_difficulty() == 1)
        {
            StartCoroutine(play_cue_and_record_ienumerator());
            therapy_instance.GetComponent<ImageManager>().stop_therapy_image_movement();
        }
        else
        {
            toggle_microphone_on();
        }
    }

    void toggle_microphone_on()
    {
        astronauta.toggle_recording();
        start_timer_debug = true;
        AudioRecorder.Instance.StartRecorder(0, 4f, string.Concat("Records/", therapy_images_to_go[0].image_instance.get_image_name(),".", therapy_images_to_go[0].image_instance.get_difficulty(), ".wav"));
    }

    IEnumerator play_cue_and_record_ienumerator()
    {
        astronauta.toggle_speaking();
        yield return new WaitForSeconds(speaker.clip.length * 2);
        speaker.Play();
        yield return new WaitForSeconds(speaker.clip.length * 3);
        therapy_instance.GetComponent<ImageManager>().continue_therapy_image_movement();
        toggle_microphone_on();
    }

    public void stop_record_and_analyze()
    {
        start_timer_debug = false;
        therapy_instance.GetComponent<ImageManager>().stop_therapy_image_movement();
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
        //SEND IMAGE RESPONSE OF RESULT
        therapy_instance.GetComponent<ImageManager>().set_result(image_therapy_result);
        if(image_therapy_result) //IF CORRECT
        {
            astronauta.toggle_win_and_update_bag();
            StartCoroutine(wait_for_sec_to_disable_well_done_message());
            //delete from list of iamges to go and add it to the list of images done
            therapy_images_done.Add(therapy_images_to_go[0]);
            therapy_images_to_go.Remove(therapy_images_to_go[0]);
        }
        else
        {
            //FAIL increment attemp + 1
            therapy_images_to_go[0].image_instance.image_instance_failure();
            //if incorrect, add to the end of the list
            therapy_images_to_go.Add(therapy_images_to_go[0]);
            therapy_images_to_go.Remove(therapy_images_to_go[0]);
        }
    }

    public void spawn_therapy_instance()
    {
        Debug.Log(therapy_images_to_go[0].image_instance.get_image_name());
        therapy_instance = Instantiate(image);
        therapy_instance.transform.position = spawning_point.position;

        //the image result is false at the beggining
        image_therapy_result = false;
        //set incorrect by default - image pass by --DEBUG
        temp_correct.SetActive(image_therapy_result);
        temp_incorrect.SetActive(!image_therapy_result);
        //set incorrect by default - image pass by --DEBUG

        //SETTING IMAGE
        therapy_instance.GetComponent<ImageManager>().init(
            Resources.Load("therapy_images/" + therapy_images_to_go[0].image_instance.get_image_name(), typeof(Sprite)) as Sprite,
            string.Concat(therapy_images_to_go[0].image_instance.get_image_name(), ".", therapy_images_to_go[0].image_instance.get_difficulty().ToString())
            , this
        );

        //SETTING audio
        //WHOLE CUE
        if (therapy_images_to_go[0].image_instance.get_difficulty() == 0)
            speaker.clip = Resources.Load("whole_word_cue/" + therapy_images_to_go[0].image_instance.get_image_name(), typeof(AudioClip)) as AudioClip;
        //INITIAL CUE
        if (therapy_images_to_go[0].image_instance.get_difficulty() == 1)
            speaker.clip = Resources.Load("initial_phoneme_cue/i_N_" + therapy_images_to_go[0].image_instance.get_image_name(), typeof(AudioClip)) as AudioClip;

        //Debug.Log(speaker.clip.length);

    }

    IEnumerator wait_for_sec_to_disable_well_done_message()
    {
        well_done.SetActive(true);
        yield return new WaitForSeconds(2);
        well_done.SetActive(false);
    }
    
    public void is_correct_button()
    {
        image_therapy_result = true;
        temp_correct.SetActive(image_therapy_result);
        temp_incorrect.SetActive(!image_therapy_result);
    }

    public void is_wrong_button()
    {
        image_therapy_result = false;
        temp_correct.SetActive(image_therapy_result);
        temp_incorrect.SetActive(!image_therapy_result);
    }

}
