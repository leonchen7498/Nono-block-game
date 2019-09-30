using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class BlockAmount : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            TMPro.TextMeshProUGUI text = GetComponentInChildren<TMPro.TextMeshProUGUI>();
            if (text != null)
            {
                text.text = LevelController.getBlockAmount(tag);
            }
        }
    }
}
