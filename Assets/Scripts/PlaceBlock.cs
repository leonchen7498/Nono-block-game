using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts { 
    public class PlaceBlock : MonoBehaviour
    {
        public GameObject BlueObject;
        public GameObject RedObject;
        public GameObject YellowObject;
        public bool visible;


        // Start is called before the first frame update
        void Start()
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            visible = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var position = Input.mousePosition;
                Vector2 touchPositionToWorld = Camera.main.ScreenToWorldPoint(position);
                RaycastHit2D hit = Physics2D.Raycast(touchPositionToWorld, Vector2.zero);

                //if circle is hit and it is the correct circle
                if (hit.collider != null && hit.collider.gameObject == this.gameObject && visible == false)
                {
                    gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    visible = true;
                    
                } else if (hit.collider != null && hit.collider.gameObject == this.gameObject && visible == true)
                {
                    switch (DragController.carryingBlock)
                    {
                        case "yellow":
                            Instantiate(YellowObject, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                            DragController.carryingBlock = "blue";
                            break;
                        case "blue":
                            Instantiate(BlueObject, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                            DragController.carryingBlock = "red";
                            break;
                        case "red":
                            Instantiate(RedObject, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                            DragController.carryingBlock = "yellow";
                            break;
                    }

                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    visible = false;
                } else
                {
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    visible = false;
                }
            }
        }
    }
}
