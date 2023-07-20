using System;
using Scripts.Interfaces;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, ITakeDamage
{
    [SerializeField] private int health;
    [SerializeField] private Animator animator;
    
    public static event Action EnemyHit;
    
    private static readonly int Hit = Animator.StringToHash("Hit");
    private static readonly int Dead = Animator.StringToHash("Dead");

    public void TakeDamage(int damage)
    {
        animator.SetTrigger(Hit);
        EnemyHit?.Invoke();
        health -= damage;
        CheckForDeath();
    }

    private void CheckForDeath()
    {
        if (health <= 0)
        {
            animator.SetTrigger(Dead);
        }
    }
}