using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private int healAmount;
    public static event Action<int> HealthPickedUp;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HealthPickedUp?.Invoke(healAmount);
            // play particles
            // give health to player
            Destroy(gameObject);
        }
    }
}
