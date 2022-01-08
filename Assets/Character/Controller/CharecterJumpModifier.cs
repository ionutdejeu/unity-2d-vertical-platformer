using Assets.Character.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharecterJumpModifier : MonoBehaviour, IMovementModifier
{
    public Vector3 Value { get; }

    [SerializeField] private CharacterHandler charHandler;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()=>charHandler.AddModifier(this);
    private void OnDisable() => charHandler.RemoveModifier(this);


    // Update is called once per frame
    void Update()
    {
        float a = Input.GetAxis("jump");
    }
}
