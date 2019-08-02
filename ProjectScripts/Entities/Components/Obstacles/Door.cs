using UnityEngine;
using RedPanda.Storage;
using RedPanda.Interaction;
using RedPanda.Inventory;
using RedPanda.UserInput;

namespace RedPanda.Entities
{
    public class Door : FieldEntity, IInteractible
    {
        // You may wish to have an 'open state' also for massive doors / bridges in other components.
        public bool IsUnlocked = false; // <-- Use an interface for things like this (ILockable) or something.
        public CollectibleItem ExpectedObjectInInventory;
        public Transform Transform => transform;
        public INTERACTIBLE_TYPE GetInteractibleType() => INTERACTIBLE_TYPE.DOORWAY;

        private PlayerKeyItemInventory KeyItemInventory;

        public override void OnAssert(bool alreadyUnlocked)
        {
            KeyItemInventory = GameObject.FindGameObjectWithTag(DataConsts.GLOBAL_CONTEXT_TAG)
                .GetComponentInChildren<PlayerKeyItemInventory>();

            IsUnlocked = alreadyUnlocked && KeyItemInventory.HasItem(ExpectedObjectInInventory.Id);
        }

        public void Use(Collider2D collider, INPUT_TYPE inputType)
        {
            IsUnlocked = KeyItemInventory.HasItem(ExpectedObjectInInventory.Id);
            UpdateBoolState(IsUnlocked);

            if (IsUnlocked)
                RemotePrefabInstance.StartInteraction();
        }
    }
}