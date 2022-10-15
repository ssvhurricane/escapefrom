using Services.Anchor;
using Services.Camera;
using Services.Essence;
using Services.Factory;
using System.Linq;
using UnityEngine;
using View;
using Zenject;

namespace Presenters
{
    public class CameraPresenter
    {
        private readonly SignalBus _signalBus;
        private readonly CameraService _cameraService;
        private readonly EssenceService _essenceService;

        private readonly FactoryService _factoryService;
        private readonly HolderService _holderService;

        private IView _cameraView;

        public CameraPresenter(SignalBus signalBus,
            CameraService cameraService, 
            EssenceService essenceService, 
            FactoryService factoryService,
            HolderService holderService
            ) 
        {
            _signalBus = signalBus;
            _cameraService = cameraService;
            _essenceService = essenceService;

            _factoryService = factoryService;
            _holderService = holderService;
        }

        public void ShowView<TCameraView>(string cameraId, IView baseView) where TCameraView : class, IEssence
        {
            if (_essenceService.IsEssenceShowing<TCameraView>())
                return;

            if (_essenceService.GetEssence<TCameraView>() != null)
                _cameraView = (TCameraView)_essenceService.ShowEssence<TCameraView>();
            else
            {
                Transform holderTansform = _holderService._essenceTypeTypeHolders
                    .FirstOrDefault(holder => holder.Key == EssenceType.CameraGameObject).Value;

                if (holderTansform != null)
                    _cameraView = _factoryService.Spawn<TCameraView>(holderTansform);
            }

            _cameraService.InitializeCamera(cameraId, baseView, _cameraView);
        }

        public IView GetView()
        {
            return _cameraView;
        }
    }
}