using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player" && !collision.isTrigger)
        {
            var list = GameObject.FindGameObjectsWithTag("door");
            anim.SetTrigger("Press");
            foreach (GameObject block in list)
            {
                Destroy(block);
            }
        }
    }
}
