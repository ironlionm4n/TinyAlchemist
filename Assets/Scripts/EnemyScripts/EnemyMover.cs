using System;
using System.Collections;
using System.Collections.Generic;
using EnemyScripts;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float idleTime = 3f;
    [SerializeField] private Animator animator;

    private EnemyStates _currentState;
    private Rigidbody2D _rigidbody2D;
    private bool _isMoving;
    private float _currentIdleTime;
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");

    #region UnityFunctions

    private void OnEnable()
    {
        EnemyHealth.EnemyHit += OnEnemyHit;
    }

    // Start is called before the first frame update
    private void Start()
    {
        _currentIdleTime = idleTime;
        _currentState = EnemyStates.Idle;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        switch (_currentState)
        {
            case EnemyStates.Idle:
                Idle();
                break;
            case EnemyStates.Moving:
                if (!_isMoving) StartCoroutine(Moving());
                break;
            case EnemyStates.Hit:
                HandleHit();
                break;
        }
    }

    private void OnDisable()
    {
        EnemyHealth.EnemyHit -= OnEnemyHit;
    }

    #endregion

    #region CustomFunctions

    private void HandleHit()
    {
        StopCoroutine(Moving());
        animator.SetBool(IsMoving, false);
        UpdateCurrentState(EnemyStates.Idle);
    }

    private IEnumerator Moving()
    {
        _isMoving = true;
        animator.SetBool(IsMoving, true);
        var desiredDirection = ChooseLeftOrRight();
        _rigidbody2D.velocity = desiredDirection * (moveSpeed * Time.deltaTime);
        var timeToMove = Random.Range(2.5f, 4f);
        yield return new WaitForSeconds(timeToMove);
        _isMoving = false;
        animator.SetBool(IsMoving, false);
        UpdateCurrentState(EnemyStates.Idle);
    }

    private void Idle()
    {
        _currentIdleTime -= Time.deltaTime;
        StopCoroutine(Moving());
        _rigidbody2D.velocity = Vector2.zero;
        if (_currentIdleTime <= 0)
        {
            _currentIdleTime = idleTime;
            UpdateCurrentState(EnemyStates.Moving);
        }
    }

    private void OnEnemyHit()
    {
        UpdateCurrentState(EnemyStates.Hit);
    }

    private void UpdateCurrentState(EnemyStates newState)
    {
        _currentState = newState;
    }

    private Vector2 ChooseLeftOrRight()
    {
        var flip = Random.Range(0, 2);
        return flip == 0 ? Vector2.left : Vector2.right;
    }

    #endregion
}