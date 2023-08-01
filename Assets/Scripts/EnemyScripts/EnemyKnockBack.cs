using Scripts.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace EnemyScripts
{
    public class EnemyKnockBack : MonoBehaviour, IKnockback
    {
        [SerializeField] private Rigidbody2D enemyRigidbody2D;
    
        public void KnockBack(Vector2 knockBackDirection, float knockBackForce)
        {
            Debug.Log(knockBackDirection + " "+ knockBackForce);
            enemyRigidbody2D.AddForce(knockBackDirection * knockBackForce, ForceMode2D.Impulse);
        }
    }
}
