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

        [SerializeField]
        public Vector2 _previousComputedSpeed = Vector2.zero;

        private long jumpUntillInTicks = 0;
        


      
         
 
        public Vector2 ComputeBehavior(Vector2 currentSpeed, CustomCharacterState state)
        {
            if (state.controlEnabled && (state.isGrounded || state.isJumping || state.isTouchingWall))
            {

                float a = Input.GetAxis("Jump");
                _previousComputedSpeed.y = jumpForce * a;
                if (a > 0)
                {
                    if (!state.isJumping)
                    {
                        jumpUntillInTicks = DateTime.UtcNow.Ticks + 5000000;
                    }
                    state.isJumping = true;
                    if (jumpUntillInTicks < DateTime.UtcNow.Ticks)
                    {
                        state.isJumping = false;
                        Debug.Log("Timestamp :" + jumpUntillInTicks + " is reached");
                    }

                }
                else
                { state.isJumping = false; }
                if (state.isTouchingWall && state.isJumping)
                {
                    Debug.Log("Jump off wall");
                    state.isJumpingOffWall = true;
                }
                else
                {
                    state.isJumpingOffWall = false;
                }

                if (state.isTouchingWall && !state.isJumping)
                {
                    state.isSlidingWall = true;
                }
                else
                {
                    state.isSlidingWall = false;
                }
            }
            else
            {
                _previousComputedSpeed.y = 0;
            }
            return _previousComputedSpeed;
        }
    }
}
