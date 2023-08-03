using UnityEngine;

namespace EnemyScripts.Behaviors.EnemyBehaviorStates
{
    public class IdleState : IState
    {
        private Animator _animator;
        private float _idleTime;
        private float _currentIdleTime;
        private bool _isDoneIdling;
        
        public IdleState(float idleTime, Animator animator)
        {
            _idleTime = idleTime;
            _animator = animator;
        }
        
        public void Enter()
        {
            _currentIdleTime = _idleTime;
        }

        public void Execute()
        {
            _currentIdleTime -= Time.deltaTime;
            _isDoneIdling = _currentIdleTime <= 0;
        }

        public void Exit()
        {
                
        }

        public bool CheckIfDoneIdling() => _isDoneIdling;
    }
}