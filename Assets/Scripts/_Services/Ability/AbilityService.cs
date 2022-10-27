using Model;
using Presenters;
using Services.Input;
using Services.Item.Weapon;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Services.Ability
{
    public class AbilityService
    {
        private readonly SignalBus _signalBus;
        public AbilityService(SignalBus signalBus) 
        {
            _signalBus = signalBus;
        }
      
        public void UseAbility(IAbilityWithOutParam ability, IPresenter presenter, ActionModifier actionModifier)
        {
            if (ability.ActivateAbility)
            {
                ability.StartAbility(presenter, actionModifier);
            }
        }

        public void UseAbility(IAbilityWithVector2Param ability, IPresenter presenter, Vector2 param, ActionModifier actionModifier)
        {
            if (ability.ActivateAbility)
            {
                ability.StartAbility(presenter, param, actionModifier);
            }
        }
        public void UseAbility(IAbilityWithBoolParam ability, IPresenter presenter, bool param, ActionModifier actionModifier)
        {
            if (ability.ActivateAbility)
            {
                ability.StartAbility(presenter, param, actionModifier);
            }
        }

        public void UseAbility(IAbilityWithAffectedPresenterParam ability, IPresenter presenter, IPresenter affectedPresenter, ActionModifier actionModifier)
        {
            if (ability.ActivateAbility)
            {
                ability.StartAbility(presenter, affectedPresenter, actionModifier);
            }
        }
        public void UseAbility(IAbilityWithAffectedPresenterVector2Param ability, IPresenter presenter, IPresenter affectedPresenter,Vector2 param, ActionModifier actionModifier)
        {
            if (ability.ActivateAbility)
            {
                ability.StartAbility(presenter, affectedPresenter, param, actionModifier);
            }
        }

        public void UseAbility(IAbilityWithAffectedPresentersParam ability, IPresenter presenter, IPresenter[] affectedPresenters, ActionModifier actionModifier)
        {
            if (ability.ActivateAbility)
            {
                ability.StartAbility(presenter, affectedPresenters, actionModifier);
            }
        }

        public List<IAbility> GetAllAbility(IPresenter presenter)
        {
            return  ((ILiveModel)presenter.GetModel()).GetAbilityContainer().abilities;
        }

        public IAbility GetAbilityById(IPresenter presenter, string abilityId)
        {
            return  ((ILiveModel)presenter.GetModel()).GetAbilityContainer().abilities.
                FirstOrDefault(ability => ability.Id == abilityId);
        }

        public IEnumerable<IAbility> GetAbilitiesyByAbilityType(IPresenter presenter, AbilityType abilityType)
        {
            return ((ILiveModel)presenter.GetModel()).GetAbilityContainer().abilities.Where(ability => ability.AbilityType == abilityType);
        }

        public IEnumerable<IAbility> GetAbilityByWeaponType(IPresenter presenter, WeaponType weaponType) 
        {
            return ((ILiveModel)presenter.GetModel()).GetAbilityContainer().abilities.
              Where(ability => ability.WeaponType == weaponType);
        }
    }
}
