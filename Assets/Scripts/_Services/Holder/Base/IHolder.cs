namespace Services.Anchor
{
    public interface IHolder
    {
        string Name { get; set; }

        HolderType HolderType { get; set; }
    }
}
