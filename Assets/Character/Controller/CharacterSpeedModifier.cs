using Assets.Character.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CharacterController2D;

public class CharacterSpeedModifier : MonoBehaviour,IMovementModifier
{
    public Vector3 Value { get; }

    [SerializeField] private CharacterHandler charHandler;
    [SerializeField] private CharacterPhysics charPhysics;
    private WalkDirection m_WalkDirection = WalkDirection.Right;


    private void OnEnable() => charHandler.AddModifier(this);
    private void OnDisable() => charHandler.RemoveModifier(this);

    private bool isGrounded = false;
    private bool wasGrounded = false;
    private bool touchingLeftWall = false;
    private bool touchingRightWall = false;
    private int m_WalkDirectionRight = 1; // walking direction 1: walk right, -1 walk left
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
