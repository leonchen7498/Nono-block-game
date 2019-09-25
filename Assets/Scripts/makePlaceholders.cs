using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makePlaceholders : MonoBehaviour
{
    public GameObject placeholder;
    // Start is called before the first frame update
    void Start()
    {
        for (float x = 0; x < 9; x++)
        {
            Instantiate(placeholder, new Vector3(-480f + 120f * x, -840, 0), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
