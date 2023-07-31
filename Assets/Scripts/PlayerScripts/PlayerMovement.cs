using System.Collections;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float maxHorizontalDelta;
        [SerializeField] private float maxMoveSpeedScaler;
        [SerializeField] private float dashForce;
        [SerializeField] private float knockBackDelay = 1f;
        [SerializeField] private LayerMask invulnerableLayerMask;
        [SerializeField] private LayerMask normalLayerMask;
        [SerializeField] private AudioSource dashAudioSource;

        private Rigidbody2D _rigidbody2D;
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private float _lastDirection = 0f;
        private float _targetVelocity;
        private static readonly int MoveX = Animator.StringToHash("moveX");
        private static readonly int Dash = Animator.StringToHash("Dash");
        private bool _isGettingKnockBacked;
        private bool _isDead;
        private bool _isDashing;
        public bool IsDashing => _isDashing;

        private void OnEnable()
        {
            PlayerHealth.PlayerDied += OnPlayerDied;
        }

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
            if (_isDead)
            {
                _rigidbody2D.velocity = Vector2.zero;
                return;
            }

            if (!_isGettingKnockBacked)
            {
                _targetVelocity = Input.GetAxis("Horizontal") * maxMoveSpeedScaler;
                _rigidbody2D.velocity = LerpedVelocity();
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    HandleDash(_targetVelocity);
                }

                HandleAnimator();
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

        private void HandleDash(float targetVelocity)
        {
            _isDashing = true;
            dashAudioSource.PlayOneShot(dashAudioSource.clip);
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

        public void WasJustKnockedBack()
        {
            if (!_isGettingKnockBacked)
            {
                StartCoroutine(KnockBackRoutine());
            }
        }

        private IEnumerator KnockBackRoutine()
        {
            _isGettingKnockBacked = true;
            yield return new WaitForSeconds(knockBackDelay);
            _isGettingKnockBacked = false;  
        }

        public void BeginDashInvulnerabilityAnimationEvent()
        {
            _rigidbody2D.excludeLayers = invulnerableLayerMask;
        }
    
        public void EndDashInvulnerabilityAnimationEvent()
        {
            _isDashing = false;
            _rigidbody2D.excludeLayers = normalLayerMask;
        }
    }
}