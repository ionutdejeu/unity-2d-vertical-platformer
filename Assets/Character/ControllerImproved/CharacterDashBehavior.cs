using Assets.Character.Controller;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static CharacterController2D;

namespace Assets.Character.ControllerImproved
{
    public class CharacterDashBehavior : MonoBehaviour, ICharacterBehavior
    {

        private Vector2 _lastComputedSpeed = Vector2.zero;
        [SerializeField] private int dashDuration = 2;
        [SerializeField] private float dashSpeed = 10f;
        [SerializeField] private int cooldown = 1;
        CharacterEvents charEvents;

        public UnityEvent<float> onDashCooldownUpdate = new UnityEvent<float>();
        float last_computed_progress = 0;

        private long dashUntilTicks = 0;
        private bool isDashing = false;
        private Vector2 dashDirection;
        private Vector2 speedPrevFrame;


        [SerializeField] private int dashCooldown = 2;
        private long canDashAfter,dashStartedOn = 0;

        public void Start()
        {
            charEvents = GetComponent<CharacterEvents>();
        }


        private bool continueDashing(long currentTimestamp)
        {
            return dashUntilTicks > currentTimestamp;
        }

        /*
         * Support for dash cooldown
         */
        private bool canDash(long lastDashTimestamp,long currentTimestamp, int cooldown)
        {
            return lastDashTimestamp == 0 || (currentTimestamp > (lastDashTimestamp+ 1000000 * cooldown));
        }

        private bool canDashAfterJump(CustomCharacterState state)
        {
            return true;
        }

        private float colldownCounter(long canDashAfter,long currentTimestamp,long dashStartedOn)
        {
            if (canDashAfter == 0)
            {
                return 0;
            }
            //if(currentTimestamp> canDashAfter)
            //{
            //    return 0;
            //}
            return Mathf.Clamp((float)(dashStartedOn - currentTimestamp) / (float)(canDashAfter - dashStartedOn),0,1);
        }

        public Vector2 ComputeBehavior(Vector2 currentSpeed, CustomCharacterState state)
        {
            // need to handle dash after jump to stop the jumping
            float a = Input.GetAxis("Fire3");
            if (a > 0 && !isDashing && canDash(dashUntilTicks, DateTime.UtcNow.Ticks, cooldown))
            {
                dashStartedOn = DateTime.UtcNow.Ticks;
                dashUntilTicks = DateTime.UtcNow.Ticks + 1000000 * dashDuration;
                canDashAfter = dashStartedOn + 1000000 * dashCooldown;
                float direction = Vector2.Dot(speedPrevFrame, new Vector2(1, 0));
                direction = direction > 0 ? 1 : -1;
                dashDirection = new Vector2(direction,0);
                isDashing = true;

            }
            else if(isDashing && !continueDashing(DateTime.UtcNow.Ticks))
            {
                isDashing = false;
            }

            if (isDashing && continueDashing(DateTime.UtcNow.Ticks))
            {
                state.isDashing = true;
                state.isJumping = false;
                _lastComputedSpeed = dashDirection * dashSpeed * Time.deltaTime;
            }
            else
            {
                state.isDashing = false;
                state.controlEnabled = state.gravityEnabled = !state.isDashing;
                
                _lastComputedSpeed = currentSpeed;
            }
            float curr_progress = colldownCounter(canDashAfter, DateTime.UtcNow.Ticks, dashStartedOn);
            if (curr_progress != last_computed_progress)
            {
                onDashCooldownUpdate.Invoke(curr_progress);
                last_computed_progress = curr_progress;
            }
            state.controlEnabled = state.gravityEnabled = !state.isDashing;
            speedPrevFrame = new Vector2(currentSpeed.x, currentSpeed.y).normalized;

            return _lastComputedSpeed;
        }
    }
}
