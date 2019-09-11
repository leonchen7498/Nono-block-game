using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts {
    public class PlayerDrop : MonoBehaviour
    {
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
        }
    }
}
