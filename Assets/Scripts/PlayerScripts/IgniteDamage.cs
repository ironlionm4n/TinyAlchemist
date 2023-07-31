using UnityEngine;

namespace PlayerScripts
{
    public class IgniteDamage : MonoBehaviour
    {
        [SerializeField] private PlayerPowers powers;

        private void OnTriggerEnter2D(Collider2D other)
        {
            var otherHealth = other.GetComponent<EnemyHealth>();
            if (otherHealth)
            {
                otherHealth.TakeDamage(powers.GetPlayerPowers[0].Damage);
            }
        }
    }
}