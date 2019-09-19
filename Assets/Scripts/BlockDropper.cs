using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDropper : MonoBehaviour
{
    public GameObject yellowBlock;
    public GameObject redBlock;
    public GameObject blueBlock;
    public GameObject yellowTimerBlock;
    public GameObject redTimerBlock;
    public GameObject blueTimerBlock;
    public GameObject slimeBlock;
    public GameObject ironBlock;
    public GameObject glassBlock;

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
        switch(rnd.Next(1, 20))
        {
            case 1:
            case 2:
            case 3:
            case 4:
                Instantiate(yellowBlock, new Vector3(rnd.Next(-400, 400), 921, 0), Quaternion.identity);
                break;
            case 5:
            case 6:
            case 7:
            case 8:
                Instantiate(redBlock, new Vector3(rnd.Next(-400, 400), 921, 0), Quaternion.identity);
                break;
            case 9:
            case 10:
            case 11:
            case 12:
                Instantiate(blueBlock, new Vector3(rnd.Next(-400, 400), 921, 0), Quaternion.identity);
                break;
            case 13:
                Instantiate(slimeBlock == null ? blueBlock : slimeBlock , new Vector3(rnd.Next(-400, 400), 921, 0), Quaternion.identity);
                break;
            case 14:
                Instantiate(ironBlock == null ? redBlock : ironBlock, new Vector3(rnd.Next(-400, 400), 921, 0), Quaternion.identity);
                break;
            case 15:
            case 16:
                Instantiate(glassBlock == null ? yellowBlock : glassBlock, new Vector3(rnd.Next(-400, 400), 921, 0), Quaternion.identity);
                break;
            case 17:
                Instantiate(blueTimerBlock == null ? blueBlock : blueTimerBlock, new Vector3(rnd.Next(-400, 400), 921, 0), Quaternion.identity);
                break;
            case 18:
                Instantiate(yellowTimerBlock == null ? redBlock : redTimerBlock, new Vector3(rnd.Next(-400, 400), 921, 0), Quaternion.identity);
                break;
            case 19:
                Instantiate(redTimerBlock == null ? yellowBlock : yellowTimerBlock, new Vector3(rnd.Next(-400, 400), 921, 0), Quaternion.identity);
                break;
        }
    }
}
