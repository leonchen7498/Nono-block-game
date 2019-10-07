using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Tappable : MonoBehaviour
    {

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (LevelController.allowedToChangeModes && LevelController.getBlockAmount(tag) != "0")
            {
                Vector2 position = LevelController.getTouch();

                if (position != Vector2.zero)
                {
                    bool hitSomething = false;
                    RaycastHit2D[] hits = Physics2D.RaycastAll(position, Vector2.zero);

                    foreach (RaycastHit2D hit in hits)
                    {
                        if (hit.collider.gameObject == gameObject)
                        {
                            if (LevelController.currentBlock == gameObject)
                            {
                                LevelController.currentBlock = null;
                            }
                            else
                            {
                                LevelController.currentBlock = gameObject;
                            }
                        }

                        if (!hit.collider.name.Contains("foreground"))
                        {
                            hitSomething = true;
                        }
                    }

                    if (!hitSomething)
                    {
                        LevelController.currentBlock = null;
                    }
                }
            }
        }
        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
    }
}
