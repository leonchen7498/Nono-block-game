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
        private bool isStatic;

        // Start is called before the first frame update
        void Start()
        {
            addBlocks();
        }

        // Update is called once per frame
        void Update()
        {
            if (!isStatic)
            {
                movePlaceholderToBlock();
                checkIfBlockIsSameAsThisBlock(this.transform.position.x - 120);
                checkIfBlockIsSameAsThisBlock(this.transform.position.x + 120);
            }
        }

        void checkIfBlockIsSameAsThisBlock(float positionX)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(new Vector2(positionX, transform.position.y), Vector2.zero);

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.tag == this.gameObject.tag)
                {
                    OnTriggerEnter2D(hit.collider);
                    return;
                }
            }
        }

        void movePlaceholderToBlock()
        {
            foreach (GameObject block in tempBlocks)
            {
                float differenceX = Mathf.Abs(block.transform.position.x - transform.position.x);
                float differenceY = Mathf.Abs(block.transform.position.y - transform.position.y);

                if (block.transform.position.x < -500 || block.transform.position.x > 500 ||
                    block.transform.position.y > 1000)
                {
                    block.SetActive(false);
                }
                else if (block.activeSelf == false)
                {
                    block.SetActive(true);
                }

                if (differenceY != 120 && differenceX == 0)
                {
                    if (block.transform.position.y > transform.position.y)
                    {
                        block.transform.position = new Vector3(transform.position.x, transform.position.y + 120, 10);
                    }
                    else
                    {
                        block.transform.position = new Vector3(transform.position.x, transform.position.y - 120, 10);
                    }
                }
                else if (differenceX == 120)
                {
                    if (block.transform.position.x > transform.position.x)
                    {
                        block.transform.position = new Vector3(transform.position.x + 120, transform.position.y, 10);
                    }
                    else
                    {
                        block.transform.position = new Vector3(transform.position.x - 120, transform.position.y, 10);
                    }
                }
            }
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            // Debug-draw all contact points and normals
            if (this.gameObject.GetComponent<BoxCollider2D>().attachedRigidbody == collider.attachedRigidbody || 
                this.gameObject.GetComponent<PolygonCollider2D>().attachedRigidbody == collider.attachedRigidbody || 
                this.tag != collider.gameObject.tag)
            {
            }
            else
            {
                foreach(GameObject block in adjacentMatchingBlocks)
                {
                    if (block.transform.position == collider.gameObject.transform.position)
                    {
                        return;
                    }
                }

                if (collider.gameObject != null &&
                    Mathf.Abs(collider.gameObject.transform.position.x - gameObject.transform.position.x) <= 3)
                {
                    adjacentMatchingBlocks.Add(collider.gameObject);
                    if (collider.gameObject.name.Contains("Glass") && adjacentMatchingBlocks.Count == 1)
                    {
                        adjacentMatchingBlocks.ForEach(deleteBlock => Destroy(deleteBlock));
                        SoundController.glassBroken = true;
                        return;
                    }
                    else
                    {
                        float blockPositionY = Mathf.Round(collider.gameObject.transform.position.y / 120.0f) * 120;
                        collider.gameObject.transform.position = new Vector2(collider.gameObject.transform.position.x, blockPositionY);
                        collider.gameObject.GetComponent<MatchThree>().toStatic();
                    }
                }
                else if (collider.gameObject != null && 
                    Mathf.Abs(collider.gameObject.transform.position.y - gameObject.transform.position.y) <= 60)
                {
                    adjacentMatchingBlocks.Add(collider.gameObject);
                    float yPosition = Mathf.Round(gameObject.transform.position.y / 120.0f) * 120;
                    collider.gameObject.transform.position = new Vector2(collider.gameObject.transform.position.x, yPosition);
                    collider.gameObject.GetComponent<MatchThree>().toStatic();
                }

                if (adjacentMatchingBlocks.Count >= 2)
                {
                    foreach (GameObject block in adjacentMatchingBlocks)
                    {
                        if (block.name.Contains("Glass") && adjacentMatchingBlocks.Count == 1)
                        {
                            SoundController.glassBroken = true;
                        }
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

            foreach (GameObject deletable in adjacentMatchingBlocks)
            {
                Destroy(deletable);
            }
        }

        void addBlocks()
        {
            tempBlocks.Add(Instantiate(tempBlock, new Vector3(transform.position.x - 120, transform.position.y, 10), Quaternion.identity));
            tempBlocks.Add(Instantiate(tempBlock, new Vector3(transform.position.x + 120, transform.position.y, 10), Quaternion.identity));
            tempBlocks.Add(Instantiate(tempBlock, new Vector3(transform.position.x, transform.position.y + 120, 10), Quaternion.identity));
            tempBlocks.Add(Instantiate(tempBlock, new Vector3(transform.position.x, transform.position.y - 120, 10), Quaternion.identity));
        }

        void toStatic()
        {
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            movePlaceholderToBlock();
            isStatic = true;
        }
    }
}
