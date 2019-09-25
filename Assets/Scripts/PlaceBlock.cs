﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts { 
    public class PlaceBlock : MonoBehaviour
    {
        public GameObject BlueObject;
        public GameObject RedObject;
        public GameObject YellowObject;
        public GameObject player;
        new public ParticleSystem particleSystem;
        private bool ableToPlace;
        private bool collidesWithPlayer;
        public GameObject SlimeObject;
        public GameObject IronObject;
        public GameObject glassObject;
        Animator animator;
        public bool visible;
        public GameObject BlueTimerObject;
        public GameObject RedTimerObject;
        public GameObject YellowTimerObject;

        new private BoxCollider2D collider;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name.Contains("player") && collision.GetType() != typeof(CircleCollider2D))
            {
                collidesWithPlayer = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.name.Contains("player") && collision.GetType() != typeof(CircleCollider2D))
            {
                collidesWithPlayer = false;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            collider = GetComponent<BoxCollider2D>();
            ableToPlace = true;
        }

        // Update is called once per frame
        void Update()
        {
            Vector2 position = LevelController.getTouch();

            if (position != Vector2.zero && ableToPlace && !collidesWithPlayer)
            {
                LevelController.justPlaced = false;
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
                    if (LevelController.carryingBlock == null)
                    {
                        LevelController.moveToPosition = position;
                    }
                    else if (LevelController.carryingBlock != null && !gameObject.GetComponent<SpriteRenderer>().enabled)
                    {
                        gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    }
                    else if (LevelController.carryingBlock != null && gameObject.GetComponent<SpriteRenderer>().enabled)
                    {
                        LevelController.blockToPlacePosition = placeholderHit.collider.bounds.center;
                        LevelController.touchedPlaceholder = true;
                        LevelController.moveToPosition = position;
                    }
                }
                else
                {
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                } 
            }
            else if (LevelController.readyToPlace && collider.bounds.center == LevelController.blockToPlacePosition)
            {
                TimerManager.CountDown();

                switch (LevelController.carryingBlock.tag)
                {
                    case "yellow_tag":
                        Instantiate(YellowObject, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                        break;
                    case "blue_tag":
                        Instantiate(BlueObject, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                        break;
                    case "red_tag":
                        Instantiate(RedObject, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                        break;
                    case "slime_tag":
                        Instantiate(SlimeObject, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                        break;
                    case "iron_tag":
                        Instantiate(IronObject, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                        break;
                    case "glass_tag":
                        Instantiate(glassObject, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
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
                Destroy(LevelController.carryingBlock);
                LevelController.carryingBlock = null;

                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                ableToPlace = false;
                LevelController.readyToPlace = false;
                LevelController.blockToPlacePosition = Vector3.zero;
                LevelController.justPlaced = true;

                Vector3 particlePosition = new Vector3(transform.position.x, transform.position.y + 10f, transform.position.z   );
                ParticleSystem placeParticle = Instantiate(particleSystem, particlePosition, Quaternion.identity);
                placeParticle.Play();
            }

            if (LevelController.justPlaced && gameObject.GetComponent<SpriteRenderer>().enabled)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }
}
