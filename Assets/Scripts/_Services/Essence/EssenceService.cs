using Services.Log;
using Signals;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Services.Essence
{
    public class EssenceService
    {
        private readonly SignalBus _signalBus;

        private readonly LogService _logService;
        public List<IEssence> _registeredEssences { get; private set; }

        public Dictionary<Type, MemoryPool<IEssence>> _essencePools { get; private set; }// TODO:

        public EssenceService(SignalBus signalBus, LogService logService) 
        {
            _signalBus = signalBus;

            _logService = logService;

            _registeredEssences = new List<IEssence>(); 
            
            _essencePools = new Dictionary<Type, MemoryPool<IEssence>>();

            _signalBus.Subscribe<EssenceServiceSignals.Register>(signal => OnRegisterEssence(signal.Essence));

            _logService.ShowLog(GetType().Name, Log.LogType.Message, "Call Constructor Method.", LogOutputLocationType.Console);
        }

        private void OnRegisterEssence(IEssence essence) 
        {
            if (!_registeredEssences.Contains(essence))
                _registeredEssences.Add(essence);
        }

        public void AddItemEssencePools(KeyValuePair<Type, MemoryPool<IEssence>> pool)
        {
            if (!_essencePools.Contains(pool)) _essencePools.Add(pool.Key, pool.Value);
        }

        public void RemoveItemEssencePools(KeyValuePair<Type, MemoryPool<IEssence>> pool)
        {
            if (_essencePools.Contains(pool)) _essencePools.Remove(pool.Key);
        }

        public IEssence GetEssence<TEssence>() where TEssence : class, IEssence
        {
            IEssence essence = null;

            if (_registeredEssences.Count != 0)
                essence = _registeredEssences.FirstOrDefault(essenceItem => essenceItem is TEssence);

            return essence;
        }

        public bool IsEssenceShowing<TEssence>() where TEssence : class, IEssence
        {
            return _registeredEssences.Exists(window => window.GetType() == typeof(TEssence)) 
                    && _registeredEssences.FirstOrDefault(essenceItem => essenceItem is TEssence).IsShown;
        }

        public IEssence ShowEssence<TEssence>() where TEssence : class, IEssence
        {
            var essence = GetEssence<TEssence>();
            
            essence.Show();

            return essence;
        }

        public void HideEssence<TEssence>() where TEssence : class, IEssence
        {
            IEssence essence = GetEssence<TEssence>();

            if (essence != null)
            {
                essence.Hide();

                _signalBus.Fire(new EssenceServiceSignals.Hidden(essence));
            }
        }

        public void HideAllEssences()
        {
            if (_registeredEssences.Count != 0)
                foreach (var regEssence in _registeredEssences) regEssence.Hide();
        }
     
        public void ClearServiceValues() 
        {
            if(_registeredEssences.Count != 0)
                _registeredEssences.Clear();

            _essencePools?.Clear();
        }
    }
}