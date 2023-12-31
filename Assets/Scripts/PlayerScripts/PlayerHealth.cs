﻿using System;
using System.Collections;
using Scripts.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace PlayerScripts
{
    public class PlayerHealth : MonoBehaviour, ITakeDamage
    {
        public static event Action PlayerDied;
        [SerializeField] private int playerHealth;
        public int MaxHealth => playerHealth;
        
        [SerializeField] private Color damageFlashColor;
        [SerializeField] private Color normalColor;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Animator animator;
        [SerializeField] private AudioSource playerDamagedAudioSource;
        [SerializeField] private AudioSource healthPickUpAudioSource;
        [SerializeField] private AudioSource playerDeathAudioSource;

        private int _currentHealth;
        public int CurrentHealth => _currentHealth;
        
        private PlayerDamageFlash _playerDamageFlash;
        private bool _isDead;
        private static readonly int IsDead = Animator.StringToHash("IsDead");

        private void OnEnable()
        {
            HealthPickup.HealthPickedUp += OnHealthPickedUp;
        }
        
        private void OnDisable()
        {
            HealthPickup.HealthPickedUp -= OnHealthPickedUp;
        }

        private void OnHealthPickedUp(int healAmount)
        {
            _currentHealth += healAmount;
            healthPickUpAudioSource.PlayOneShot(healthPickUpAudioSource.clip);
            if (_currentHealth > playerHealth)
            {
                _currentHealth = playerHealth;
            }
        }

        private void Start()
        {
            _playerDamageFlash = new PlayerDamageFlash(spriteRenderer, damageFlashColor, normalColor);
            _currentHealth = playerHealth;
        }

        private void Update()
        {
            if (_currentHealth <= 0)
            {
                HandlePlayerDeath();
            }
        }

        public void TakeDamage(int damage)
        {
            if (_isDead) return;
            
            playerDamagedAudioSource.PlayOneShot(playerDamagedAudioSource.clip);
            _currentHealth -= damage;
            StartCoroutine(FlashWhenDamagedRoutine());
        }

        private IEnumerator FlashWhenDamagedRoutine()
        {
            _playerDamageFlash.DamageFlashSpriteColor();
            yield return new WaitForSeconds(.2f);
            _playerDamageFlash.NormalSpriteColor();
            yield return new WaitForSeconds(.1f);
            _playerDamageFlash.DamageFlashSpriteColor();
            yield return new WaitForSeconds(.2f);
            _playerDamageFlash.NormalSpriteColor();
        }

        public void HandlePlayerDeath()
        {
            Debug.Log("HandlePlayerDeath");
            animator.SetBool(IsDead, true);
            playerDeathAudioSource.PlayOneShot(playerDeathAudioSource.clip);
            PlayerDied?.Invoke();
        }
    }
}