using System.Collections;
using System.Linq;
using UnityEngine;

namespace EnemyScripts.Behaviors.EnemyBehaviorStates
{
    public class AttackState : IState
    {
        private Animator _animator;
        private GameObject _enemyGameObject, _playerGameObject;
        private float _playerDetectionRange, _timeBetweenAttacks;
        private MonoBehaviour _enemyMono;
        private static readonly int Attack = Animator.StringToHash("Attack");
        public bool IsAttacking { get; set; }

        public AttackState(Animator animator, GameObject enemyGameObject, GameObject playerGameObject, float playerDetectionRange, float timeBetweenAttacks)
        {
            _animator = animator;
            _enemyGameObject = enemyGameObject;
            _playerGameObject = playerGameObject;
            _playerDetectionRange = playerDetectionRange;
            _timeBetweenAttacks = timeBetweenAttacks;
        }
        
        public void Enter()
        {
            _animator.Play("Idle");
            _enemyMono = _enemyGameObject.GetComponent<MonoBehaviour>();
        }

        public void Execute()
        {
            HandleAttack();
        }

        private void HandleAttack()
        {
            if (!IsAttacking)
            {
                IsAttacking = true;
                _animator.SetTrigger(Attack);
                _enemyMono.StartCoroutine(AttackDelay());
            }
        }

        private IEnumerator AttackDelay()
        {
            yield return new WaitForSeconds(_timeBetweenAttacks);
            IsAttacking = false;
        }

        public void Exit()
        {
            //_enemyMono.StopCoroutine(AttackDelay());
        }

        public bool PlayerOutOfRange()
        {
            return Vector2.Distance(_enemyGameObject.transform.position, _playerGameObject.transform.position) > _playerDetectionRange;
        }
        
    }
}