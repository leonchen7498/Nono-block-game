using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sticky : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.transform.position.y <= gameObject.transform.position.y || gameObject.transform.position.x + -10 < collision.gameObject.transform.position.x && collision.gameObject.transform.position.x < gameObject.transform.position.x + +10)
        {
            if (collision.gameObject.tag == "yellowMatch" || collision.gameObject.tag == "redMatch" || collision.gameObject.tag == "blueMatch") {
                collision.attachedRigidbody.bodyType = RigidbodyType2D.Static;
            }
        }
    }
}
