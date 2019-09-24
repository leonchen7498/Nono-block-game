using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Draggable : MonoBehaviour
    {
        [HideInInspector]
        public bool drag;
        public Vector2 lastPosition;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                var position = Input.mousePosition;
                Vector2 touchPositionToWorld = Camera.main.ScreenToWorldPoint(position);
                RaycastHit2D hit = Physics2D.Raycast(touchPositionToWorld, Vector2.zero);

                //if circle is hit and it is the correct circle
                if (hit.collider != null && hit.collider.gameObject == this.gameObject)
                {
                    if (LevelController.isDragging == false)
                    {
                        LevelController.draggingBlock = gameObject;
                        lastPosition = transform.position;
                        drag = true;
                        LevelController.isDragging = true;
                    }
                }

                if (drag == true)
                {
                    transform.position = touchPositionToWorld;
                }
            }
            else
            {
                if (drag == true)
                {
                    transform.position = lastPosition;
                    LevelController.isDragging = false;
                }
                drag = false;
            }
        }
    }
}
