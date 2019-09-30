using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class MatchThree : MonoBehaviour
    {
        public List<GameObject> adjacentMatchingBlocks;
        public GameObject tempBlock;
        public List<GameObject> tempBlocks;
        public bool setStatic;
        private bool isFalling;
        private bool placeholderVisible;
        private bool isStatic;
        new private BoxCollider2D collider;

        // Start is called before the first frame update
        void Start()
        {
            setStatic = false;
            placeholderVisible = false;
            collider = GetComponent<BoxCollider2D>();
        }

        // Update is called once per frame
        void Update()
        {
            foreach (GameObject block in adjacentMatchingBlocks)
            {
                if (block.transform.position.y <= gameObject.transform.position.y)
                {
                    block.GetComponent<MatchThree>().toStatic();
                }
            }

            /*if (tempBlocks[0].transform.position.y > gameObject.transform.position.y)
            {
                tempBlocks[1].transform.position = new Vector3(tempBlocks[1].transform.position.x, gameObject.transform.position.y + 120, 10);
                tempBlocks[0].transform.position = new Vector3(tempBlocks[0].transform.position.x, gameObject.transform.position.y, 10);
                tempBlocks[2].transform.position = new Vector3(tempBlocks[2].transform.position.x, gameObject.transform.position.y, 10);
            }*/

            RaycastHit2D[] hits = Physics2D.RaycastAll(new Vector2(collider.bounds.center.x, collider.bounds.center.y - collider.bounds.size.y / 2 - 1f),
                Vector2.zero);
            bool somethingUnder = false;

            foreach(RaycastHit2D hit in hits)
            {
                if (!hit.collider.gameObject.name.Contains("player") && hit.collider.gameObject != this.gameObject &&
                    !hit.collider.gameObject.name.Contains("Placeholder") && hit.collider.GetType() == typeof(BoxCollider2D))
                {
                    somethingUnder = true;
                }
            }

            if (isStatic)
            {
                if (placeholderVisible == false)
                {
                    placeholderVisible = true;
                    addBlocks();
                }
            }
            else
            {
                if (!somethingUnder)
                {
                    isFalling = true;
                }
                else
                {
                    isFalling = false;
                }

                if (isFalling && placeholderVisible)
                {
                    placeholderVisible = false;
                    OnDestroy();
                }

                if (!isFalling && !placeholderVisible)
                {
                    placeholderVisible = true;
                    addBlocks();
                }
            }
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            // Debug-draw all contact points and normals
            if (this.gameObject.GetComponent<BoxCollider2D>().attachedRigidbody == collider.attachedRigidbody || this.gameObject.GetComponent<PolygonCollider2D>().attachedRigidbody == collider.attachedRigidbody || this.tag != collider.gameObject.tag)
            {
            }
            else 
            {
                adjacentMatchingBlocks.Add(collider.gameObject);
                if (adjacentMatchingBlocks.Count > 3)
                {
                    foreach ( GameObject block in adjacentMatchingBlocks)
                    {
                        Destroy(block);
                    }
                    Destroy(gameObject);
                }

            }
        }

        void OnDestroy()
        {
            foreach (GameObject deletable in tempBlocks)
            {
                Destroy(deletable);
            }
        }

        void addBlocks()
        {
            if (transform.position.x - 120 > Screen.width / -2)
            {
                tempBlocks.Add(Instantiate(tempBlock, new Vector3(transform.position.x - 120, transform.position.y, 10), Quaternion.identity));
            }
                
            if (transform.position.x + 120 < Screen.width / 2)
            {
                tempBlocks.Add(Instantiate(tempBlock, new Vector3(transform.position.x + 120, transform.position.y, 10), Quaternion.identity));
            }
            tempBlocks.Add(Instantiate(tempBlock, new Vector3(transform.position.x, transform.position.y + 120, 10), Quaternion.identity));
        }

        void toStatic()
        {
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            isStatic = true;
        }


    }
}
