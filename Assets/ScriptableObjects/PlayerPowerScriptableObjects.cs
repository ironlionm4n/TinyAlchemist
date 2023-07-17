using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Player Powers", fileName = "NewPlayerPower")]
    public class PlayerPowerScriptableObjects : ScriptableObject
    {
        [SerializeField] private int damage;
        [SerializeField] private float powerCooldown;
        public int Damage => damage;
        public float PowerCooldown => powerCooldown;

    }
}
