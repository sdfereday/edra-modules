using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RedPanda.Storage;
using RedPanda.Interaction;

namespace RedPanda.Inventory
{
    public class PlayerKeyItemInventory : MonoBehaviour
    {
        [System.Serializable]
        public class KeyItemMeta
        {
            public string Name;
            public string Id;
            public ITEM_TYPE Type;
        }

        public List<KeyItemMeta> _keyItems;
        public List<KeyItemMeta> Keyitems { get => _keyItems; }

        private void Awake()
        {
            var loadedItems = SaveDataManager.LoadData<List<KeyItemMeta>>(DataConsts.KEY_ITEM_DATA_FILE);
            _keyItems = loadedItems != null ? loadedItems : new List<KeyItemMeta>();
        }

        private void OnEnable()
        {
            SaveGame.OnSaveSignal += SaveToDisk;
        }

        private void OnDisable()
        {
            SaveGame.OnSaveSignal -= SaveToDisk;
        }

        public void SaveToDisk()
        {
            SaveDataManager.SaveData(_keyItems, DataConsts.KEY_ITEM_DATA_FILE);
        }

        public void AddItem(CollectibleItem _keyItemObject)
        {
            var existing = _keyItems.Find(x => x.Id == _keyItemObject.Id || x.Name == _keyItemObject.CollectibleItemName);

            if (existing != null)
            {
                throw new UnityException("You shouldn't have more than one of these items.");
            }
            else
            {
                // Meta is just used to populate the temporary instance of the UI, etc.
                _keyItems.Add(new KeyItemMeta()
                {
                    Name = _keyItemObject.CollectibleItemName,
                    Id = _keyItemObject.Id,
                    Type = ITEM_TYPE.KEY_ITEM
                });
            }

        }

        public bool HasItem(string _id) => _keyItems.Any(x => x.Id == _id);
    }
}