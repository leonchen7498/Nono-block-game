using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDropper : MonoBehaviour
{
    public GameObject yellowBlock;
    public GameObject redBlock;
    public GameObject blueBlock;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("DropBlock", 3f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DropBlock()
    {
        System.Random rnd = new System.Random();
        switch(rnd.Next(1, 4))
        {
            case 1:
                Instantiate(yellowBlock, new Vector3(rnd.Next(-400, 400), 921, 0), Quaternion.identity);
                break;
            case 2:
                Instantiate(redBlock, new Vector3(rnd.Next(-400, 400), 921, 0), Quaternion.identity);
                break;
            case 3:
                Instantiate(blueBlock, new Vector3(rnd.Next(-400, 400), 921, 0), Quaternion.identity);
                break;
        }
    }
}
