using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts {
    public class PlayerDrop : MonoBehaviour
    {
        Animator animator;
        new SpriteRenderer renderer;

        public GameObject currentBlock;
        public GameObject yellowCarry;
        public GameObject blueCarry;
        public GameObject redCarry;
        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
            renderer = GetComponent<SpriteRenderer>();
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
                        carryBlock(yellowCarry);
                        break;
                    case "blue_tag":
                        carryBlock(blueCarry);
                        break;
                    case "red_tag":
                        carryBlock(redCarry);
                        break;
                }
            } else if (DragController.carryingBlock == null)
            {
                Destroy(currentBlock);
                currentBlock = null;
            } else
            {
                currentBlock.transform.position = new Vector2(
                    transform.position.x + renderer.bounds.size.x / 2,
                    transform.position.y + renderer.bounds.size.y / 2 - 10f);
            }
        }

        void carryBlock(GameObject block)
        {
            currentBlock = Instantiate(block, new Vector2(
                            transform.position.x + renderer.bounds.size.x / 2,
                            transform.position.y + renderer.bounds.size.y / 2), Quaternion.identity);
            animator.SetTrigger("transform_hold");
        }
    }
}
