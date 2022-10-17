using Cysharp.Threading.Tasks;
using Data.Settings;
using Signals;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Services.Scene
{
    public class SceneService : ISceneService
    {
        private readonly SceneServiceSettings[] _sceneServiceSettings;
        private readonly SignalBus _signalBus;

        private SceneServiceSettings _loadedScene;
        private SceneServiceSettings _nextScene;

        public SceneService(SignalBus signalBus, SceneServiceSettings[] sceneServiceSettings) 
        {
            _signalBus = signalBus;
            _sceneServiceSettings = sceneServiceSettings;
        }

        public AsyncOperation LoadLevelBase(string id)
        {
            AsyncOperation asyncOperation = null;

            foreach (var item in _sceneServiceSettings)
            {
                _nextScene = item;

                if (_loadedScene != null
                    && item.Id == id
                    && !item.Level.IsSingleScene
                    && !item.Level.Additive)
                {
                    asyncOperation = SceneManager
                         .LoadSceneAsync(_loadedScene.Level.ScenePath);
                }
                else
                {
                    if (!SceneManager.GetSceneByName(_nextScene.Level.Name).isLoaded && item.Id == id)
                    {
                        _loadedScene = _nextScene;

                        asyncOperation = SceneManager.LoadSceneAsync(_nextScene.Level.ScenePath,
                                                         _nextScene.Level.IsSingleScene ? LoadSceneMode.Single : LoadSceneMode.Additive);

                        if(asyncOperation != null)
                                asyncOperation.completed += _ => _signalBus.TryFire(new SceneServiceSignals.SceneLoadingCompleted(_loadedScene.Id));
                    }
                }
            }

            return asyncOperation;
        }

        public async void LoadLevelAdvanced(string id, LoadMode loadMode = LoadMode.Unitask)
        {
           
            foreach (var item in _sceneServiceSettings) 
            {
                _nextScene = item;

                if (_loadedScene != null 
                    && item.Id == id
                    && !item.Level.IsSingleScene 
                    && !item.Level.Additive) 
                {
                    
                     await UT_UnloadLevelAsync().ContinueWith(() =>
                           {
                                GC.Collect();

                                 if (_nextScene.Level != null) SceneManager.LoadScene(_nextScene.Level.ScenePath);
                           });
                
                    break;
                }
                else
                {
                    if (!SceneManager.GetSceneByName(_nextScene.Level.Name).isLoaded && item.Id == id)
                    {
                        await UT_LoadLevelAsync().ContinueWith(() =>
                        {
                            _loadedScene = _nextScene;

                            GC.Collect();

                            _signalBus.TryFire(new SceneServiceSignals.SceneLoadingCompleted(_loadedScene.Id));
                        });
                     
                        break;
                    }
                }
            }
        }

        private async UniTask UT_LoadLevelAsync()
        {
            await SceneManager
                .LoadSceneAsync(_nextScene.Level.ScenePath,
                                    _nextScene.Level.IsSingleScene ? LoadSceneMode.Single : LoadSceneMode.Additive);
        }

        private async UniTask UT_UnloadLevelAsync() 
        {
           
            await SceneManager
             .LoadSceneAsync(_loadedScene.Level.ScenePath);
        }


        public enum LoadMode 
        {
            //Unirx, Legacy
            Unitask
        }
    }
}