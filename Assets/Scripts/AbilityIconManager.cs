using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    
    public class AbilityIconManager : MonoBehaviour
    {
        [SerializeField] Image igniteImage;
        
        private void OnEnable()
        {
            PlayerPowers.IgniteUsed += OnIgniteUsed;
        }

        private void OnDisable()
        {
            PlayerPowers.IgniteUsed -= OnIgniteUsed;
        }

        private void OnIgniteUsed(PlayerPowers playerPowers)
        {
            igniteImage.color = new Color(.5f, .5f, .5f, 1f);
            StartCoroutine(IgniteCooldown(playerPowers));
        }

        private IEnumerator IgniteCooldown(PlayerPowers playerPowers)
        {
            Debug.Log(playerPowers.IgniteCooldownTime);
            yield return new WaitForSeconds(playerPowers.IgniteCooldownTime);
            igniteImage.color = new Color(1f, 1f, 1f, 1f);
            playerPowers.IgniteRefreshed();
        }
    }
}