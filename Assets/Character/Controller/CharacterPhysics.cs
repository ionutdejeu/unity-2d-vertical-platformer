using Assets.Character.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static CharacterController2D;

public class CharacterPhysics : MonoBehaviour
{
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
	public Vector3 Value { get; }

    [SerializeField] private CharacterHandler charHandler;

    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private LayerMask m_WhatIsWall;                          // A mask determining what is ground to the character

    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
    [SerializeField] private Collider2D m_LeftCollider;                // A collider that will be disabled when crouching
    [SerializeField] private Collider2D m_RightCollider;                // A collider that will be disabled when crouching
    [SerializeField] private Collider2D m_BottomColloder;                // A collider that will be disabled when crouching

    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	public bool m_Grounded;            // Whether or not the player is grounded.
    const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    [SerializeField] private Vector3 m_Velocity = Vector3.zero;
    private int m_WalkDirectionRight = 1; // walking direction 1: walk right, -1 walk left
    private int m_walkDirectionLeft = -1;
    private WalkDirection m_WalkDirection = WalkDirection.Right;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    public bool touchingLeftWall = false;
    public bool touchingRightWall = false;

 
    // Start is called before the first frame update
    void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

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

	private float MoveWithWalkDirection(float move)
	{
		if (m_Grounded)
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

	public void Move(float move, bool crouch, bool jump)
	{
		// If crouching, check to see if the character can stand up
		if (!crouch)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (m_CeilingCheck != null && Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				crouch = true;
			}
		}

		//only control the player if grounded or airControl is turned on
		if (m_Grounded)
		{

	
			move = MoveWithWalkDirection(move);

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);


			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}

			//do a raycast in front of the player and see if end of a track 

		}
	}


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;
	}
}
