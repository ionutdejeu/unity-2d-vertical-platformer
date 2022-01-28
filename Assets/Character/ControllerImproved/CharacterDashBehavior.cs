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


        private long dashUntilTicks = 0;
        private bool isDashing = false;
        

        public Vector2 ComputeBehavior(Vector2 currentSpeed, CustomCharacterState state)
        {
            float a = Input.GetAxis("Fire1");
            if (a > 0 && !state.isDashing)
            {
                dashUntilTicks = DateTime.UtcNow.Ticks + 10000000 * dashDuration;
                isDashing = true;
            }
            else
            {
                isDashing = false;
            }

            Debug.Log(isDashing);
            if (isDashing && dashUntilTicks > DateTime.UtcNow.Ticks)
            {
                state.isDashing = true;
                state.controlEnabled = false;
                Debug.Log("Dash timestamp :" + dashUntilTicks + " is reached");
                _lastComputedSpeed = currentSpeed * dashSpeed * Time.deltaTime;
            }
            else
            {
                state.isDashing = false;
                state.controlEnabled = true;
                _lastComputedSpeed = Vector2.zero;
            }
            Debug.Log(_lastComputedSpeed);
            return _lastComputedSpeed;
        }
    }
}
