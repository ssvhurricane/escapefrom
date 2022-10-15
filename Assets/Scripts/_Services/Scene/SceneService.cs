using Cysharp.Threading.Tasks;
using Data.Settings;
using Services.Disposable;
using Signals;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Services.Scene
{
    public class SceneService : DisposableService, ISceneService
    {
        private readonly SceneServiceSettings[] _sceneServiceSettings;
        private readonly SignalBus _signalBus;

        private IDisposable _loadingOperation;

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

        public async void LoadLevelAdvanced(string id, LoadMode loadMode = LoadMode.Unirx)
        {
           
            foreach (var item in _sceneServiceSettings) 
            {
                _nextScene = item;

                if (_loadedScene != null 
                    && item.Id == id
                    && !item.Level.IsSingleScene 
                    && !item.Level.Additive) 
                {
                    switch (loadMode)
                    { 
                        case LoadMode.Unirx: 
                            {
                                UnloadLevelAsync();

                                break;
                            }
                        case LoadMode.Unitask: 
                            {
                                await UT_UnloadLevelAsync().ContinueWith(() =>
                                {
                                    GC.Collect();

                                    if (_nextScene.Level != null) SceneManager.LoadScene(_nextScene.Level.ScenePath);
                                });
                                break;
                            }
                    }
                   
                    break;
                }
                else
                {
                    if (!SceneManager.GetSceneByName(_nextScene.Level.Name).isLoaded && item.Id == id)
                    {
                        switch (loadMode)
                        {
                            case LoadMode.Unirx:
                                {
                                    LoadLevelAsync();

                                    break;
                                }
                            case LoadMode.Unitask:
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
                       
                        break;
                    }
                }
            }
        }

        private void LoadLevelAsync()
        {
            _loadingOperation?.Dispose();

            var loadingProgress = new Subject<float>();
            var progress = new Progress<float>(loadingProgress.OnNext);

            var loadingStream = SceneManager
                .LoadSceneAsync(_nextScene.Level.ScenePath, _nextScene.Level.IsSingleScene ? LoadSceneMode.Single : LoadSceneMode.Additive)
                .AsAsyncOperationObservable(progress);

            _loadingOperation = loadingStream
                .DoOnCompleted(() =>
                {
                    _loadedScene = _nextScene;

                    GC.Collect();

                    loadingProgress.OnCompleted();
                    _signalBus.TryFire(new SceneServiceSignals.SceneLoadingCompleted(_loadedScene.Id));
                })
                .DoOnError(error =>
                {
                    loadingProgress.OnError(error);
                    Debug.LogError(error.Message);
                })
                .Subscribe();

           
            _signalBus.TryFire(new SceneServiceSignals.SceneLoadingStarted(loadingProgress));
        }

        private async UniTask UT_LoadLevelAsync()
        {
            _loadingOperation?.Dispose();

            await SceneManager
                .LoadSceneAsync(_nextScene.Level.ScenePath,
                                    _nextScene.Level.IsSingleScene ? LoadSceneMode.Single : LoadSceneMode.Additive);
        }

        private void UnloadLevelAsync() 
        {
            if (_loadingOperation != null) _loadingOperation.Dispose();

            var loadingProgress = new Subject<float>();
            var progress = new Progress<float>(loadingProgress.OnNext);


            _loadingOperation = SceneManager.LoadSceneAsync(_loadedScene.Level.ScenePath)
               .AsAsyncOperationObservable(progress)
               .Delay(TimeSpan.FromSeconds(0.3f))
               .DoOnCompleted(() =>
               {
                  // EditorUtility.UnloadUnusedAssetsImmediate();
                  // Resources.UnloadUnusedAssets();
                   GC.Collect();

                   if (_nextScene.Level != null) SceneManager.LoadScene(_nextScene.Level.ScenePath);

                   loadingProgress.OnCompleted();
               })
               .Subscribe();

            _signalBus.TryFire(new SceneServiceSignals.SceneLoadingStarted(loadingProgress));
        }

        private async UniTask UT_UnloadLevelAsync() 
        {
            if (_loadingOperation != null) _loadingOperation.Dispose();

            await SceneManager
             .LoadSceneAsync(_loadedScene.Level.ScenePath);
        }


        public enum LoadMode 
        {
            Unirx,
            Unitask
        }
    }
}