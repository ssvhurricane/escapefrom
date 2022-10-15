using Model.Inventory;
using Services.Ability;
using System;

namespace Model
{
    public interface IModel : IDisposable
    {
        string Id { get; }

        ModelType ModelType { get; }
    }
}
