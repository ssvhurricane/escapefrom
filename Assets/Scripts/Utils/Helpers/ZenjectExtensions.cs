using Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Utils.Helpers
{
    public static class ZenjectExtensions
    {
        public static void InstallTransientArray<TItem>(this DiContainer container, params Type[] concreteTypes)
            where TItem : class
        {
            foreach (var concreteType in concreteTypes) container.Bind(concreteType).AsTransient();

            container
                .Bind<TItem[]>()
                .FromMethod(context =>
                    concreteTypes
                        .Select(x => container.Resolve(x) as TItem)
                        .ToArray()
                );
        }

        public static void InstallElementAsSingle<TElement>(this DiContainer container)
        {
            container
                .BindInterfacesAndSelfTo<TElement>()
                .AsSingle()
                .NonLazy();
        }

        public static void InstallElementAsTransient<TElement>(this DiContainer container)
        {
            container.BindInterfacesAndSelfTo<TElement>().AsTransient();
        }

        public static void InstallSingleModel<TModel>(this DiContainer container)
        {
            container
                .BindInterfacesAndSelfTo<TModel>()
                .AsSingle();
        }

        public static void InstallDataModel<TData, TModel>(this DiContainer container)
        {
            container
                .BindIFactory<TData, TModel>()
                .FromNew();
        }

        public static void BindServiceToInterface<TInterface, TService>(this DiContainer container) where TService : TInterface
        {
            container
                .Bind<TInterface>()
                .To<TService>()
                .AsSingle()
                .NonLazy();
        }

        /// <summary>
        /// Bind interfaces and self to model as Single
        /// </summary>
        /// <param name="container"></param>
        /// <typeparam name="TModel"></typeparam>
        public static void InstallModel<TModel>(this DiContainer container)
        {
            container
                .BindInterfacesAndSelfTo<TModel>()
                .AsSingle();
        }

        //public static void InstallPool<T>(this DiContainer container, int size = 0) where T : MonoBehaviour, Pool.IPoolable
        //{
        //    container.BindMemoryPool<T, Pool.Pool<T>>()
        //        .WithInitialSize(size)
        //        .FromComponentInNewPrefabResource(PathContainer.RESOURCES_FOLDER_VIEWS_PREFABS + typeof(T).Name);
        //}
    }
}
