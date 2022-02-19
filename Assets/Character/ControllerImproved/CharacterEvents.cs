using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterEvents : MonoBehaviour
{
    [Header("Controller Events")]
    [Space]
    public UnityEvent<Vector2> OnLandEvent = new UnityEvent<Vector2>();
    public UnityEvent<Vector2> OnTouchWall = new UnityEvent<Vector2>();
    public UnityEvent<Vector2> OnJump = new UnityEvent<Vector2>();
    public UnityEvent<Vector2> OnDoubleJump = new UnityEvent<Vector2>();
    public UnityEvent<Vector2> OnDash = new UnityEvent<Vector2>();
    public UnityEvent<Vector2> OnJumpMaxHeightReached = new UnityEvent<Vector2>();

    private void OnEnable()
    {
        // enable all events

    }

    private void OnDisable()
    {
        // disable all events
    }

}
