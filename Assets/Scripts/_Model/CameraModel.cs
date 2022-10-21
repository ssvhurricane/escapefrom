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
        private CameraFollowAbility _cameraFollowAbility;
        private CameraParentAbility _cameraParentAbility;

        private IAbility _currentAbility;

        public CameraModel(CameraServiceSettings[] settings,
                           CameraRotateAbility cameraRotateAbility,
                           CameraFollowAbility cameraFollowAbility,
                           CameraParentAbility cameraParentAbility)
        {
            _settings = settings;

            _cameraRotateAbility = cameraRotateAbility;
            _cameraFollowAbility = cameraFollowAbility;
            _cameraParentAbility = cameraParentAbility;

            //Init Base Ability.
            _cameraAbilityContainer = new CameraAbilityContainer();

            _cameraAbilityContainer.abilities.Add(_cameraRotateAbility);

            _cameraAbilityContainer.abilities.Add(_cameraFollowAbility);

            _cameraAbilityContainer.abilities.Add(_cameraParentAbility);
           
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
