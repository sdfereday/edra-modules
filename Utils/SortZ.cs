using UnityEngine;

namespace RedPanda.Utils
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SortZ : MonoBehaviour
    {
        private SpriteRenderer spr;

        private void Awake()
        {
            spr = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (spr != null && Camera.main != null)
                spr.sortingOrder = (int)Camera.main.WorldToScreenPoint(spr.bounds.min).y * -1;
        }

    }
}