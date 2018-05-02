namespace CommonTools.Lib11.DependencyInjection
{
    public interface IComponentResolver
    {
        T Resolve<T>() where T : class;
    }
}
