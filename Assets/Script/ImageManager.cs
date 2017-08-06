using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageManager : MonoBehaviour {
    
    float movement_speed = 100.0f;
    GameManager gm;
    bool is_moving = true;


    public void init(Sprite current_image, string image_name, GameManager gm)
    {
        GetComponentInChildren<SpriteRenderer>().sprite = current_image;
        name = image_name;
        this.gm = gm;
        GetComponent<Rigidbody2D>().AddForce(Vector2.left * movement_speed, ForceMode2D.Force);
    }

    // Update is called once per frame
    void Update()
    {
        //if (is_moving)
        //    transform.position += -transform.right * speed * Time.deltaTime;
#if UNITY_STANDALONE_WIN
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Debug.Log("good result");
        //    is_result_good = true;
        //    gm.temp_correct.SetActive(is_result_good);
        //    gm.temp_incorrect.SetActive(!is_result_good);
        //}

        //if (Input.GetMouseButtonDown(1))
        //{
        //    Debug.Log("bad result");
        //    is_result_good = false;
        //    gm.temp_correct.SetActive(is_result_good);
        //    gm.temp_incorrect.SetActive(!is_result_good);
        //}
#endif
        //DELETE IMAGE AND START THE OTHER - ANIMATION MAKE THE IMAGE SCALE = 0
        if(transform.GetChild(0).lossyScale.x == 0)
        {
            gm.spawn_image_instance();
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "question") //WHEN IT STARTS RECORDING
        {
            gm.toggle_question();
        }
        else if (other.tag == "start") //WHEN IT STARTS RECORDING
        {
            gm.show_recording_panel();
        }
        else if (other.tag == "end") //ANALAZING PANEL
        {
            stop_object();
            gm.show_analyzing_panel(this);
        }
        else if(other.tag == "destroy") //IMAGE IS DESTROYED AT THE END IF THE RESULT IS WRONG
        {
            gm.spawn_image_instance();
            Destroy(gameObject);
        }
    }

    public void set_result(bool result)
    {
        GetComponentInChildren<Animator>().SetBool("good", result);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        if(result)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * movement_speed, ForceMode2D.Force);
            GetComponent<CircleCollider2D>().isTrigger = true;
        }
        else
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 0.3f) * (movement_speed + 100), ForceMode2D.Force);
        }
    }

    public void stop_object()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
