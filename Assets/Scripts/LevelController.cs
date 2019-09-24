using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class LevelController : MonoBehaviour
    {
        public static GameObject carryingBlock;
        public static GameObject draggingBlock;
        public static Vector3 blockToPlacePosition;
        public static bool readyToPlace;
        public static bool justPlaced;
        public static string currentLevel;
        public static string highestLevel;
        public static bool moveToPosition;
        public static bool touchedScreen;

        // Start is called before the first frame update
        void Start()
        {
            blockToPlacePosition = Vector3.zero;
            carryingBlock = null;
            draggingBlock = null;
            currentLevel = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
