using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerJump : MonoBehaviour
    {
        [SerializeField] private float initialJumpForce;
        [SerializeField] private float additionalJumpForce;
        [SerializeField] private float maxJumpTime = 0.2f;
        [SerializeField] private float groundedRaycastDistance = 0.2f;
        [SerializeField] private LayerMask whatIsGround;
        [SerializeField] private float jumpCooldown = 0.2f;
        
        private Rigidbody2D _rigidbody2D;
        private float _jumpTime;
        private bool _isJumping;
        private float _lastJumpTime;
        private Animator _animator;
        private static readonly int Jump = Animator.StringToHash("Jump");
        private static readonly int FallY = Animator.StringToHash("fallY");
        private static readonly int Land = Animator.StringToHash("Land");

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundedRaycastDistance, whatIsGround);

            // Draw the raycast for debugging
            Debug.DrawRay(transform.position, Vector2.down * groundedRaycastDistance, Color.green);

            if (hit.collider != null)
            {
                // Reset Y velocity on landing
                if(_rigidbody2D.velocity.y < 0)
                {
                    _animator.SetTrigger(Land);
                    _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0);
                    _animator.SetFloat(FallY, 0);
                }
                if (Input.GetAxisRaw("Vertical") > 0 && Time.time - _lastJumpTime >= jumpCooldown)
                {
                    _isJumping = true;
                    _animator.ResetTrigger(Land);
                    _animator.SetTrigger(Jump);
                    _jumpTime = 0;
                    _rigidbody2D.AddForce(Vector2.up * initialJumpForce, ForceMode2D.Impulse);
                    _lastJumpTime = Time.time;
                }
            }
            else if (_isJumping && Input.GetAxisRaw("Vertical") > 0 && _jumpTime < maxJumpTime)
            {
                // Apply continuous force while jumping and button is held down
                _jumpTime += Time.deltaTime;
                _rigidbody2D.AddForce(Vector2.up * additionalJumpForce, ForceMode2D.Force);
            }
            else if (_isJumping)
            {
                // The player is no longer jumping when they let go of the button
                _isJumping = false;
            }

            if (hit.collider == null && _rigidbody2D.velocity.y < 0)
            {
                _animator.ResetTrigger(Jump);
                _animator.SetFloat(FallY, _rigidbody2D.velocity.y);
            }
        }
    }
}