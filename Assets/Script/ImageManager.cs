using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageManager : MonoBehaviour {

    float speed;
    GameManager gm;
    bool triggered_start = false;
    bool triggered_end = false;
    float time_alive;
    float delta_time;
    bool is_result_good;
    bool is_moving = true;

    // Use this for initialization
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (is_moving)
            transform.position += -transform.right * speed * Time.deltaTime;

        if ((delta_time += Time.deltaTime) > time_alive)
            Destroy(gameObject);

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("good result");
            is_result_good = true;
            gm.temp_correct.SetActive(true);
            gm.temp_incorrect.SetActive(false);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("bad result");
            is_result_good = false;
            gm.temp_correct.SetActive(false);
            gm.temp_incorrect.SetActive(true);

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
            gm.show_analyzing_panel(this);
        }
    }

    public void init(Sprite current_image, float time_alive_temp, float speed_temp)
    {
        GetComponentInChildren<SpriteRenderer>().sprite = current_image;
        time_alive = time_alive_temp;
        speed = speed_temp;
    }

    public void set_result()
    {
        is_moving = true;
        GetComponentInChildren<Animator>().SetBool("good", is_result_good);
    }

}
