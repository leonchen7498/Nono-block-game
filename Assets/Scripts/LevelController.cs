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
        public static Vector2 moveToPosition;
        public static bool touchedPlaceholder;
        public GameObject menuButton;
        public GameObject Timer;
        public int seconds;

        // Start is called before the first frame update
        void Start()
        {
            if (menuButton != null) {
                Instantiate(menuButton, new Vector3(482, 1147, 0), Quaternion.identity);
            }
            blockToPlacePosition = Vector3.zero;
            carryingBlock = null;
            draggingBlock = null;
            currentLevel = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
            GameObject newTimer = Instantiate(Timer, new Vector3(0, 0, 0), Quaternion.identity);
            newTimer.GetComponentInChildren<LevelTimer>().seconds = seconds; 
        }

        // Update is called once per frame
        void Update()
        {

        }

        public static Vector2 getTouch()
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    return Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                }
            }
            else if (Input.GetMouseButtonDown(0))
            {
                return Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            return Vector2.zero;
        }
    }
}
