using Data.Settings;
using Services.Log;
using Services.Network;
using Zenject;

namespace Services.Project
{
    public class ProjectService
    {
        private readonly SignalBus _signalBus;
        private readonly LogService _logService;
        private readonly NetworkService _networkService;

        private readonly ProjectServiceSettings _projectServiceSettings;
        public ProjectService(SignalBus signalBus,
            LogService logService,
            NetworkService networkService,
            ProjectServiceSettings projectServiceSettings)
        {
            _signalBus = signalBus;

            _logService = logService;

            _networkService = networkService;

            _projectServiceSettings = projectServiceSettings;
        }

        public void Configurate()
        {
            if(_projectServiceSettings.ProjectType == ProjectType.Online) 
            {
                _networkService.Initialize();
            }
            else if(_projectServiceSettings.ProjectType == ProjectType.Offline)
            {
                // TODO:
            }
        }

        public ProjectType GetProjectType() 
        {
            return _projectServiceSettings.ProjectType;
        }
    }
}