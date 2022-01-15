using Assets.Character.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static CharacterController2D;

public class CharacterPhysics : MonoBehaviour
{
	[SerializeField] private CharacterState charState;
	[SerializeField] private LayerMask m_WhatIsGround;        // A mask determining what is ground to the character
    [SerializeField] private LayerMask m_WhatIsWall;          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;         // A position marking where to check if the player is grounded.
    [SerializeField] private Collider2D m_LeftCollider;       // A collider that will be disabled when crouching
    [SerializeField] private Collider2D m_RightCollider;      // A collider that will be disabled when crouching
    [SerializeField] private Collider2D m_BottomColloder;     // A collider that will be disabled when crouching

    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	public bool m_Grounded;            // Whether or not the player is grounded.

    [Header("Events")]
    [Space]
    public UnityEvent OnLandEvent;

	[SerializeField] public bool touchingLeftWall = false;
	[SerializeField] public bool touchingRightWall = false;

 
    // Start is called before the first frame update
    void Awake()
    {
        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

    }


	private void FixedUpdate()
	{

		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		touchingLeftWall = Physics2D.IsTouchingLayers(m_LeftCollider, m_WhatIsWall);
		touchingRightWall = Physics2D.IsTouchingLayers(m_RightCollider, m_WhatIsWall);
		charState.isTouchingWall = touchingLeftWall || touchingRightWall;
		bool c = Physics2D.IsTouchingLayers(m_BottomColloder, m_WhatIsGround);

		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
		
	}
 
}
