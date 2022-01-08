using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Character.Stats
{
   
    public class CharacterStatFlatModifier:CharacterStatModifier
    {
        public CharacterStatFlatModifier()
        {
            type = CharacterStatModifierType.Flat;
            order = (int)type;
        }
        public override float ComputeValue(float initialValue)
        {
            return initialValue + value;
        }
    }
}
