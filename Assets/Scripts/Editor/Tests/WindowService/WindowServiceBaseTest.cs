using NUnit.Framework;
using UnityEngine;

namespace Editor.Tests
{
    public class WindowServiceBaseTest : IntegrationTestBase
    {
        [Test]
        public void _00_OpenWindow()
        {
            Debug.Log("_00_OpenWindow");
        }

        [Test]
        public void _01_GetWindow()
        {
            Debug.Log("_01_GetWindow");
        }

        [Test]
        public void _02_CloseWindow()
        {
            Debug.Log("_02_CloseWindow");
        }
    }
}
