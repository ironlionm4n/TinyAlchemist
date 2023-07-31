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
                var knockBackDirection = other.transform.position - transform.position;
                var normalizedKnockBackDirection = new Vector2(knockBackDirection.x, knockBackDirection.y).normalized;
                Debug.Log(normalizedKnockBackDirection);
                playerKnockBack.KnockBack(normalizedKnockBackDirection, knockBackForce);
            }
        }
    }
}
