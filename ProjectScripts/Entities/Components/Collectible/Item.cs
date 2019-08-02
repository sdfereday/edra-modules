using UnityEngine;
using RedPanda.Global;
using RedPanda.Interaction;
using RedPanda.UserInput;
using RedPanda.Inventory;

/*
    Use this for when an item has been placed not in a chest, but statically
    like a chest in the game world. This is not the same type as say something
    that might spawn out of a dead enemy for example (since they don't need to
    be logged).
*/
namespace RedPanda.Entities
{
    public class Item : FieldEntity, IInteractible, ICollectible
    {
        public CollectibleItem _CollectibleItemObject;
        public CollectibleItem CollectibleItemObject => _CollectibleItemObject;
        public Transform Transform => transform;
        public INTERACTIBLE_TYPE GetInteractibleType() => INTERACTIBLE_TYPE.COLLECTIBLE;

        public override void OnAssert(bool alreadyAcquired)
        {
            if (alreadyAcquired)
                Destroy(gameObject);
        }

        public void Use(Collider2D collider, INPUT_TYPE inputType)
        {
            if (CollectibleItemObject.IsKeyItem)
                throw new UnityException(ErrorConsts.NON_NORMAL_ITEM_ERROR);

            UpdateBoolState(true);

            RemotePrefabInstance.StartInteraction(() =>
            {
                Destroy(gameObject);
            });
        }
    }
}