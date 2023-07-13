using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerPowers : MonoBehaviour
    {
        [SerializeField] private float igniteCooldownTime;
        public float IgniteCooldownTime => igniteCooldownTime;
        private Animator _playerAnimator;
        private static readonly int Ignite = Animator.StringToHash("Ignite");
        private bool _canIgnite = true;

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
                IgniteUsed?.Invoke(this);
            }
        }

        public void IgniteRefreshed()
        {
            _canIgnite = true;
        }
    }
}