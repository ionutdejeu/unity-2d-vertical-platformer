using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Character.Stats
{
   
    public class CharacterStatPercentMultModifer : CharacterStatModifier
    {
        public CharacterStatPercentMultModifer()
        {
            type = CharacterStatModifierType.PercentMult;
            order = (int)type;
        }
        public override float ComputeValue(float initialValue)
        {
            return initialValue *=1+ value;
        }
    }
}
