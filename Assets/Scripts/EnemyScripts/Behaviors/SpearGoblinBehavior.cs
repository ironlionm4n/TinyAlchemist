using EnemyScripts.Behaviors.EnemyBehaviorStates;
using UnityEngine;

namespace EnemyScripts.Behaviors
{
    public class SpearGoblinBehavior : MonoBehaviour
    {
        [SerializeField] private float idleTime, wanderTime;
        [SerializeField] private Transform edgeCheck;
        [SerializeField] private LayerMask groundLayerMask;
        [SerializeField] private Rigidbody2D enemyRigidbody2D;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float playerDetectionRange;
        [SerializeField] private float timeBetweenAttacks;
        [SerializeField] private GameObject playerGameObject;
        private Animator _animator;
    
        private IState _currentState;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            UpdateCurrentState(new IdleState(idleTime, _animator));

        }

        private void Update()
        {
            _currentState.Execute();
        
            if (IsPlayerYIsOnOrAboveEnemyY() && IsPlayerDetected())
            {
                if (_currentState is not AttackState)
                {
                    UpdateCurrentState(new AttackState(_animator, gameObject, playerGameObject, playerDetectionRange, timeBetweenAttacks));    
                }
            
                var pos = transform.position - playerGameObject.transform.position;
            }

            if (_currentState is AttackState)
            {
                var attackState = (AttackState)_currentState;
                if (attackState.PlayerOutOfRange() || !IsPlayerYIsOnOrAboveEnemyY())
                {
                    UpdateCurrentState(GetNewIdleState());
                }
            }
            if (_currentState is IdleState)
            {
                var idleState = (IdleState)_currentState;
                if (idleState.CheckIfDoneIdling())
                {
                    UpdateCurrentState(new WanderState(wanderTime, edgeCheck, _animator, groundLayerMask, enemyRigidbody2D, moveSpeed));
                }
            }

            if (_currentState is WanderState)
            {
                var wanderState = (WanderState)_currentState;
                if (wanderState.IsDoneWondering())
                {
                    UpdateCurrentState(GetNewIdleState());
                }
            }

            if (_currentState is DeathState)
            {
                Destroy(gameObject, 1.5f);
            }
        }

        private bool IsPlayerYIsOnOrAboveEnemyY()
        {
            return (transform.position.y <= playerGameObject.transform.position.y);
        }

        private IdleState GetNewIdleState()
        {
            return new IdleState(idleTime, _animator);
        }

        public void UpdateCurrentState(IState newState)
        {
            Debug.Log(newState.stateName);
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }

        private bool IsPlayerDetected()
        {
            return Vector2.Distance(transform.position, playerGameObject.transform.position) <= playerDetectionRange;
        }
    

    }
}
