using System.Collections;
using System.Linq;
using UnityEngine;

namespace EnemyScripts.Behaviors.EnemyBehaviorStates
{
    public class AttackState : IState
    {
        public string stateName => "AttackState";
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
                var direction = _enemyGameObject.transform.position - _playerGameObject.transform.position;
                if (direction.x > 0)
                {
                    _enemyGameObject.transform.localScale = new Vector3(1,1,1);
                }
                else
                {
                    _enemyGameObject.transform.localScale = new Vector3(-1,1,1);
                }
                IsAttacking = true;
                _animator.SetTrigger(Attack);
                _enemyMono.StartCoroutine(AttackDelay());
            }
        }

        private IEnumerator AttackDelay()
        {
            yield return new WaitForSeconds(_timeBetweenAttacks/2f + _animator.GetCurrentAnimatorClipInfo(0).Length/2f);
            _animator.ResetTrigger(Attack);
            yield return new WaitForSeconds(_timeBetweenAttacks/2f + _animator.GetCurrentAnimatorClipInfo(0).Length/2f);
            IsAttacking = false;
        }

        public void Exit()
        {
            //_enemyMono.StopCoroutine(AttackDelay());
            _animator.ResetTrigger(Attack);
        }

        public bool PlayerOutOfRange()
        {
            return Vector2.Distance(_enemyGameObject.transform.position, _playerGameObject.transform.position) > _playerDetectionRange;
        }
        
    }
}