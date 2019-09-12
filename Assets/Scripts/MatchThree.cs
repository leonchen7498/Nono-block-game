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
        // Start is called before the first frame update
        void Start()
        {
            tempBlocks.Add(Instantiate(tempBlock, new Vector3(transform.position.x + 120, transform.position.y, 10), Quaternion.identity));
            tempBlocks.Add(Instantiate(tempBlock, new Vector3(transform.position.x, transform.position.y + 120, 10), Quaternion.identity));
            tempBlocks.Add(Instantiate(tempBlock, new Vector3(transform.position.x - 120, transform.position.y, 10), Quaternion.identity));
            setStatic = false;
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

            if (tempBlocks[0].transform.position.y > gameObject.transform.position.y)
            {
                foreach (GameObject block in tempBlocks)
                {
                    if (block.transform.position.x == gameObject.transform.position.x)
                    {
                        block.transform.position = new Vector3(block.transform.position.x, gameObject.transform.position.y + 120, 10);
                    } else {
                        block.transform.position = new Vector3(block.transform.position.x, gameObject.transform.position.y, 10);
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

        void toStatic()
        {
                gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }


    }
}
