using CommonTools.Lib11.DatabaseTools;
using CommonTools.Lib11.DataStructures;
using CommonTools.Lib11.GoogleTools;
using CommonTools.Lib45.LiteDbTools;
using System.Collections.Generic;
using System.Linq;

namespace CommonTools.Lib45.BaseViewModels
{
    public abstract class IndirectFilteredListVMBase<TRow, TDTO, TFilter, TArg>
        : SavedListVMBase<TDTO, TArg> 
            where TRow    : IHasDTO<TDTO>
            where TDTO    : IDocumentDTO
            where TFilter : TextFilterBase<TRow>, new()
            where TArg    : ICredentialsProvider
    {
        private List<TRow> _cache;


        public IndirectFilteredListVMBase(ISimpleRepo<TDTO> simpleRepo, TArg appArguments, bool doReload = true) : base(simpleRepo, appArguments, doReload)
        {
            Filter.TextFilterChanged += (s, e) => ApplyTextFilters();
        }


        public UIList<TRow>  Rows    { get; } = new UIList<TRow>();
        public TFilter       Filter  { get; } = new TFilter();


        protected abstract TRow CastToRow(TDTO dto);


        public override void ReloadFromDB()
        {
            var dtos = GetPostProcessedResult().ToList();
            _cache   = dtos.Select(_ => CastToRow(_)).ToList();
            ApplyTextFilters();
        }


        private void ApplyTextFilters()
        {
            var list = _cache.ToList();
            Filter.RemoveNonMatches(ref list);
            Rows.SetItems(list);
        }
    }
}
