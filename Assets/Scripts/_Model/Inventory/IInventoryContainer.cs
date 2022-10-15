using Services.Item;
using System.Collections.Generic;

namespace Model.Inventory
{
    public interface IInventoryContainer
    {
        public List<IItem> Items { get; set; }
      
    }
}
