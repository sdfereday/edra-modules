using UnityEngine;

namespace RedPanda.Interaction
{
    public interface IResponseTask
    {
        INTERACTIBLE_TYPE RespondsTo { get; }
        RESPONSE_TYPE ResponseType { get; }
        bool IsActive { get; }
        void Run(INTERACTIBLE_TYPE originType, Transform originTransform);
        void Next();
        void Complete();
    }
}