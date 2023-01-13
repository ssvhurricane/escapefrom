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
using View;
using Zenject;

namespace Services.Ability
{
    public class PlayerJumpAbility :  IAbilityWithBoolParam
    {
        private SignalBus _signalBus;

        private readonly MovementService _movementService;
        private readonly MovementServiceSettings _movementServiceSettings;
        private readonly AnimationService _animationService;
        private readonly SFXService _sFXService;
        private readonly VFXService _vFXService;
        
        private AbilitySettings _abilitySettings;

        public string Id { get; set; }
        public AbilityType AbilityType { get; set; }
        public WeaponType WeaponType { get; set; } 
        public ActionModifier ActionModifier { get; set; }
        public bool ActivateAbility { get; set; } = true;
        public Sprite Icon { get; set; }

        private int _jumpHash;
        private Rigidbody _rigidbody;
        private PlayerView _view;

        public PlayerJumpAbility(SignalBus signalBus,
            MovementService movementService,
             AnimationService animationService,
             SFXService sFXService,
             VFXService vFXService,
              AbilitySettings[] abilitiesSettings)
        {
            _signalBus = signalBus;

            _movementService = movementService;
            _movementServiceSettings = _movementService.InitService(MovementServiceConstants.BasePlayerMovement);

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

            _jumpHash = Animator.StringToHash("Jump");
        }

        public void StartAbility(IPresenter ownerPresenter, bool param, ActionModifier actionModifier)
        {
            if (ownerPresenter != null)
            {
                if (_view == null) _view = (PlayerView)ownerPresenter.GetView();

                if (_rigidbody == null) _rigidbody = _view.GetComponent<Rigidbody>();

                // if (!param) return;
               
                if (!_movementService.IsGrounded(_view, _rigidbody)) return;

                 _movementService.Jump(_view, _rigidbody);

                _animationService.SetTrigger(_view.GetAnimator(), _jumpHash);
              
            }
        }
    }
}