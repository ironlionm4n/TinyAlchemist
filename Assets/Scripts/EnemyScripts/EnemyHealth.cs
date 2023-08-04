using System;
using Scripts.Interfaces;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, ITakeDamage
{
    [SerializeField] private int health;
    [SerializeField] private Animator animator;
    [SerializeField] private BoxCollider2D damageCollider;
    [SerializeField] private AudioSource audioSource;

    public static event Action EnemyHit;
    
    private static readonly int Hit = Animator.StringToHash("Hit");
    private static readonly int Dead = Animator.StringToHash("Dead");

    public void TakeDamage(int damage)
    {
        Debug.Log("Hello");
        animator.SetTrigger(Hit);
        EnemyHit?.Invoke();
        health -= damage;
        CheckForDeath();
    }

    private void CheckForDeath()
    {
        if (health <= 0)
        {
            damageCollider.enabled = false;
            animator.SetTrigger(Dead);
            audioSource.PlayOneShot(audioSource.clip);
        }
    }
}