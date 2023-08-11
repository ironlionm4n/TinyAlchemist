using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Misc
{
    public class ArrowCollision : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D arrowRigidbody2D;
        [SerializeField] private float gravityScaleDelta;
        [SerializeField] private float alphaChangeDelta;
        [SerializeField, Range(33,90)] private float angularChangeInDegrees;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
            else
            {
                arrowRigidbody2D.AddTorque(angularChangeInDegrees * Mathf.Deg2Rad, ForceMode2D.Impulse);
                StartCoroutine(HandleArrowHitGroundLayerRoutine());
            }
        }

        private IEnumerator HandleArrowHitGroundLayerRoutine()
        {
            GetComponent<PolygonCollider2D>().enabled = false;
            var startingGravity = 0f; 
            var arrowSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
            var startingSpriteAlpha = arrowSpriteRenderer.color.a;
            while ((1 - arrowRigidbody2D.gravityScale) > 0.1f)
            {
                startingGravity = Mathf.MoveTowards(startingGravity, 1f, gravityScaleDelta);
                arrowRigidbody2D.gravityScale = startingGravity;
                Debug.Log(arrowRigidbody2D.gravityScale);
                startingSpriteAlpha = Mathf.MoveTowards(startingSpriteAlpha, 0, alphaChangeDelta);
                arrowSpriteRenderer.color = new Color(arrowSpriteRenderer.color.r, arrowSpriteRenderer.color.g,
                    arrowSpriteRenderer.color.b, startingSpriteAlpha);
                yield return null;
            }
        }
    }
}
