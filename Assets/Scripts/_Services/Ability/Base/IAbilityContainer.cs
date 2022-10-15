using System.Collections.Generic;

namespace Services.Ability
{
    public interface IAbilityContainer
    {
        public List<IAbility> abilities { get; set; }
    }
}