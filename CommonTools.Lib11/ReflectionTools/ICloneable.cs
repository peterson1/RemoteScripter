namespace CommonTools.Lib11.ReflectionTools
{
    public interface ICloneable
    {
        T ShallowClone <T>();
        T DeepClone    <T>();
    }
}
