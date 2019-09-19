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
        private bool ableToPlace;
        private bool collidesWithPlayer;

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
            if (Input.GetMouseButtonDown(0) && ableToPlace && !collidesWithPlayer)
            {
                DragController.justPlaced = false;

                var position = Input.mousePosition;
                Vector2 touchPositionToWorld = Camera.main.ScreenToWorldPoint(position);
                RaycastHit2D[] hits = Physics2D.RaycastAll(touchPositionToWorld, Vector2.zero);
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
                    //if circle is hit and it is the correct circle
                    if (!string.IsNullOrEmpty(DragController.carryingBlock) && !gameObject.GetComponent<SpriteRenderer>().enabled)
                    {
                        gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    }
                    else if (!string.IsNullOrEmpty(DragController.carryingBlock) && gameObject.GetComponent<SpriteRenderer>().enabled)
                    {
                        DragController.blockToPlacePosition = placeholderHit.collider.bounds.center;
                    }
                }
                else
                {
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                } 
            }
            else if (DragController.readyToPlace && collider.bounds.center == DragController.blockToPlacePosition)
            {
                switch (DragController.carryingBlock)
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
                }
                DragController.carryingBlock = null;

                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                ableToPlace = false;
                DragController.readyToPlace = false;
                DragController.blockToPlacePosition = Vector3.zero;
                DragController.justPlaced = true;
            }

            if (DragController.justPlaced && gameObject.GetComponent<SpriteRenderer>().enabled)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }
}
