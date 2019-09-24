using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts {
    public class PlayerDrop : MonoBehaviour
    {
        Animator animator;
        new SpriteRenderer renderer;

        public GameObject yellowCarry;
        public GameObject blueCarry;
        public GameObject redCarry;
        public GameObject slimeCarry;
        public GameObject ironCarry;
        public GameObject glassCarry;
        public GameObject yellowTimerCarry;
        public GameObject blueTimerCarry;
        public GameObject redTimerCarry;
        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
            renderer = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            if (LevelController.draggingBlock != null)
            {
                switch (LevelController.draggingBlock.tag)
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
                    case "slime_tag":
                        carryBlock(slimeCarry);
                        break;
                    case "iron_tag":
                        carryBlock(ironCarry);
                        break;
                    case "glass_tag":
                        carryBlock(glassCarry);
                        break;
                    case "yellow_tag_timer":
                        carryBlock(yellowTimerCarry);
                        break;
                    case "blue_tag_timer":
                        carryBlock(blueTimerCarry);
                        break;
                    case "red_tag_timer":
                        carryBlock(redTimerCarry);
                        break;
                }
            }
            else if (LevelController.carryingBlock != null)
            {
                LevelController.carryingBlock.transform.position = new Vector2(
                    transform.position.x + renderer.bounds.size.x / 2,
                    transform.position.y + renderer.bounds.size.y / 2 - 10f);
            }
        }

        void carryBlock(GameObject block)
        {
            LevelController.carryingBlock = Instantiate(block, new Vector2(
                            transform.position.x + renderer.bounds.size.x / 2,
                            transform.position.y + renderer.bounds.size.y / 2), Quaternion.identity);
            LevelController.carryingBlock.tag = LevelController.draggingBlock.tag;

            if (PlayerMovement.timeLeftHolding <= 0)
            {
                animator.SetTrigger("transform_hold");
            }
            else
            {
                PlayerMovement.timeLeftHolding = 0;
            }

            Destroy(LevelController.draggingBlock);
            LevelController.draggingBlock = null;
        }
    }
}
