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
using Constants;

namespace Services.Ability
{
    public class PlayerIdleAbility : IAbilityWithOutParam
    {
        private SignalBus _signalBus;

        private readonly MovementService _movementService;
        private readonly AnimationService _animationService;
        private readonly SFXService _sFXService;
        private readonly VFXService _vFXService;
      
        private  AbilitySettings _abilitySettings;

        public string Id { get; set; }
        public AbilityType AbilityType { get; set; }
        public WeaponType WeaponType { get; set; } 
        public ActionModifier ActionModifier { get; set; }
        public bool ActivateAbility { get; set; } = true;
        public Sprite Icon { get; set; }

        private int _xVelHash;
        private int _yVelHash;
        private int _zVelHash;
        private int _groundHash;
        private int _fallingHash;
        private int _crouchHash;
        private PlayerView _view;
        private MovementServiceSettings _movementServiceSettings;

        private Rigidbody _rigidbody;

        public PlayerIdleAbility(SignalBus signalBus,
             MovementService movementService,
             AnimationService animationService,
             SFXService sFXService,
             VFXService vFXService,
              AbilitySettings[] abilitiesSettings)
        {
            _signalBus = signalBus;

            _movementService = movementService;
            _animationService = animationService;
            _sFXService = sFXService;
            _vFXService = vFXService;

            InitAbility(abilitiesSettings);

            _movementServiceSettings = _movementService.InitService(MovementServiceConstants.BasePlayerMovement);
        }
        public void InitAbility(AbilitySettings[] abilitiesSettings)
        {
            Id = this.GetType().Name;

            _abilitySettings = abilitiesSettings.FirstOrDefault(s => s.Id == Id);

            AbilityType = _abilitySettings.AbilityType;

            WeaponType = _abilitySettings.WeaponType;

            Icon = _abilitySettings.Icon;


            _xVelHash = Animator.StringToHash("X_Velocity");
            _yVelHash = Animator.StringToHash("Y_Velocity");
            _zVelHash = Animator.StringToHash("Z_Velocity");
            _groundHash = Animator.StringToHash("Grounded");
            _fallingHash = Animator.StringToHash("Falling");
            _crouchHash = Animator.StringToHash("Crouch");

        }
        public void StartAbility(IPresenter ownerPresenter, ActionModifier actionModifier)
        {
            if (ownerPresenter != null)
            {
                if (_view == null) _view = (PlayerView) ownerPresenter.GetView();

                _animationService.SetFloat(_view.GetAnimator(), _xVelHash, 0f);
                _animationService.SetFloat(_view.GetAnimator(), _yVelHash, 0f);
                _animationService.SetFloat(_view.GetAnimator(), _zVelHash , 0f);

                if (_rigidbody == null) _rigidbody = _view.GetComponent<Rigidbody>();

                _animationService.SetBool(_view.GetAnimator(), _fallingHash, !_movementService.IsGrounded(_view, _rigidbody));
                _animationService.SetBool(_view.GetAnimator(), _groundHash, _movementService.IsGrounded(_view, _rigidbody));
            }
        }
    }
}