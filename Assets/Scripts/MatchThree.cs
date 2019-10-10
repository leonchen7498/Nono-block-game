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
            if (this.gameObject.GetComponent<BoxCollider2D>().attachedRigidbody == collider.attachedRigidbody || this.gameObject.GetComponent<PolygonCollider2D>().attachedRigidbody == collider.attachedRigidbody || this.tag != collider.gameObject.tag)
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

                adjacentMatchingBlocks.Add(collider.gameObject);

                if (adjacentMatchingBlocks.Count >= 2)
                {
                    foreach (GameObject block in adjacentMatchingBlocks)
                    {
                        Destroy(block);
                    }
                    Destroy(gameObject);
                }

                foreach (GameObject block in adjacentMatchingBlocks)
                {
                    if (block != null)
                    {
                        if (block.transform.position.x == gameObject.transform.position.x)
                        {
                            float blockPositionY = Mathf.Round(block.transform.position.y / 120.0f) * 120;
                            block.transform.position = new Vector2(block.transform.position.x, blockPositionY);
                            block.GetComponent<MatchThree>().toStatic();
                        }
                        else if (Mathf.Abs(block.transform.position.y - gameObject.transform.position.y) <= 60)
                        {
                            float yPosition = Mathf.Round(gameObject.transform.position.y / 120.0f) * 120;
                            block.transform.position = new Vector2(block.transform.position.x, yPosition);
                            block.GetComponent<MatchThree>().toStatic();
                        }
                    }
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
