using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Assets.Scripts
{
    public class TimerManager : MonoBehaviour
    {
        public static List<GameObject> timerBlocks = new List<GameObject>();
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }

        public static void CountDown()
        {
            if (timerBlocks.Count >= 1)
            {
                List<GameObject> newlist = new List<GameObject>();
                foreach (GameObject block in timerBlocks)
                {
                    var newTimer = System.Convert.ToInt32(block.GetComponentInChildren<TextMeshProUGUI>().text) - 1;
                    if (newTimer == 0)
                    {
                        newlist.Add(block);
                    }
                    block.GetComponentInChildren<TextMeshProUGUI>().text = "0" + newTimer.ToString();
                }

                foreach (GameObject block in newlist)
                {
                    timerBlocks.Remove(block);
                    Destroy(block);
                }
            }
        }
    }
}
