using System.Collections.Generic;
using RedPanda.Inventory;

/* A mock collection of items that, if game saved, will appear here. Usually
 * you'd see these in a JSON file or something. They only get updated when you
 * actually save the game though. You'll find there's no schema right now
 * for the different items but there 'are' objects pre-built that the system
 * will look for. So basically you can't just throw any old thing in here
 * and expect it to work. It has to be an item that exists in the game. */
namespace RedPanda.MockServices
{
    public static class MockInventoryService
    {
        public static List<PlayerInventory.ItemMeta> items = new List<PlayerInventory.ItemMeta>()
        {
            new PlayerInventory.ItemMeta()
            {
                Id = "chickenId",
                Name = "Chicken",
                Qty = 1,
                Type = ITEM_TYPE.STANDARD_ITEM,
                HealthValue = 100,
                MpValue = 50
            }
        };

        public static List<PlayerKeyItemInventory.KeyItemMeta> keyItems = new List<PlayerKeyItemInventory.KeyItemMeta>()
        {
            new PlayerKeyItemInventory.KeyItemMeta()
            {
                Id = "keyItemId",
                Name = "Golden Key Of Legend",
                Type = ITEM_TYPE.KEY_ITEM
            }
        };
    }
}