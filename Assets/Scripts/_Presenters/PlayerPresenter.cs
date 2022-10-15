using Model;
using Services.Anchor;
using Services.Essence;
using UnityEngine;
using View;
using Zenject;
using System.Linq;
using UniRx;
using System;
using Services.Factory;
using Services.Window;
using Services.Log;

namespace Presenters
{
    public class PlayerPresenter : IPresenter, IDamage 
    {
        private readonly SignalBus _signalBus;
        private readonly LogService _logService;
        private readonly PlayerModel _playerModel;
        private readonly EssenceService _essenceService;
        private readonly AnchorService _anchorService;
       
        private readonly FactoryService _factoryService;
        private readonly HolderService _holderService;

        private PlayerView _playerView;

        private IDisposable _disposable;

        public PlayerPresenter(SignalBus signalBus,
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
            if (_essenceService.IsEssenceShowing<PlayerView>())
                return;

            OnDispose();

            if (_essenceService.GetEssence<PlayerView>() != null)
                _playerView = (PlayerView)_essenceService.ShowEssence<PlayerView>();
            else
            {
                if (hTransform == null)
                {
                    Transform holderTansform = _holderService._essenceTypeTypeHolders.FirstOrDefault(holder => holder.Key == EssenceType.PlayerGameObject).Value;

                    if (holderTansform != null)
                        _playerView = _factoryService.Spawn<PlayerView>(holderTansform, prefab);
                }
                else
                    _playerView = _factoryService.Spawn<PlayerView>(hTransform, prefab);
            }

            //ToDo...Remove Listener
            _disposable = _anchorService._anchors.Subscribe(_ =>
            {
                _logService.ShowLog(GetType().Name,
                Services.Log.LogType.Message,
                "_anchorService._anchors.Subscribe.",
                LogOutputLocationType.Console);

                if (_playerView != null)
                { 
                   var anchorTransform = _anchorService._anchors.Value.FirstOrDefault(anchor => anchor.AnchorType == AnchorType.Player)?.Transform;

                    if (anchorTransform != null)
                    {
                        _playerView.transform.position = anchorTransform.position;
                        _playerView.transform.rotation = anchorTransform.rotation;
                       
                    }
                }
            });
        }

        public IView GetView()
        {
            return _playerView;
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