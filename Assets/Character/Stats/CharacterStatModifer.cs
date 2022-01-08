using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Character.Stats
{
    public enum CharacterStatModifierType
    {
        Flat, 
        PercentAdd,
        PercentMult
    }
    public abstract class CharacterStatModifier
    {
        public int order;
        public float value;
        public CharacterStatModifierType type;
        public object source;
        public abstract float ComputeValue(float initialValue);
        
    }
}
