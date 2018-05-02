using System;

namespace CommonTools.Lib11.EventHandlerTools
{
    public static class EventHandlerExtensions
    {
        public static void Raise(this EventHandler handlr, object sender = null)
            => handlr?.Invoke(sender, EventArgs.Empty);


        public static void Raise<T>(this EventHandler<T> handlr, T parameter, object sender = null)
            => handlr?.Invoke(sender, parameter);
    }
}
