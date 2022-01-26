using Assets.Character.Controller;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDashModifier : MovementModifier
{
    public override Vector2 Value { get { return _lastComputedSpeed; } }

    private Vector2 _lastComputedSpeed = Vector2.zero;
    [SerializeField] private CharacterHandler charHandler;
    [SerializeField] private CharacterPhysics charPhysics;
    [SerializeField] private CharacterState charState;

    private void OnEnable() => charHandler.AddModifier(this);
    private void OnDisable() => charHandler.RemoveModifier(this);

    
}
