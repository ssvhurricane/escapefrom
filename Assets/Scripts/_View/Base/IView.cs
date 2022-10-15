using UnityEngine;

namespace View
{
    public interface IView
    {
        bool IsShown { get; }

        string Id { get; set; }

        CreationMethod CreationMethod { get; set; }
        OwnerType OwnerType { get; set; }

        void Initialize(Transform parent);

        void Show();

        void Hide();

        GameObject GetGameObject();
    }
}