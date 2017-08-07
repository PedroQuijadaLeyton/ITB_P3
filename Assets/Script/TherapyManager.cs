using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TherapyManager : MonoBehaviour {

    public struct Therapy_images
    {
        public string image_name;
        public int level;

        public Therapy_images(string image_name, int level)
        {
            this.image_name = image_name;
            this.level = level;
        }

        public int get_level()
        {
            return this.level;
        }

        public string get_image_name()
        {
            return this.image_name;
        }
    };

    public Therapy_images[] therapy_images = {
       new Therapy_images("cage", 0),
       new Therapy_images("cap", 0)
       //new Therapy_images("cat", 0),
       //new Therapy_images("dome", 0),
       //new Therapy_images("eye", 0),
       //new Therapy_images("film", 0),
       //new Therapy_images("fish", 0),
       //new Therapy_images("fly", 0),
       //new Therapy_images("glass", 0),
       //new Therapy_images("gold", 0),
       //new Therapy_images("lock", 0),
       //new Therapy_images("maze", 0),
       //new Therapy_images("mug", 0),
       //new Therapy_images("pearl", 0),
       //new Therapy_images("pound", 0),
       //new Therapy_images("queen", 0),
       //new Therapy_images("ring", 0),
       //new Therapy_images("wall", 0),
    };
}
