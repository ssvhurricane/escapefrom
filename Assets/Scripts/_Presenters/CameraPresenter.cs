using Model;
using Services.Anchor;
using Services.Camera;
using Services.Essence;
using Services.Factory;
using UnityEngine;
using View;
using Zenject;

namespace Presenters
{
    public class CameraPresenter : IPresenter
    {
        private readonly SignalBus _signalBus;
        private readonly CameraService _cameraService;
        private readonly EssenceService _essenceService;

        private readonly FactoryService _factoryService;
        private readonly HolderService _holderService;

        private IView _cameraView;

        private readonly CameraModel _cameraModel;
        public CameraPresenter(SignalBus signalBus,
            CameraService cameraService, 
            EssenceService essenceService, 
            FactoryService factoryService,
            HolderService holderService,
            CameraModel cameraModel)
        {
            _signalBus = signalBus;
            _cameraService = cameraService;
            _essenceService = essenceService;

            _factoryService = factoryService;
            _holderService = holderService;

            _cameraModel = cameraModel;
        }

        public void ShowView<TCameraView>(string cameraId, IView baseView) where TCameraView : class, IEssence
        {
            if (_essenceService.IsEssenceShowing<TCameraView>())
                return;

            if (_essenceService.GetEssence<TCameraView>() != null)
                _cameraView = (TCameraView)_essenceService.ShowEssence<TCameraView>();
            else
            {
                Transform holderTansform = (baseView as PlayerView).transform;

                if (holderTansform != null)
                    _cameraView = _factoryService.Spawn<TCameraView>(holderTansform);
            }

            _cameraService.InitializeCamera(cameraId, baseView, _cameraView);
        }

        public IView GetView()
        {
            return _cameraView;
        }

        public void ShowView(GameObject prefab = null, Transform hTransform = null)
        {
            // TODO:
        }

        public void HideView()
        {
           // TODO:
        }

        public IModel GetModel()
        {
            return _cameraModel;
        }
    }
}