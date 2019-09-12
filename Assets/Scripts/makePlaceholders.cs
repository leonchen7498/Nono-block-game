using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makePlaceholders : MonoBehaviour
{
    public GameObject placholder;
    // Start is called before the first frame update
    void Start()
    {
        for (float x = 0; x < 9; x++)
        {
            Instantiate(placholder, new Vector3(-478f + 122f * x, -776.1018f, 0), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
