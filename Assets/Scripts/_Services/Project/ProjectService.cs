using Data.Settings;
using Services.Log;
using Zenject;

namespace Services.Project
{
    public class ProjectService
    {
        private readonly SignalBus _signalBus;
        private readonly LogService _logService;

        private readonly ProjectServiceSettings _projectServiceSettings;

        private ProjectState _projectState;
        public ProjectService(SignalBus signalBus,
            LogService logService,
            ProjectServiceSettings projectServiceSettings)
        {
            _signalBus = signalBus;

            _logService = logService;

            _projectServiceSettings = projectServiceSettings;

            _projectState = ProjectState.Stop;
        }

        public void Configurate()
        {
           // TODO:
        }

        public ProjectType GetProjectType() 
        {
            return _projectServiceSettings.ProjectType;
        }

        public ProjectState GetProjectState() 
        {
            return _projectState;
        }

        public void SetProjectState(ProjectState projectState) 
        {
            _projectState = projectState;
        }
    }
}