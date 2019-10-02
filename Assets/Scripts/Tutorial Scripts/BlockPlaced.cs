using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class BlockPlaced : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (LevelController.currentBlock != null && LevelController.getBlockAmount("yellow_tag") != "1")
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            } else
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }
}
