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
using System.Collections.Generic;
using Services.Pool;
using Constants;
using Services.Resources;
using Services.Essence;
using Model;

namespace Services.Ability
{
    public class PlayerNoneAbility : IAbilityWithOutParam
    {
        private SignalBus _signalBus;

        private readonly MovementService _movementService;
        private readonly AnimationService _animationService;
        private readonly SFXService _sFXService;
        private readonly VFXService _vFXService;
        private readonly ItemService _itemService;
        private readonly PoolService _poolService;
        private readonly ResourcesService _resourcesService;
     
        private AbilitySettings _abilitySettings;
       
        private IEnumerable<IItem> _playerWeaponItems;
        private WeaponItemView _playerWeaponItem;
       
        public string Id { get; set; }
        public AbilityType AbilityType { get; set; }
        public WeaponType WeaponType { get; set; }
        public ActionModifier ActionModifier { get; set; }
        public Sprite Icon { get; set; }
        public bool ActivateAbility { get; set; } = true;
   
        public PlayerNoneAbility(SignalBus signalBus,
           MovementService movementService,
           AnimationService animationService,
           SFXService sFXService,
           VFXService vFXService,
           ItemService itemService,
           PoolService poolService,
           ResourcesService resourcesService,
            AbilitySettings[] abilitiesSettings)
        {
            _signalBus = signalBus;

            _movementService = movementService;
            _animationService = animationService;
            _sFXService = sFXService;
            _vFXService = vFXService;
            _itemService = itemService;
            _poolService = poolService;
            _resourcesService = resourcesService;

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
                var view = (PlayerView) presenter.GetView();

            }
        }
    }
}