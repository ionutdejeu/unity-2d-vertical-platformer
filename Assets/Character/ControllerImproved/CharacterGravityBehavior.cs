using Assets.Character.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static CharacterController2D;

namespace Assets.Character.ControllerImproved
{
    public class CharacterGravityBehavior : MonoBehaviour, ICharacterBehavior
    {

        [SerializeField] private float gravity = Physics.gravity.y;
        private Vector2 gravityVector;
        void Awake()
        {
            gravityVector = new Vector2(0, gravity);
        }
        public Vector2 ComputeBehavior(Vector2 currentSpeed, CustomCharacterState state)
        {
            if (state.gravityEnabled)
                return gravityVector;
            else
                return Vector2.zero;
        }
    }
}
