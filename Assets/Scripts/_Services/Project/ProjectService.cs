using Data.Settings;
using Services.Log;
using UnityEngine;
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

        public void StartGame()
        {
            Time.timeScale = 1;
            SetProjectState(ProjectState.Start);
        }

        public void PauseGame()
        {
            Time.timeScale = 0;
            SetProjectState(ProjectState.Pause);
        }

        public void StopGame()
        {
            Time.timeScale = 0;
            SetProjectState(ProjectState.Stop);
        }

        public void CursorLocked(bool isLocked, CursorLockMode cursorLockMode) 
        {
            Cursor.visible = isLocked;
            Cursor.lockState = cursorLockMode;
        }
    }
}