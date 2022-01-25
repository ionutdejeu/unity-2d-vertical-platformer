using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Character.Controller
{
    
    
    public abstract class MovementModifier : MonoBehaviour
    {
        
        public int order;
        public abstract Vector2 Value { get; }
        
    }
}
