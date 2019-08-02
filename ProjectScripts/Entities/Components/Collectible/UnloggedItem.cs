using UnityEngine;
using RedPanda.Global;
using RedPanda.Interaction;
using RedPanda.Inventory;
using RedPanda.UserInput;

/*
    This is the type of item that just gets instantiated and picked up without any
    logging being done. Sort of simulates what an enemy might pop out when you
    slay it, or some sort of exploding treasure chest of wonders.
*/
namespace RedPanda.Entities
{
    public class UnloggedItem : FieldEntity, IInteractible, ICollectible
    {
        public CollectibleItem _CollectibleItemObject;
        public CollectibleItem CollectibleItemObject => _CollectibleItemObject;
        public Transform Transform => transform;
        public INTERACTIBLE_TYPE GetInteractibleType() => INTERACTIBLE_TYPE.COLLECTIBLE;

        public void Use(Collider2D collider, INPUT_TYPE inputType)
        {
            if (CollectibleItemObject.IsKeyItem)
                throw new UnityException(ErrorConsts.NON_NORMAL_ITEM_ERROR);

            Destroy(gameObject);
        }
    }
}