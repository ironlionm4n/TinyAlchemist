using System.Collections;
using UnityEngine;

namespace EnemyScripts.Behaviors.EnemyBehaviorStates
{
    public class DeathState : IState
    {
        private Animator _animator;
        private GameObject _enemyGameObject;
        private static readonly int IsDead = Animator.StringToHash("IsDead");

        public DeathState(Animator animator, GameObject enemyGameObject)
        {
            _animator = animator;
            _enemyGameObject = enemyGameObject;
        }
        public string stateName { get; } = "Death";
        public void Enter()
        {
            _animator.SetBool(IsDead, true);
            
        }

        public void Execute()
        {
            
        }

        public void Exit()
        {
            
        }
    }
}