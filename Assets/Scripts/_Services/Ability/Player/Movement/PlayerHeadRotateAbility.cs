using Constants;
using Data.Settings;
using Presenters;
using Services.Animation;
using Services.Input;
using Services.Movement;
using Services.SFX;
using Services.VFX;
using Services.Item.Weapon;
using System.Linq;
using UnityEngine;
using Zenject;
using View.Camera;
using View;

namespace Services.Ability
{
    public class PlayerHeadRotateAbility : IAbilityWithAffectedPresenterParam
    {
        private SignalBus _signalBus;

        private readonly MovementService _movementService;
        private readonly AnimationService _animationService;
        private readonly SFXService _sFXService;
        private readonly VFXService _vFXService;

        private AbilitySettings _abilitySettings;
        private CameraPresenter _cameraPresenter;
        private PlayerPresenter _playerPresenter;
        private FPSCameraView _cameraView;
        private PlayerView _playerView;
        public string Id { get; set; }
        public AbilityType AbilityType { get; set; }
        public WeaponType WeaponType { get; set; }
        public bool ActivateAbility { get; set; } = true;
        public ActionModifier ActionModifier { get; set; }
        public Sprite Icon { get; set; }

        public PlayerHeadRotateAbility(SignalBus signalBus,
            MovementService movementService,
             AnimationService animationService,
             SFXService sFXService,
             VFXService vFXService,
              AbilitySettings[] abilitiesSettings)
        {
            _signalBus = signalBus;

            _movementService = movementService;
            _movementService.InitService(MovementServiceConstants.BasePlayerMovement);

            _animationService = animationService;
            _sFXService = sFXService;
            _vFXService = vFXService;

            InitAbility(abilitiesSettings);
        }
        public void InitAbility(AbilitySettings[] abilitiesSettings)
        {
            Id = this.GetType().Name;

            _abilitySettings = abilitiesSettings.FirstOrDefault(s => s.Id == Id);

            AbilityType = _abilitySettings.AbilityType;

            WeaponType = _abilitySettings.WeaponType;

            Icon = _abilitySettings.Icon;
        }

        public void StartAbility(IPresenter ownerPresenter, IPresenter affectedPresenter, ActionModifier actionModifier)
        {
            if (_cameraPresenter == null) _cameraPresenter = ownerPresenter as CameraPresenter;
                
            if (_playerPresenter == null) _playerPresenter = affectedPresenter as PlayerPresenter;

            if (_cameraView == null) _cameraView = _cameraPresenter.GetView() as FPSCameraView;

            if (_playerView == null) _playerView = _playerPresenter.GetView() as PlayerView;

            _playerView.GetHead().transform.rotation = _cameraView.transform.rotation;

            _playerView.GetLeftArmRoot().transform.rotation = _cameraView.transform.rotation;
            _playerView.GetRightArmRoot().transform.rotation = _cameraView.transform.rotation;

            //_playerView.GetLeftArm().transform.position = _playerView.GetLeftArmRoot().transform.position;
           //  _playerView.GetRightArm().transform.position = _playerView.GetRightArmRoot().transform.position;

            //_playerView.GetLeftArm().transform.rotation = _playerView.GetLeftArmRoot().transform.rotation;
            // _playerView.GetRightArm().transform.rotation = _playerView.GetRightArmRoot().transform.rotation;
        }
    }
}