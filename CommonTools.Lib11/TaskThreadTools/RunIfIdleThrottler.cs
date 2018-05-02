using System;
using System.Threading.Tasks;

namespace CommonTools.Lib11.TaskThreadTools
{
    public class RunIfIdleThrottler
    {
        private Func<Task> _taskCreator;
        private Task _task;

        public RunIfIdleThrottler(Func<Task> taskCreator)
        {
            _taskCreator = taskCreator;
        }

        public void RunIfIdle()
        {
            if (_task == null)
                _task = _taskCreator.Invoke();

            Task.Run(async () => await _task);
        }
    }
}
