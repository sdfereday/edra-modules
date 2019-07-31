using UnityEngine;
using RedPanda.Inventory;

namespace RedPanda.Inventory
{
    [CreateAssetMenu(fileName = "New Collectible Item", menuName = "Collectible Item", order = 51)]
    public class CollectibleItem : ScriptableObject
    {
        public string Id;

        public ITEM_TYPE _CollectibleItemType;
        public ITEM_TYPE CollectibleItemType => _CollectibleItemType;

        public bool IsKeyItem => CollectibleItemType == ITEM_TYPE.KEY_ITEM;

        public string _CollectibleItemName;
        public string CollectibleItemName => _CollectibleItemName;

        [TextArea]
        public string _CollectibleItemMeta;
        public string CollectibleItemMeta => _CollectibleItemMeta;

        public int _CollectibleItemHealthValue;
        public int CollectibleItemHealthValue => _CollectibleItemHealthValue;

        public int _CollectibleItemMpValue;
        public int CollectibleItemMpValue => _CollectibleItemMpValue;
    }
}