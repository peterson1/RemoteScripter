using CommonTools.Lib11.GoogleTools;
using CommonTools.Lib11.InputCommands;
using CommonTools.Lib45.InputCommands;
using PropertyChanged;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace CommonTools.Lib45.BaseViewModels
{
    [AddINotifyPropertyChangedInterface]
    public abstract class CrudWindowVMBase<TDraft, TWindow, TArg> : MainWindowVMBase<TArg>
        where TWindow : Window, new()
        where TDraft  : Lib11.ReflectionTools.ICloneable, new()
        where TArg    : ICredentialsProvider
    {
        public event EventHandler<TDraft> NewRecordSaved = delegate { };
        public event EventHandler<TDraft> RecordUpdated  = delegate { };
        public event EventHandler         SaveCompleted  = delegate { };


        public CrudWindowVMBase(TArg appArguments) : base(appArguments)
        {
            EncodeNewDraftCmd = R2Command.Relay(EncodeNewDraft, _ => CanEncodeNewDraft(), $"Add New {TypeDescription}");
            SaveCompleted    += (s, e) => OnSaveCompleted();
        }


        public  IR2Command  EncodeNewDraftCmd  { get; }
        public  IR2Command  SaveDraftCmd       { get; private set; }
        public  TDraft      Draft              { get; private set; }
        public string       WhyInvalid         { get; private set; }


        public    abstract string  TypeDescription  { get; }
        protected abstract bool    IsValidDraft     (TDraft draft, out string whyInvalid);

        protected virtual bool   CanEncodeNewDraft  ()             => true;
        protected virtual void   SaveNewRecord      (TDraft draft) { }
        protected virtual Task   SaveNewRecordAsync (TDraft draft) => Task.Delay(0);
        protected virtual void   UpdateRecord       (TDraft record) { }
        protected virtual Task   UpdateRecordAsync  (TDraft record) => Task.Delay(0);
        protected virtual TDraft GetNewDraft        () => new TDraft();

        protected virtual void OnSaveCompleted    () { }


        private bool CanSave()
        {
            if (IsBusy) return false;
            if (!AllFieldsValid()) return false;
            var ok = IsValidDraft(Draft, out string whyNot);
            WhyInvalid = whyNot;
            return ok;
        }


        private void EncodeNewDraft()
        {
            SaveDraftCmd = R2Command.Async(ExecuteSaveDraft, _ => CanSave(), $"Save {TypeDescription}");
            Draft = GetNewDraft();
            if (Draft != null)
                this.Show<TWindow>(showModal: true);
        }




        public bool? EditCurrentRecord(TDraft currentItem)
        {
            SaveDraftCmd = R2Command.Async(ExecuteUpdateRecord, _ => CanSave(), $"Save {TypeDescription}");
            Draft = CreateDraftFromRecord(currentItem);
            return this.Show<TWindow>(showModal: true);
        }


        protected virtual TDraft CreateDraftFromRecord(TDraft record) 
            => record.ShallowClone<TDraft>();


        private async Task ExecuteSaveDraft()
        {
            if (!CanSave()) return;
            StartBeingBusy($"Saving new ‹{TypeDescription}› record ...");

            SaveNewRecord(Draft);
            await SaveNewRecordAsync(Draft);
            NewRecordSaved?.Invoke(this, Draft);
            SaveCompleted ?.Invoke(this, EventArgs.Empty);

            StopBeingBusy();
            ReturnDialogResult(true);
        }


        private async Task ExecuteUpdateRecord()
        {
            if (!CanSave()) return;
            StartBeingBusy($"Updating ‹{TypeDescription}› record ...");

            UpdateRecord(Draft);
            await UpdateRecordAsync(Draft);
            RecordUpdated?.Invoke(this, Draft);
            SaveCompleted?.Invoke(this, EventArgs.Empty);

            StopBeingBusy();
            ReturnDialogResult(true);
        }
    }
}
