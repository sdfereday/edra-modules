using UnityEngine;

namespace RedPanda.Interaction
{
    public interface IInteractible
    {
        void Trigger(INTERACTIBLE_TYPE originType, Transform originTransform);
        void Cancel(INTERACTIBLE_TYPE originType, Transform originTransform);
        Transform Transform { get; }
        INTERACTIBLE_TYPE InteractibleType { get; }
    }
}