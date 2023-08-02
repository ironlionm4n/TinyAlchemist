using System.Collections;
using PlayerScripts;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class AbilityIconManager : MonoBehaviour
    {
        [SerializeField] private Image igniteImage;
        [SerializeField] private Image furyFistImage;
        [SerializeField] private Image fireDashImage;
        [SerializeField] private Image magmaShotImage;
        
        private void OnEnable()
        {
            PlayerPowers.IgniteUsed += OnIgniteUsed;
            PlayerPowers.FuryFistUsed += OnFuryFistUsed;
            PlayerPowers.FireDashUsed += OnFireDashUsed;
            PlayerPowers.MagmaShotUsed += OnMagmaShotUsed;
        }

        private void OnDisable()
        {
            // prevent memory leak
            PlayerPowers.IgniteUsed -= OnIgniteUsed;
            PlayerPowers.FuryFistUsed -= OnFuryFistUsed;
            PlayerPowers.FireDashUsed -= OnFireDashUsed;
            PlayerPowers.MagmaShotUsed -= OnMagmaShotUsed;
        }

        private void OnIgniteUsed(PlayerPowers playerPowers)
        {
            igniteImage.color = new Color(.5f, .5f, .5f, 1f);
            StartCoroutine(IgniteCooldown(playerPowers));
        }

        private void OnFuryFistUsed(PlayerPowers playerPowers)
        {
            furyFistImage.color = new Color(.5f, .5f, .5f, 1f);
            StartCoroutine(FuryFistRefreshed(playerPowers));
        }

        private void OnFireDashUsed(PlayerPowers playerPowers)
        {
            fireDashImage.color = new Color(.5f, .5f, .5f, 1f);
            StartCoroutine(FireDashRefreshed(playerPowers));
        }

        private void OnMagmaShotUsed(PlayerPowers playerPowers)
        {
            magmaShotImage.color = new Color(.5f, .5f, .5f, 1f);
            StartCoroutine(MagmaShotRefreshed(playerPowers));
        }

        private IEnumerator IgniteCooldown(PlayerPowers playerPowers)
        {
            yield return new WaitForSeconds(playerPowers.GetPlayerPowers[0].PowerCooldown);
            igniteImage.color = new Color(1f, 1f, 1f, 1f);
            playerPowers.IgniteRefreshed();
        }

        private IEnumerator FuryFistRefreshed(PlayerPowers playerPowers)
        {
            yield return new WaitForSeconds(playerPowers.GetPlayerPowers[1].PowerCooldown);
            furyFistImage.color = new Color(1f, 1f, 1f, 1f);
            playerPowers.FuryFistRefreshed();
        }

        private IEnumerator FireDashRefreshed(PlayerPowers playerPowers)
        {
            yield return new WaitForSeconds(playerPowers.GetPlayerPowers[2].PowerCooldown);
            fireDashImage.color = new Color(1f, 1f, 1f, 1f);
            playerPowers.FireDashRefreshed();
        }

        private IEnumerator MagmaShotRefreshed(PlayerPowers playerPowers)
        {
            yield return new WaitForSeconds(playerPowers.GetPlayerPowers[3].PowerCooldown);
            magmaShotImage.color =  new Color(1f, 1f, 1f, 1f);
            playerPowers.MagmaShotRefreshed();
        }
    }
}