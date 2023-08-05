using UnityEngine;

namespace EnemyScripts.Behaviors.EnemyBehaviorStates
{
    public class WanderState : IState
    {
        public string stateName => "Wander";
        private float _wanderTime, _currentWanderTime;
        private bool _foundLeftEdge, _foundRightEdge, _isWandering;
        private Transform _edgeCheck;
        private Animator _animator;
        private Rigidbody2D _rigidbody;
        private LayerMask _groundLayerMask;
        private Vector2 _direction;
        private float _moveSpeed;
        private static readonly int IsWandering = Animator.StringToHash("IsWandering");

        public WanderState(float wanderTime, Transform edgeCheck, Animator animator,
            LayerMask groundLayerMask, Rigidbody2D rigidbody, float moveSpeed)
        {
            _wanderTime = wanderTime;
            _edgeCheck = edgeCheck;
            _animator = animator;
            _groundLayerMask = groundLayerMask;
            _rigidbody = rigidbody;
            _moveSpeed = moveSpeed;
        }
        
        public void Enter()
        {
            _currentWanderTime = _wanderTime;
            _isWandering = true;
            _animator.SetBool(IsWandering, true);
            _direction = GetRandomDirection();
        }

        public void Execute()
        {
            if (_direction == Vector2.left)
            {
                _rigidbody.gameObject.transform.localScale = new Vector3(1, 1, 1);
                var raycastHit2D = Physics2D.Raycast(_edgeCheck.position, Vector2.down, .25f, (int) _groundLayerMask);
                if (raycastHit2D && _currentWanderTime > 0)
                {
                    _rigidbody.velocity = _direction * (_moveSpeed * Time.deltaTime);
                }
                else if(!raycastHit2D && _currentWanderTime > 0)
                {
                    _direction = Vector2.right;
                }
            }
            else if (_direction == Vector2.right)
            {
                _rigidbody.gameObject.transform.localScale = new Vector3(-1, 1, 1);
                var raycastHit2D = Physics2D.Raycast(_edgeCheck.position, Vector2.down, .25f, (int) _groundLayerMask);
                if (raycastHit2D && _currentWanderTime > 0)
                {
                    _rigidbody.velocity = _direction * (_moveSpeed * Time.deltaTime);
                }
                else if(!raycastHit2D && _currentWanderTime > 0)
                {
                    _direction = Vector2.left;
                }
            }
            
  
        }

        public void Exit()
        {
            _rigidbody.velocity = Vector2.zero;
            _animator.SetBool(IsWandering, false);
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