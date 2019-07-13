using UnityEngine;

namespace RedPanda.Effects
{
    public class ToggledSprite : MonoBehaviour
    {
        public Sprite OnSprite;
        public Sprite OffSprite;
        public SpriteRenderer Spr;

        private bool IsOn = false;

        private void SetSprite(bool _isOn)
        {
            Spr.sprite = _isOn ? OnSprite : OffSprite;
        }

        public void Toggle()
        {
            SetSprite(IsOn);
            IsOn = !IsOn;
        }

        public void On()
        {
            IsOn = true;
            SetSprite(IsOn);
        }

        public void Off()
        {
            IsOn = false;
            SetSprite(IsOn);
        }
    }
}