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
    [SerializeField] public bool isTouchingWallOnLeft = false;
    [SerializeField] public bool isTouchingWallOnRight = false;
    [SerializeField] public List<Vector2> wallTouchPoinPosition = new List<Vector2>();

    [SerializeField] public bool isSlidingWall = false;
    [SerializeField] public bool isJumpingOffWall = false;

    [Header("Systems Controls")]
    [Space]
    [SerializeField] public bool controlEnabled = true;
    [SerializeField] public bool gravityEnabled = true;
    [SerializeField] public bool jumpEnabled = true;


    [Header("Jumping Controls")]
    [Space]
    [SerializeField] public long lastJumpTimestamp=0;
    [SerializeField] public bool canDashAfterJump = true;
    [SerializeField] public int maxJumpsAvailable = 4;
    [SerializeField] public int remainingJumps = 4;


    [Header("Animation Controls")]
    [Space]
    [SerializeField] public int animationFacingDirection = 1;

    [SerializeField] public Vector2 speed = Vector2.zero;
    [SerializeField] public Vector2 facingDiretion = Vector2.zero;

}
