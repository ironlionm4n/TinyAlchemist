using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyScripts.Behaviors.EnemyBehaviorStates
{
    public interface IState
    {
        void Enter();
        void Execute();
        void Exit();
    }
}