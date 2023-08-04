using UnityEngine;

namespace EnemyScripts.Behaviors.EnemyBehaviorStates
{
    public class IdleState : IState
    {
        private string _stateName;
        public string StateName => _stateName;
        private Animator _animator;
        private float _idleTime;
        private float _currentIdleTime;
        private bool _isDoneIdling;
        
        public IdleState(float idleTime, Animator animator)
        {
            _idleTime = idleTime;
            _animator = animator;
            _stateName = "Idle";
        }
        
        public void Enter()
        {
            _currentIdleTime = _idleTime;
            _animator.Play("Idle");
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
                
        }

        public bool CheckIfDoneIdling() => _isDoneIdling;
        
    }
}