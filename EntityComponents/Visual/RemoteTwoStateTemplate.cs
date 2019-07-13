using UnityEngine;
using System;
using RedPanda.Effects;

namespace RedPanda.Entities
{
    public class RemoteTwoStateTemplate : MonoBehaviour, IRemotePrefab
    {
        public bool IsActive = false;
        public ToggledSprite ToggleSprite;

        private void Start()
        {
            if (IsActive)
            {
                ToggleSprite.On();
            }
            else
            {
                ToggleSprite.Off();
            }
        }

        public void StartInteraction(Action OnComplete = null)
        {
            ToggleSprite.Off();
            IsActive = !IsActive;

            if (IsActive)
            {
                ToggleSprite.On();
            }
            else
            {
                ToggleSprite.Off();
            }

            OnComplete?.Invoke();
        }

        public void SetActive(bool state)
        {
            IsActive = state;
            if (IsActive)
            {
                ToggleSprite.On();
            }
            else
            {
                ToggleSprite.Off();
            }
        }
    }
}