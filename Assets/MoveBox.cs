using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MoveBox : MonoBehaviour
{
    [SerializeField] private Transform desiredTransform;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        transform.DOMove(desiredTransform.position, 2f, false);
    }
}
