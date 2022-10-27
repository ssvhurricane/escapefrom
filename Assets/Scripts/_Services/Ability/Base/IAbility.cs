using Presenters;
using Services.Input;
using Services.Item.Weapon;
using UnityEngine;

namespace Services.Ability
{
    public interface IAbility
    {
        public string Id { get; set; }
        public AbilityType AbilityType { get; set; }
        public WeaponType WeaponType { get; set; }
        public ActionModifier ActionModifier { get; set; }
        public Sprite Icon { get; set; }

        public bool ActivateAbility { get; set; }
    }

    public interface IAbilityWithOutParam : IAbility
    {
        public void StartAbility(IPresenter ownerPresenter, ActionModifier actionModifier);
    }
    public interface IAbilityWithVector2Param : IAbility
    {
        public void StartAbility(IPresenter ownerPresenter, Vector2 param, ActionModifier actionModifier);
    }

    public interface IAbilityWithBoolParam : IAbility
    {
        public void StartAbility(IPresenter ownerPresenter, bool param, ActionModifier actionModifier);
    }


    public interface IAbilityWithAffectedPresenterParam : IAbility
    {
         public void StartAbility(IPresenter ownerPresenter, IPresenter affectedPresenter, ActionModifier actionModifier);
    }

    public interface IAbilityWithAffectedPresenterVector2Param : IAbility
    {
        public void StartAbility(IPresenter ownerPresenter, IPresenter affectedPresenter, Vector2 param, ActionModifier actionModifier);
    }

    public interface IAbilityWithAffectedPresentersParam : IAbility
    {
        public void StartAbility(IPresenter ownerPresenter, IPresenter[] affectedPresenters, ActionModifier actionModifier);
    }

}