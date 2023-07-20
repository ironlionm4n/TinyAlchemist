using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public void AnimationEventDeath()
    {
        Destroy(transform.parent.gameObject);
    }
}