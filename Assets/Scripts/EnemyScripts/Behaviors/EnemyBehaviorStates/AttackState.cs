using UnityEngine;

namespace EnemyScripts.Behaviors.EnemyBehaviorStates
{
    public class AttackState : IState
    {
        private Animator _animator;
        private GameObject _enemyGameObject, _playerGameObject;
        private float _playerDetectionRange;
        
        public AttackState(Animator animator, GameObject enemyGameObject, GameObject playerGameObject, float playerDetectionRange)
        {
            _animator = animator;
            _enemyGameObject = enemyGameObject;
            _playerGameObject = playerGameObject;
            _playerDetectionRange = playerDetectionRange;
        }
        
        public void Enter()
        {
            
        }

        public void Execute()
        {
            _animator.Play("Attack");    
        }

        public void Exit()
        {
            
        }

        public bool PlayerOutOfRange()
        {
            return Vector2.Distance(_enemyGameObject.transform.position, _playerGameObject.transform.position) > _playerDetectionRange;
        }
    }
}