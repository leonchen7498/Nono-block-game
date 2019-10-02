using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Pause : MonoBehaviour
    {
        public static GameObject[] menu;
        public GameObject menuPrefab;
        // Start is called before the first frame update
        void Start()
        {
            Time.timeScale = 1;
            Instantiate(menuPrefab, new Vector3(210f, 288f, -90), Quaternion.identity);
            menu = GameObject.FindGameObjectsWithTag("menu");
            foreach (GameObject item in menu)
            {
                item.SetActive(false);
            }

        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnMouseDown()
        {
            foreach (GameObject item in menu)
            {
                item.SetActive(true);
            }
            Time.timeScale = 0;
        }
    }
}
