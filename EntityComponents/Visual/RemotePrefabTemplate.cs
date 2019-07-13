using UnityEngine;
using System;

namespace RedPanda.Entities
{
    public class RemotePrefabTemplate : MonoBehaviour, IRemotePrefab
    {
        public void StartInteraction(Action OnComplete = null)
        {
            OnComplete?.Invoke();
        }
    }
}