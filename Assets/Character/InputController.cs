using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController 
{
    public Vector2 GetMovementDirection()
    {
        float h= Input.GetAxis("horizontal");
        float v = Input.GetAxis("vertical");
        return new Vector2(h,v);
    }
}
