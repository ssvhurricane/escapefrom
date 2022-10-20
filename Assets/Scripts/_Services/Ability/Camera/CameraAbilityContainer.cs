using System.Collections.Generic;

namespace Services.Ability
{
    public class CameraAbilityContainer : IAbilityContainer
    {
       public List<IAbility> abilities { get ; set; }
       public CameraAbilityContainer() 
        {
            abilities = new List<IAbility>();
        }

       
    }
}
