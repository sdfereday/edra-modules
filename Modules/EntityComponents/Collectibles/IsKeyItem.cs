using UnityEngine;
using RedPanda.Global;
using RedPanda.Interaction;
using RedPanda.UserInput;
using RedPanda.Inventory;
using RedPanda.Storage;

/*
    Use this for when a container (such as a chest, etc) will give the player
    a key item. Difference here is that it gets added to a special key item bag.
    Doesn't need a world ID since we can only have on type of this key item, so
    we can just check if we have one or not.
*/
namespace RedPanda.Entities
{
    public class IsKeyItem : FieldEntity, IInteractible, ICollectible
    {
        public CollectibleItem _KeyItemObject;
        public CollectibleItem CollectibleItemObject => _KeyItemObject;
        public Transform Transform => transform;
        public INTERACTIBLE_TYPE GetInteractibleType() => INTERACTIBLE_TYPE.COLLECTIBLE;

        private PlayerKeyItemInventory KeyItemInventory;

        public override void OnAssert(bool alreadyAcquired)
        {
            KeyItemInventory = GameObject.FindGameObjectWithTag(DataConsts.GLOBAL_CONTEXT_TAG)
                .GetComponentInChildren<PlayerKeyItemInventory>();

            if (alreadyAcquired)
                Destroy(gameObject);

            /// Integrity check to see if we have the item yet if it's not set in save data (should never be a thing).
            if (!alreadyAcquired && KeyItemInventory.HasItem(CollectibleItemObject.Id) ||
                alreadyAcquired && !KeyItemInventory.HasItem(CollectibleItemObject.Id))
            {
                throw new System.Exception(ErrorConsts.KEY_ITEM_INTEGRITY_FAILUE);
            }
        }

        public void Use(Collider2D collider, INPUT_TYPE inputType)
        {
            if (!CollectibleItemObject.IsKeyItem)
                throw new UnityException(ErrorConsts.NON_KEY_ITEM_ERROR);

            UpdateBoolState(true);

            RemotePrefabInstance.StartInteraction(() =>
            {
                Destroy(gameObject);
            });
        }
    }
}