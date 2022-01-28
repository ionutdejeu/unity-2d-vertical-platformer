using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;


namespace Assets.Character.ControllerImproved
{
    public interface ICharacterBehavior
    {
        public Vector2 ComputeBehavior(Vector2 currentSpeed,CustomCharacterState state);
    }
}
