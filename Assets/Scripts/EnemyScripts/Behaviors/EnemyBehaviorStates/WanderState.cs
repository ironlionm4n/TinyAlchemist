using UnityEngine;

namespace EnemyScripts.Behaviors.EnemyBehaviorStates
{
    public class WanderState : IState
    {
        private float _wanderTime, _currentWanderTime;
        private bool _foundLeftEdge, _foundRightEdge, _isWandering;
        private Transform _leftEdgeCheck, _rightEdgeCheck;
        private Animator _animator;
        private Rigidbody2D _rigidbody;
        private SpriteRenderer _spriteRenderer;
        private string _stateName;
        
        public string StateName => _stateName;
        private LayerMask _groundLayerMask;
        private Vector2 _direction;
        private float _moveSpeed;

        public WanderState(float wanderTime, Transform leftEdgeCheck, Transform rightEdgeCheck, Animator animator,
            LayerMask groundLayerMask, Rigidbody2D rigidbody, float moveSpeed, SpriteRenderer spriteRenderer)
        {
            _wanderTime = wanderTime;
            _leftEdgeCheck = leftEdgeCheck;
            _rightEdgeCheck = rightEdgeCheck;
            _animator = animator;
            _stateName = "Wander";
            _groundLayerMask = groundLayerMask;
            _rigidbody = rigidbody;
            _moveSpeed = moveSpeed;
            _spriteRenderer = spriteRenderer;
        }
        
        public void Enter()
        {
            _currentWanderTime = _wanderTime;
            _isWandering = true;
            _animator.Play("Run");
            _direction = GetRandomDirection();
        }

        public void Execute()
        {
            if (_direction == Vector2.left)
            {
                _spriteRenderer.flipX = true;
                var leftRaycastHit2D = Physics2D.Raycast(_leftEdgeCheck.position, Vector2.down, .25f, (int) _groundLayerMask);
                Debug.DrawRay(_leftEdgeCheck.position, Vector2.down * .25f, Color.yellow);
                if (leftRaycastHit2D && _currentWanderTime > 0)
                {
                    _rigidbody.velocity = _direction * (_moveSpeed * Time.deltaTime);
                }
                else if(!leftRaycastHit2D && _currentWanderTime > 0)
                {
                    _direction = Vector2.right;
                }
            }
            else if (_direction == Vector2.right)
            {
                _spriteRenderer.flipX = false;
                var rigRaycastHit2D = Physics2D.Raycast(_rightEdgeCheck.position, Vector2.down, .25f, (int) _groundLayerMask);
                Debug.DrawRay(_rightEdgeCheck.position, Vector2.down * .25f, Color.cyan);
                if (rigRaycastHit2D && _currentWanderTime > 0)
                {
                    _rigidbody.velocity = _direction * (_moveSpeed * Time.deltaTime);
                }
                else if(!rigRaycastHit2D && _currentWanderTime > 0)
                {
                    _direction = Vector2.left;
                }
            }
            
  
        }

        private bool IsPlayerDetected()
        {
            throw new System.NotImplementedException();
        }

        public void Exit()
        {
            _rigidbody.velocity = Vector2.zero;
        }

        public bool IsDoneWondering()
        {
            _currentWanderTime -= Time.deltaTime;
            if (_currentWanderTime > 0)
            {
                _isWandering = false;
            }
            else
            {
                _isWandering = true;
            }

            return _isWandering;
        }

        public Vector2 GetRandomDirection()
        {
            var random = Random.Range(0, 2);
            return random > 0 ? Vector2.right : Vector2.left;
        }
    }
}