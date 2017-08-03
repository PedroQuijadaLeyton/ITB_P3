using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageManager : MonoBehaviour {

    float speed;
    GameManager gm;
    bool triggered_start = false;
    bool triggered_end = false;
    bool is_result_good = false;
    bool is_moving = true;

    // Use this for initialization
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        //set incorrect by default - image pass by
        gm.temp_correct.SetActive(is_result_good);
        gm.temp_incorrect.SetActive(!is_result_good);

    }

    // Update is called once per frame
    void Update()
    {
        if (is_moving)
            transform.position += -transform.right * speed * Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("good result");
            is_result_good = true;
            gm.temp_correct.SetActive(is_result_good);
            gm.temp_incorrect.SetActive(!is_result_good);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("bad result");
            is_result_good = false;
            gm.temp_correct.SetActive(is_result_good);
            gm.temp_incorrect.SetActive(!is_result_good);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "start" && !triggered_start)
        {
            triggered_start = true;
            gm.show_recording_panel();
        }
        else if (!triggered_end)
        {
            is_moving = false;
            triggered_end = true;
            gm.show_analyzing_panel(this, is_result_good);
        }
        else if(other.tag == "destroy")
        {
            gm.spawn_image();
            Destroy(gameObject);
        }
    }

    public void init(Sprite current_image, float speed_temp)
    {
        GetComponentInChildren<SpriteRenderer>().sprite = current_image;
        speed = speed_temp;
    }

    public void set_result()
    {
        is_moving = true;
        GetComponentInChildren<Animator>().SetBool("good", is_result_good);
    }

}
