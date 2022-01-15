using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : MonoBehaviour
{
    [SerializeField] public bool isWalking = false;
    [SerializeField] public bool isJumping = false;
    [SerializeField] public bool isTouchingWall = false;
    [SerializeField] public bool isSlidingWall = false;
    [SerializeField] public bool isJumpingOffWall = false;

    private void FixedUpdate()
    {
        
    }
}
