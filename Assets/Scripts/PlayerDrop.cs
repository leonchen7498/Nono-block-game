using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts {
    public class PlayerDrop : MonoBehaviour
    {
        public GameObject currentBlock;
        public GameObject yellowCarry;
        public GameObject BlueCarry;
        public GameObject RedCarry;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (DragController.isDragging == true)
            {
                var position = Input.mousePosition;
                Vector2 touchPositionToWorld = Camera.main.ScreenToWorldPoint(position);
                RaycastHit2D hit = Physics2D.Raycast(touchPositionToWorld, Vector2.zero);

                if (hit.collider != null && hit.collider.gameObject == this.gameObject)
                {
                    DragController.carryingBlock = DragController.draggingBlock.tag;
                    Destroy(DragController.draggingBlock);
                    DragController.isDragging = false;
                }
            }
            if (currentBlock == null && DragController.carryingBlock != null)
            {
                switch (DragController.carryingBlock)
                {
                    case "yellow_tag":
                        currentBlock = Instantiate(yellowCarry, new Vector3(transform.position.x + 55, transform.position.y + 60, 0), Quaternion.identity);
                        break;
                    case "blue_tag":
                        currentBlock = Instantiate(BlueCarry, new Vector3(transform.position.x + 55, transform.position.y + 60, 0), Quaternion.identity);
                        break;
                    case "red_tag":
                        currentBlock = Instantiate(RedCarry, new Vector3(transform.position.x + 55, transform.position.y + 60, 0), Quaternion.identity);
                        break;
                }
            } else if (DragController.carryingBlock == null)
            {
                Destroy(currentBlock);
                currentBlock = null;
            } else
            {
                currentBlock.transform.position = new Vector3(transform.position.x + 55, transform.position.y + 60, 0);
            }

            
        }
    }
}
