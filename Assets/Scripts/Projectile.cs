using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public enum direction { Up, Left, Down, Right }
    public direction facing;

    // Start is called before the first frame update
    void Start()
    {
        if (facing == direction.Up)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 500);
        }
        else if (facing == direction.Left)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-500, 0);
        }
        else if (facing == direction.Down)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, -500);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(500, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
