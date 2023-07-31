using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerDamageFlash
    {
        private SpriteRenderer _spriteRenderer;
        private Color _damageFlashColor, _normalColor;

        public PlayerDamageFlash(SpriteRenderer spriteRenderer, Color damageFlashColor, Color normalColor)
        {
            _spriteRenderer = spriteRenderer;
            _damageFlashColor = damageFlashColor;
            _normalColor = normalColor;
        }

        public void DamageFlashSpriteColor()
        {
            _spriteRenderer.color = _damageFlashColor;
        }

        public void NormalSpriteColor()
        {
            _spriteRenderer.color = _normalColor;
        }
    }
}