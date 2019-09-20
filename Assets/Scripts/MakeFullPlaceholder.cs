using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeFullPlaceholder : MonoBehaviour
{
    public GameObject tempBlock;
    // Start is called before the first frame update
    void Start()
    {
        var pos = gameObject.GetComponent<BoxCollider2D>().bounds.center;
        Instantiate(tempBlock, new Vector3(pos.x + 120, pos.y, 10), Quaternion.identity);
        Instantiate(tempBlock, new Vector3(pos.x, pos.y + 120, 10), Quaternion.identity);
        Instantiate(tempBlock, new Vector3(pos.x - 120, pos.y, 10), Quaternion.identity);
        Instantiate(tempBlock, new Vector3(pos.x, pos.y - 120, 10), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
