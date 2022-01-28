using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCharacterState : MonoBehaviour
{

    [SerializeField] public bool isWalking = false;
    [SerializeField] public bool isJumping = false;
    [SerializeField] public bool isDashing = false;
    [SerializeField] public bool isGrounded = false;
    [SerializeField] public bool isTouchingWall = false;
    [SerializeField] public bool isSlidingWall = false;
    [SerializeField] public bool isJumpingOffWall = false;

    [Header("Systems Controls")]
    [Space]
    [SerializeField] public bool controlEnabled = false;
    [SerializeField] public bool gravityEnabled = false;
    [SerializeField] public bool jumpEnabled = false;
    


    [SerializeField] public int facingDirection = 1;

    [SerializeField] public Vector2 speed = Vector2.zero;
    [SerializeField] public Vector2 facingDiretion = Vector2.zero;

    public void setIsWalking(bool value)
    {
        isWalking = value;
    }

    public void setJumping(bool value)
    {
        isJumping = value;
    }

}
