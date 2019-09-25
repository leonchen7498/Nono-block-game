using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Tappable : MonoBehaviour
    {
        private float currentShakeDuration;
        private Vector3 initialPosition;

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
                        if (LevelController.carryingBlock == null)
                        {
                            LevelController.draggingBlock = gameObject;
                        }
                        else if (currentShakeDuration <= 0)
                        {
                            initialPosition = transform.position;
                            currentShakeDuration = 0.3f;
                        }
                    }
                }
            }

            if (currentShakeDuration > 0)
            {
                currentShakeDuration -= Time.deltaTime;
                transform.position = initialPosition + Random.insideUnitSphere * 10;
            }
        }
    }
}
