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
                //var knockBackDirection = other.transform.position - playerParent.transform.position;
                //var calculatedKnockBackDirection = new Vector2(knockBackDirection.x, Mathf.Abs(knockBackDirection.y)).normalized;
                //other.GetComponent<EnemyKnockBack>().KnockBack(calculatedKnockBackDirection, knockBackForce);
            }
        }
    }
}