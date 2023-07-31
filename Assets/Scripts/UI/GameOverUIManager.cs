using System;
using PlayerScripts;
using UnityEngine;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class GameOverUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverUIPanel;
        
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
            gameOverUIPanel.SetActive(true);
        }

        public void HandleRestartClicked()
        {
            var currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
            UnityEngine.SceneManagement.SceneManager.LoadScene(currentScene.name);
        }
    }
}