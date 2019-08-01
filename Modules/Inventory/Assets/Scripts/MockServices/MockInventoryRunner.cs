using UnityEngine;
using RedPanda.Inventory;

namespace RedPanda.MockServices
{
    public class MockInventoryRunner : MonoBehaviour
    {
        public GameObject testItemPrefab;
        public GameObject testKeyItemPrefab;

        private PlayerInventory itemInventory;
        private PlayerKeyItemInventory keyItemInventory;

        private void Start()
        {
            /// Standard item inventory
            itemInventory = FindObjectOfType<PlayerInventory>();
            
            // Simulate loading in existing saved inventory data
            itemInventory.Init(MockInventoryService.items);

            // Create item to exist in game world
            GameObject testItem = Instantiate(testItemPrefab, transform.position, Quaternion.identity);

            // Simulate picking up an item (using Scriptable Object data) - 1 existing + 10 below added = 11
            for (int i = 1; i <= 10; i++)
            {
                itemInventory.AddItem(testItem.GetComponent<MockTestItem>().ItemObject, 1);
            }

            // Simulate removing an item - 11 existing - 1 removed = 10
            itemInventory.RemoveItem("chickenId", 1);

            /// Key item inventory
            keyItemInventory = FindObjectOfType<PlayerKeyItemInventory>();
            
            // Simulate loading in existing saved key item inventory data
            keyItemInventory.Init(MockInventoryService.keyItems);

            // Create item to exist in game world
            GameObject testKeyItem = Instantiate(testKeyItemPrefab, transform.position, Quaternion.identity);

            // Simulate picking up a key item (using Scriptable Object data), should error
            keyItemInventory.AddItem(testKeyItem.GetComponent<MockTestItem>().ItemObject);
        }
    }
}