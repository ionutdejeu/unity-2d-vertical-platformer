using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterEvents : MonoBehaviour
{
    [Header("Events")]
    [Space]
    public UnityEvent OnLandEvent;
    public UnityEvent OnTouchWall;
    public UnityEvent OnJump;
    public UnityEvent OnDoubleJump;
    public UnityEvent OnDash;
    public UnityEvent OnJumpMaxHeightReached;


    

    private void OnEnable()
    {
        // enable all events

    }

    private void OnDisable()
    {
        // disable all events
    }

}
