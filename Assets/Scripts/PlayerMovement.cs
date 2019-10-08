using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Assets.Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        [Range(100, 1000)]
        public float movementSpeed;

        Animator animator;
        Rigidbody2D body;
        new BoxCollider2D collider;

        private Vector3 touchPosition;
        private Vector3 flyingGoal;
        private Vector3 moveDirection;
        private float distanceToGoal;
        private float previousDistanceToGoal;

        private bool isFlying;
        private bool isMoving;
        public bool isFalling;

        // the amount of time the robot will show it's flying animation before going back to normal
        public float timeToFloat;
        private float timeLeftFloating;

        // the amount of time the robot will keep the move animation before going back to normal
        public float timeToMove;
        private float timeLeftMoving;

        private float defaultGravity;
        private bool touchedTheGround;

        private bool buildPhase;
        private Vector3 touchPositionAfterFlying;

        // Start is called before the first frame update
        public void Start()
        {
            body = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            collider = GetComponent<BoxCollider2D>();
            isFlying = false;
            isMoving = false;
            isFalling = false;
            touchedTheGround = true;

            touchPositionAfterFlying = Vector3.zero;
            defaultGravity = body.gravityScale;
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            // Check if collision isnt because the player is on top of the block
            if ((collider.bounds.center.y - collider.bounds.size.y / 2) < collision.collider.bounds.center.y &&
                !isFlying && touchedTheGround)
            {
                bool blockAboveOther = false;

                if (touchPositionAfterFlying == Vector3.zero)
                {
                    Vector2 colliderPosition = collision.collider.bounds.center;
                    colliderPosition.y += 120;
                    RaycastHit2D[] hits = Physics2D.RaycastAll(colliderPosition, Vector2.zero);
                    
                    foreach (RaycastHit2D hit in hits)
                    {
                        if (hit.collider.name.Contains("Block") || hit.collider.name.Contains("foreground"))
                        {
                            blockAboveOther = true;
                        }
                    }
                }

                if (!blockAboveOther)
                {
                    if (timeLeftFloating <= 0)
                    {
                        animator.SetTrigger("transform_flying");
                    }

                    buildPhase = true;
                    touchedTheGround = false;
                    timeLeftFloating = 0;
                    body.velocity = new Vector2(0f, 140f);
                    flyingGoal = transform.position;
                    flyingGoal.y += 125f;

                    distanceToGoal = 0;
                    previousDistanceToGoal = (flyingGoal - transform.position).magnitude;

                    isFlying = true;
                    isMoving = false;
                }
                else
                {
                    Vector3 position = transform.position;

                    if (transform.position.x > collider.bounds.center.x)
                    {
                        position.x += 5f;
                    }
                    else
                    {
                        position.x -= 5f;
                    }

                    touchPosition = position;
                    distanceToGoal = 0;
                    previousDistanceToGoal = (touchPosition - transform.position).magnitude;
                    startMoving();
                }
            }
        }

        // Update is called once per frame
        public void Update()
        {
            if (LevelController.currentBlock != null)
            {
                if (!buildPhase)
                {
                    buildPhase = true;
                }
            } else
            {
                if (buildPhase)
                {
                    buildPhase = false;
                }
            }

            distanceToGoal = getDistance();
            checkIfFalling();

            if (!buildPhase || (buildPhase && touchPosition == Vector3.zero))
            {
                onTouch();
            }
            checkTimers();

            if (!touchedTheGround && !isFlying)
            {
                if (checkIfOnGround(0))
                {
                    touchedTheGround = true;
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
                        timeLeftMoving = timeToMove;
                    }
                    isMoving = false;
                    body.velocity = new Vector2(0f, 0f);
                    touchPosition = Vector3.zero;
                }
                if (isFlying)
                {
                    if (buildPhase && touchPositionAfterFlying == Vector3.zero)
                    {
                        timeLeftFloating = timeToFloat;
                    }
                    else if (buildPhase && touchPositionAfterFlying != Vector3.zero)
                    {
                        timeLeftFloating = 0.1f;
                        touchPosition = touchPositionAfterFlying;
                        touchPositionAfterFlying = Vector3.zero;
                    }
                    else
                    {
                        timeLeftFloating = timeToFloat;
                    }

                    distanceToGoal = 0;
                    previousDistanceToGoal = (touchPosition - transform.position).magnitude;
                    isFlying = false;
                    startMoving();
                }
            }

            //Checks the distance again between the player and touch position
            previousDistanceToGoal = getDistance();

            if (LevelController.stopMoving && touchPositionAfterFlying == Vector3.zero && timeLeftFloating <= 0)
            {
                LevelController.stopMoving = false;
                touchPosition = Vector3.zero;
                body.velocity = new Vector2(0f, 0f);

                if (isFlying)
                {
                    animator.SetTrigger("transform_flying");
                }
                else if (isMoving || timeLeftMoving > 0)
                {
                    animator.SetTrigger("transform_move");
                }

                isMoving = false;
                isFlying = false;
                timeLeftFloating = 0;
                timeLeftMoving = 0;
                distanceToGoal = 0;
                previousDistanceToGoal = 0;
            }
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
                    if (!LevelController.stopMoving)
                    {
                        animator.SetTrigger("transform_move");
                    }
                    else
                    {
                        touchPosition = Vector3.zero;
                    }
                    timeLeftFloating = 0;
                }
            }
        }
        
        void checkIfFalling()
        {
            if (!isFalling && !isFlying && timeLeftFloating <= 0)
            {
                bool onGroundLeft = checkIfOnGround(-collider.bounds.size.x / 2 - 5f);
                bool onGroundRight = checkIfOnGround(collider.bounds.size.x / 2 + 5f);

                if (!onGroundLeft && !onGroundRight)
                {
                    isFalling = true;
                    body.velocity = Vector2.zero;
                    body.gravityScale = 50f;
                }
            }

            if (isFalling)
            {
                bool onGroundLeft = checkIfOnGround(-collider.bounds.size.x / 2);
                bool onGroundRight = checkIfOnGround(collider.bounds.size.x / 2);

                if (onGroundLeft || onGroundRight)
                {
                    body.gravityScale = defaultGravity;
                    isFalling = false;
                    if (isMoving && touchPosition != Vector3.zero)
                    {
                        startMoving();
                    }
                    else if (!isMoving && touchPosition != Vector3.zero)
                    {
                        animator.SetTrigger("transform_move");
                    }
                }
            }
        }

        bool checkIfOnGround(float extraPositionX)
        {
            RaycastHit2D[] groundHits = Physics2D.RaycastAll(new Vector2(collider.bounds.center.x + extraPositionX,
                    collider.bounds.center.y - collider.bounds.size.y / 2 - 10f), Vector2.zero);

            foreach (RaycastHit2D groundHit in groundHits)
            {
                if ((groundHit.collider.name.Contains("Block") && groundHit.collider.GetType() == typeof(BoxCollider2D)) 
                    || groundHit.collider.name.Contains("foreground"))
                {
                    return true;
                }
            }

            return false;
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
            Vector2 position = LevelController.getTouch();

            if (position != Vector2.zero)
            {
                Vector3 oldTouchPosition = touchPosition;
                getTouchPosition(position);

                if (touchPosition != oldTouchPosition)
                {
                    //Check if x is out of bounds
                    if (touchPosition.x > 500)
                    {
                        touchPosition.x = 500;
                    }
                    else if (touchPosition.x < -500)
                    { 
                        touchPosition.x = -500;
                    }
                    //corrects the position, otherwise the player will move to the right of the touch position
                    touchPosition.x -= collider.bounds.size.x / 2 + 30;

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
                    }
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
        private void getTouchPosition(Vector2 position)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(position, Vector2.zero);
            Vector2 placeholderOnPlayerPosition = Vector2.zero;

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.gameObject.layer == 5)
                {
                    return;
                }

                if (buildPhase &&
                    hit.collider.gameObject.name.Contains("Placeholder") &&
                    hit.collider.gameObject.GetComponent<PlaceBlock>().player != null &&
                    collider.bounds.center.y - 30 <= hit.collider.gameObject.GetComponent<BoxCollider2D>().bounds.center.y + 60)
                {
                    placeholderOnPlayerPosition = hit.collider.bounds.center;
                }
            }

            //This decides which direction the robot decides to go when the player wants to place a block on the robot.
            if (placeholderOnPlayerPosition != Vector2.zero)
            {
                Vector3 RIGHT = new Vector3(540, transform.position.y);
                Vector3 LEFT = new Vector3(-540, transform.position.y);

                bool dangerLeft = checkIfItHitsObject(new Vector2(placeholderOnPlayerPosition.x - 120, placeholderOnPlayerPosition.y), new string[] {"No-NoNo"}); 
                bool dangerRight = checkIfItHitsObject(new Vector2(placeholderOnPlayerPosition.x + 120, placeholderOnPlayerPosition.y), new string[] {"No-NoNo"});
                bool dangerBottomLeft = checkIfItHitsObject(new Vector2(placeholderOnPlayerPosition.x - 120, placeholderOnPlayerPosition.y - 120), new string[] {"No-NoNo"});
                bool dangerBottomRight = checkIfItHitsObject(new Vector2(placeholderOnPlayerPosition.x + 120, placeholderOnPlayerPosition.y - 120), new string[] {"No-NoNo"});

                bool sawBottomLeft = checkIfItHitsObject(new Vector2(placeholderOnPlayerPosition.x - 120, placeholderOnPlayerPosition.y - 120), new string[] { "Saw" });
                bool sawBottomRight = checkIfItHitsObject(new Vector2(placeholderOnPlayerPosition.x + 120, placeholderOnPlayerPosition.y - 120), new string[] { "Saw" });
                bool sawBottomBottomLeft = checkIfItHitsObject(new Vector2(placeholderOnPlayerPosition.x - 120, placeholderOnPlayerPosition.y - 240), new string[] { "Saw" });
                bool sawBottomBottomRight = checkIfItHitsObject(new Vector2(placeholderOnPlayerPosition.x + 120, placeholderOnPlayerPosition.y - 240), new string[] { "Saw" });

                if (sawBottomLeft)
                {
                    dangerLeft = true;
                }
                if (sawBottomRight)
                {
                    dangerRight = true;
                }
                if (sawBottomBottomLeft)
                {
                    dangerBottomLeft = true;
                }
                if (sawBottomBottomRight)
                {
                    dangerBottomRight = true;
                }

                bool hitLeft = checkIfItHitsObject(new Vector2(placeholderOnPlayerPosition.x - 120, placeholderOnPlayerPosition.y), new string[] {"foreground","Block","Saw"});
                bool hitRight = checkIfItHitsObject(new Vector2(placeholderOnPlayerPosition.x + 120, placeholderOnPlayerPosition.y), new string[] { "foreground", "Block","Saw"});
                bool hitTop = checkIfItHitsObject(new Vector2(placeholderOnPlayerPosition.x, placeholderOnPlayerPosition.y + 120), new string[] { "foreground", "Block","Saw"});
                bool hitBottomLeft = checkIfItHitsObject(new Vector2(placeholderOnPlayerPosition.x - 120, placeholderOnPlayerPosition.y - 120), new string[] { "foreground", "Block","Saw"});
                bool hitBottomRight = checkIfItHitsObject(new Vector2(placeholderOnPlayerPosition.x + 120, placeholderOnPlayerPosition.y - 120), new string[] { "foreground", "Block","Saw"});

                if ((dangerLeft && dangerRight) || (dangerBottomLeft && dangerBottomRight))
                {
                    //je hebt jezelf ingebouwd
                }
                else if (hitLeft && hitRight)
                {
                    if (hitTop)
                    {
                        //je hebt jezelf hier ook ingebouwd
                    }
                    else if (dangerLeft)
                    {
                        touchPosition = RIGHT;
                        touchPositionAfterFlying = LEFT;
                    }
                    else if (dangerRight)
                    {
                        touchPosition = LEFT;
                        touchPositionAfterFlying = RIGHT;
                    }
                    else
                    {
                        touchPosition = position.x < 0 ? RIGHT : LEFT;
                        touchPositionAfterFlying = position.x > 0 ? RIGHT : LEFT;
                    }
                } 
                else if (hitLeft)
                {
                    if (dangerLeft)
                    {
                        touchPosition = RIGHT;
                    }
                    else if (!hitTop)
                    {
                        touchPosition = LEFT;
                        touchPositionAfterFlying = RIGHT;
                    }
                    else
                    {
                        touchPosition = RIGHT;
                        touchPositionAfterFlying = LEFT;
                    }
                } 
                else if (hitRight)
                {
                    if (dangerRight)
                    {
                        touchPosition = LEFT;
                    }
                    else if (!hitTop)
                    {
                        touchPosition = RIGHT;
                        touchPositionAfterFlying = LEFT;
                    }
                    else
                    {
                        touchPosition = LEFT;
                    }
                }
                else
                {
                    if (dangerBottomLeft || dangerLeft)
                    {
                        touchPosition = RIGHT;
                    }
                    else if (dangerBottomRight || dangerRight)
                    {
                        touchPosition = LEFT;
                    }
                    else if (hitBottomLeft && !hitBottomRight)
                    {
                        touchPosition = LEFT;
                    }
                    else if (hitBottomRight && !hitBottomLeft)
                    {
                        touchPosition = RIGHT;
                    }
                    else if (placeholderOnPlayerPosition.x + 60 < collider.bounds.center.x + collider.bounds.size.x / 2 &&
                        placeholderOnPlayerPosition.x + 60 > collider.bounds.center.x - collider.bounds.size.x / 2)
                    {
                        touchPosition = RIGHT;
                    }
                    else if (placeholderOnPlayerPosition.x - 60 < collider.bounds.center.x + collider.bounds.size.x / 2 &&
                        placeholderOnPlayerPosition.x - 60 > collider.bounds.center.x - collider.bounds.size.x / 2)
                    {
                        touchPosition = LEFT;
                    }
                    else
                    {
                        touchPosition = position.x < 0 ? RIGHT : LEFT;
                    }
                }
            }
            else if (!buildPhase)
            {
                touchPosition = position;
                touchPosition.z = 0;
            }
        }

        bool checkIfItHitsObject(Vector2 position, string[] objectsToCheck)
        {
            if (position.x < -540 || position.x > 540)
            {
                return true;
            }

            RaycastHit2D[] hits = Physics2D.RaycastAll(position, Vector2.zero);
            ArrayList objectList = new ArrayList();
            objectList.AddRange(objectsToCheck);

            foreach(RaycastHit2D hit in hits)
            {
                foreach(string objectToCheck in objectList)
                {
                    if (hit.collider.gameObject.name.Contains(objectToCheck))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
