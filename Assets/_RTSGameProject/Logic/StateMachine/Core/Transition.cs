﻿using System;
using UnityEngine;

namespace _RTSGameProject.Logic.StateMachine.Core
{
    public class Transition
    {
        public Type From { get; }
        public Type To { get; }
        public Func<bool> Condition { get; }

        public Transition(Type from, Type to, Func<bool> condition)
        {
            From = from;
            To = to;
            Condition = condition;
        }
    }
}