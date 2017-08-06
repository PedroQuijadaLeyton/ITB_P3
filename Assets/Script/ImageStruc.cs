using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageStruc : MonoBehaviour {

    public struct Image_instance
    {
        public string image_name;
        public int level;
        public int difficulty;
        public int attemps;

        public Image_instance(string image_name, int level, int difficulty, int attemps)
        {
            this.image_name = image_name;
            this.level = level;
            this.difficulty = difficulty;
            this.attemps = attemps;
        }

        public void image_instance_failure()
        {
            this.attemps++;
        }
        public int get_level()
        {
            return this.level;
        }

        public string get_image_name()
        {
            return this.image_name;
        }

        public int get_difficulty()
        {
            return this.difficulty;
        }

    };

    public Image_instance image_instance;
}
