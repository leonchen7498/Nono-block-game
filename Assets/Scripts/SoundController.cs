using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class SoundController : MonoBehaviour
    {
        public static bool blockPlaced;
        public static bool glassPlaced;
        public static bool metalPlaced;
        public static bool slimePlaced;
        public static bool glassBroken;
        public static bool robotIsDriving;
        public AudioSource blockPlaceSound;
        public AudioSource glassPlaceSound;
        public AudioSource metalPlaceSound;
        public AudioSource slimePlaceSound;
        public AudioSource robotMoveSound;
        public AudioSource glassBreakSound;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (blockPlaced)
            {
                blockPlaceSound.Play();
                blockPlaced = false;
            }

            if (glassPlaced)
            {
                glassPlaceSound.Play();
                glassPlaced = false;
            }

            if (metalPlaced)
            {
                metalPlaceSound.Play();
                metalPlaced = false;
            }

            if (slimePlaced)
            {
                slimePlaceSound.Play();
                slimePlaced = false;
            }

            if (glassBroken)
            {
                glassBreakSound.Play();
                glassBroken = false;
            }

            if (robotIsDriving)
            {
                robotMoveSound.Play();
            }
            else if (robotMoveSound.isPlaying && !robotIsDriving)
            {
                robotMoveSound.Stop();
            }
        }
    }
}
