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
using Services.Camera;

namespace Services.Ability
{
    public class CameraParentAbility : IAbilityWithAffectedPresenterParam
    {
        private SignalBus _signalBus;

        private readonly MovementService _movementService;
        private readonly AnimationService _animationService;
        private readonly CameraService _cameraService;
        private readonly SFXService _sFXService;
        private readonly VFXService _vFXService;

        private AbilitySettings _abilitySettings;

        public string Id { get; set; }
        public AbilityType AbilityType { get; set; }
        public WeaponType WeaponType { get; set; }
        public bool ActivateAbility { get; set; } = true;
        public ActionModifier ActionModifier { get; set; }
        public Sprite Icon { get; set; }

        public CameraParentAbility(SignalBus signalBus,
             MovementService movementService,
             AnimationService animationService,
             CameraService cameraService,
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

            _cameraService = cameraService;

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
            _movementService.Parent(
               ownerPresenter.GetView(),
                affectedPresenter.GetView());
        }
    }
}