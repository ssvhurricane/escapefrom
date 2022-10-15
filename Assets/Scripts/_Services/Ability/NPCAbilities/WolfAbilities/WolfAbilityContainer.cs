using System.Collections.Generic;

namespace Services.Ability
{
    public class WolfAbilityContainer : IAbilityContainer
    {
        public List<IAbility> abilities { get; set; }

        public WolfAbilityContainer() 
        {
            abilities = new List<IAbility>();
        }

        public void AddAbility(IAbility ability)
        {
           
        }

        public void ClearAbilityContainer()
        {
          
        }

        public void DeleteAbility(IAbility ability)
        {
          
        }

        public void GetAbilityById(string id)
        {
           
        }
    }
}