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
    public class PlayerMoveAbility : IAbilityWithVector2Param
    {
        private SignalBus _signalBus;
       
        private readonly MovementService _movementService;
        private readonly AnimationService _animationService;
        private readonly SFXService _sFXService;
        private readonly VFXService _vFXService; 
        
        private AbilitySettings _abilitySettings;

        public string Id { get; set; }
        public AbilityType AbilityType { get; set; }
        public WeaponType WeaponType { get; set; }
        public bool ActivateAbility { get; set; } = true;
        public ActionModifier ActionModifier { get; set; }
        public Sprite Icon { get; set; }

        private PlayerView _view;

        public PlayerMoveAbility(SignalBus signalBus,
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
       
        public void StartAbility(IPresenter ownerPresenter, Vector2 param, ActionModifier actionModifier)
        {
            if (ownerPresenter != null)
            {
                _view = (PlayerView) ownerPresenter.GetView();

                if (actionModifier == ActionModifier.None) 
                {
                    _movementService.Move(ownerPresenter.GetView(), param);
                }

                switch (actionModifier)
                {
                    case ActionModifier.None:
                        {
                            _movementService.Move(ownerPresenter.GetView(), param);

                            if (_animationService.GetBool(_view.Animator, "IsIdleResting"))
                            {
                                // If Resting Mode.
                                _animationService.SetFloat(_view.Animator, "WalkRestingValue", 0f);
                                _animationService.SetBool(_view.Animator, "IsWalkResting", true);

                                _animationService.SetBool(_view.Animator, "IsRunResting", false);
                            }

                            if (_animationService.GetBool(_view.Animator, "IsIdleCombat"))
                            {
                                // If Resting Mode.
                                _animationService.SetFloat(_view.Animator, "WalkCombatValue", 0f);
                                _animationService.SetBool(_view.Animator, "IsWalkCombat", true);

                                _animationService.SetBool(_view.Animator, "IsRunCombat", false);
                            }
                        }
                            break;
                        
                    case ActionModifier.Shift:
                        {
                            _movementService.Move(ownerPresenter.GetView(), param * 3.5f);

                            if (_animationService.GetBool(_view.Animator, "IsIdleResting"))
                            {
                                _animationService.SetFloat(_view.Animator, "RunRestingValue", 0f);
                                _animationService.SetBool(_view.Animator, "IsRunResting", true);

                                _animationService.SetBool(_view.Animator, "IsWalkResting", false);
                            }

                            if (_animationService.GetBool(_view.Animator, "IsIdleCombat"))
                            {
                                _animationService.SetFloat(_view.Animator, "RunCombatValue", 0f);
                                _animationService.SetBool(_view.Animator, "IsRunCombat", true);

                                _animationService.SetBool(_view.Animator, "IsWalkCombat", false);
                            }
                            break;
                        }
                }
            }
        }

    }
}