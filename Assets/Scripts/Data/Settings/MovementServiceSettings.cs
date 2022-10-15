using Config;
using System;
using UnityEngine;

namespace Data.Settings
{
    [Serializable]
    public class Move 
    {
        public float Speed;
    }

    [Serializable]
    public class Rotate
    {
        public float Speed;
    }

    [Serializable]
    public class Jump
    {
        public float Speed;
        public ForceMode ForceMode;
        public LayerMask GroundLayers;
        public float RadiusModifier;
    }

    [Serializable]
    public class Fly
    {
        public float Speed;
    }

    [Serializable]
    public class Drive
    {
        public float Speed;
    }

    [Serializable]
    public class MovementServiceSettings : IRegistryData
    {
        public string Id;

        public Move Move;
        public Jump Jump;
        public Rotate Rotate;
        public Fly Fly;
        public Drive Drive;

        string IRegistryData.Id => Id;
    }
}