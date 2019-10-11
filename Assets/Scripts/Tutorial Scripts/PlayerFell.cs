using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFell : MonoBehaviour
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
        if (other.gameObject.tag == "player")
        {
            other.gameObject.transform.position = new Vector3(-531, -311, 0);
        }
    }
}
