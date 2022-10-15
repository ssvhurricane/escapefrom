using Model.Inventory;
using Services.Ability;

namespace Model
{
    public interface ILiveModel : IModel
    {
        public IAbilityContainer GetAbilityContainer();

        public IInventoryContainer GetInventoryContainer();
        public void SetCurrentAbility(IAbility ability);
        public IAbility GetCurrentAbility();
    }
}
