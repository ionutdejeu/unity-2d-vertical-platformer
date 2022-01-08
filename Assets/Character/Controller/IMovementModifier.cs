using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Character.Controller
{
    public interface IMovementModifier
    {
        Vector3 Value { get; }
    }
}
