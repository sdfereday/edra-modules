using UnityEngine;

namespace RedPanda.Effects
{
    public class VelocitySpriteFlip : MonoBehaviour
    {
        public bool StartsRight = true;

        private Rigidbody2D rBody;
        private SpriteRenderer SpriteRender;
        private float lastVelocity;
        private bool facingRight;

        private void Start()
        {
            rBody = GetComponent<Rigidbody2D>();
            SpriteRender = GetComponent<SpriteRenderer>();
            SpriteRender.flipX = !StartsRight;
        }

        private void Update()
        {
            if (rBody.velocity.magnitude != 0)
            {
                SpriteRender.flipX = rBody.velocity.normalized.x < 0;
            }
        }
    }
}