using Data.Settings;
using System.Linq;
using UnityEngine;
using View;
using Zenject;

namespace Services.Pool
{
    public class PoolService
    {
        private SignalBus _signalBus;

        private ViewPool _viewPool;

        private PoolServiceSettings[] _poolServiceSettings;
        private PoolServiceSettings _settings;

        public PoolService(SignalBus signalBus,
            ViewPool viewPool,
            PoolServiceSettings[] poolServiceSettings)
        {
            _signalBus = signalBus;

            _poolServiceSettings = poolServiceSettings;

            _viewPool = viewPool;
        }

        public void InitPool(string settingsId)
        {
            _settings = _poolServiceSettings.FirstOrDefault(item => item.Id == settingsId);
        }

        public TView Spawn<TView>(Transform parentTransform) where TView : IView
        {
            if (_settings != null)
            {
                _viewPool.InitViewPool(_settings.InitialSize, _settings.MaxPoolSize);
                _viewPool.SetParent(parentTransform);

                return _viewPool.Spawn<TView>();
            }
            else
            {
                Debug.LogWarning("[PoolService] -> Service not initialize!");
                return default(TView);
            }
        }

        public void Despawn(IView baseView, bool isRemove = false)
        {
            _viewPool.Despawn(baseView, isRemove);
        }

        public ViewPool GetPool()
        {
            return _viewPool;
        }

        public void Despose(ViewPool viewPool)
        {
            foreach (var pool in viewPool?.GetViewPools())
            {
                pool.Value?.Clear();
                pool.Value?.Dispose();
            }
            viewPool?.GetViewPools().Clear();
        } 
        public void ClearServiceValues() 
        {
            Despose(_viewPool);
        }
    }
}
