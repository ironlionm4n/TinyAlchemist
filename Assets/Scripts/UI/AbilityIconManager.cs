using System.Collections;
using PlayerScripts;
using UnityEngine;
using UnityEngine.UI;

namespace UI
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
            // prevent memory leak
            PlayerPowers.IgniteUsed -= OnIgniteUsed;
        }

        private void OnIgniteUsed(PlayerPowers playerPowers)
        {
            igniteImage.color = new Color(.5f, .5f, .5f, 1f);
            StartCoroutine(IgniteCooldown(playerPowers));
        }

        private IEnumerator IgniteCooldown(PlayerPowers playerPowers)
        {
            yield return new WaitForSeconds(playerPowers.GetPlayerPowers[0].PowerCooldown);
            igniteImage.color = new Color(1f, 1f, 1f, 1f);
            playerPowers.IgniteRefreshed();
        }
    }
}