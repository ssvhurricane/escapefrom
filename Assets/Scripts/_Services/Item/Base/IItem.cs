using UnityEngine;

namespace Services.Item
{
    public interface IItem 
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public GameObject Prefab { get; set; }

        public ItemType ItemType { get; set; }

        public GameObject Owner { get; set; }
    }
}
