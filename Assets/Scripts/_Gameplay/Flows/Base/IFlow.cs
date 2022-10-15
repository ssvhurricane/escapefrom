using UnityEngine;

namespace Gameplay.Flow
{
    public interface IFlow
    {
        int Id { get; }

        GameObject Owner { get; }
    }
}
