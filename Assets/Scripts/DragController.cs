﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class DragController : MonoBehaviour
    {

        public static bool isDragging;
        public static string carryingBlock;
        public static GameObject draggingBlock;
        public static Vector3 blockToPlacePosition;
        public static bool readyToPlace;
        public static bool justPlaced;

        // Start is called before the first frame update
        void Start()
        {
            carryingBlock = string.Empty;
            blockToPlacePosition = Vector3.zero;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
