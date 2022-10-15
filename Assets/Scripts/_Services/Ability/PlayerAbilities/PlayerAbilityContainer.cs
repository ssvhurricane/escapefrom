using System.Collections.Generic;

namespace Services.Ability
{
    public class PlayerAbilityContainer : IAbilityContainer
    {
       public List<IAbility> abilities { get ; set; }
       public PlayerAbilityContainer() 
        {
            abilities = new List<IAbility>();
        }

       
    }
}
