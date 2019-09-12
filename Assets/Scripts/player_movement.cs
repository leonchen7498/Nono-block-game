using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts
{
    public class player_movement : MonoBehaviour
    {
        [Range(100, 1000)]
        public float movementSpeed;
        public LayerMask layer;

        Rigidbody2D body;
        Animator animator;
        // have to add "new" otherwise it'll confuse this renderer with the deprecated Component.renderer
        new SpriteRenderer renderer;
        new BoxCollider2D collider;

        private Vector3 touchPosition;
        private Vector3 flyingGoal;
        private float distanceToGoal;
        private float previousDistanceToGoal;

        private bool isStandingOnBlock;
        private bool isFlying;
        private bool isMoving;

        // the amount of time the robot will show it's flying animation before going back to normal
        public float timeToFloat = 3f;
        private float timeLeftFloating;
        private float defaultGravity;

        public void OnCollisionEnter2D(Collision2D collision)
        {
            // Check if collision isnt because the player is on top of the block
            if (collision.collider.name != "foreground_game" &&
                collision.contacts[0].point.y < collision.collider.bounds.center.y)
            {
                if (collision.contacts[0].point.x < collision.collider.bounds.center.x ||
                    collision.contacts[0].point.x > collision.collider.bounds.center.x)
                {
                    timeLeftFloating = 0;
                    animator.SetTrigger("transform_flying");
                    body.velocity = new Vector2(0f, 140f);
                    flyingGoal = transform.position;
                    flyingGoal.y = flyingGoal.y + 120f;

                    distanceToGoal = 0;
                    previousDistanceToGoal = (flyingGoal - transform.position).magnitude;

                    isFlying = true;
                    isMoving = false;
                }
            }
        }

        // Start is called before the first frame update
        public void Start()
        {
            body = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            renderer = GetComponent<SpriteRenderer>();
            collider = GetComponent<BoxCollider2D>();

            defaultGravity = body.gravityScale;
        }

        // Update is called once per frame
        public void Update()
        {
            distanceToGoal = getDistance();

            /*if (!Physics2D.Raycast(transform.position, Vector2.down, 1f) &&
                !isFlying)
            {
                Debug.Log("niks");
            }*/

            if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
            {
                Vector2 touchPositionToWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(touchPositionToWorld, Vector2.zero);
                if (hit.collider != null)
                {
                    Debug.Log(hit.collider.gameObject.name);
                    if (!hit.collider.gameObject.name.Contains("Falling"))
                    {
                        onTouch();
                    }
                }
                else
                {
                    onTouch();
                }
            }
        
            if (timeLeftFloating > 0)
            {
                timeLeftFloating -= Time.deltaTime;

                if (timeLeftFloating < 0)
                {
                    animator.SetTrigger("transform_move");
                    timeLeftFloating = 0;
                }
            }

            // If the player managed to reach the touch position and starts moving past the touch position
            // it will check if the player didnt move too far. If he did move too far then stop moving
            if (distanceToGoal > previousDistanceToGoal)
            {
                if (isMoving)
                {
                    if (timeLeftFloating > 0)
                    {
                        animator.SetTrigger("transform_flying");
                        timeLeftFloating = 0;
                    }
                    else
                    {
                        animator.SetTrigger("transform_move");
                    }
                    isMoving = false;
                    body.velocity = new Vector2(0f, 0f);
                }
                if (isFlying)
                {
                    isFlying = false;
                    isMoving = true;
                    Vector3 moveDirection = (touchPosition - transform.position).normalized;
                    body.velocity = new Vector2(moveDirection.x * movementSpeed, 0f);
                    timeLeftFloating = Time.deltaTime;
                }
            }

            //Checks the distance again between the player and touch position
            previousDistanceToGoal = getDistance();
        }

        /*
         * Checks the distance between the touch position and player, always a positive number
         */
        private float getDistance()
        {
            if (isMoving)
            {
                return Mathf.Abs(touchPosition.x - transform.position.x);
            }
            else if (isFlying)
            {
                return Mathf.Abs(flyingGoal.y - transform.position.y);
            }

            return 0;
        }

        /*
         * Starts moving the player around when the user touches the screen
         */
        private void onTouch()
        {
            getTouchPosition();

            if (!isMoving && !isFlying)
            {
                // so the player's move animation doesn't disappear when the robot is already moving
                animator.SetTrigger("transform_move");
            }

            // Have to determine if the touch position is left or right from the sprite position
            if (!isFlying)
            {
                distanceToGoal = 0;
                previousDistanceToGoal = 0;
                Vector3 moveDirection = (touchPosition - transform.position).normalized;
                body.velocity = new Vector2(moveDirection.x * movementSpeed, 0f);
                isMoving = true;
            }
        }

        /**
         * Gets the position of the touch and corrects the position in case its out of bounds
         */
        private void getTouchPosition()
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
}
