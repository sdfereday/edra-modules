using System;

namespace RedPanda.Entities
{
    public interface IRemotePrefab
    {
        void StartInteraction(Action OnComplete = null);
    }
}