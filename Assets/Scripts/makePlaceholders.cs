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
            RaycastHit2D[] hits = Physics2D.RaycastAll(new Vector2(-480f + 120f * x, -840), Vector2.zero);
            bool hitSomething = false;

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.name.Contains("foreground") || hit.collider.name.Contains("Saw"))
                {
                    hitSomething = true;
                }
            }

            if (!hitSomething)
            {
                Instantiate(placeholder, new Vector2(-480f + 120f * x, -840), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
