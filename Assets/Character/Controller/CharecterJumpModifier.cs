using Assets.Character.Controller;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class handling jumping 
 * 
 * 
 */
public class CharecterJumpModifier : MovementModifier
{
    [SerializeField] private float jumpForce = 10f;      // Amount of force added when the player jumps.
    public override Vector2 Value { get { return _previousComputedSpeed; } }

    [SerializeField]
    public Vector2 _previousComputedSpeed = Vector2.zero;

    [SerializeField] private CharacterHandler charHandler;
    [SerializeField] private CharacterPhysics charPhysics;
    [SerializeField] private CharacterState charState;

    private long jumpUntillInTicks = 0;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    private void OnEnable()=>charHandler.AddModifier(this);
    private void OnDisable() => charHandler.RemoveModifier(this);


    // Update is called once per frame
    void FixedUpdate()
    {   
        if (charPhysics.m_Grounded || charState.isJumping || charState.isTouchingWall) 
        {

            float a = Input.GetAxis("Jump");
            _previousComputedSpeed.y = jumpForce * a;
            if(a > 0)
            {
                if (!charState.isJumping)
                {
                    jumpUntillInTicks = DateTime.UtcNow.Ticks + 5000000;
                }
                charState.isJumping = true;
                if (jumpUntillInTicks < DateTime.UtcNow.Ticks)
                {
                    charState.isJumping = false;
                    Debug.Log("Timestamp :" + jumpUntillInTicks + " is reached");
                }
               
            }
            else
            { charState.isJumping = false; }
            if (charState.isTouchingWall && charState.isJumping)
            {
                Debug.Log("Jump off wall");
                charState.isJumpingOffWall = true;
            }
            else
            {
                charState.isJumpingOffWall = false;
            }

            if (charState.isTouchingWall && !charState.isJumping)
            {
                charState.isSlidingWall = true;
            }
            else
            {
                charState.isSlidingWall = false;
            }
        }
        else
        {
            _previousComputedSpeed.y = 0;
        }


    }
    
}
