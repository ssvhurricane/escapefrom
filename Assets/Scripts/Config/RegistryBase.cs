using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Config
{
    public abstract class RegistryBase<TData> : ScriptableObject, IRegistry 
        where TData : class
    {
        [SerializeField] protected TData RegistryData;

        public TData Data { get { return RegistryData; } }
    }

    public abstract class RegistryListBase<TData> : ScriptableObject, IRegistryList
        where TData : class, IRegistryData
    {
        [SerializeField] protected List<TData> RegistryItems;

        public int Length 
        {
            get { return RegistryItems.Count; }
        }

        public IEnumerator GetEnumerator()
        {
            return RegistryItems.GetEnumerator();
        }

        public IEnumerable<TData> GetItems() 
        {
            return RegistryItems;
        }

        public Dictionary<string, TData> ToDictionary() 
        {
            return RegistryItems.ToDictionary(key => key.Id, value => value);
        }

        public TData GetById(string id)
        {
            return RegistryItems.FirstOrDefault(item=>string.CompareOrdinal(item.Id, id) == 0);
        }
    }

}
