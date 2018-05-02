namespace CommonTools.Lib11.DataStructures
{
    public interface IHasDTO<T>
        where T : IDocumentDTO
    {
        T DTO  { get; }
    }
}
