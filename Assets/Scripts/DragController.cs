using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class DragController : MonoBehaviour
    {
        [HideInInspector]
        public static bool isDragging;
        public static string carryingBlock;
        // Start is called before the first frame update
        void Start()
        {
            carryingBlock = "red";
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
