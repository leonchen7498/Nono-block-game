﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts
{
    public class BreakGlass : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "actual_glass")
            {
                Destroy(collision.gameObject.transform.parent.gameObject);
                SoundController.glassBroken = true;
            }
        }
    }
}
