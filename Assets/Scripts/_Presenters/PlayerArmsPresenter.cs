using Model;
using Services.Anchor;
using Services.Essence;
using UnityEngine;
using View;
using Zenject;
using System.Linq;
using System;
using Services.Factory;
using Services.Log;

namespace Presenters
{
    public class PlayerArmsPresenter : IPresenter, IDamage 
    {
        private readonly SignalBus _signalBus;
        private readonly LogService _logService;
        private readonly PlayerModel _playerModel;
        private readonly EssenceService _essenceService;
        private readonly AnchorService _anchorService;
       
        private readonly FactoryService _factoryService;
        private readonly HolderService _holderService;

        private PlayerArmsView _playerArmsView;

        private IDisposable _disposable;

        public PlayerArmsPresenter(SignalBus signalBus,
            LogService logService,
            PlayerModel playerModel,
            EssenceService essenceService,
            AnchorService anchorService,
            FactoryService factoryService,
            HolderService holderService
            ) 
        {
            _signalBus = signalBus;
            _logService = logService;
            _playerModel = playerModel;
            _essenceService = essenceService;
            _anchorService = anchorService;

            _factoryService = factoryService;
            _holderService = holderService;


            _logService.ShowLog(GetType().Name,
                Services.Log.LogType.Message,
                "Call Constructor Method.", 
                LogOutputLocationType.Console);
        }

        public void ShowView(GameObject prefab = null, Transform hTransform = null) 
        {
            if (_essenceService.IsEssenceShowing<PlayerArmsView>())
                return;

            OnDispose();

            if (_essenceService.GetEssence<PlayerArmsView>() != null)
                _playerArmsView = (PlayerArmsView)_essenceService.ShowEssence<PlayerArmsView>();
            else
            {
                if (hTransform == null)
                {
                    Transform holderTansform = _holderService._essenceTypeTypeHolders.FirstOrDefault(holder => holder.Key == EssenceType.PlayerGameObject).Value;

                    if (holderTansform != null)
                        _playerArmsView = _factoryService.Spawn<PlayerArmsView>(holderTansform, prefab);
                }
                else
                    _playerArmsView = _factoryService.Spawn<PlayerArmsView>(hTransform, prefab);
            }

            _logService.ShowLog(GetType().Name,
            Services.Log.LogType.Message,
            "_anchorService._anchors.Subscribe.",
            LogOutputLocationType.Console);

            if (_playerArmsView != null)
            {
                var anchorTransform = _anchorService._anchors.FirstOrDefault(anchor => anchor.AnchorType == AnchorType.Player)?.Transform;

                if (anchorTransform != null)
                {
                    _playerArmsView.transform.position = anchorTransform.position;
                    _playerArmsView.transform.rotation = anchorTransform.rotation;

                }
            }
          
        }

        public IView GetView()
        {
            return _playerArmsView;
        }

        public IModel GetModel() 
        {
            return _playerModel;
        }

        private void OnDispose()
        {
            _disposable?.Dispose();
        }

        public void HideView()
        {
           
        }

        public void ToDamage()
        {
            //ToDo...
        }

        public void TakeDamage(float damageValue, IPresenter ownedPresenter)
        {
            //ToDo...
        }
    }
}