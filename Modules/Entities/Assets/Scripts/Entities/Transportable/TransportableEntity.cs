using UnityEngine;
using RedPanda.Interaction;
using RedPanda.UserInput;

namespace RedPanda.Entities
{
    public class TransportableEntity : MonoBehaviour, IInteractible, ICarryable
    {
        public bool CanInteract { get; private set; }
        public bool CanThrow { get { return true; } }
        public bool CanCarry { get; private set; }
        public int CurrentWeightValue { get { return 0; } }

        public Collider2D Collider;

        public Transform Transform => transform;
        public INTERACTIBLE_TYPE GetInteractibleType() => INTERACTIBLE_TYPE.TRANSPORTABLE;

        private void Start()
        {
            SetInteractible(true);
            SetCarryable(true);
        }

        public void SetInteractible(bool state)
        {
            CanInteract = state;
            Collider.enabled = state;
        }

        public void SetCarryable(bool state)
        {
            CanCarry = state;
        }

        public void Use(Collider2D collider, INPUT_TYPE inputType)
        {
            // ...
        }
    }
}