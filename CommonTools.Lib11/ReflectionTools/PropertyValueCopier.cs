using System;
using System.Linq;
using System.Reflection;

namespace CommonTools.Lib11.ReflectionTools
{
    public static class PropertyValueCopier
    {
        public static void CopyByNameFrom<TDestination, TSource>(this TDestination targetObj, TSource sourceObj)
        {
            if (targetObj == null || sourceObj == null) return;

            var writableProps = typeof(TDestination)
                .GetRuntimeProperties().Where(x => x.CanWrite).ToList();

            foreach (var destProp in writableProps)
            {
                //var srcProp = typeof(TSource).GetRuntimeProperty(destProp.Name);
                var srcProp = typeof(TSource).FindProperty(destProp.Name);
                if (srcProp == null) continue;
                var srcVal = srcProp.GetValue(sourceObj);

                try
                {
                    destProp.SetValue(targetObj, srcVal);
                }
                catch (Exception) { }
            }
        }


        public static PropertyInfo FindProperty(this Type type, string propertyName)
        {
            if (type.GetTypeInfo().IsInterface)
                return FindInterfaceProperty(type, propertyName);
            else
                return type.GetRuntimeProperty(propertyName);
        }


        private static PropertyInfo FindInterfaceProperty(Type type, string propertyName)
        {
            var prop = type.GetRuntimeProperty(propertyName);
            if (prop != null) return prop;

            foreach (var subTyp in type.GetTypeInfo().ImplementedInterfaces)
            {
                var subProp = FindInterfaceProperty(subTyp, propertyName);
                if (subProp != null) return subProp;
            }

            return null;
        }
    }
}
