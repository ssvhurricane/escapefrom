using System.Collections;

namespace Config
{
    public interface IRegistry
    {

    }

    public interface IRegistryList : IEnumerable
    {

    }

    public interface IRegistryData
    {
        string Id { get; }
    }
}