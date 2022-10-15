using Model.Inventory;
using Services.Ability;

namespace Model
{
    public class NetworkModel : IModel
    {
        public string Id => throw new System.NotImplementedException();

        public ModelType ModelType { get; set; }

        public void Dispose()
        {
            // TODO:
        }

        public IAbilityContainer GetAbilityContainer()
        {
            // TODO:
            return null;
        }

        public IAbility GetCurrentAbility()
        {
            // TODO:
            return null;
        }

        public IInventoryContainer GetInventoryContainer()
        {
            // TODO:
            return null;
        }

        public void SetCurrentAbility(IAbility ability)
        {
            // TODO:
        }
    }
}
