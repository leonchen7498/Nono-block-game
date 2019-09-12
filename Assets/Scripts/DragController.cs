using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class DragController : MonoBehaviour
    {

        public static bool isDragging;
        public static string carryingBlock;
        public static GameObject draggingBlock;
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
