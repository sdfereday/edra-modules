using UnityEngine;
using RedPanda.Interaction;
using RedPanda.UserInput;

namespace RedPanda.Interaction
{
    public interface IInteractible
    {
        void Use(Collider2D collider, INPUT_TYPE inputType);
        Transform Transform { get; }

        INTERACTIBLE_TYPE GetInteractibleType();
    }
}