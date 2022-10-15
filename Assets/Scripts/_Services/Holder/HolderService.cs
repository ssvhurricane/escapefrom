using Services.Essence;
using Services.Window;
using Signals;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Services.Anchor
{
    public class HolderService
    {
        private readonly SignalBus _signalBus;
        public Dictionary<EssenceType, Transform> _essenceTypeTypeHolders { get; private set; }

        public Dictionary<WindowType, Transform> _windowTypeHolders { get; private set; }
       
        public HolderService(SignalBus signalBus)
        {
            _signalBus = signalBus; 
            
            _essenceTypeTypeHolders = new Dictionary<EssenceType, Transform>();

            _windowTypeHolders = new Dictionary<WindowType, Transform>();

            _signalBus.Subscribe<EssenceServiceSignals.AddHolder>(signal => AddHolder(signal.Transform, signal.EssenceType));

            _signalBus.Subscribe<WindowServiceSignals.AddHolder>(signal => AddHolder(signal.Transform, signal.WindowType));
        }

        public void GetHoldersByType(HolderType holderType, out Dictionary<Enum, Transform> resHolders)
        {
            resHolders = new Dictionary<Enum, Transform>();

            switch (holderType)
            {
                case HolderType.UI: 
                    {
                        //TODO: 
                        break;
                    }

                case HolderType.GameObject: 
                    {
                        // TODO:
                        break;
                    }
            }
        }

        public void AddHolder(Transform transform, EssenceType essenceType)
        {
            if (!_essenceTypeTypeHolders.ContainsKey(essenceType))
                _essenceTypeTypeHolders.Add(essenceType, null);

            _essenceTypeTypeHolders[essenceType] = transform;
        }

        public void AddHolder(Transform transform, WindowType windowType)
        {
            if (!_windowTypeHolders.ContainsKey(windowType))
                _windowTypeHolders.Add(windowType, null);

            _windowTypeHolders[windowType] = transform;
        }
    }
}
