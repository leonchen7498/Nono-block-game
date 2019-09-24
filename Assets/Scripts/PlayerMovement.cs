using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        [Range(100, 1000)]
        public float movementSpeed;

        Animator animator;
        Rigidbody2D body;
        // have to add "new" otherwise it'll confuse this renderer with the deprecated Component.renderer
        new SpriteRenderer renderer;
        new BoxCollider2D collider;
        CircleCollider2D blockRangeCollider;

        private Vector3 touchPosition;
        private Vector3 flyingGoal;
        private Vector3 moveDirection;
        private float distanceToGoal;
        private float previousDistanceToGoal;

        private bool isStandingOnBlock;
        private bool isFlying;
        private bool isMoving;
        private bool isFalling;

        // the amount of time the robot will show it's flying animation before going back to normal
        public float timeToFloat;
        private float timeLeftFloating;

        // the amount of time the robot will keep the hold animation before going back to normal
        public float timeToHold;
        // public static because PlayerDrop needs to know if the player is still in the hold animation
        public static float timeLeftHolding;

        // the amount of time the robot will keep the move animation before going back to normal
        public float timeToMove;
        private float timeLeftMoving;

        private float defaultGravity;
        private bool blockPlaceConfirmed;

        private bool touchedTheGround;

        // Start is called before the first frame update
        public void Start()
        {
            body = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            renderer = GetComponent<SpriteRenderer>();
            collider = GetComponent<BoxCollider2D>();
            blockRangeCollider = GetComponent<CircleCollider2D>();
            isFlying = false;
            isMoving = false;
            isFalling = false;
            touchedTheGround = true;

            defaultGravity = body.gravityScale;
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            // Check if collision isnt because the player is on top of the block
            if ((collider.bounds.center.y - collider.bounds.size.y / 2) < collision.collider.bounds.center.y &&
                !isFlying && touchedTheGround)
            {
                if (timeLeftFloating <= 0)
                {
                    animator.SetTrigger("transform_flying");
                }

                touchedTheGround = false;
                timeLeftFloating = 0;
                body.velocity = new Vector2(0f, 140f);
                flyingGoal = transform.position;
                flyingGoal.y = flyingGoal.y + 120f;

                distanceToGoal = 0;
                previousDistanceToGoal = (flyingGoal - transform.position).magnitude;

                isFlying = true;
                isMoving = false;
            }
        }

        // Update is called once per frame
        public void Update()
        {
            distanceToGoal = getDistance();

            checkIfFalling();

            if (LevelController.moveToPosition)
            {
                LevelController.moveToPosition = false;
                onTouch();
            }

            if (LevelController.touchedScreen)
            {
                LevelController.touchedScreen = false;
                checkIfPlayerIsCloseToPlaceHolderBlock();
                if (!LevelController.readyToPlace)
                {
                    onTouch();
                    blockPlaceConfirmed = true;
                }
            }

            if (((Input.GetMouseButtonDown(0) && Application.isEditor) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)) 
                && LevelController.draggingBlock == null)
            {
                Vector2 touchPositionToWorld;

                if (Application.isEditor)
                {
                    touchPositionToWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
                else
                {
                    touchPositionToWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                }

                RaycastHit2D[] hits = Physics2D.RaycastAll(touchPositionToWorld, Vector2.zero);

                foreach (RaycastHit2D hit in hits)
                {
                    if (hit.collider != blockRangeCollider && !hit.collider.gameObject.name.Contains("Falling") && hit.collider.gameObject != this &&
                        !hit.collider.gameObject.name.Contains("Placeholder"))
                    {
                        onTouch();
                    }
                }

                if (hits.Length == 0)
                {
                    onTouch();
                }
            }

            if (!touchedTheGround && !isFlying)
            {
                if (checkIfOnGround(0))
                {
                    touchedTheGround = true;
                }
            }

            checkTimers();

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
                        timeLeftMoving = timeToMove;
                    }
                    isMoving = false;
                    body.velocity = new Vector2(0f, 0f);
                }
                if (isFlying)
                {
                    isFlying = false;
                    startMoving();
                    timeLeftFloating = timeToFloat;
                }
            }

            // If the player clicked on a placeholder then move towards that location, if the player is close enough it'll start placing that block
            if (blockPlaceConfirmed)
            {
                checkIfPlayerIsCloseToPlaceHolderBlock();
            }

            //Checks the distance again between the player and touch position
            previousDistanceToGoal = getDistance();
        }

        void checkTimers()
        {
            if (timeLeftMoving > 0)
            {
                timeLeftMoving -= Time.deltaTime;

                if (timeLeftMoving < 0)
                {
                    animator.SetTrigger("transform_move");
                    timeLeftMoving = 0;
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

            if (timeLeftHolding > 0)
            {
                timeLeftHolding -= Time.deltaTime;

                if (timeLeftHolding < 0)
                {
                    animator.SetTrigger("transform_hold");
                    timeLeftHolding = 0;
                }
            }
        }
        
        void checkIfFalling()
        {
            if (!isFalling && !isFlying && timeLeftFloating <= 0)
            {
                bool onGroundLeft = checkIfOnGround(-collider.bounds.size.x / 2 - 20f);
                bool onGroundRight = checkIfOnGround(collider.bounds.size.x / 2 + 20f);

                if (!onGroundLeft && !onGroundRight)
                {
                    isFalling = true;
                    body.velocity = Vector2.zero;
                    body.gravityScale = 50f;
                }
            }

            if (isFalling)
            {
                bool onGroundLeft = checkIfOnGround(-collider.bounds.size.x / 2 + 20f);
                bool onGroundRight = checkIfOnGround(collider.bounds.size.x / 2 - 20f);

                if (onGroundLeft || onGroundRight)
                {
                    body.gravityScale = defaultGravity;
                    isFalling = false;
                    if (isMoving)
                    {
                        startMoving();
                    }
                }
            }
        }

        bool checkIfOnGround(float extraPositionX)
        {
            RaycastHit2D[] groundHits = Physics2D.RaycastAll(new Vector2(collider.bounds.center.x + extraPositionX,
                    collider.bounds.center.y - collider.bounds.size.y + 40f), Vector2.zero);

            foreach (RaycastHit2D groundHit in groundHits)
            {
                if (groundHit.collider.name.Contains("Block") || groundHit.collider.name.Contains("foreground"))
                {
                    return true;
                }
            }

            return false;
        }


        void checkIfPlayerIsCloseToPlaceHolderBlock()
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(LevelController.blockToPlacePosition, Vector2.zero);

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider == blockRangeCollider)
                {
                    blockPlaceConfirmed = false;
                    LevelController.readyToPlace = true;
                    timeLeftHolding = timeToHold;

                    distanceToGoal = 0;
                    previousDistanceToGoal = 0;
                    body.velocity = Vector2.zero;

                    if (isMoving && timeLeftFloating <= 0)
                    {
                        animator.SetTrigger("transform_move");
                    }
                    else if (isFlying || timeLeftFloating > 0)
                    {
                        animator.SetTrigger("transform_flying");
                        isFlying = false;
                        timeLeftFloating = 0;
                    }
                    isMoving = false;
                }
            }
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

            if (!isMoving && !isFlying && timeLeftMoving <= 0)
            {
                // so the player's move animation doesn't disappear when the robot is already moving
                animator.SetTrigger("transform_move");
            }

            // Have to determine if the touch position is left or right from the sprite position
            if (!isFlying && !isFalling)
            {
                distanceToGoal = 0;
                previousDistanceToGoal = 0;
                startMoving();

                if (blockPlaceConfirmed)
                {
                    blockPlaceConfirmed = false;
                }
            }
        }

        void startMoving()
        {
            moveDirection = (touchPosition - transform.position).normalized;
            if (moveDirection.x >= 0)
            {
                body.velocity = new Vector2(movementSpeed, 0f);
            }
            else
            {
                body.velocity = new Vector2(-movementSpeed, 0f);
            }

            timeLeftMoving = 0;
            isMoving = true;
        }

        /**
         * Gets the position of the touch and corrects the position in case its out of bounds
         */
        private void getTouchPosition()
        {
            if (Application.isEditor)
            {
                touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            else
            {
                touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            }
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
