using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace CommonTools.Lib11.ExceptionTools
{
    public static class Fault
    {
        public static InvalidOperationException CallFirst(string requiredMethod, [CallerMemberName] string callingMethod = null)
            => new InvalidOperationException(
                $"Please call method “{requiredMethod}” before calling “{callingMethod}”.");


        public static InvalidDataException BadData(string description)
            => new InvalidDataException(description);


        public static NullReferenceException NullRef(string variableName)
            => new NullReferenceException(
                $"Variable [{variableName}] should NOT be NULL.");


        public static ArgumentException BadArg(string argumentName, object argumentValue)
            => new ArgumentException(
                $"Invalid [{argumentName}]: “{argumentValue}”");


        public static InvalidCastException BadCast<T>(string textToParse, T targetType)
            => new InvalidCastException(
                $"Non-convertible to ‹{typeof(T).Name}›: “{textToParse}”.");


        public static IntrusionAttemptException Intruder()
            => new IntrusionAttemptException();


        public static FileNotFoundException MissingFile(string filePath)
            => new FileNotFoundException($"Missing file: {filePath}");

        public static FileNotFoundException MissingDir(string foldrPath)
            => new FileNotFoundException($"Missing folder: {foldrPath}");
    }
}
