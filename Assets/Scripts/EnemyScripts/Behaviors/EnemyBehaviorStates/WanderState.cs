using UnityEngine;

namespace EnemyScripts.Behaviors.EnemyBehaviorStates
{
    public class WanderState : IState
    {
        private float _wanderTime, _currentWanderTime;
        private bool _foundLeftEdge, _foundRightEdge, _isWandering;
        private Transform _leftEdgeCheck, _rightEdgeCheck;
        private Animator _animator;

        public WanderState(float wanderTime, Transform leftEdgeCheck, Transform rightEdgeCheck, Animator animator)
        {
            _wanderTime = wanderTime;
            _leftEdgeCheck = leftEdgeCheck;
            _rightEdgeCheck = rightEdgeCheck;
            _animator = animator;
        }
        
        public void Enter()
        {
            _currentWanderTime = _wanderTime;
            _isWandering = true;
            _animator.Play("Run");
        }

        public void Execute()
        {
            IsDoneWondering();
        }

        public void Exit()
        {
            
        }

        public bool IsDoneWondering()
        {
            _currentWanderTime -= Time.deltaTime;
            if (_currentWanderTime > 0)
            {
                _isWandering = false;
            }

            return _isWandering;
        }
    }
}