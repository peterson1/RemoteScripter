using CommonTools.Lib11.DatabaseTools;
using CommonTools.Lib11.DataStructures;
using CommonTools.Lib11.GoogleTools;
using CommonTools.Lib45.LiteDbTools;
using System.Collections.Generic;
using System.Linq;

namespace CommonTools.Lib45.BaseViewModels
{
    public abstract class FilteredSavedListVMBase<TDTO, TFilter, TArg>
        : SavedListVMBase<TDTO, TArg> where TDTO    : IDocumentDTO
                                      where TFilter : TextFilterBase<TDTO>, new()
                                      where TArg    : ICredentialsProvider
    {
        private List<TDTO> _queried;


        public FilteredSavedListVMBase(ISimpleRepo<TDTO> simpleRepo, TArg appArguments, bool doReload = true) : base(simpleRepo, appArguments, doReload)
        {
            Filter.TextFilterChanged += (s, e) => ApplyTextFilters();
        }


        public TFilter Filter { get; } = new TFilter();


        public override void ReloadFromDB()
        {
            _queried = GetPostProcessedResult().ToList();
            ApplyTextFilters();
        }


        private void ApplyTextFilters()
        {
            var list = _queried.ToList();
            Filter.RemoveNonMatches(ref list);
            ItemsList.SetItems(list);
        }
    }
}
