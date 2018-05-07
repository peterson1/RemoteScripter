using CommonTools.Lib11.DatabaseTools;
using CommonTools.Lib11.GoogleTools;
using PropertyChanged;
using System.Windows;

namespace CommonTools.Lib45.BaseViewModels
{
    [AddINotifyPropertyChangedInterface]
    public abstract class RepoCrudWindowVMBase<TRepo, TDraft, TWindow, TArg> : CrudWindowVMBase<TDraft, TWindow, TArg>
        where TRepo   : ISimpleRepo<TDraft>
        where TWindow : Window, new()
        where TDraft  : Lib11.ReflectionTools.ICloneable, new()
        where TArg    : ICredentialsProvider
    {
        protected TRepo _repo;


        public RepoCrudWindowVMBase(TRepo repository, TArg appArguments) : base(appArguments)
        {
            _repo = repository;
        }


        protected override void SaveNewRecord(TDraft draft)
            => _repo.Insert(draft);


        protected override void UpdateRecord(TDraft record)
            => _repo.Update(record);
    }
}
