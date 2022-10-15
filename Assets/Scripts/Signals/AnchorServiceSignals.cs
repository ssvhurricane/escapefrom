using Services.Anchor;
using UnityEngine;

namespace Signals
{
    public class AnchorServiceSignals 
    {
        public class Add
        {
            public string Name { get; }
            public Transform Transform { get; }

            public AnchorType AnchorType { get; }

            public Add(string nameAnchor, Transform transform, AnchorType AnchorType)
            {
                Name = nameAnchor;
                Transform = transform;
                this.AnchorType = AnchorType;
            }
        }

        public class Activate
        {
           // TODO:
        }

        public class Deactivate
        {
           // TODO:
        }
    }
}
