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
        public static string currentLevel;
        public static string highestLevel;
        // Start is called before the first frame update
        void Start()
        {
            carryingBlock = null;
            currentLevel = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
