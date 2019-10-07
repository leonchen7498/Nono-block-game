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
                if (block != null)
                {
                    if (block.transform.position.x == gameObject.transform.position.x &&
                        Mathf.Abs(block.transform.position.y - gameObject.transform.position.y) <= 120)
                    {
                        block.GetComponent<MatchThree>().toStatic();
                    }
                    else if (Mathf.Abs(block.transform.position.y - gameObject.transform.position.y) <= 60)
                    {
                        float yPosition = Mathf.Round(gameObject.transform.position.y / 120.0f) * 120;
                        block.transform.position = new Vector2(block.transform.position.x, yPosition);
                        gameObject.transform.position = new Vector2(gameObject.transform.position.x, yPosition);
                        block.GetComponent<MatchThree>().toStatic();
                    }
                }
            }

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
            if (transform.position.x - 120 > -500)
            {
                tempBlocks.Add(Instantiate(tempBlock, new Vector3(transform.position.x - 120, transform.position.y, 0), Quaternion.identity));
            }
                
            if (transform.position.x + 120 < 500)
            {
                tempBlocks.Add(Instantiate(tempBlock, new Vector3(transform.position.x + 120, transform.position.y, 0), Quaternion.identity));
            }

            if (transform.position.y + 120 < 1000)
            {
                tempBlocks.Add(Instantiate(tempBlock, new Vector3(transform.position.x, transform.position.y + 120, 0), Quaternion.identity));
            }
        }

        void toStatic()
        {
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            isStatic = true;
        }


    }
}
