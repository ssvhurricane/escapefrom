using Config;
using Services.Ability;
using Services.Input;
using Services.Item.Weapon;
using System;
using UnityEngine;

namespace Data.Settings
{
    [Serializable]
    public class AbilitySettings : IRegistryData
    {
        public string Id;

        public string Name;

        public string AbilityDescription;

        public AbilityType AbilityType;

        public WeaponType WeaponType;
        
        public Sprite Icon;

        public ActionModifier[] ActionModifier;
      
        string IRegistryData.Id => Id;
    }
}