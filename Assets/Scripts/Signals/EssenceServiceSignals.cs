using Services.Essence;
using UnityEngine;

namespace Signals
{
    public class EssenceServiceSignals
    {
        public class Register 
        {
            public IEssence Essence { get; set; }

            public Register(IEssence essence) 
            {
                Essence = essence;
            }
        }
        public class AddHolder
        {
            public Transform Transform { get; }

            public EssenceType EssenceType { get; }

            public AddHolder(Transform transform, EssenceType EssenceType)
            {
                Transform = transform;
                this.EssenceType = EssenceType;
            }
        }

        public class Shown
        {
            public IEssence Essence { get; }

            public Shown(IEssence Essence)
            {
                this.Essence = Essence;
            }
        }

        public class Hidden
        {
            public IEssence Essence { get; }

            public Hidden(IEssence essence)
            {
                this.Essence = essence;
            }
        }
    }
}
