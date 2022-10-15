using Constants;
using Presenters;
using Presenters.Window;
using Services.Essence;
using Services.Input;
using Signals;
using TMPro;
using UnityEngine;
using View.Camera;
using Zenject;

namespace View
{
    public class PlayerView : NetworkEssence
    {
        [SerializeField] public TextMeshProUGUI PlayerName;

        [SerializeField] public Animator Animator;
        [SerializeField] protected EssenceType Layer;

        [SerializeField] public GameObject FirstJointHand;
        [SerializeField] public GameObject FirstJointBack;

        [SerializeField] public GameObject SecondJointHand;
        [SerializeField] public GameObject SecondJointBack;

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
    }
}
