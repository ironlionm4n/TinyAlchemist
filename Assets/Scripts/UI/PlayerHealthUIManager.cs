using PlayerScripts;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerHealthUIManager : MonoBehaviour
    {
        [SerializeField] private PlayerHealth playerHealth;
        [SerializeField] private Image HealthGlobe;
        
        void Update()
        {
            HealthGlobe.fillAmount = (float) playerHealth.CurrentHealth / playerHealth.MaxHealth;
        }
    }
}