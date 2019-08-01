using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RedPanda.Inventory
{
    public class PlayerKeyItemInventory : MonoBehaviour
    {
        [System.Serializable]
        public class KeyItemMeta
        {
            public string Id;
            public string Name;
            public ITEM_TYPE Type;
        }

        [SerializeField]
        private List<KeyItemMeta> itemsField;
        public List<KeyItemMeta> Items { get => itemsField; }

        public void Init(List<KeyItemMeta> loadedItems = null)
        {
            itemsField = loadedItems != null ? new List<KeyItemMeta>(loadedItems) : new List<KeyItemMeta>();
        }

        public void AddItem(CollectibleItem keyItemObject)
        {
            var existing = itemsField.Find(x => x.Id == keyItemObject.Id || x.Name == keyItemObject.CollectibleItemName);

            if (existing != null)
            {
                throw new UnityException("You shouldn't have more than one of these items.");
            }
            else
            {
                // Meta is just used to populate the temporary instance of the UI, etc.
                itemsField.Add(new KeyItemMeta()
                {
                    Id = keyItemObject.Id,
                    Name = keyItemObject.CollectibleItemName,
                    Type = ITEM_TYPE.KEY_ITEM
                });
            }

        }

        public bool HasItem(string id) => itemsField.Any(x => x.Id == id);
    }
}