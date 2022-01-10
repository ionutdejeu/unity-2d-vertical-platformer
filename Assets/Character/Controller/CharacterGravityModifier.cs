using Assets.Character.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGravityModifier : MovementModifier
{
    public override Vector2 Value { get { return _lastComputedSpeed; } }

    [SerializeField] private CharacterHandler charHandler;
    [SerializeField] private float gravity = Physics.gravity.y;
    [SerializeField] private Vector2 _lastComputedSpeed = Vector2.zero;

    private void OnEnable() => charHandler.AddModifier(this);
    private void OnDisable() => charHandler.RemoveModifier(this);



    void FixedUpdate()
    {
        _lastComputedSpeed = new Vector2(0, gravity);
    }
}
