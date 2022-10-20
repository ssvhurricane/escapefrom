using Data.Settings;
using Model.Inventory;
using Services.Ability;
using System.Linq;

namespace Model
{
    public class CameraModel : ILiveModel
    {
        public string Id => _settings.FirstOrDefault().Id; // TODO:

        public ModelType ModelType { get; set; }

        private readonly CameraServiceSettings[] _settings;

        private CameraAbilityContainer _cameraAbilityContainer;

        private CameraRotateAbility _cameraRotateAbility;

        private IAbility _currentAbility;

        public CameraModel(CameraServiceSettings[] settings,
                           CameraRotateAbility cameraRotateAbility) 
        {
            _settings = settings;
            _cameraRotateAbility = cameraRotateAbility;


            //Init Base Ability.
            _cameraAbilityContainer = new CameraAbilityContainer();

            _cameraAbilityContainer.abilities.Add(_cameraRotateAbility);
        }

        public void Dispose()
        {
            // TODO:
        }

        public IAbilityContainer GetAbilityContainer()
        {
            return _cameraAbilityContainer;
        }

        public IAbility GetCurrentAbility()
        {
            return _currentAbility;
        }

        public IInventoryContainer GetInventoryContainer()
        {
            throw new System.NotImplementedException();
        }

        public void SetCurrentAbility(IAbility ability)
        {
            _currentAbility = ability;
        }
    }
}
