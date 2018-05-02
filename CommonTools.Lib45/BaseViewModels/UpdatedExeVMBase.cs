using CommonTools.Lib11.StringTools;
using CommonTools.Lib45.FileSystemTools;
using PropertyChanged;

namespace CommonTools.Lib45.BaseViewModels
{
    [AddINotifyPropertyChangedInterface]
    public abstract class UpdatedExeVMBase<TArg> : MainWindowVMBase<TArg>
        where TArg : IHasUpdatedCopy
    {
        public UpdatedExeVMBase(TArg appArguments) : base(appArguments)
        {
            UpdateNotifier = new UpdatedExeNotifier(AppArgs.UpdatedCopyPath);
        }


        public UpdatedExeNotifier  UpdateNotifier  { get; }
    }
}
