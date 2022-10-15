using UnityEngine;
using View;

namespace Services.Essence
{
    public interface IEssence : IView
    {
        EssenceType EssenceType { get; set; }
    }
}