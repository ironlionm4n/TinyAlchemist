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
        [SerializeField] private float knockBackForce;
        [SerializeField] private GameObject playerParent;

        private void OnTriggerEnter2D(Collider2D other)
        {
            var otherHealth = other.GetComponent<EnemyHealth>();
            if (otherHealth)
            {
                otherHealth.TakeDamage(powers.GetPlayerPowers[1].Damage);
                var knockBackDirection = other.transform.position - playerParent.transform.position;
                var calculatedKnockBackDirection = new Vector2(knockBackDirection.x, Mathf.Abs(knockBackDirection.y)).normalized;
                other.GetComponent<EnemyKnockBack>().KnockBack(calculatedKnockBackDirection, knockBackForce);
            }
        }
    }
}