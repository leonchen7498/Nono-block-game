using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class MovementTutorial : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (LevelController.getBlockAmount("yellow_tag") == "0")
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }
}
