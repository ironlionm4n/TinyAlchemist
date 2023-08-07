using System;
using System.Collections;
using System.Collections.Generic;
using PlayerScripts;
using UnityEngine;

public class LavaDetection : MonoBehaviour
{
    [SerializeField] private CompositeCollider2D lavaCompositeCollider;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            lavaCompositeCollider.enabled = false;
            other.GetComponent<PlayerHealth>().HandlePlayerDeath();
        }
    }
}
