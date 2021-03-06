using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController
{
    public Vector2 GetMovementDirection()
    {
        float h= Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        return new Vector2(h,v);
    }

    public Vector2 GetHorizonalMovement()
    {
        float h = Input.GetAxis("Horizontal");
        return new Vector2(h, 0);
    }
}
