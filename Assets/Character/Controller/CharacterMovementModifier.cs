using Assets.Character.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static CharacterController2D;

namespace Assets.Character.Controller
{
    public class CharacterMovementModifier : MovementModifier
    {
        [SerializeField] private float baseSpeed = 2f;
        [SerializeField] private float acceleration = 0.5f;
        [SerializeField] private float deceleration = 0.2f;
        private InputController controller;
        private Vector2 _value;
        private Vector2 _movementDiretion;

        [SerializeField] private CharacterHandler charHandler;
        private void OnEnable() => charHandler.AddModifier(this);
        private void OnDisable() => charHandler.RemoveModifier(this);

        private void Start()
        {
            controller = new InputController();
        }
        public override Vector2 Value
        {
            get
            {
                return _value;
            }
        }

        private void FixedUpdate()
        {
            _value = new Vector2(_movementDiretion.x,_movementDiretion.y);
            _value *= baseSpeed * Time.deltaTime;
        }
        private void Update()
        {
            _movementDiretion = controller.GetMovementDirection();
            Debug.Log(_value);
        }
    }
}
