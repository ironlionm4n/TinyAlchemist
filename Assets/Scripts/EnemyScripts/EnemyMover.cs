using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Rigidbody2D _rigidbody2D;

    [SerializeField] private float moveTime;

    private float _currentMoveTime;
    // Start is called before the first frame update
    void Start()
    {
        _currentMoveTime = moveTime;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _currentMoveTime -= Time.deltaTime;
        if(_currentMoveTime <= 0)
        {
            _rigidbody2D.velocity = ChooseDirection() * (moveSpeed * Time.deltaTime);
        }
    }

    private Vector2 ChooseDirection()
    {
        var flip = Random.Range(0, 2);
        _currentMoveTime = moveTime;
        return flip == 0 ? Vector2.left : Vector2.right;
        
    }
}
