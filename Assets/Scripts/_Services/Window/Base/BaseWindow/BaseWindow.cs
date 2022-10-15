using UnityEngine;
using View;

namespace Services.Window
{
    public abstract class BaseWindow : MonoBehaviour, IWindow
    {
        public bool IsShown { get; protected set; }

        public IWindowArgs Arguments { get; set; }

        public WindowType WindowType { get; set; }

        public string Id { get; set; }

        public CreationMethod CreationMethod { get; set; }

        public OwnerType OwnerType { get; set; }

        public void Initialize(Transform parent)
        {
            transform.SetParent(parent, false);

            transform.SetAsLastSibling();
        }

        public virtual void Show()
        {
            IsShown = true;

            gameObject.SetActive(true);
        }
        
        public virtual void Hide()
        {
            gameObject.SetActive(false);

            IsShown = false;
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }
    }
}