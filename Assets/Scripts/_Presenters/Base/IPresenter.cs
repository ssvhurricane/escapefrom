using Model;
using UnityEngine;
using View;

namespace Presenters
{
    /// <summary>
    /// Implemented only by those who have model and view.
    /// </summary>
    public interface IPresenter
    {
        public void ShowView(GameObject prefab = null, Transform hTransform = null);

        public void HideView();

        public IView GetView();

        public IModel GetModel();
    }

    public interface IDamage 
    { 
        public void ToDamage();
        public void TakeDamage(float damageValue, IPresenter ownedPresenter);

    }
}
