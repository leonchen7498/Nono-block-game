using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts {
    public class SelectBlock : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (LevelController.currentBlock != null)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
