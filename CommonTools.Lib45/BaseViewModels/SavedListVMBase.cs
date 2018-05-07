using CommonTools.Lib11.DatabaseTools;
using CommonTools.Lib11.DataStructures;
using CommonTools.Lib11.GoogleTools;
using CommonTools.Lib11.InputCommands;
using CommonTools.Lib45.InputCommands;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonTools.Lib45.BaseViewModels
{
    [AddINotifyPropertyChangedInterface]
    public abstract class SavedListVMBase<TDTO, TArg>
        where TDTO : IDocumentDTO
        where TArg : ICredentialsProvider
    {
        public event EventHandler<decimal> TotalSumChanged;
        public event EventHandler<TDTO>    SelectedChanged;

        private ISimpleRepo<TDTO> _repo;


        public SavedListVMBase(ISimpleRepo<TDTO> repository, TArg appArguments, bool doReload = true)
        {
            _repo      = repository;
            AppArgs    = appArguments;
            RefreshCmd = R2Command.Relay(ReloadFromDB, null, "Refresh");

            _repo.ContentChanged        += (s, e) => ReloadFromDB();
            ItemsList.ItemDeleted       += (s, e) => ExecuteDeleteRecord(e);
            ItemsList.CollectionChanged += (s, e) => UpdateTotalSum();
            ItemsList.ItemOpened        += ItemsList_ItemOpened;

            if (doReload) ReloadFromDB();
        }


        public TArg          AppArgs    { get; }
        public IR2Command    RefreshCmd { get; }
        public UIList<TDTO>  ItemsList  { get; } = new UIList<TDTO>();
        public TDTO          Selected   { get; set; }
        public decimal       TotalSum   { get; private set; }


        protected virtual Func<TDTO, decimal> SummedAmount { get; }
        protected virtual bool CanDeletetRecord    (TDTO rec) => true;
        protected virtual bool CanEditRecord       (TDTO rec) => true;
        protected virtual void LoadRecordForEditing(TDTO rec) { }
        protected virtual IEnumerable<TDTO> PostProcessQueried(IEnumerable<TDTO> items) => items;

        protected virtual void OnSelectedChanged()
        {
            SelectedChanged?.Invoke(this, Selected);
        }


        protected void ExecuteDeleteRecord(TDTO dto)
        {
            DeleteRecord(_repo, dto);
            UpdateTotalSum();
            TotalSumChanged?.Invoke(this, TotalSum);
        }


        protected virtual void DeleteRecord(ISimpleRepo<TDTO> db, TDTO dto)
        {
            if (CanDeletetRecord(dto)) db.Delete(dto);
            ReloadFromDB();
        }


        private void ItemsList_ItemOpened(object sender, TDTO e)
        {
            OnItemOpened(e);
            UpdateTotalSum();
        }


        protected virtual void OnItemOpened(TDTO e)
        {
            if (CanEditRecord(e))
                LoadRecordForEditing(e);
        }


        public virtual void ReloadFromDB()
            => ItemsList.SetItems(GetPostProcessedResult());


        protected IEnumerable<TDTO> GetPostProcessedResult()
            => PostProcessQueried(QueryItems(_repo));


        protected virtual List<TDTO> QueryItems(ISimpleRepo<TDTO> db)
            => db.GetAll();




        protected void UpdateTotalSum()
        {
            if (SummedAmount == null) return;

            var oldSum = TotalSum;
            TotalSum = ItemsList.Any() 
                ? ItemsList.Sum(_ => SummedAmount(_)) : 0;

            if (TotalSum != oldSum)
                TotalSumChanged?.Invoke(this, TotalSum);
        }


        //public void RaisePropertyChanged(object sender, PropertyChangedEventArgs e)
        //    => PropertyChanged?.Invoke(sender, e);
    }
}
