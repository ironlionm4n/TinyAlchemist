using System;
using EnemyScripts.Behaviors;
using EnemyScripts.Behaviors.EnemyBehaviorStates;
using Scripts.Interfaces;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, ITakeDamage
{
    [SerializeField] private int health;
    [SerializeField] private Animator animator;
    [SerializeField] private BoxCollider2D damageCollider;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private SpearGoblinBehavior _spearGoblinBehavior;

    public static event Action EnemyHit;
    
    private static readonly int Hit = Animator.StringToHash("Hit");
    private static readonly int Dead = Animator.StringToHash("Dead");

    public void TakeDamage(int damage)
    {
        Debug.Log("Enemy Damaged");
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
            audioSource.PlayOneShot(audioSource.clip);
            if (_spearGoblinBehavior)
            {
                _spearGoblinBehavior.UpdateCurrentState(new DeathState(animator, gameObject));
            }
            else
            {
                animator.SetTrigger(Dead);
            }
        }
    }
}