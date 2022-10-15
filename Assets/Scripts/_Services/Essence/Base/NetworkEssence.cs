using UnityEngine;
using View;

namespace Services.Essence
{
    public class NetworkEssence : MonoBehaviour, IEssence
    {
        public EssenceType EssenceType { get; set; }

        public bool IsShown { get; protected set; }

        public string Id { get; set; }
        public CreationMethod CreationMethod { get; set; }
        public OwnerType OwnerType { get; set; }

        public void Hide()
        {
            gameObject.SetActive(false);
            IsShown = false;
        }

        public void Initialize(Transform parent)
        {
            transform.SetParent(parent, false);
            transform.SetAsLastSibling();
        }

        public void Show()
        {
            IsShown = true;
            gameObject.SetActive(true);
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }
    }
}