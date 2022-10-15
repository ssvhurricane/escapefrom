using System;
using UnityEngine;

namespace Services.Log
{
    [Serializable]
    public class LogItemData
    {
        public string Name;

        public bool Enable;

        public Color Color;

        public LogItemType LogItemType;
    }
}
