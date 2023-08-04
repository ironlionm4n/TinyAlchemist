using System;
using UnityEngine;

namespace PlayerScripts
{
    public class MagmaShotDamage : MonoBehaviour
    {
        [SerializeField] private PlayerPowers powers;
        private void OnTriggerEnter2D(Collider2D other)
        {
            var otherHealth = other.GetComponent<EnemyHealth>();
            if (otherHealth)
            {
                otherHealth.TakeDamage(powers.GetPlayerPowers[3].Damage);
            }
        }
    }
}