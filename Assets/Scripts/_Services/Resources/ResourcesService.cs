using Constants;
using Data.Settings;
using Services.Log;
using System;
using Zenject;

namespace Services.Resources
{
    public class ResourcesService
    {  
        private readonly SignalBus _signalBus;
        private readonly LogService _logService;
        private readonly ResourcesServiceSettings _resourcesServiceSettings;
        public ResourcesService(SignalBus signalBus, LogService logService, ResourcesServiceSettings resourcesServiceSettings) 
        {
            _signalBus = signalBus;
            _logService = logService;
            _resourcesServiceSettings = resourcesServiceSettings;
        }
        
        public T GetResourceObj<T>(string resourcePath) where T : UnityEngine.Object
        {
            T obj = UnityEngine.Resources.Load<T>(resourcePath) as T;
            if (obj == null)
            {
                _logService.ShowLog(GetType().Name, 
                    Log.LogType.Warning,
                    $"Resource not found: {resourcePath}",
                    LogOutputLocationType.Console);
               
                obj = default(T);
            }
            return obj;
        }

        public UnityEngine.GameObject GetResource(TypeResource typeResource, Type type) 
        {
            UnityEngine.Object element = null;

            switch (typeResource)
            {
                case TypeResource.Window:
                    {
                        element =  UnityEngine.Resources.Load(ResourcesServiceConstants.RESOURCES_FOLDER_WINDOWS_PREFABS 
                            + type.Name);

                        break;
                    }

                case TypeResource.View:
                    {
                        element = UnityEngine.Resources.Load(ResourcesServiceConstants.RESOURCES_FOLDER_VIEWS_PREFABS 
                                                                + type.Name);
                        break;
                    }

                case TypeResource.Audio:
                    {
                        element =  UnityEngine.Resources.Load(ResourcesServiceConstants.RESOURCES_FOLDER_AUDIO 
                            + type.Name);

                        break;
                    }

                case TypeResource.Sprite:
                    {
                        element =  UnityEngine.Resources.Load(ResourcesServiceConstants.RESOURCES_FOLDER_SPRITES
                            + type.Name);

                        break;
                    }
            }

            if (element != null) 
            {
                _logService.ShowLog(GetType().Name,
                       Log.LogType.Message,
                       $"Resource loaded: {type.Name}",
                       LogOutputLocationType.Console);
               
                return (UnityEngine.GameObject) element;
            }
            else
            {
                _logService.ShowLog(GetType().Name,
                          Log.LogType.Error,
                          $"Resource not found: {type.Name}",
                          LogOutputLocationType.Console);
               
                return null;
            }
        }

        public UnityEngine.GameObject[] GetResources(TypeResource typeResource, Type type) 
        {
            UnityEngine.Object[] elements = null;

            switch (typeResource)
            {
                case TypeResource.Window:
                    {
                        elements = UnityEngine.Resources.LoadAll(ResourcesServiceConstants.RESOURCES_FOLDER_WINDOWS_PREFABS + type.Name);
                        break;
                    }

                case TypeResource.View:
                    {
                        elements = UnityEngine.Resources.LoadAll(ResourcesServiceConstants.RESOURCES_FOLDER_VIEWS_PREFABS + type.Name);
                        break;
                    }

                case TypeResource.Audio:
                    {
                        elements =  UnityEngine.Resources.LoadAll(ResourcesServiceConstants.RESOURCES_FOLDER_AUDIO + type.Name);
                        break;
                    }

                case TypeResource.Sprite:
                    {
                        elements =  UnityEngine.Resources.LoadAll(ResourcesServiceConstants.RESOURCES_FOLDER_SPRITES + type.Name);
                        break;
                    }
            }
                
            return (UnityEngine.GameObject[]) elements;
        }

        public TParam GetResourceAsync<TParam>(TypeResource typeResource)
        {
            //ToDo...
            return default(TParam);
        }

        public TParam[] GetResourcesAsync<TParam>(TypeResource typeResource)
        {
            //ToDo...
            return default(TParam[]);
        }
    }
}
