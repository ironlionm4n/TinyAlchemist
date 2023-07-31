using UnityEngine;

namespace Scripts.Interfaces
{
    public interface IKnockback
    {
        void KnockBack(Vector2 knockBackDirection, float knockBackForce);
        
    }
}