using Services.Item;
using System.Collections.Generic;

namespace Model.Inventory
{
    public class PlayerInventoryContainer : IInventoryContainer
    {
        public List<IItem> Items { get; set; }

        public PlayerInventoryContainer() 
        { 
            Items = new List<IItem>();
        }
    }
}
