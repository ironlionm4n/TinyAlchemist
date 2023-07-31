using System;
using System.Collections.Generic;
using PlayerScripts;
using ScriptableObjects;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerPowers : MonoBehaviour
    {
        [SerializeField] private List<PlayerPowerScriptableObjects> playerPowers;
        public List<PlayerPowerScriptableObjects> GetPlayerPowers => playerPowers;
        private Animator _playerAnimator;
        private static readonly int Ignite = Animator.StringToHash("Ignite");
        private bool _canIgnite = true;
        private bool _isDead;
        private static readonly int IsInPower = Animator.StringToHash("IsInPower");

        public static event Action<PlayerPowers> IgniteUsed;
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
            
            if (Input.GetKeyDown(KeyCode.Alpha1) && _canIgnite)
            {
                _canIgnite = false;
                _playerAnimator.SetTrigger(Ignite);
                _playerAnimator.SetBool(IsInPower, true);
                IgniteUsed?.Invoke(this);
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

        public void NotInPower()
        {
            _playerAnimator.SetBool(IsInPower, false);
        }
    }
}