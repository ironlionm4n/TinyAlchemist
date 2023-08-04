using System;
using System.Collections;
using System.Collections.Generic;
using EnemyScripts.Behaviors.EnemyBehaviorStates;
using UnityEngine;
using UnityEngine.Serialization;

public class SpearGoblinBehavior : MonoBehaviour
{
    [SerializeField] private float idleTime, wanderTime;
    [SerializeField] private Transform leftEdgeCheck, rightEdgeCheck;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private Rigidbody2D enemyRigidbody2D;
    [SerializeField] private float moveSpeed;
    [SerializeField] private SpriteRenderer spriteRenderer;
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
            spriteRenderer.flipX = pos.x > 0;
        }

        if (_currentState is AttackState)
        {
            var attackState = (AttackState)_currentState;
            if (!attackState.IsAttacking && attackState.PlayerOutOfRange() || !IsPlayerYIsOnOrAboveEnemyY())
            {
                UpdateCurrentState(GetNewIdleState());
            }
        }
        if (_currentState is IdleState)
        {
            var idleState = (IdleState)_currentState;
            if (idleState.CheckIfDoneIdling())
            {
                UpdateCurrentState(new WanderState(wanderTime, leftEdgeCheck, rightEdgeCheck, _animator, groundLayerMask, enemyRigidbody2D, moveSpeed, spriteRenderer));
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
    }

    private bool IsPlayerYIsOnOrAboveEnemyY()
    {
        return (transform.position.y <= playerGameObject.transform.position.y);
    }

    private IdleState GetNewIdleState()
    {
        return new IdleState(idleTime, _animator);
    }

    private void UpdateCurrentState(IState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter();
    }

    private bool IsPlayerDetected()
    {
        return Vector2.Distance(transform.position, playerGameObject.transform.position) <= playerDetectionRange;
    }
    

}
