using Assets.Character.Controller;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static CharacterController2D;

namespace Assets.Character.ControllerImproved
{
    public class CharacterJumpBehavior : MonoBehaviour, ICharacterBehavior
    {

        [SerializeField] private float jumpForce = 10f;      // Amount of force added when the player jumps.
        [SerializeField] private long lastTImestampJumpButtonWasPressed=0;      // Amount of force added when the player jumps.
        private bool jumpBtnPressedPreviousFrame = false;
        private Vector2 jumpDirection = Vector2.up;
        private bool didJumpOffWall = false;
        private bool didSimpleOrDoubleJump = true;
        public UnityEvent<float> onCooldownProgressEvent = new UnityEvent<float>();


        void Start()
        {
           
        }


        [SerializeField]
        public Vector2 _previousComputedSpeed = Vector2.zero;

        private long jumpUntillInTicks = 0;
              
        public bool canDoubleJump(CustomCharacterState state, bool canJump)
        {
            return state.remainingJumps > 0 && (state.isJumping || canJump);
        }
        public bool continueJumping(long lastJumpTimestamp,long nowTimestamp)
        {
            return lastJumpTimestamp + 5000000 > nowTimestamp;
        }


        public Vector2 GetVerticalJumpSpeed(float force,float input,Vector2 currentSpeed)
        {
            currentSpeed.y = force * input;
            return currentSpeed;
        }

        public Vector2 GetWallJumpingSpeed(float direction, float force, float input)
        {
            //45 degree angle jump 
            return new Vector2(direction, direction).normalized;
        }

        float canDoubleJumpCooldown()
        {
            return 0f;
        } 
        


        public Vector2 ComputeBehavior(Vector2 currentSpeed, CustomCharacterState state)
        {

            bool canJump = state.jumpEnabled;
            bool jumpCanStart = state.isGrounded || state.isJumping || state.isTouchingWall;
 
            float a = Input.GetAxis("Jump");
            bool jumpBtnPressed = a > 0;

            // need to handle continous button press 
            // need to handl jump button tap 
            bool jumpButtonPressedContiously = jumpBtnPressedPreviousFrame && jumpBtnPressed;
            bool jumpButtonTap = !jumpBtnPressedPreviousFrame && jumpBtnPressed;


            
            // when a new jump is started the previous state needs not be jumping 
            bool newJumpStarted = !state.isJumping && jumpBtnPressed;

            // update the timestamp for last jump duration when there's a new jump started 
            jumpUntillInTicks = canJump && jumpCanStart && newJumpStarted ? DateTime.UtcNow.Ticks : jumpUntillInTicks;
            lastTImestampJumpButtonWasPressed = jumpBtnPressed ? DateTime.UtcNow.Ticks : lastTImestampJumpButtonWasPressed;

            bool continueCurrentJump = canJump && jumpCanStart && jumpBtnPressed && continueJumping(jumpUntillInTicks, DateTime.UtcNow.Ticks);
            bool stopCurrentJumping = canJump && jumpCanStart && jumpBtnPressed  && !continueJumping(jumpUntillInTicks, DateTime.UtcNow.Ticks);


            //double jump logic 
            bool canDoubleJ = !state.isGrounded && (stopCurrentJumping || newJumpStarted);


            bool didDoubleJump = canDoubleJ && jumpButtonTap;


            bool jumpOffWall = state.isTouchingWall && state.isJumping;
            bool slideWall = state.isTouchingWall && !state.isJumping;

            didJumpOffWall = jumpOffWall;
            didSimpleOrDoubleJump = (newJumpStarted || didDoubleJump) && !didJumpOffWall;
            if (didSimpleOrDoubleJump) {
                jumpDirection = Vector2.up;
            }
            if(didJumpOffWall)
            {
                jumpDirection = Vector2.right* Convert.ToInt32(state.isTouchingWallOnLeft)
               + Vector2.left * Convert.ToInt32(state.isTouchingWallOnRight);
                jumpDirection.y = 1;

            }
            _previousComputedSpeed = jumpDirection;

            _previousComputedSpeed *=jumpForce * a * Convert.ToInt32(
                canJump && (jumpCanStart||didDoubleJump) && (continueCurrentJump|| didDoubleJump)
                );
            jumpUntillInTicks = didDoubleJump ? DateTime.UtcNow.Ticks : jumpUntillInTicks;

            state.isJumping = (continueCurrentJump || didDoubleJump) && !stopCurrentJumping ;
            state.isJumpingOffWall = state.isTouchingWall && state.isJumping;
            state.isSlidingWall = state.isTouchingWall && !state.isJumping;
            jumpBtnPressedPreviousFrame = jumpBtnPressed;
            
            return _previousComputedSpeed;
        }
    }
}
