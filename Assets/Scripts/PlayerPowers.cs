using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerPowers : MonoBehaviour
    {
        private Animator _playerAnimator;
        private static readonly int Ignite = Animator.StringToHash("Ignite");

        private void Start()
        {
            _playerAnimator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _playerAnimator.SetTrigger(Ignite);
            }
        }
    }
}