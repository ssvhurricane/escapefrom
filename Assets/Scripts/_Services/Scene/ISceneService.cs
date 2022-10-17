using UnityEngine;

namespace Services.Scene
{
    public interface ISceneService
    {
        AsyncOperation LoadLevelBase(string id);

        void LoadLevelAdvanced(string id, SceneService.LoadMode loadMode = SceneService.LoadMode.Unitask);
    }
}