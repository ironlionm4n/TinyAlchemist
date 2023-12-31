﻿using Scripts.Interfaces;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerKnockBack : MonoBehaviour, IKnockback
    {
        [SerializeField] private Rigidbody2D playerRigidbody2D;
        [SerializeField] private PlayerMovement playerMovement;

        private bool _isDead;

        private void OnEnable()
        {
            PlayerHealth.PlayerDied += OnPlayerDied;
        }
        
        private void OnDisable()
        {
            PlayerHealth.PlayerDied -= OnPlayerDied;
        }

        private void OnPlayerDied()
        {
            _isDead = true;
        }

        public void KnockBack(Vector2 knockBackDirection, float knockBackForce)
        {
            if (_isDead)
            {
                return;
            }
            
            playerMovement.WasJustKnockedBack();
            playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x, 0f);
            playerRigidbody2D.AddForce(knockBackDirection * knockBackForce, ForceMode2D.Impulse);
        }
    }
}