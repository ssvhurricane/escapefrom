using Config;
using System;
using UnityEngine;

namespace Data.Settings
{
    [Serializable]
    public class CameraServiceSettings : IRegistryData
    {
        public string Id;

        public string Name;

        public Services.Camera.CameraType CameraType;

        public Vector3 Position;

        public Vector3 Rotation;

        public float CameraFollowSmoothSpeed;

        public Vector3 CameraFollowOffset;

        string IRegistryData.Id => Id;
    }
}