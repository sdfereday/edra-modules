using UnityEngine;
using RedPanda.Interaction;
using RedPanda.UserInput;

namespace RedPanda.Entities
{
    public class StartsSaveGame : MonoBehaviour, IInteractible
    {
        public Transform Transform => transform;
        public INTERACTIBLE_TYPE GetInteractibleType() => INTERACTIBLE_TYPE.SAVE_THE_GAME;

        public void Use(Collider2D collider, INPUT_TYPE inputType)
        {
            // ...
        }
    }
}