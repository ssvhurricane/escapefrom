using UnityEngine;

namespace Services.Anchor
{
    public class Anchor
    {
        public string Name { get; set; }

        public bool IsActive { get; set; }

        public Transform Transform{ get; set; }

        public AnchorType AnchorType { get; set; }

        public AnchorGroup AnchorGroup { get; set; }

        public Anchor(string name, bool isActive, Transform transform, AnchorType anchorType, AnchorGroup anchorGroup) 
        {
            Name = name;

            IsActive = isActive;

            Transform = transform;

            AnchorType = anchorType;

            AnchorGroup = anchorGroup;
        }
    }
}