﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block_placeholder : MonoBehaviour
{
    public GameObject placeholder;

    // Start is called before the first frame update
    void Start()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();

        Vector3 placeholderPosition = collider.bounds.center;
        placeholderPosition.y += 120f;

        if (placeholderPosition.y < 1000)
        {
            setPlaceholderAtPosition(placeholderPosition);
        }

        placeholderPosition = collider.bounds.center;
        placeholderPosition.y -= 120f;

        setPlaceholderAtPosition(placeholderPosition);

        placeholderPosition = collider.bounds.center;
        placeholderPosition.x -= 120f;

        if (placeholderPosition.x > -500)
        {
            setPlaceholderAtPosition(placeholderPosition);
        }

        placeholderPosition = collider.bounds.center;
        placeholderPosition.x += 120f;

        if (placeholderPosition.x < 500)
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
        Vector3 placeholderPosition = new Vector3(position.x, position.y, 10f);

        if (hits.Length == 0)
        {
            Instantiate(placeholder, placeholderPosition, Quaternion.identity);
        }
        else
        {
            bool hitSomething = false;

            foreach(RaycastHit2D hit in hits)
            {
                if (hit.collider.name.Contains("foreground") || hit.collider.name.Contains("Flag"))
                {
                    hitSomething = true;
                }
            }

            if (!hitSomething)
            {
                Instantiate(placeholder, placeholderPosition, Quaternion.identity);
            }
        }
    }
}
