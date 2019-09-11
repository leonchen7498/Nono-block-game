using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    [Range(100, 1000)]
    public float movementSpeed;

    Rigidbody2D body;
    Animator animator;
    // have to add "new" otherwise it'll confuse this renderer with the deprecated Component.renderer
    new SpriteRenderer renderer;
    private Vector3 touchPosition;

    private bool isFlying;
    private bool isMoving;
    private float distanceToGoal;
    private float previousDistanceToGoal;

    void OnCollisionEnter2D(Collision2D collision)
    {
        isFlying = true;

        if(collision.collider.name != "foreground_game")
        {
            // Check if collision isnt because the player is on top of the block
            if (collision.contacts[0].point.y < collision.collider.bounds.center.y)
            {
                if (collision.contacts[0].point.x < collision.collider.bounds.center.x)
                {
                    Debug.Log("left");
                }
                else if (collision.contacts[0].point.x > collision.collider.bounds.center.x)
                {
                    Debug.Log("right");
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            //Checks the distance between the touch position and player, always a positive number
            distanceToGoal = (touchPosition - transform.position).magnitude;
        }

        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            onTouch();
        }

        // If the player managed to reach the touch position and starts moving past the touch position
        // it will check if the player didnt move too far. If he did move too far then stop moving
        if (distanceToGoal > previousDistanceToGoal && isMoving)
        {
            animator.SetTrigger("transform_move");
            isMoving = false;
            body.velocity = new Vector2(0f, 0f);
        }

        if (isMoving)
        {
            //Checks the distance again between the player and touch position
            previousDistanceToGoal = (touchPosition - transform.position).magnitude;
        }
    }

    /*
     * Starts moving the player around when the user touches the screen
     */
    void onTouch()
    {
        if (!isMoving)
        {
            // so the player's move animation doesn't disappear when the robot is already moving
            animator.SetTrigger("transform_move");
        }

        distanceToGoal = 0;
        previousDistanceToGoal = 0;
        getTouchPosition();

        // Have to determine if the touch position is left or right from the sprite position
        Vector3 movePosition = (touchPosition - transform.position).normalized;
        body.velocity = new Vector2(movePosition.x * movementSpeed, 0f);
        isMoving = true;
    }

    /**
     * Gets the position of the touch and corrects the position in case its out of bounds
     */
    void getTouchPosition()
    {
        //Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        touchPosition.z = 0f;

        //Check if x is out of bounds
        if (touchPosition.x > (Screen.width - renderer.bounds.size.x) / 2)
        {
            touchPosition.x = (Screen.width - renderer.bounds.size.x) / 2;
        }
        else if (touchPosition.x < (Screen.width - renderer.bounds.size.x) / -2)
        {
            touchPosition.x = (Screen.width - renderer.bounds.size.x) / -2;
        }

        //corrects the position, otherwise the player will move to the right of the touch position
        touchPosition.x -= renderer.bounds.size.x / 2;
    }
}
