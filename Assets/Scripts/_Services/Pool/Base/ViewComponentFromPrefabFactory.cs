using Services.Essence;
using Services.Resources;
using Services.Window;
using UnityEngine;
using View;
using Zenject;

namespace Services.Pool
{
    public class ViewComponentFromPrefabFactory<TView> : IFactory<TView> where TView : IView
    {
        private readonly DiContainer _container;

        private  GameObject _prefab;

        private ResourcesService _resourcesService;
       
        public ViewComponentFromPrefabFactory(DiContainer container)
        {
            _container = container;

            _resourcesService = container.Resolve<ResourcesService>();
        }

        public TView Create()
        {
            if(typeof(TView).GetInterface("IWindow") != null)
                _prefab = _resourcesService.GetResource(TypeResource.Window, typeof(TView));
            else if(typeof(TView).GetInterface("IEssence") != null)
                _prefab = _resourcesService.GetResource(TypeResource.View, typeof(TView));

            return _container.InstantiatePrefabForComponent<TView>(_prefab);
        }
    }
}
