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
using Services.Item;
using Services.Log;

namespace Services.Ability
{
    public class PlayerAdvancedAttackAbility : IAbilityWithOutParam
    {
        private readonly SignalBus _signalBus;
        private readonly MovementService _movementService;
        private readonly AnimationService _animationService;
        private readonly SFXService _sFXService;
        private readonly VFXService _vFXService;
        private readonly ItemService _itemService;
        private readonly LogService _logService;

        private AbilitySettings _abilitySettings;

        public string Id { get; set; }
        public AbilityType AbilityType { get; set; }
        public WeaponType WeaponType { get; set; }
        public ActionModifier ActionModifier { get; set; }
        public bool ActivateAbility { get; set; } = true;
        public Sprite Icon { get; set; }

        public PlayerAdvancedAttackAbility(SignalBus signalBus,
             MovementService movementService,
             AnimationService animationService,
             SFXService sFXService,
             VFXService vFXService,
             ItemService itemService,
             LogService logService,
              AbilitySettings[] abilitiesSettings) 
        {
            _signalBus = signalBus;

            _movementService = movementService;
            _animationService = animationService;
            _sFXService = sFXService;
            _vFXService = vFXService;
            _itemService = itemService;
            _logService = logService;

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

        public void StartAbility(IPresenter ownerPresenter, ActionModifier actionModifier)
        {
            var presenter = ownerPresenter;

            if (presenter != null)
            {
                var view = (PlayerView)presenter.GetView();

                //_animationService.SetBool(view.Animator, "IsIdleCombat", true);
                //_animationService.SetBool(view.Animator, "IsIdleResting", false);

                
                //BaseEssence bowView = _itemService
                //  .GetAllItemViews()
                //  .FirstOrDefault(item => item.Id == ItemServiceConstants.BowItem);

                ////bowView.gameObject.transform.SetParent(view.SecondJointHand.transform);
                //bowView.gameObject.transform.localPosition = Vector3.zero;
                //bowView.gameObject.transform.localRotation = Quaternion.identity;
                //bowView.gameObject.transform.localScale = Vector3.one;

                //// ToDo...
                ////BaseView axeView = _itemService
                ////  .GetAllItemViews()
                ////  .FirstOrDefault(item => item.Id == ItemServiceConstants.BowItem);

                ////axeView.gameObject.transform.SetParent(view.FirstJointBack.transform);
                ////axeView.gameObject.transform.localPosition = Vector3.zero;
                ////axeView.gameObject.transform.localRotation = Quaternion.identity;
                ////axeView.gameObject.transform.localScale = Vector3.one;

                if (actionModifier == ActionModifier.None)
                {
                   // ToDo...
                    _logService.ShowLog(GetType().Name,
                               Services.Log.LogType.Message,
                               "Advanced Attack!.",
                               LogOutputLocationType.Console);

                }
                else if (actionModifier == ActionModifier.Power)
                {
                    // ToDo...
                    _logService.ShowLog(GetType().Name,
                             Services.Log.LogType.Message,
                             "Power Advanced Attack!.",
                             LogOutputLocationType.Console);
                }
            }
        }
    }
}