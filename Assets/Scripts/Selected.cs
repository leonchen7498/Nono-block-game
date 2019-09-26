using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Selected : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (LevelController.currentBlock != null)
            {
                if (LevelController.currentBlock.tag == tag)
                {
                    gameObject.GetComponent<SpriteRenderer>().enabled = true;
                }
                else
                {
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                }
                
            } else
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
            TMPro.TextMeshProUGUI text = GetComponentInChildren<TMPro.TextMeshProUGUI>();
            if (text != null)
            {
                text.text = LevelController.getBlockAmount(tag);
            }
        }
    }
}
