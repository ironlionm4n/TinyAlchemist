using System;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerPowers : MonoBehaviour
    {
        [SerializeField] private List<PlayerPowerScriptableObjects> playerPowers;
        public List<PlayerPowerScriptableObjects> GetPlayerPowers => playerPowers;
        private Animator _playerAnimator;
        private static readonly int Ignite = Animator.StringToHash("Ignite");
        private bool _canIgnite = true;
        private static readonly int IsInPower = Animator.StringToHash("IsInPower");

        public static event Action<PlayerPowers> IgniteUsed;
        
        private void Start()
        {
            _playerAnimator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && _canIgnite)
            {
                _canIgnite = false;
                _playerAnimator.SetTrigger(Ignite);
                _playerAnimator.SetBool(IsInPower, true);
                IgniteUsed?.Invoke(this);
            }
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