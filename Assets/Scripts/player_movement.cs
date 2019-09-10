using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    [Range(100, 1000)]
    public float movementSpeed;

    Rigidbody2D body;
    Animator animator;
    private Vector3 touchPosition;
    private bool isMoving;
    private float distanceToGoal;
    private float previousDistanceToGoal;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            distanceToGoal = (touchPosition - transform.position).magnitude;
        }

        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("transform_move");

            distanceToGoal = 0;
            previousDistanceToGoal = 0;
            touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            touchPosition.z = 0f;

            Vector3 movePosition = (touchPosition - transform.position).normalized;
            body.velocity = new Vector2(movePosition.x * movementSpeed, 0f);
            isMoving = true;
        }

        if (distanceToGoal > previousDistanceToGoal && isMoving)
        {
            animator.SetTrigger("transform_move");
            isMoving = false;
            body.velocity = new Vector2(0f, 0f);
        }

        if (isMoving)
        {
            previousDistanceToGoal = (touchPosition - transform.position).magnitude;
        }
    }
}
