using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace Assets.Scripts {
    public class PopulateGrid : MonoBehaviour
    {
        public GameObject prefab; // This is our prefab object that will be exposed in the inspector

        public int levels; // number of objects to create. Exposed in inspector

        void Start()
        {
            Populate();
        }

        void Update()
        {

        }

        void Populate()
        {
            GameObject newButton; // Create GameObject instance

            for (int i = 1; i <= levels; i++)
            {
                if (i <= Convert.ToInt32(DragController.highestLevel)) {
                    // Create new instances of our prefab until we've created as many as we specified
                    newButton = Instantiate(prefab, transform);
                    newButton.GetComponent<selectLevel>().scene = i.ToString();
                    newButton.GetComponentInChildren<TextMeshProUGUI>().text = i.ToString();
                } else
                {
                    newButton = Instantiate(prefab, transform);
                    newButton.GetComponentInChildren<TextMeshProUGUI>().text = "locked";
                }

            }

        }
    }
}