using Bootstrap;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Editor.Tests
{
    public class IntegrationTestBase : ZenjectUnitTestFixture
    {
        public override void Setup()
        {
            base.Setup();

            Container.Install<CoreInstaller>();
            Container.Install<GameInstaller>();

            var registryInstaller = AssetDatabase.LoadAssetAtPath<RegistryInstaller>(
                "Assets/Data/RegistryInstaller.asset");
            Assert.NotNull(registryInstaller, "cant resolve RegistryInstaller");
            registryInstaller.InstallBindingsWithCustomContainer(Container);
        }
    }
}
