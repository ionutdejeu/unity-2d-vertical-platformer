using Assets.Character.Controller;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Character.ControllerImproved
{
    public class CustomCharacterController2D : MonoBehaviour
    {

        public static UnityEvent<CharacterHandler> OnPlayerDiedEvent = new UnityEvent<CharacterHandler>();
        public static UnityEvent<CharacterHandler> OnPlayerSpawendEvent = new UnityEvent<CharacterHandler>();
        [SerializeField] private List<MovementModifier> modifiers = new List<MovementModifier>();
        [SerializeField] private ReadOnlyCollection<MovementModifier> modifiersList;
        // Start is called before the first frame update
        private Rigidbody2D m_Rigidbody2D;
        CharacterCollisionBehavior collisionBehavior;
        CharacterDashBehavior dashBehavior;
        CharacterGravityBehavior gravityBehavior;
        CharacterJumpBehavior jumpBehavior;
        CharacterMovementBehavior moveBehavior;
        CustomCharacterState state;

        void Awake()
        {
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            modifiersList = modifiers.AsReadOnly();
            collisionBehavior = GetComponent<CharacterCollisionBehavior>();
            dashBehavior = GetComponent<CharacterDashBehavior>();
            gravityBehavior = GetComponent<CharacterGravityBehavior>();
            jumpBehavior = GetComponent<CharacterJumpBehavior>();
            moveBehavior = GetComponent<CharacterMovementBehavior>();
            state = GetComponent<CustomCharacterState>();

        }


        // Update is called once per frame
        void FixedUpdate()
        {
            Vector2 targetSpeed = Vector2.zero;
            targetSpeed += moveBehavior.ComputeBehavior(targetSpeed, state);
            targetSpeed += jumpBehavior.ComputeBehavior(targetSpeed, state);
            targetSpeed += gravityBehavior.ComputeBehavior(targetSpeed, state);
            targetSpeed += collisionBehavior.ComputeBehavior(targetSpeed, state);
            targetSpeed += dashBehavior.ComputeBehavior(targetSpeed, state);

            m_Rigidbody2D.velocity = targetSpeed;
        }
    }
}