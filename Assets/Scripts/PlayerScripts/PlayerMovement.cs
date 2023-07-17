using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float maxHorizontalDelta;
    [SerializeField] private float maxMoveSpeedScaler;
    [SerializeField] private float dashForce;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private float _lastDirection = 0f;
    private float _targetVelocity;
    private static readonly int MoveX = Animator.StringToHash("moveX");
    private static readonly int Dash = Animator.StringToHash("Dash");

    // Start is called before the first frame update
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    // Update is called once per frame
    private void Update()
    {
        _targetVelocity = Input.GetAxis("Horizontal") * maxMoveSpeedScaler;
        _rigidbody2D.velocity = LerpedVelocity();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HandleDash(_targetVelocity);
        }
        HandleAnimator();
    }

    private void HandleDash(float targetVelocity)
    {
        Debug.Log("???");
        var dashDirection = targetVelocity < 0 ? Vector2.left : Vector2.right;
        _rigidbody2D.AddForce(dashDirection * dashForce, ForceMode2D.Impulse);
        _animator.SetTrigger(Dash);
    }

    private void HandleAnimator()
    {
        _animator.SetFloat(MoveX, Mathf.Abs(_rigidbody2D.velocity.x));
        // If player is moving, set flipX and update lastDirection
        if (_targetVelocity != 0)
        {
            _spriteRenderer.flipX = _targetVelocity < 0;
            _lastDirection = _targetVelocity;
        }
        // If player is idle, set flipX based on lastDirection
        else
        {
            _spriteRenderer.flipX = _lastDirection < 0;
        }
    }

    private Vector2 LerpedVelocity()
    {
        return new Vector2(Mathf.Lerp(_rigidbody2D.velocity.x, _targetVelocity, maxHorizontalDelta),
            _rigidbody2D.velocity.y);
    }
    
}