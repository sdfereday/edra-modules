using System.Collections.Generic;
using UnityEngine;

namespace RedPanda.Inventory
{
    public class PlayerInventory : MonoBehaviour
    {
        [System.Serializable]
        public class ItemMeta
        {
            public string Id;
            public string Name;
            public int Qty;
            public ITEM_TYPE Type;
            public int HealthValue;
            public int MpValue;
        }

        [SerializeField]
        public List<ItemMeta> itemsField;
        public List<ItemMeta> Items { get => itemsField; }

        public void Init(List<ItemMeta> loadedItems = null)
        {
            // Notice how it's the meta that's getting stored here. The SO doesn't get touched
            // and is only used for reference. This might be what needs to be done on the 
            // scene context also...
            itemsField = loadedItems != null ? new List<ItemMeta>(loadedItems) : new List<ItemMeta>();
        }

        public void AddItem(CollectibleItem collectibleItemObject, int qty = 1)
        {
            // TODO: Maybe don't find by type. An id is far more effective (use a table for this).
            // Check for existing and increase qty if so.
            var existing = itemsField.Find(x => x.Type == collectibleItemObject.CollectibleItemType);

            if (existing != null)
            {
                existing.Qty += qty;
            }
            else
            {
                // Meta is just used to populate the temporary instance of the UI, etc.
                itemsField.Add(new ItemMeta()
                {
                    // TODO: Make sure we're checking against a schema to cut down on splicing hacks (if it's not in schema, it's not getting in).
                    Id = collectibleItemObject.Id,
                    Name = collectibleItemObject.CollectibleItemName,
                    Qty = qty,
                    Type = collectibleItemObject.CollectibleItemType,
                    HealthValue = collectibleItemObject.CollectibleItemHealthValue,
                    MpValue = collectibleItemObject.CollectibleItemMpValue
                });
            }
        }

        public void RemoveItem(string Id, int qty = 1)
        {
            var existing = itemsField.Find(x => x.Id == Id);

            if (existing != null)
            {
                if (existing.Qty - qty > 0)
                {
                    existing.Qty -= qty;
                }
                else
                {
                    itemsField.RemoveAll(x => x.Id == Id);
                }
            }
        }
    }
}