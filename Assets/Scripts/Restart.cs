using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class Restart : MonoBehaviour
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
                RaycastHit2D[] hits = Physics2D.RaycastAll(position, Vector2.zero);

                foreach(RaycastHit2D hit in hits)
                {
                    if (hit.collider.gameObject == gameObject)
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    }
                }
            }
        }
    }
}
