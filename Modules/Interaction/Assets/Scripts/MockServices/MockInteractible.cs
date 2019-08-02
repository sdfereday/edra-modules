using UnityEngine;
using RedPanda.Interaction;

namespace RedPanda.MockServices
{
    public class MockInteractible : MonoBehaviour, IInteractible
    {
        public Transform Transform => transform;
        public INTERACTIBLE_TYPE InteractibleType => INTERACTIBLE_TYPE.VOID;
                
        public void Trigger(INTERACTIBLE_TYPE originType, Transform originTransform)
        {
            Debug.Log($"{transform.name} got interaction signal from {originTransform.name}.");
            Debug.Log($"The interactible type of this mock is {InteractibleType}.");
        }

        public void Cancel(INTERACTIBLE_TYPE originType, Transform originTransform)
        {
            Debug.Log($"{transform.name} got cancel signal from {originTransform.name}.");
            Debug.Log($"The interactible type of this mock is {InteractibleType}.");
        }
    }
}
