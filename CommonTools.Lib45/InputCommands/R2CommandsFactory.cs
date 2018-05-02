using System;
using System.Threading.Tasks;

namespace CommonTools.Lib45.InputCommands
{
    public class R2Command
    {
        public static R2AsyncCommandWPF Async(Func<object, Task> task, Predicate<object> canExecute = null, string buttonLabel = null)
            => new R2AsyncCommandWPF(task, canExecute, buttonLabel);


        public static R2AsyncCommandWPF Async(Func<Task> task, Predicate<object> canExecute = null, string buttonLabel = null)
            => new R2AsyncCommandWPF(x => task(), canExecute, buttonLabel);

        public static R2AsyncCommandWPF Relay(Action action, Predicate<object> canExecute = null, string buttonLabel = null)
            => Relay(x => action(), canExecute, buttonLabel);


        public static R2AsyncCommandWPF Relay(Action<object> action, Predicate<object> canExecute = null, string buttonLabel = null)
            => new R2AsyncCommandWPF(async x =>
            {
                await Task.Delay(1);
                action?.Invoke(x);
            },
            canExecute, buttonLabel);
    }
}
