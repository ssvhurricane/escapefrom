using Model;
using Services.Anchor;
using Services.Essence;
using Services.Factory;
using Services.Log;
using System;
using System.Linq;
using UnityEngine;
using View;
using Zenject;

namespace Presenters
{
    public class WolfPresenter : IPresenter
    {
        private readonly SignalBus _signalBus;
        private readonly LogService _logService;
        private readonly WolfModel _wolfModel;
        private readonly EssenceService _essenceService; 
        
        private readonly FactoryService _factoryService;
        private readonly HolderService _holderService;

        private WolfView _wolfView;

        private IDisposable _disposable;

        public WolfPresenter(SignalBus signalBus,
            LogService logService,
            WolfModel wolfModel,
            EssenceService essenceService,
            FactoryService factoryService, 
            HolderService holderService
            ) 
        {
            _signalBus = signalBus;
            _logService = logService;
            _wolfModel = wolfModel;
            _essenceService = essenceService;

            _factoryService = factoryService;
            _holderService = holderService;

            _logService.ShowLog(GetType().Name,
                Services.Log.LogType.Message,
                "Call Constructor Method.", 
                LogOutputLocationType.Console);
        }
        
        public void ShowView(GameObject prefab = null, Transform hTransform = null)
        {
            if (_essenceService.IsEssenceShowing<WolfView>()) return;

            OnDispose(); 
            
            if (_essenceService.GetEssence<WolfView>() != null)
                _wolfView = (WolfView)_essenceService.ShowEssence<WolfView>();
            else
            {
                Transform holderTansform = _holderService._essenceTypeTypeHolders
                    .FirstOrDefault(holder => holder.Key == EssenceType.NPCGameObject).Value;

                if (holderTansform != null)
                    _wolfView = _factoryService.Spawn<WolfView>(holderTansform);
            }
        }
        public IModel GetModel()
        {
            return _wolfModel;
        }

        public IView GetView()
        {
            return _wolfView;
        }

        public void HideView()
        {
           
        }

       
        private void OnDispose()
        {
            _disposable?.Dispose();
        }

    }
}
