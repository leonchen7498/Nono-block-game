﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Finish : MonoBehaviour
    {
        public string scene;

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
            if (collision.gameObject.tag == "player")
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
            }
        }
    }
}
