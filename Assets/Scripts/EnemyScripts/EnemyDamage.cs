using PlayerScripts;
using UnityEngine;

namespace EnemyScripts
{
    
    public class EnemyDamage : MonoBehaviour
    {
        [SerializeField] private int damage;
        [SerializeField] private float knockBackForce;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                var playerHealth = other.GetComponent<PlayerHealth>();
                var playerKnockBack = other.GetComponent<PlayerKnockBack>();
                playerHealth.TakeDamage(damage);

                var knockBackDirection = Vector2.zero;
                if (other.transform.position.y > transform.position.y)
                {
                    // Player is above the enemy
                    // Here, we use the direction of the player relative to the enemy to determine the direction of the knockback:
                    var horizontalDirection = (other.transform.position.x > transform.position.x) ? 1 : -1;
                    knockBackDirection = new Vector2(horizontalDirection, 1); // Assuming you want to knock upwards
                }
                else
                {
                    // Player is to the side of the enemy
                    var horizontalDirection = (other.transform.position.x > transform.position.x) ? 1 : -1;
                    knockBackDirection = new Vector2(horizontalDirection, 0); // No vertical knockback
                }

                var normalizedKnockBackDirection = knockBackDirection.normalized;
                playerKnockBack.KnockBack(normalizedKnockBackDirection, knockBackForce);
            }
        }
    }
}
