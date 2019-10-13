using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class LevelController : MonoBehaviour
    {
        public static GameObject currentBlock;
        public static string currentLevel;
        public static string highestLevel;
        public static bool allowedToChangeModes;
        public ParticleSystem mouseClickParticle;

        public GameObject menuButton;
        public int yellowBlockCount;
        public static int yellowBlockAmount;
        public int redBlockCount;
        public static int redBlockAmount;
        public int blueBlockCount;
        public static int blueBlockAmount;
        public int slimeBlockCount;
        public static int slimeBlockAmount;
        public int ironBlockCount;
        public static int ironBlockAmount;
        public int glassBlockCount;
        public static int glassBlockAmount;
        public static GameObject lastPlacedBlock;
        public GameObject selectionMenu;
        public GameObject buildModeOverlay;

        public static bool stopMoving;

        // Start is called before the first frame update
        void Start()
        {
            if (MainMenuMusic.instance != null && MainMenuMusic.instance.GetComponent<AudioSource>().isPlaying)
            {
                MainMenuMusic.instance.GetComponent<AudioSource>().Stop();
            }

            if (menuButton != null) {
                Instantiate(menuButton, new Vector3(432.5f, 1058.6f, 0), Quaternion.identity);
            }
            Instantiate(selectionMenu, new Vector3(3, 200, -1), Quaternion.identity);
            buildModeOverlay = Instantiate(buildModeOverlay);

            allowedToChangeModes = true;
            currentBlock = null;
            currentLevel = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
            yellowBlockAmount = yellowBlockCount;
            redBlockAmount = redBlockCount;
            blueBlockAmount = blueBlockCount;
            ironBlockAmount = ironBlockCount;
            slimeBlockAmount = slimeBlockCount;
            glassBlockAmount = glassBlockCount;
        }

        // Update is called once per frame
        void Update()
        {
            Vector2 position = getTouch();

            if (position != Vector2.zero)
            {
                ParticleSystem placeParticle = Instantiate(mouseClickParticle, position, Quaternion.identity);
                placeParticle.Play();
            }

            if (currentBlock == null && buildModeOverlay.activeSelf == true)
            {
                buildModeOverlay.SetActive(false);
            }
            else if (currentBlock != null && buildModeOverlay.activeSelf == false)
            {
                buildModeOverlay.SetActive(true);
            }
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

        public static string getBlockAmount(string countTag)
        {
            switch(countTag)
            {
                case "yellow_tag":
                    return yellowBlockAmount.ToString();
                case "blue_tag":
                    return blueBlockAmount.ToString();
                case "red_tag":
                    return redBlockAmount.ToString();
                case "slime_tag":
                    return slimeBlockAmount.ToString();
                case "iron_tag":
                    return ironBlockAmount.ToString();
                case "glass_tag":
                    return glassBlockAmount.ToString();
                case "yellow_tag_timer":
                    return yellowBlockAmount.ToString();
                case "blue_tag_timer":
                    return yellowBlockAmount.ToString();
                case "red_tag_timer":
                    return yellowBlockAmount.ToString();
            }
            return yellowBlockAmount.ToString();
        }
    }
}
