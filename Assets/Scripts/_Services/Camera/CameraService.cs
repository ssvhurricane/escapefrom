using Data.Settings;
using System.Linq;
using UnityEngine;
using View;
using Zenject;

namespace Services.Camera
{
    public class CameraService
    {
        private readonly SignalBus _signalBus;
        private readonly CameraServiceSettings[] _cameraServiceSettings;

        private CameraServiceSettings _settings;

        private IView _ownerView, _currentCameraView;
      
        public CameraService(SignalBus signalBus, CameraServiceSettings[] cameraServiceSettings)
        {
            _signalBus = signalBus;

            _cameraServiceSettings = cameraServiceSettings;
        }
        public void ClearServiceValues()
        {
            // TODO:
        }

        public void InitializeCamera(string cameraId, IView baseView, IView cameraView)
        {
           _settings = _cameraServiceSettings.FirstOrDefault(cam => cam.Id == cameraId);

            if (_settings != null) 
            {
                switch (_settings.CameraType) 
                {
                    case CameraType.FPSCamera:
                        {
                            FPSCamera(baseView, cameraView, _settings);

                            break;
                        }
                        // TODO:
                }
            }
        }

        private void FPSCamera(IView bsView, IView camView, CameraServiceSettings cameraServiceSettings)
        {
            _ownerView = bsView;

            _currentCameraView = camView;

            _currentCameraView.GetGameObject().transform.localPosition = cameraServiceSettings.Position;

            _currentCameraView.GetGameObject().transform.localRotation = Quaternion.Euler(cameraServiceSettings.Rotation);

            _currentCameraView.GetGameObject().transform.localScale = Vector3.one;
        }

        public IView GetCurrentCamera() 
        {
            return _currentCameraView;
        }

        public IView GetOwnerView() 
        {
            return this._ownerView;
        }

        public CameraServiceSettings GetCurrentCameraSettings() 
        {
            return _settings;
        }
    }
}