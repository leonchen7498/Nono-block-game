using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block_placeholder : MonoBehaviour
{
    public GameObject placeholder;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();

        Vector3 placeholderPosition = renderer.bounds.center;
        placeholderPosition.y += 120f;

        setPlaceholderAtPosition(placeholderPosition);

        /*placeholderPosition = renderer.bounds.center;
        placeholderPosition.y -= 120f;

        setPlaceholderAtPosition(placeholderPosition);*/

        placeholderPosition = renderer.bounds.center;
        placeholderPosition.x -= 120f;

        if (placeholderPosition.x > Screen.width / -2)
        {
            setPlaceholderAtPosition(placeholderPosition);
        }

        placeholderPosition = renderer.bounds.center;
        placeholderPosition.x += 120f;

        if (placeholderPosition.x < Screen.width / 2)
        {
            setPlaceholderAtPosition(placeholderPosition);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setPlaceholderAtPosition(Vector3 position)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(position, Vector2.zero);

        if (hits.Length == 0)
        {
            Instantiate(placeholder, position, Quaternion.identity);
        }
        else
        {
            bool hitSomething = false;

            foreach(RaycastHit2D hit in hits)
            {
                if (!hit.collider.tag.Contains("player"))
                {
                    hitSomething = true;
                }
            }

            if (!hitSomething)
            {
                Instantiate(placeholder, position, Quaternion.identity);
            }
        }
    }
}
