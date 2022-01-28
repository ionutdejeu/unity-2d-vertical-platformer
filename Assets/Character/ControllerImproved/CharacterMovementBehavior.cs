using Assets.Character.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static CharacterController2D;

namespace Assets.Character.ControllerImproved
{
    public class CharacterMovementBehavior : MonoBehaviour, ICharacterBehavior
    {

        [SerializeField] private float baseSpeed = 2f;
        [SerializeField] private float acceleration = 0.5f;
        [SerializeField] private float deceleration = 0.2f;
        private InputController controller;
        private Vector2 _value;
        private Vector2 _movementDiretion;
       
        private void Start()
        {
            controller = new InputController();
        }
       
        private void Update()
        {
            _movementDiretion = controller.GetHorizonalMovement();
        }

        
        public Vector2 ComputeBehavior(Vector2 currentSpeed, CustomCharacterState state)
        {

            _value = new Vector2(_movementDiretion.x, _movementDiretion.y);
            _value *= baseSpeed * Time.deltaTime;
            
            return _value;
        }
    }
}
