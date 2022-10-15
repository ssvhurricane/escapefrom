using Services.Essence;
using Services.Resources;
using Services.Window;
using UnityEngine;
using View;
using Zenject;

namespace Services.Factory
{
    public class FactoryService 
    {
        private readonly DiContainer _container;

        private readonly ResourcesService _resources;

        private readonly SignalBus _signalBus;

        private const string IEssence = "IEssence";

        private const string IWindow = "IWindow";

        public FactoryService(DiContainer container, ResourcesService resources, SignalBus signalBus) 
        {
            _container = container;

            _resources = resources;

            _signalBus = signalBus;
        }

        public TView Spawn<TView>(Transform parentTransform, GameObject prefab = null) where TView : class, IView
        {
            GameObject innerPrefab = null;

            TView resultView = null;

            if (typeof(TView).GetInterface(IEssence) != null) 
            {

                innerPrefab = prefab == null ? _resources.GetResource(TypeResource.View, typeof(TView)) : prefab;

                if (innerPrefab == null)
                {
                    Debug.LogWarning("[FactoryService] -> can't find essence for type : " + typeof(TView));
                    return null;
                }

                resultView = _container.InstantiatePrefabForComponent<TView>(innerPrefab.gameObject); 
                
                if (resultView == null)
                {
                    Debug.LogError("[FactoryService] -> There is no view with type " + typeof(TView).Name);
                    return null;
                }

                ((IEssence)resultView).Initialize(parentTransform);
            }
            else if (typeof(TView).GetInterface(IWindow) != null) 
            {
                innerPrefab = prefab == null ? _resources.GetResource(TypeResource.Window, typeof(TView)) : prefab; 
                
                if (innerPrefab == null)
                {
                    Debug.LogWarning("[FactoryService] -> can't find window for type : " + typeof(TView));
                    return null;
                }

                resultView = _container.InstantiatePrefabForComponent<TView>(innerPrefab.gameObject); 
                
                if (resultView == null)
                {
                    Debug.LogError("[FactoryService] -> There is no view with type " + typeof(TView).Name);
                    return null;
                }

                ((IWindow)resultView).Initialize(parentTransform);
            }

            return resultView;
        }
    }
}