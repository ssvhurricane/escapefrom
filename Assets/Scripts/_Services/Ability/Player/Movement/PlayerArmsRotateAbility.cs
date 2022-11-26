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
    public class PlayerArmsRotateAbility : IAbilityWithAffectedPresenterParam
    {
        private SignalBus _signalBus;

        private readonly MovementService _movementService;
        private readonly AnimationService _animationService;
        private readonly SFXService _sFXService;
        private readonly VFXService _vFXService;

        private AbilitySettings _abilitySettings;
      
        private PlayerPresenter _playerPresenter;
        private PlayerArmsPresenter _playerArmsPresenter;
      
        private PlayerView _playerView;
        private PlayerArmsView _playerArmsView;
        private Vector3 _offset; // TODO:

        public string Id { get; set; }
        public AbilityType AbilityType { get; set; }
        public WeaponType WeaponType { get; set; }
        public bool ActivateAbility { get; set; } = true;
        public ActionModifier ActionModifier { get; set; }
        public Sprite Icon { get; set; }

        public PlayerArmsRotateAbility(SignalBus signalBus,
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
            if (_playerArmsPresenter == null) _playerArmsPresenter = ownerPresenter as PlayerArmsPresenter;
                
            if (_playerPresenter == null) _playerPresenter = affectedPresenter as PlayerPresenter;

            if (_playerArmsView == null) _playerArmsView = _playerArmsPresenter.GetView() as PlayerArmsView;

            if (_playerView == null) _playerView = _playerPresenter.GetView() as PlayerView;

            _movementService.Follow(_playerArmsView, _playerView, Vector3.zero, Vector3.zero, 100f);

            _playerView.SetLeftArmRoot(_playerArmsView.GetLeftArmRoot());

            _playerView.SetRightArmRoot(_playerArmsView.GetRightArmRoot());

            // TODO:
            //_playerArmsView.transform.rotation = _playerView.GetHead().transform.rotation;
            //_playerArmsView.transform.Rotate(_playerView.GetHead().transform.position);
            //_playerArmsView.transform.position = _playerView.GetHead().transform.position
            //    - (_playerView.GetHead().transform.rotation * _offset);
        }
    }
}