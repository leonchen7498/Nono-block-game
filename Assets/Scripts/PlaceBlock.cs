using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts { 
    public class PlaceBlock : MonoBehaviour
    {
        new public ParticleSystem particleSystem;
        new private SpriteRenderer renderer;
        public bool collidesWithPlayer;
        public bool readyToPlaceBlock;

        public GameObject BlueObject;
        public GameObject RedObject;
        public GameObject YellowObject;
        public GameObject SlimeObject;
        public GameObject IronObject;
        public GameObject glassObject;
        public GameObject BlueTimerObject;
        public GameObject RedTimerObject;
        public GameObject YellowTimerObject;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name.Contains("player"))
            {
                collidesWithPlayer = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.name.Contains("player"))
            {
                collidesWithPlayer = false;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            renderer = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            if (LevelController.currentBlock == null)
            {
                if (renderer.enabled)
                {
                    renderer.enabled = false;
                }
                return;
            }
            if (LevelController.currentBlock != null)
            {
                if (!renderer.enabled)
                {
                    renderer.enabled = true;
                }
            }

            Vector2 position = LevelController.getTouch();

            if (position != Vector2.zero)
            {
                RaycastHit2D[] hits = Physics2D.RaycastAll(position, Vector2.zero);
                RaycastHit2D placeholderHit = new RaycastHit2D();
                RaycastHit2D blockHit = new RaycastHit2D();

                foreach(RaycastHit2D raycastHit in hits)
                {
                    if (raycastHit.collider.gameObject == gameObject)
                    {
                        placeholderHit = raycastHit;
                    }

                    if (raycastHit.collider.name.Contains("Block"))
                    {
                        blockHit = raycastHit;
                    }
                }

                if (!blockHit && placeholderHit.collider != null)
                {
                    if (LevelController.currentBlock != null && gameObject.GetComponent<SpriteRenderer>().enabled && !readyToPlaceBlock)
                    {
                        if (collidesWithPlayer)
                        {
                            readyToPlaceBlock = true;
                            LevelController.allowedToChangeModes = false;
                        }
                        else
                        {
                            placeBlock();
                        }
                    }
                }
            }

            if (readyToPlaceBlock && !collidesWithPlayer)
            {
                readyToPlaceBlock = false;

                Vector2 placeholderPosition = this.GetComponent<BoxCollider2D>().bounds.center;
                RaycastHit2D[] hits = Physics2D.RaycastAll(placeholderPosition, Vector2.zero);
                bool blockHit = false;

                foreach(RaycastHit2D hit in hits)
                {
                    if (hit.collider.name.Contains("Block"))
                    {
                        blockHit = true;
                    }
                }

                if (!blockHit)
                {
                    LevelController.stopMoving = true;
                    placeBlock();
                    LevelController.allowedToChangeModes = true;
                }
            }
        }

        void placeBlock()
        {
            TimerManager.CountDown();

            switch (LevelController.currentBlock.tag)
            {
                case "yellow_tag":
                    Instantiate(YellowObject, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                    LevelController.yellowBlockAmount -= 1;
                    if (LevelController.yellowBlockAmount == 0)
                    {
                        LevelController.currentBlock = null;
                    }
                    break;
                case "blue_tag":
                    Instantiate(BlueObject, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                    LevelController.blueBlockAmount -= 1;
                    if (LevelController.blueBlockAmount == 0)
                    {
                        LevelController.currentBlock = null;
                    }
                    break;
                case "red_tag":
                    Instantiate(RedObject, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                    LevelController.redBlockAmount -= 1;
                    if (LevelController.redBlockAmount == 0)
                    {
                        LevelController.currentBlock = null;
                    }
                    break;
                case "slime_tag":
                    Instantiate(SlimeObject, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                    LevelController.slimeBlockAmount -= 1;
                    if (LevelController.slimeBlockAmount == 0)
                    {
                        LevelController.currentBlock = null;
                    }
                    break;
                case "iron_tag":
                    Instantiate(IronObject, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                    LevelController.ironBlockAmount -= 1;
                    if (LevelController.ironBlockAmount == 0)
                    {
                        LevelController.currentBlock = null;
                    }
                    break;
                case "glass_tag":
                    Instantiate(glassObject, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                    LevelController.glassBlockAmount -= 1;
                    if (LevelController.glassBlockAmount == 0)
                    {
                        LevelController.currentBlock = null;
                    }
                    break;
                case "yellow_tag_timer":
                    Instantiate(YellowTimerObject, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                    break;
                case "blue_tag_timer":
                    Instantiate(BlueTimerObject, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                    break;
                case "red_tag_timer":
                    Instantiate(RedTimerObject, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                    break;
            }

            gameObject.GetComponent<SpriteRenderer>().enabled = false;

            Vector3 particlePosition = new Vector3(transform.position.x, transform.position.y + 10f, transform.position.z);
            ParticleSystem placeParticle = Instantiate(particleSystem, particlePosition, Quaternion.identity);
            placeParticle.Play();
        }
    }
}
