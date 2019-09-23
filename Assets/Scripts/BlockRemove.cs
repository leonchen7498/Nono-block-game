using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRemove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "yellowMatch" || other.gameObject.tag == "blueMatch" || other.gameObject.tag == "redMatch")
        {
            Destroy(other.gameObject);
        }
    }
}
