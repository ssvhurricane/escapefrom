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
    public class PlayerMoveAbility : IAbilityWithVector2Param, IAbilityWithBoolParam
    {
        private SignalBus _signalBus;
       
        private readonly MovementService _movementService;
        private readonly AnimationService _animationService;
        private readonly SFXService _sFXService;
        private readonly VFXService _vFXService; 
        
        private AbilitySettings _abilitySettings;

        private MovementServiceSettings _movementServiceSettings;
        public string Id { get; set; }
        public AbilityType AbilityType { get; set; }
        public WeaponType WeaponType { get; set; }
        public bool ActivateAbility { get; set; } = true;
        public ActionModifier ActionModifier { get; set; }
        public Sprite Icon { get; set; }

        private PlayerView _view;
        private int _xVelHash;
        private int _yVelHash;
        private int _zVelHash;
        private int _groundHash;
        private int _fallingHash;
        private int _crouchHash;

        public PlayerMoveAbility(SignalBus signalBus,
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
            
           _xVelHash = Animator.StringToHash("X_Velocity");
            _yVelHash = Animator.StringToHash("Y_Velocity");
           _zVelHash = Animator.StringToHash("Z_Velocity");
           _groundHash = Animator.StringToHash("Grounded");
           _fallingHash = Animator.StringToHash("Falling");
           _crouchHash = Animator.StringToHash("Crouch");
        }
       
        public void StartAbility(IPresenter ownerPresenter, Vector2 param, ActionModifier actionModifier)
        {
            if (ownerPresenter != null)
            {
               _view = (PlayerView) ownerPresenter.GetView();

                switch (actionModifier)
                {
                    case ActionModifier.None:
                        {
                            _movementService.Move(_view, param * _movementServiceSettings.Move.Speed);
                             
                            _animationService.SetFloat(_view.GetAnimator(), "X_Velocity", param.x * _movementServiceSettings.Move.Speed);
                            _animationService.SetFloat(_view.GetAnimator(), "Y_Velocity", param.y * _movementServiceSettings.Move.Speed);
                        }
                            break;
                        
                    case ActionModifier.Run:
                        {
                           
                            _movementService.Move(_view, param * _movementServiceSettings.Run.Speed);

                            _animationService.SetFloat(_view.GetAnimator(), "X_Velocity", param.x * _movementServiceSettings.Run.Speed);
                            _animationService.SetFloat(_view.GetAnimator(), "Y_Velocity", param.y * _movementServiceSettings.Run.Speed);
                          
                            break;
                        }
                    case ActionModifier.Crouch:
                        {
                            _movementService.Move(_view, param * _movementServiceSettings.Crouch.Speed);

                            _animationService.SetFloat(_view.GetAnimator(), "X_Velocity", param.x * _movementServiceSettings.Crouch.Speed);
                            _animationService.SetFloat(_view.GetAnimator(), "Y_Velocity", param.y * _movementServiceSettings.Crouch.Speed);
                         
                            break;
                        }
                      
                }
                
                if(!_movementService.IsGrounded(_view)) // TODO: ref
                            _animationService.SetFloat(_view.GetAnimator(), "Z_Velocity", _view.GetComponent<Rigidbody>().velocity.y);


            }
        }

        public void StartAbility(IPresenter ownerPresenter, bool param, ActionModifier actionModifier)
        {
            if (ownerPresenter != null)
            {
                _view = (PlayerView)ownerPresenter.GetView();

                if(_movementService.IsGrounded(_view))
                   _animationService.SetBool(_view.GetAnimator(), "Crouch", param);
            }
        }
    }
}