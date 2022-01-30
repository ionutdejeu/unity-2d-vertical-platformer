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


        [SerializeField]
        public Vector2 _previousComputedSpeed = Vector2.zero;

        private long jumpUntillInTicks = 0;
              
        public bool canDoubleJump(CustomCharacterState state, long lastJumpTimestamp, long nowTimestamp)
        {
            return state.remainingJumps > 0;
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
            return new Vector2(direction, 1);
        }
        
        
        public Vector2 ComputeBehavior(Vector2 currentSpeed, CustomCharacterState state)
        {

            bool canJump = state.jumpEnabled;
            bool jumpCanStart = state.isGrounded || state.isJumping || state.isTouchingWall;
 
            float a = Input.GetAxis("Jump");
            bool jumpBtnPressed = a > 0;
            bool newJumpStarted = !state.isJumping && jumpBtnPressed;

            jumpUntillInTicks = canJump && jumpCanStart && jumpBtnPressed && newJumpStarted ? DateTime.UtcNow.Ticks : jumpUntillInTicks;
            lastTImestampJumpButtonWasPressed = jumpBtnPressed ? DateTime.UtcNow.Ticks : lastTImestampJumpButtonWasPressed;

            bool continueCurrentJump = canJump && jumpCanStart && jumpBtnPressed && continueJumping(jumpUntillInTicks, DateTime.UtcNow.Ticks);
            bool stopCurrentJumping = canJump && jumpCanStart && jumpBtnPressed  && !continueJumping(jumpUntillInTicks, DateTime.UtcNow.Ticks);


            //double jump loigc 

            bool canPerformDoubleJump = newJumpStarted && canDoubleJump(state,jumpUntillInTicks, DateTime.UtcNow.Ticks);
            bool didDoubleJump = canPerformDoubleJump;

            bool jumpOffWall = state.isTouchingWall && state.isJumping;
            bool slideWall = state.isTouchingWall && !state.isJumping;
            Debug.Log(canPerformDoubleJump);

            _previousComputedSpeed.y = jumpForce * a * Convert.ToInt32(canJump && jumpCanStart && (continueCurrentJump|| canPerformDoubleJump));


            state.isJumping = continueCurrentJump && !stopCurrentJumping;
            state.isJumpingOffWall = state.isTouchingWall && state.isJumping;
            state.isSlidingWall = state.isTouchingWall && !state.isJumping;

            
            return _previousComputedSpeed;
        }
    }
}
