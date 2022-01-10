using Assets.Character.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CharacterController2D;

/**
 * This class computes the speed of the player 
 * Handle wall collision speed calculation 
 * Handle ground collision speed calculation 
 */
public class CharacterSpeedModifier : MovementModifier
{

    
    public override Vector2 Value { get { return _lastComputedSpeed; } }

    [SerializeField] private CharacterHandler charHandler;
    [SerializeField] private CharacterPhysics charPhysics;
    private WalkDirection m_WalkDirection = WalkDirection.Right;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private float baseSpeed = 10f;
    [SerializeField] private Vector2 _lastComputedSpeed = Vector2.zero;
    [SerializeField] private Vector2 m_Velocity = Vector2.zero;

    private void OnEnable() => charHandler.AddModifier(this);
    private void OnDisable() => charHandler.RemoveModifier(this);

    private bool isGrounded = false;
    private bool wasGrounded = false;
    private bool touchingLeftWall = false;
    private bool touchingRightWall = false;
    private int m_WalkDirectionRight = 1; 
    private int m_walkDirectionLeft = -1;


    // Start is called before the first frame update
    void Start()
    {
        updatePhysicsData();
    }

    void updatePhysicsData()
    {
        isGrounded = charPhysics.m_Grounded;
        touchingLeftWall = charPhysics.touchingLeftWall;
        touchingRightWall = charPhysics.touchingRightWall;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        updatePhysicsData();
        float speed = MoveWithWalkDirection(baseSpeed);
        // Move the character by finding the target velocity
        Vector3 targetVelocity = new Vector2(speed, _lastComputedSpeed.y);
        // And then smoothing it out and applying it to the character
        _lastComputedSpeed = Vector2.SmoothDamp(_lastComputedSpeed, targetVelocity, ref m_Velocity, m_MovementSmoothing);


    }
    private float MoveWithWalkDirection(float move)
    {
        if (isGrounded)
        {
            if (touchingLeftWall && m_WalkDirection == WalkDirection.Left)
            {
                m_WalkDirection = WalkDirection.Right;
            }
            if (touchingRightWall && m_WalkDirection == WalkDirection.Right)
            {
                m_WalkDirection = WalkDirection.Left;
            }
        }
        if (m_WalkDirection == WalkDirection.Right) return m_WalkDirectionRight * move;
        if (m_WalkDirection == WalkDirection.Left) return m_walkDirectionLeft * move;
        return move;
    }
}
