using UnityEngine;

namespace EnemyScripts.Behaviors.EnemyBehaviorStates
{
    public class IdleState : IState
    {
        public string stateName => "Idle";
        private Animator _animator;
        private float _idleTime;
        private float _currentIdleTime;
        private bool _isDoneIdling;
        private static readonly int IsIdle = Animator.StringToHash("IsIdle");

        public IdleState(float idleTime, Animator animator)
        {
            _idleTime = idleTime;
            _animator = animator;
        }
        
        public void Enter()
        {
            _currentIdleTime = _idleTime;
            _animator.SetBool(IsIdle, true);
        }

        public void Execute()
        {
            _currentIdleTime -= Time.deltaTime;
            if (_currentIdleTime <= 0f)
            {
                _isDoneIdling = true;
            }
        }

        public void Exit()
        {
            _animator.SetBool(IsIdle, false);
        }

        public bool CheckIfDoneIdling() => _isDoneIdling;
        
    }
}