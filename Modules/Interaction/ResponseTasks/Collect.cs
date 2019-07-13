using UnityEngine;
using RedPanda.Inventory;
using RedPanda.Entities;

namespace RedPanda.Interaction
{
    public class Collect : MonoBehaviour, IResponseTask
    {
        public PlayerKeyItemInventory KeyItemInventory;
        public PlayerInventory Inventory;

        public bool IsActive { get; private set; }
        public RESPONSE_TYPE ResponseType => RESPONSE_TYPE.DEFAULT;

        public void Run(INTERACTIBLE_TYPE originType, Transform originTransform)
        {
            var itemData = originTransform.GetComponent<ICollectible>();

            if (itemData != null)
            {
                Debug.Log("Collected an item:");
                Debug.Log(itemData);
            }
            else
            {
                throw new UnityException("Item was not available. Did you add an 'ICollectible' interface?");
            }

            if (itemData.CollectibleItemObject.IsKeyItem)
            {
                KeyItemInventory.AddItem(itemData.CollectibleItemObject);
            }
            else
            {
                Inventory.AddItem(itemData.CollectibleItemObject);
            }
        }

        public void Complete()
        {
            // ...
        }

        public void Next()
        {
            // ...
        }
    }
}