using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float maxHorizontalDelta;
    [SerializeField] private float maxMoveSpeedScaler;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private float lastDirection = 0f;
    private static readonly int MoveX = Animator.StringToHash("moveX");

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        var targetVelocity = Input.GetAxis("Horizontal") * maxMoveSpeedScaler;
        _rigidbody2D.velocity = new Vector2(Mathf.Lerp(_rigidbody2D.velocity.x, targetVelocity, maxHorizontalDelta), _rigidbody2D.velocity.y);
        _animator.SetFloat(MoveX, Mathf.Abs(_rigidbody2D.velocity.x));
        // If player is moving, set flipX and update lastDirection
        if (targetVelocity != 0)
        {
            _spriteRenderer.flipX = targetVelocity < 0;
            lastDirection = targetVelocity;
        }
        // If player is idle, set flipX based on lastDirection
        else
        {
            _spriteRenderer.flipX = lastDirection < 0;
        }
    }
}
