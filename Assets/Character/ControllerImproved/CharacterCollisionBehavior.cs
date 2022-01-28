using Assets.Character.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static CharacterController2D;

namespace Assets.Character.ControllerImproved
{
    public class CharacterCollisionBehavior:MonoBehaviour,ICharacterBehavior
    {
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
 

        public Vector2 ComputeBehavior(Vector2 currentSpeed, CustomCharacterState state)
        {
			bool wasGrounded = m_Grounded;
			m_Grounded = false;

			Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
			touchingLeftWall = Physics2D.IsTouchingLayers(m_LeftCollider, m_WhatIsWall);
			touchingRightWall = Physics2D.IsTouchingLayers(m_RightCollider, m_WhatIsWall);
			state.isTouchingWall = touchingLeftWall || touchingRightWall;
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
			state.isGrounded = m_Grounded;

			return Vector2.zero;
		}
    }
}
