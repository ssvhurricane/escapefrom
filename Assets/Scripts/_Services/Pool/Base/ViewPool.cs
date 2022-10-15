using System;
using System.Collections.Generic;
using UnityEngine;
using View;
using Zenject;

namespace Services.Pool
{
    public class ViewPool
    {
        private readonly DiContainer _diContainer;
        private int _initialSize;
        private int _maxPoolSize;

        private Transform _parentTransform;

        private Dictionary<Type, MemoryPool<IView>> _viewPools = new Dictionary<Type, MemoryPool<IView>>();

        private List<IView> _viewItems = new List<IView>();

        MemoryPool<IView> _windowMemoryPool;

        public ViewPool(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public Dictionary<Type, MemoryPool<IView>> GetViewPools()
        {
            return _viewPools;
        }

        public List<IView> GetViewPoolItems()
        {
            return _viewItems;
        }

        public MemoryPool<IView> GetMemoryPool()
        {
            return _windowMemoryPool;
        }

        public void InitViewPool(int initPoolSize, int maxPoolSize)
        {
            _initialSize = initPoolSize;
            _maxPoolSize = maxPoolSize;
        }

        public void SetParent(Transform parentTransform)
        {
            _parentTransform = parentTransform;
        }

        private Transform ResolveParent()
        {
            if (_parentTransform != null)
            {
                return _parentTransform;
            }

            _parentTransform = new GameObject("PoolContainer").transform;
            return _parentTransform;
        }

        public TView Spawn<TView>() where TView : IView
        {
            TView view;

            if (!_viewPools.ContainsKey(typeof(TView)))
            {
                _windowMemoryPool = CreatePool<TView>();
                _viewPools.Add(typeof(TView), _windowMemoryPool);
            }

            view = (TView)_viewPools[typeof(TView)].Spawn();

            _viewItems.Add(view);

            view.GetGameObject().transform.SetParent(ResolveParent(), false);
            view.GetGameObject().SetActive(true);

            return view;
        }
        public void Despawn(IView view, bool isRemove = false)
        {
            _viewPools[view.GetType()].Despawn(view);

            if (isRemove)
                _viewItems.Remove(view);

            view.GetGameObject().transform.SetParent(_parentTransform, false);
            view.GetGameObject().gameObject.SetActive(false);
        }

        private MemoryPool<IView> CreatePool<TView>() where TView : IView
        {
            var innerSettings = new MemoryPoolSettings(_initialSize, _maxPoolSize, PoolExpandMethods.OneAtATime);
            return _diContainer.Instantiate<MemoryPool<IView>>(new object[]
            {
                innerSettings,
                new ViewComponentFromPrefabFactory<TView>(_diContainer)
            });
        }
    }
}
