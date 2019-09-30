using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class ToHighestLevel : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnMouseUp()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(LevelController.highestLevel);
        }
    }
}
