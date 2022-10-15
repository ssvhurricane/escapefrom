using Config;
using Services.Item;
using Services.Item.Weapon;
using System;
using UnityEngine;

namespace Data.Settings
{
    [Serializable]
    public class ItemServiceSettings : IRegistryData
    {
        public string Id;

        public string Name;

        public string Description;

        public Sprite Icon;

        public GameObject Prefab;

        public ItemType ItemType;

        string IRegistryData.Id => Id;
    }
}
