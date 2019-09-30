using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerDied : MonoBehaviour
    {
        public enum state { invisible, disabled }
        public state change;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (LevelController.getBlockAmount("blue_tag") == "2")
            {
                if (change == state.invisible)
                {
                    gameObject.GetComponent<SpriteRenderer>().enabled = true;
                } else
                {
                    gameObject.SetActive(false);
                }
            } else if (change == state.invisible)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
        }


    }
}
