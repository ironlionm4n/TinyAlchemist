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
        [SerializeField] private Transform firingPoint;
        [SerializeField] private float _minSpeed;
        [SerializeField] private float _maxSpeed;

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
            var instanceArrow = Instantiate(arrowPrefab, firingPoint.position, (Quaternion)firingPoint.rotation);
            var arrowRigidbody = instanceArrow.GetComponent<Rigidbody2D>();
        }
    }
}
