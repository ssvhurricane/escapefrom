using Presenters;
using Presenters.Window;
using Services.Essence;
using Services.Input;
using Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace View
{
    public class PlayerView : NetworkEssence
    {
        [SerializeField] protected EssenceType Layer;

        [SerializeField] public GameObject Armature;

        private SignalBus _signalBus;

        private MainHUDPresenter _mainHUDPresenter;

        private CameraPresenter _cameraPresenter;

        private PlayerPresenter _playerPresenter;

        private InputService _inputService;

        [Inject]
        public void Constrcut(SignalBus signalBus, 
            CameraPresenter cameraPresenter,
            PlayerPresenter playerPresenter, MainHUDPresenter mainHUDPresenter,
            InputService inputService)
            
        {
            _signalBus = signalBus;

            _cameraPresenter = cameraPresenter;

            _playerPresenter = playerPresenter;

            _mainHUDPresenter = mainHUDPresenter;

            _inputService = inputService;

             EssenceType = Layer;

            _signalBus.Fire(new EssenceServiceSignals.Register(this));
        }

        public GameObject GetArmature() 
        {
            return Armature;
        }
    }
}
