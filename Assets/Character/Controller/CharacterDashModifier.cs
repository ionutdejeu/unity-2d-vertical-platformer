using Assets.Character.Controller;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDashModifier : MovementModifier
{
    public override Vector2 Value { get { return _lastComputedSpeed; } }
    
    private Vector2 _lastComputedSpeed = Vector2.zero;
    [SerializeField] private CharacterHandler charHandler;
    [SerializeField] private CharacterPhysics charPhysics;
    [SerializeField] private CharacterState charState;
    [SerializeField] private int dashDuration = 2;
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private Vector2 dashDirection = Vector2.zero;


    private long dashUntilTicks = 0;
    private void OnEnable() => charHandler.AddModifier(this);
    private void OnDisable() => charHandler.RemoveModifier(this);

    void Update()
    {
        float a = Input.GetAxis("Fire1");
        if(a > 0)
        {
            charState.isDashing = true;
            dashUntilTicks = DateTime.UtcNow.Ticks + 1000000*dashDuration;

        }
        if (charState.isDashing && 
            (charState.isJumping || charState.isJumpingOffWall || charState.isTouchingWall))
        {
            charState.isDashing = false;

        }
    }

    private void FixedUpdate()
    {
        if (charState.isDashing && dashUntilTicks < DateTime.UtcNow.Ticks)
        {
            charState.isDashing = true;
            _lastComputedSpeed = Vector2.one * dashSpeed * Time.deltaTime;
        }
        else
        {
            charState.isDashing = false;
            _lastComputedSpeed = Vector2.zero;
        }
    }

    public override Vector2 AddMovementValue(Vector2 currValue)
    {
        return Vector2.zero;
    }
}
