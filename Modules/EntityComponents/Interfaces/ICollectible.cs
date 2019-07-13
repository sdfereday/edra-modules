using RedPanda.Inventory;

namespace RedPanda.Entities
{
    public interface ICollectible
    {
        CollectibleItem CollectibleItemObject { get; }
    }
}