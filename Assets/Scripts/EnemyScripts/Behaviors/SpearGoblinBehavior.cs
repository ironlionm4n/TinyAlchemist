using System;
using System.Collections;
using System.Collections.Generic;
using EnemyScripts.Behaviors.EnemyBehaviorStates;
using UnityEngine;

public class SpearGoblinBehavior : MonoBehaviour
{
    [SerializeField] private float idleTime, wanderTime;
    [SerializeField] private Transform leftEdgeCheck, rightEdgeCheck;
    private Animator _animator;
    
    private IState _currentState;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _currentState = new IdleState(idleTime, _animator);
    }

    private void Update()
    {
        _currentState.Execute();
        if (_currentState is IdleState)
        {
            var idleState = (IdleState)_currentState;
            if (idleState.CheckIfDoneIdling())
            {
                UpdateCurrentState(new WanderState(wanderTime, leftEdgeCheck, rightEdgeCheck, _animator));
            }
        }

        if (_currentState is WanderState)
        {
            var wanderState = (WanderState)_currentState;
            if (wanderState.IsDoneWondering())
            {
                UpdateCurrentState(new IdleState(idleTime, _animator));
            }
        }
    }

    private void UpdateCurrentState(IState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter();
    }
}
