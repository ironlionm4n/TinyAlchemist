using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Traps
{
    public class Launcher : MonoBehaviour
    {
        [SerializeField] private GameObject arrowPrefab;
        [SerializeField] private float startDelay;
        [SerializeField] private ShotDirection shotDirection;
        [SerializeField] private float _minSpeed;
        [SerializeField] private float _maxSpeed;

        private enum ShotDirection
        {
            Left,
            Right,
            Up,
            Down
        }

        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(startDelay);
                LaunchArrow();
            }
        }

        private void LaunchArrow()
        {
            var instanceArrow = Instantiate(arrowPrefab, transform.position, quaternion.identity);
            var arrowRigidbody = instanceArrow.GetComponent<Rigidbody2D>();
            var direction = CreateDirection(arrowRigidbody);
    
        }

        private Vector2 CreateDirection(Rigidbody2D arrowRigidbody)
        {
            Vector2 direction;
            GetDirection(out direction, arrowRigidbody);
            return direction;
        }

        private void GetDirection(out Vector2 direction, Rigidbody2D arrowRigidbody)
        {
            switch (shotDirection)
            {
                case ShotDirection.Right:
                    direction = Vector2.right;
                    arrowRigidbody.transform.Rotate(0,0,-90);
                    break;
                
                case ShotDirection.Down:
                    direction = Vector2.down;
                    arrowRigidbody.transform.Rotate(0f,0f,180f);
                    break;
                case ShotDirection.Left:
                    direction = Vector2.left;
                    arrowRigidbody.transform.Rotate(0,0,90);
                    break;
                case ShotDirection.Up:
                    direction = Vector2.up;
                    break;
                default:
                    direction = Vector2.zero;
                    Debug.LogWarning("Default case of ShotDirection switch");
                    break;
            }
            arrowRigidbody.AddForce(direction* Random.Range(_minSpeed,_maxSpeed), ForceMode2D.Impulse);
        }
    }
}
