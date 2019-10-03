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
            Vector2 position = LevelController.getTouch();
            if (position != Vector2.zero)
            {
                RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero);

                if (hit.collider.gameObject == this.gameObject)
                {
                    foreach (GameObject item in Pause.menu)
                    {
                        item.SetActive(false);
                    }
                    Time.timeScale = 1;
                }
            }
        }
    }
}
