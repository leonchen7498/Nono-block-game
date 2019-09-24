using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Play : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnMouseUp()
        {
            foreach (GameObject item in Pause.menu)
            {
                item.SetActive(false);
            }
            Time.timeScale = 1;
        }
    }
}
