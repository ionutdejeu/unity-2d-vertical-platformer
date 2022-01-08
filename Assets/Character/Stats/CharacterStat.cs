using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Character.Stats
{
    public class CharacterStat
    {
        public float baseValue, _value;
        private bool _isDirty;

        public readonly List<CharacterStatModifier> statModifiers;

        public float Value
        {
            get
            {
                if (_isDirty)
                {
                    _value = CalculateFinalValue();
                    _isDirty = false;
                }
                return _value;
            }
        }
        public bool RemoveModifier(CharacterStatFlatModifier m)
        {
            if (statModifiers.Remove(m))
            {
                _isDirty = true;
                return true;
            }
            return false;
        }
        public bool RemoveAllModifiersFromSource(object source)
        {
            bool didRemove = false;
            for (int i = 0; i < statModifiers.Count; i++)
            {
                if(statModifiers[i].source == source)
                {
                    _isDirty = true;
                    statModifiers.RemoveAt(i);
                    didRemove = true;
                }
            }
            return didRemove;
        }
        public CharacterStat(float value)
        {
            baseValue = value;
            statModifiers = new List<CharacterStatModifier>();

        }
        private int CompareStatsModifier(CharacterStatModifier a, CharacterStatModifier b)
        {
            if (a.order > b.order) return 1;
            if (a.order < b.order) return -1;
            return 0;

        }
        public void AddModifier(CharacterStatModifier m)
        {
            statModifiers.Add(m);
            statModifiers.Sort(CompareStatsModifier);

        }
        public void RemoveModifier(CharacterStatModifier m)
        {
            _isDirty = true;
            statModifiers.Remove(m);
        }

        public float CalculateFinalValue()
        {
            float finalValue = baseValue;
            foreach (var modifier in statModifiers)
            {
                finalValue = modifier.ComputeValue(finalValue);
            }
            
            return (float)Math.Round(finalValue,4);
        }
    }
}
