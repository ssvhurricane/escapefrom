using Model.Inventory;
using Services.Essence;
using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Services.Item
{
    public class ItemService
    {
        private readonly SignalBus _signalBus;

        private List<BaseEssence> _itemViews, _itemWindows;
        public ItemService(SignalBus signalBus)
        {
            _signalBus = signalBus;

            _itemViews = new List<BaseEssence>();
            _itemWindows = new List<BaseEssence>();
        }

        public void AddItemToContainer(IInventoryContainer inventoryContainer, IItem item)
        {
            inventoryContainer.Items.Add(item);
        }

        public void RemoveItemFromContainer(IInventoryContainer inventoryContainer, IItem item)
        {
            inventoryContainer.Items.Remove(item);

            // ToDo... Remove _itemWiews and _itemWindows
        }

        public IItem GetItemById(IInventoryContainer inventoryContainer, string id)
        {
            return inventoryContainer.Items.FirstOrDefault(item => item.Id == id);
        }

        public IItem GetItemByName(IInventoryContainer inventoryContainer, string name)
        {
            return inventoryContainer.Items.FirstOrDefault(item => item.Name == name);
        }

        public IEnumerable<IItem> GetAllItems(IInventoryContainer inventoryContainer)
        {
            return inventoryContainer.Items;
        }

        public IEnumerable<IItem> GetItemsByType(IInventoryContainer inventoryContainer, ItemType itemType)
        {
            return inventoryContainer.Items.Where(item => item.ItemType == itemType);
        }

        public void AddItemView(BaseEssence baseView)
        {
            _itemViews.Add(baseView);
        }

        public void RemoveItemView(BaseEssence baseView)
        {
            _itemViews.Remove(baseView);
        }

        public List<BaseEssence> GetAllItemViews()
        {
            return _itemViews;
        }
        public void ClearServiceValues() 
        {
            _itemViews.Clear();
            _itemWindows.Clear();
        }
    }
}
