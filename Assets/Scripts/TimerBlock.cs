using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class TimerBlock : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            TimerManager.timerBlocks.Add(gameObject);

        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnDestroy()
        {
            TimerManager.timerBlocks.Remove(gameObject);
        }
    }
}
