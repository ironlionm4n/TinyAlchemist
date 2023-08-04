using UnityEngine;

namespace PlayerScripts
{
    public class PlayerJump : MonoBehaviour
    {
        [SerializeField] private float initialJumpForce;
        [SerializeField] private float additionalJumpForce;
        [SerializeField] private float maxJumpTime = 0.2f;
        [SerializeField] private float groundedRaycastDistance = 0.2f;
        [SerializeField] private LayerMask whatIsGround;
        [SerializeField] private float jumpCooldown = 0.2f;
        [SerializeField] private float groundRayModifier;
        [SerializeField] private AudioSource jumpAudioSource;
        [SerializeField] private Transform leftGroundCheck, rightGroundCheck;
        
        private Rigidbody2D _rigidbody2D;
        private float _jumpTime;
        private bool _isJumping;
        private bool _isGrounded;
        public bool IsGrounded => _isGrounded;
        private float _lastJumpTime;
        private Animator _animator;
        private static readonly int Jump = Animator.StringToHash("Jump");
        private static readonly int FallY = Animator.StringToHash("fallY");
        private static readonly int Land = Animator.StringToHash("Land");
        private static readonly int IsJumping = Animator.StringToHash("IsJumping");
        private bool _isDead;
        private bool _canJump = true;

        private void OnEnable()
        {
            PlayerHealth.PlayerDied += OnPlayerDied;
        }

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (_isDead || !_canJump) return;
            
            // Two raycasts from the left and right sides of the player
            RaycastHit2D hitLeft = Physics2D.Raycast(leftGroundCheck.position, Vector2.down, groundedRaycastDistance, whatIsGround);
            RaycastHit2D hitRight = Physics2D.Raycast(rightGroundCheck.position, Vector2.down, groundedRaycastDistance, whatIsGround);

            // Draw the raycasts for debugging
            Debug.DrawRay(leftGroundCheck.position, Vector2.down *  groundedRaycastDistance, Color.green);
            Debug.DrawRay(rightGroundCheck.position, Vector2.down * groundedRaycastDistance, Color.blue);

            _isGrounded = hitLeft.collider != null || hitRight.collider != null;

            if (_isGrounded)
            {
                _rigidbody2D.gravityScale = 2f;
                // Reset Y velocity on landing
                if(_rigidbody2D.velocity.y <= 0)
                {
                    _animator.SetTrigger(Land);
                    _animator.ResetTrigger(Jump);
                    _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0);
                    _animator.SetFloat(FallY, 0);
                }
                
                if (Input.GetAxisRaw("Vertical") > 0 && Time.time - _lastJumpTime >= jumpCooldown)
                {
                    _isJumping = true;
                    _animator.ResetTrigger(Land);
                    _animator.SetTrigger(Jump);
                    jumpAudioSource.PlayOneShot(jumpAudioSource.clip);
                    _animator.SetBool(IsJumping, true);
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
                _rigidbody2D.gravityScale = 5f;
                _isJumping = false;
                _animator.SetBool(IsJumping, false);
            }

            if (!_isGrounded && _rigidbody2D.velocity.y < 0)
            {
                _animator.ResetTrigger(Jump);
                _animator.SetFloat(FallY, _rigidbody2D.velocity.y);
            }
        }
        
        private void OnDisable()
        {
            PlayerHealth.PlayerDied -= OnPlayerDied;
        }

        private void OnPlayerDied()
        {
            _isDead = true;
        }

        public void UpdatePlayerJumpFalse()
        {
            _canJump = false;
        }
        
        public void UpdatePlayerJumpTrue()
        {
            _canJump = true;
        }
        
    }
}