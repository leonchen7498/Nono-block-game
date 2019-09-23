using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts {
    public class CheckSave : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            if (DragController.highestLevel == null)
            {
                DragController.highestLevel = "1";
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
