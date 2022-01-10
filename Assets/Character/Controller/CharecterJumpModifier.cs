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
    [SerializeField] private float jumpDuration = 10f;   // Amount of time that the force is being applied
    private long applyForceUntil;
    public override Vector2 Value { get { return _previousComputedSpeed; } }

    [SerializeField]
    public Vector2 _previousComputedSpeed = Vector2.zero;

    [SerializeField] private CharacterHandler charHandler;
    [SerializeField] private CharacterPhysics charPhysics;



    // Start is called before the first frame update
    void Start()
    {
       
    }

    private void OnEnable()=>charHandler.AddModifier(this);
    private void OnDisable() => charHandler.RemoveModifier(this);


    // Update is called once per frame
    void FixedUpdate()
    {
        if (charPhysics.m_Grounded)
        {
            float a = Input.GetAxis("Jump");
            _previousComputedSpeed.y = jumpForce * a;
            applyForceUntil = DateTime.UtcNow.Ticks+100;
        }
        else
        {
            _previousComputedSpeed.y = 0;
        }
    }
}
