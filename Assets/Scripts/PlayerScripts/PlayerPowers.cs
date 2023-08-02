﻿using System;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerPowers : MonoBehaviour
    {
        [SerializeField] private List<PlayerPowerScriptableObjects> playerPowers;
        [SerializeField] private PlayerMovement playerMovement;
        [Header("AudioSources")]
        [SerializeField] private AudioSource igniteAudioSource;
        [SerializeField] private AudioSource furyFistAudioSource;
        [SerializeField] private AudioSource fireDashAudioSource;
        [SerializeField] private AudioSource magmaShotAudioSource;
        
        public List<PlayerPowerScriptableObjects> GetPlayerPowers => playerPowers;
        private Animator _playerAnimator;
        private static readonly int Ignite = Animator.StringToHash("Ignite");
        private bool _canIgnite = true;
        private bool _isDead;
        private bool _canFuryFist = true;
        private static readonly int IsInPower = Animator.StringToHash("IsInPower");
        private static readonly int FuryFist = Animator.StringToHash("FuryFist");
        private bool _canFireDash = true;
        private static readonly int FireDash = Animator.StringToHash("FireDash");
        private static readonly int MagmaShot = Animator.StringToHash("MagmaShot");
        private bool _canMagmaShot = true;

        public static event Action<PlayerPowers> IgniteUsed;
        public static event Action<PlayerPowers> FuryFistUsed;
        public static event Action<PlayerPowers> FireDashUsed;
        public static event Action<PlayerPowers> MagmaShotUsed; 
        private void OnEnable()
        {
            PlayerHealth.PlayerDied += OnPlayerDied;
        }

        private void OnPlayerDied()
        {
            _isDead = true;
        }

        private void Start()
        {
            _playerAnimator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (_isDead) return;
            
            if (!playerMovement.IsDashing && (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) && _canIgnite)
            {
                _canIgnite = false;
                _playerAnimator.SetTrigger(Ignite);
                igniteAudioSource.PlayOneShot(igniteAudioSource.clip);
                _playerAnimator.SetBool(IsInPower, true);
                IgniteUsed?.Invoke(this);
            }
            if (!playerMovement.IsDashing && (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)) && _canFuryFist)
            {
                _canFuryFist = false;
                _playerAnimator.SetTrigger(FuryFist);
                furyFistAudioSource.PlayOneShot(furyFistAudioSource.clip);
                _playerAnimator.SetBool(IsInPower, true);
                FuryFistUsed?.Invoke(this);
            }
            if (!playerMovement.IsDashing && (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3)) && _canFireDash)
            {
                _canFireDash = false;
                _playerAnimator.SetTrigger(FireDash);
                fireDashAudioSource.PlayOneShot(fireDashAudioSource.clip);
                _playerAnimator.SetBool(IsInPower, true);
                FireDashUsed?.Invoke(this);
            }
            if (!playerMovement.IsDashing && (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4)) && _canMagmaShot)
            {
                _canMagmaShot = false;
                _playerAnimator.SetTrigger(MagmaShot);
                magmaShotAudioSource.PlayOneShot(magmaShotAudioSource.clip);
                _playerAnimator.SetBool(IsInPower, true);
                MagmaShotUsed?.Invoke(this);
            }
        }
        
        private void OnDisable()
        {
            PlayerHealth.PlayerDied -= OnPlayerDied;
        }

        public void IgniteRefreshed()
        {
            _canIgnite = true;
        }

        public void FuryFistRefreshed()
        {
            _canFuryFist = true;
        }

        public void FireDashRefreshed()
        {
            _canFireDash = true;
        }

        public void MagmaShotRefreshed()
        {
            _canMagmaShot = true;
        }

        public void NotInPower()
        {
            _playerAnimator.SetBool(IsInPower, false);
        }
    }
}