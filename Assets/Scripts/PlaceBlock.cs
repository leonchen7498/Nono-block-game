using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts { 
    public class PlaceBlock : MonoBehaviour
    {
        public GameObject tempObject;
        // Start is called before the first frame update
        void Start()
        {
        
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
                if (hit.collider != null && hit.collider.gameObject == this.gameObject)
                {
                    Instantiate(tempObject, transform.position, Quaternion.identity);
                    transform.position = new Vector3(transform.position.x, transform.position.y + 125, transform.position.z);
                }
            }
        }
    }
}
