using System.Collections;
using EnemyScripts;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float idleTime = 3f;
    [SerializeField] private Animator animator;
    [SerializeField] private float rayDistance;
    [SerializeField] private LayerMask groundLayer;

    private EnemyStates _currentState;
    private Rigidbody2D _rigidbody2D;
    private bool _isMoving;
    private float _currentIdleTime;
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private bool _isGettingKnockedBack;

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
            case EnemyStates.KnockBack:
                if (!_isGettingKnockedBack) StartCoroutine(KnockBackRoutine());
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
        UpdateCurrentState(EnemyStates.KnockBack);
    }

    private IEnumerator Moving()
    {
        var forwardHitCheck = Physics2D.Raycast(transform.position, Vector2.right, rayDistance, groundLayer);
        var backwardsHitCheck = Physics2D.Raycast(transform.position, Vector2.left, rayDistance, groundLayer);

        Vector2 desiredDirection;

        if (forwardHitCheck.collider != null)
            desiredDirection = Vector2.left;
        else if (backwardsHitCheck.collider != null)
            desiredDirection = Vector2.right;
        else
            desiredDirection = ChooseLeftOrRight();
        _isMoving = true;
        animator.SetBool(IsMoving, true);
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

    private IEnumerator KnockBackRoutine()
    {
        _isGettingKnockedBack = true;
        yield return new WaitForSeconds(1f);
        _isGettingKnockedBack = false;
        UpdateCurrentState(EnemyStates.Idle);
    }

    #endregion
}