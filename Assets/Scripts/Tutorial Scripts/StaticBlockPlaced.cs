using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class StaticBlockPlaced : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (LevelController.getBlockAmount("blue_tag") == "0" && LevelController.getBlockAmount("red_tag") == "1")
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                gameObject.GetComponent<PlaceBlock>().enabled = true;
                foreach (Transform child in transform)
                {
                    child.transform.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                }
            } else
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                gameObject.GetComponent<PlaceBlock>().enabled = false;
                foreach (Transform child in transform)
                {
                    child.transform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                }
            }
        }
    }
}
