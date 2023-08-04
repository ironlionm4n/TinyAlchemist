using System.Collections;
using System.Collections.Generic;
using EnemyScripts;
using PlayerScripts;
using UnityEngine;

namespace PlayerScripts
{
    public class FuryFistDamage : MonoBehaviour
    {
        [SerializeField] private PlayerPowers powers;

        private void OnTriggerEnter2D(Collider2D other)
        {
            var otherHealth = other.GetComponent<EnemyHealth>();
            if (otherHealth)
            {
                otherHealth.TakeDamage(powers.GetPlayerPowers[1].Damage);
            }
        }
    }
}